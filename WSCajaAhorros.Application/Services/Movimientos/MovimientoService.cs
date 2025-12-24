using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Movimientos;
using WSCajaAhorros.Application.Interfaces.Repositories.Movimientos;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Application.Interfaces.Services.Movimientos;
using WSCajaAhorros.Domain.Common;
using WSCajaAhorros.Domain.Cuentas;
using WSCajaAhorros.Domain.Movimientos;

namespace WSCajaAhorros.Application.Services.Movimientos;

public class MovimientoService : IMovimientosService
{
    private readonly IMovimientoRepository _movimientoRepository;
    private readonly ICuentaRepository _cuentaRepository;

    public MovimientoService(
        IMovimientoRepository movimientoRepository,
        ICuentaRepository cuentaRepository)
    {
        _movimientoRepository = movimientoRepository;
        _cuentaRepository = cuentaRepository;
    }

    public async Task<Response> Deposito(CrearMovimientoRequest request, Guid usuarioId)
    {
        if (request.Monto <= 0)
            return Response.Fail("El monto debe ser mayor a cero");

        try
        {
            await _cuentaRepository.EjecutarTransaccionAsync(async () =>
            {
                var cuenta = await _cuentaRepository.ObtenerPorIdAsync(request.CuentaId);
                if (cuenta == null)
                    throw new Exception("La cuenta no existe");

                if (cuenta.Estado != EstadoCuenta.Activa)
                    throw new Exception("La cuenta no está activa");

                var monto = new Dinero(request.Monto);

                cuenta.Acreditar(monto);

                var movimiento = new Movimiento(
                    cuenta.Id,
                    TipoMovimiento.Deposito,
                    monto,
                    usuarioId,
                    "VENTANILLA",
                    request.Descripcion
                );

                await _movimientoRepository.Agregar(movimiento);
            });

            return Response.Success("Depósito realizado correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }

    // ============================
    // RETIRO
    // ============================
    public async Task<Response> Retiro(CrearMovimientoRequest request, Guid usuarioId)
    {
        if (request.Monto <= 0)
            return Response.Fail("El monto debe ser mayor a cero");

        try
        {
            await _cuentaRepository.EjecutarTransaccionAsync(async () =>
            {
                var cuenta = await _cuentaRepository.ObtenerPorIdAsync(request.CuentaId);
                if (cuenta == null)
                    throw new Exception("La cuenta no existe");

                if (cuenta.Estado != EstadoCuenta.Activa)
                    throw new Exception("La cuenta no está activa");

                var monto = new Dinero(request.Monto);

                cuenta.Debitar(monto);

                var movimiento = new Movimiento(
                    cuenta.Id,
                    TipoMovimiento.Retiro,
                    monto,
                    usuarioId,
                    "VENTANILLA",
                    request.Descripcion
                );

                await _movimientoRepository.Agregar(movimiento);
            });

            return Response.Success("Retiro realizado correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }

    // ============================
    // CONSULTA MOVIMIENTOS
    // ============================
    public async Task<Response<List<MovimientoResponse>>> ObtenerPorCuenta(Guid cuentaId)
    {
        var movimientos = await _movimientoRepository.ObtenerPorCuenta(cuentaId);

        var response = movimientos.Select(m => new MovimientoResponse
        {
            Fecha = m.FechaOperacion,
            Tipo = m.Tipo.ToString(),
            Monto = m.Monto.Monto,
            Descripcion = m.Descripcion
        }).ToList();

        return Response<List<MovimientoResponse>>.Success(response);
    }
}