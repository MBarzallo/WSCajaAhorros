using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.Common.Validators;
using WSCajaAhorros.Application.DTOs.Socios;
using WSCajaAhorros.Application.Interfaces.Repositories.Socios;
using WSCajaAhorros.Application.Interfaces.Services.Socios;
using WSCajaAhorros.Domain.Common;
using WSCajaAhorros.Domain.Socios;

namespace WSCajaAhorros.Application.Services.Socios;

public class SociosService: ISociosService
{
    
    private readonly ISocioRepository _socioRepository;

    public SociosService(ISocioRepository socioRepository)
    {
        _socioRepository = socioRepository;
    }

    public async Task<Response> Crear(CrearSocioRequest request)
    {
        try
        {
            if (!Enum.IsDefined(typeof(TipoPersona), request.TipoPersonaId))
                return Response.Fail("Tipo de persona inválido.");

            if (!Enum.IsDefined(typeof(TipoIdentificacion), request.TipoIdentificacionId))
                return Response.Fail("Tipo de identificación inválido.");

            var tipoIdentificacion = (TipoIdentificacion)request.TipoIdentificacionId;

            if (!IdentificationValidator.EsValida(
                    request.IdentificacionNumero,
                    request.TipoIdentificacionId))
            {
                return Response.Fail("La identificación no es válida.");
            }

            var existe = await _socioRepository.ObtenerPorIdentificacion(request.IdentificacionNumero);

            if (existe  != null)
                return Response.Fail("Ya existe un socio con esta identificación.");

            
            SocioValidator.ValidarDatosPorTipo(request);

            var identificacion = new Identificacion(
                tipoIdentificacion,
                request.IdentificacionNumero
            );

            Socio socio = (TipoPersona)request.TipoPersonaId switch
            {
                TipoPersona.Natural => Socio.CrearNatural(
                    identificacion,
                    request.Nombres!,
                    request.Apellidos!,
                    request.FechaNacimiento!.Value),

                TipoPersona.Juridica => Socio.CrearJuridica(
                    identificacion,
                    request.RazonSocial!,
                    request.NombreComercial,
                    request.FechaConstitucion),

                _ => throw new InvalidOperationException("Tipo de persona no soportado.")
            };

            SocioValidator.ValidarPrincipal(
                request.Telefonos,
                t => t.EsPrincipal,
                "Solo puede existir un teléfono principal.");

            SocioValidator.ValidarPrincipal(
                request.Correos,
                c => c.EsPrincipal,
                "Solo puede existir un correo principal.");

            SocioValidator.ValidarPrincipal(
                request.Direcciones,
                d => d.EsPrincipal,
                "Solo puede existir una dirección principal.");

            foreach (var tel in request.Telefonos)
            {
                var telefono = new TelefonoSocio(
                    tel.Numero,
                    tel.Etiqueta,
                    tel.EsPrincipal);

                socio.AgregarTelefono(telefono);
            }

            foreach (var correo in request.Correos)
            {
                var correoSocio = new CorreoSocio(
                    correo.Email,
                    correo.Etiqueta,
                    correo.EsPrincipal);

                socio.AgregarCorreo(correoSocio);
            }

            foreach (var dir in request.Direcciones)
            {
                var direccion = new Direccion(
                    dir.CallePrincipal,
                    dir.Ciudad,
                    dir.Provincia,
                    dir.Pais,
                    dir.CalleSecundaria,
                    dir.Referencia);

                var direccionSocio = new DireccionSocio(
                    direccion,
                    dir.Etiqueta,
                    dir.EsPrincipal);

                socio.AgregarDireccion(direccionSocio);
            }

            await _socioRepository.Crear(socio);
            await _socioRepository.SaveChangesAsync();

            return Response.Success("Socio creado correctamente.");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
    public async Task<Response<List<Socio>>> Listar(
        string? identificacion,
        string? nombres,
        bool? activo)
    {
        var socios = await _socioRepository.Listar(identificacion, nombres, activo);
        return Response<List<Socio>>.Success(socios);
    }

    public async Task<Response<Socio>> ObtenerPorId(Guid id)
    {
        var socio = await _socioRepository.ObtenerPorId(id);
        if (socio == null)
            return Response<Socio>.Fail("Socio no encontrado.");

        return Response<Socio>.Success(socio);
    }

    public async Task<Response> Actualizar(Guid id, ActualizarSocioRequest request)
    {
        var socio = await _socioRepository.ObtenerPorId(id);
        if (socio == null)
            return Response.Fail("Socio no encontrado.");

        if (socio.TipoPersona == TipoPersona.Natural)
        {
            socio.GetType().GetProperty("Nombres")?.SetValue(socio, request.Nombres);
            socio.GetType().GetProperty("Apellidos")?.SetValue(socio, request.Apellidos);
        }
        else
        {
            socio.GetType().GetProperty("RazonSocial")?.SetValue(socio, request.RazonSocial);
            socio.GetType().GetProperty("NombreComercial")?.SetValue(socio, request.NombreComercial);
        }

        await _socioRepository.SaveChangesAsync();
        return Response.Success("Socio actualizado.");
    }

    public async Task<Response> Activar(Guid id)
    {
        var socio = await _socioRepository.ObtenerPorId(id);
        if (socio == null) return Response.Fail("Socio no encontrado.");

        socio.Activar();
        await _socioRepository.SaveChangesAsync();
        return Response.Success("Socio activado.");
    }

    public async Task<Response> Desactivar(Guid id)
    {
        var socio = await _socioRepository.ObtenerPorId(id);
        if (socio == null) return Response.Fail("Socio no encontrado.");

        socio.Desactivar();
        await _socioRepository.SaveChangesAsync();
        return Response.Success("Socio desactivado.");
    }
}