using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Movimientos;

namespace WSCajaAhorros.Application.Interfaces.Services.Movimientos;

public interface IMovimientosService
{
    Task<Response> Deposito(CrearMovimientoRequest request);
    Task<Response> Retiro(CrearMovimientoRequest request);
    Task<Response<List<MovimientoResponse>>> ObtenerPorCuenta(Guid cuentaId);
}