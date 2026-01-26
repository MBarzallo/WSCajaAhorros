using WSCajaAhorros.Domain.Movimientos;

namespace WSCajaAhorros.Application.Interfaces.Repositories.Movimientos;

public interface IMovimientoRepository
{
    Task Agregar(Movimiento movimiento);
    Task<List<Movimiento>> ObtenerPorCuenta(Guid cuentaId);
    Task<List<Movimiento>> ObtenerUltimos(Guid cuentaId, int cantidad);
    Task SaveChangesAsync();
    Task GuardarCambiosAsync();
    Task EjecutarTransaccionAsync(Func<Task> operacion);
}