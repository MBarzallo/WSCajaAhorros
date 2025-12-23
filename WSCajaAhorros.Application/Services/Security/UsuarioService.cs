using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.Common.Security;
using WSCajaAhorros.Application.Common.Validators;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Services.Security;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRolRepository _rolRepository;
    
    public  UsuarioService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository)
    {
        _usuarioRepository = usuarioRepository;
        _rolRepository = rolRepository;
    }

    public async Task<Response<List<Usuario>>> ObtenerTodos()
    {
        var usuarios = await _usuarioRepository.ObtenerTodos();
        return Response<List<Usuario>>.Success(usuarios);
    }

    public async Task<Response> CrearUsuario(CrearUsuarioRequest request)
    {
        try
        {
            if (!EmailValidator.EsValido(request.CorreoElectronico))
                return Response.Fail("El correo electronico no es valido");

            if(!UsernameValidator.EsValido(request.NombreUsuario))
                return Response.Fail("El nombre usuario no es valido");
            
            var claveTemporal = PasswordGenerator.GenerarTemporal();
            
            var (hash, salt) = PasswordHasher.Hash(claveTemporal);
            
            var usuario = new Usuario(request.NombreUsuario, request.CorreoElectronico, hash, salt);
            
            await _usuarioRepository.Crear(usuario);
            
            return Response.Success($"Se ha creado el usuario correctamente, la contraseña temporal se envio al correo {request.CorreoElectronico}");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }

    public async Task<Response> CrearUsuarioRol(Guid userId, CrearUsuarioRolRequest request)
    {
        try
        {
            var usuario = await _usuarioRepository.ObtenerPorId(userId);
            if (usuario == null)
                return Response.Fail("El usuario no existe");

            var rol = await _rolRepository.ObtenerPorId(request.RolId);
            if (rol == null)
                return Response.Fail("El rol no existe");
            
            if(usuario.Roles.Any(r=>r.RolId==request.RolId))
                return Response.Fail("El rol ya esta asignado al usuario");

            var usuarioRol = new UsuarioRol(usuario, rol);
            
            usuario.AgregarRol(usuarioRol);
            await _usuarioRepository.SaveChangesAsync();

            return Response.Success();

        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
    

}