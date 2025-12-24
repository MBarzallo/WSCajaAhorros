using WSCajaAhorros.Domain.Productos;

namespace WSCajaAhorros.Application.Interfaces.Services.Cuentas;

public interface IProductoCuentaRepository
{
    Task<List<ProductoCuenta>> ObtenerTodos();
    Task<ProductoCuenta?> ObtenerPorId(Guid id);
    Task<bool> ExisteCodigo(string codigo);
    Task Agregar(ProductoCuenta productoCuenta);
    Task SaveChangesAsync();
}