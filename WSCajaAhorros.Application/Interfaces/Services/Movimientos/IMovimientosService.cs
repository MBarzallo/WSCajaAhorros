using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Movimientos;

namespace WSCajaAhorros.Application.Interfaces.Services.Movimientos;

public interface IMovimientosService
{
    Task<Response> Deposito(CrearMovimientoRequest request, Guid usuarioId);
    Task<Response> Retiro(CrearMovimientoRequest request, Guid usuarioId);
    Task<Response<List<MovimientoResponse>>> ObtenerPorCuenta(Guid cuentaId);
}