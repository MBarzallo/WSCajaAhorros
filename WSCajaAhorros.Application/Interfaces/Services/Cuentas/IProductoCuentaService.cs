using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Cuentas;
using WSCajaAhorros.Domain.Productos;

namespace WSCajaAhorros.Application.Interfaces.Services.Cuentas;

public interface IProductoCuentaService
{
    Task<Response<List<ProductoCuenta>>> ObtenerTodos();
    Task<Response> Agregar(CrearProductoCuentaRequest request);
}