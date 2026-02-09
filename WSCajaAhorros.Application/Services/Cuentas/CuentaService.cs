using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Cuentas;
using WSCajaAhorros.Application.Interfaces.Repositories.Socios;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Domain.Cuentas;

namespace WSCajaAhorros.Application.Services.Cuentas;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly ISocioRepository _socioRepository;
    private readonly IProductoCuentaRepository _productoCuentaRepository;

    public CuentaService(
        ICuentaRepository cuentaRepository,
        ISocioRepository socioRepository,
        IProductoCuentaRepository productoCuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
        _socioRepository = socioRepository;
        _productoCuentaRepository = productoCuentaRepository;
    }

    // ============================
    // Obtener cuentas por socio
    // ============================
    public async Task<Response<List<CuentaResponse>>> ObtenerPorSocio(Guid socioId)
    {
        try
        {
            var cuentas = await _cuentaRepository.ObtenerPorSocioAsync(socioId);

            var response = cuentas.Select(c => new CuentaResponse
            {
                Id = c.Id,
                NumeroCuenta = c.NumeroCuenta,
                Saldo = c.Saldo.Monto,
                Estado = c.Estado.ToString(),
                FechaApertura = c.FechaApertura
            }).ToList();

            return Response<List<CuentaResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            return Response<List<CuentaResponse>>.Fail(ex.Message);
        }
    }

    // ============================
    // Crear cuenta
    // ============================
    public async Task<Response> Crear(CrearCuentaRequest request)
    {
        if (request.SocioId == Guid.Empty)
            return Response.Fail("El socio es obligatorio");

        if (request.ProductoCuentaId == Guid.Empty)
            return Response.Fail("El producto de cuenta es obligatorio");

        try
        {
            var socio = await _socioRepository.ObtenerPorId(request.SocioId);
            if (socio == null)
                return Response.Fail("El socio no existe");

            if (!socio.EstaActivo)
                return Response.Fail("El socio está inactivo");

            var producto = await _productoCuentaRepository.ObtenerPorId(request.ProductoCuentaId);
            if (producto == null)
                return Response.Fail("El producto de cuenta no existe");

            string numeroCuenta = GenerarNumeroCuenta();

            if (await _cuentaRepository.ExisteNumeroCuentaAsync(numeroCuenta))
                return Response.Fail("Error al generar el número de cuenta, intente nuevamente");

            var cuenta = new Cuenta(
                numeroCuenta,
                socio.Id,
                producto.Id
            );

            await _cuentaRepository.AgregarAsync(cuenta);
            await _cuentaRepository.SaveChangesAsync();

            return Response.Success("La cuenta fue creada correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }

    // ============================
    // Bloquear cuenta
    // ============================
    public async Task<Response> Bloquear(Guid cuentaId)
    {
        try
        {
            var cuenta = await _cuentaRepository.ObtenerPorIdAsync(cuentaId);
            if (cuenta == null)
                return Response.Fail("La cuenta no existe");

            cuenta.Bloquear();

            await _cuentaRepository.SaveChangesAsync();

            return Response.Success("Cuenta bloqueada correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }

    // ============================
    // Cerrar cuenta
    // ============================
    public async Task<Response> Cerrar(Guid cuentaId)
    {
        try
        {
            var cuenta = await _cuentaRepository.ObtenerPorIdAsync(cuentaId);
            if (cuenta == null)
                return Response.Fail("La cuenta no existe");

            if (cuenta.Saldo.Monto > 0)
                return Response.Fail("No se puede cerrar una cuenta con saldo");

            cuenta.Cerrar();

            await _cuentaRepository.SaveChangesAsync();

            return Response.Success("Cuenta cerrada correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }

    // ============================
    // Utilidad interna
    // ============================
    private static string GenerarNumeroCuenta()
    {
        // Simple y suficiente para el proyecto
        return $"CA-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1, 9)}";
    }
}