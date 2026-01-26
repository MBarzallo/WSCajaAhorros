using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Cuentas;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Domain.Productos;

namespace WSCajaAhorros.Application.Services.Cuentas;

public class ProductoCuentaService : IProductoCuentaService
{
    
    private readonly IProductoCuentaRepository _productoCuentaRepository;

    public ProductoCuentaService(IProductoCuentaRepository productoCuentaRepository)
    {
        _productoCuentaRepository = productoCuentaRepository;
    }

    public async Task<Response<List<ProductoCuenta>>> ObtenerTodos()
    {
        var productos = await _productoCuentaRepository.ObtenerTodos();
        return Response<List<ProductoCuenta>>.Success(productos);
    }

    public async Task<Response> Agregar(CrearProductoCuentaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Codigo))
            return Response.Fail("El código del producto es obligatorio");

        if (string.IsNullOrWhiteSpace(request.Nombre))
            return Response.Fail("El nombre del producto es obligatorio");

        try
        {
            if (await _productoCuentaRepository.ExisteCodigo(request.Codigo))
                return Response.Fail("Ya existe un producto con ese código");

            var tipo = (TipoProductoCuenta)request.TipoProductoCuentaId;

            var producto = new ProductoCuenta(
                request.Codigo,
                request.Nombre,
                tipo,
                request.TasaInteres,
                request.PermiteRetiros,
                request.PermiteTransferencias,
                request.SaldoMinimo
            );

            await _productoCuentaRepository.Agregar(producto);
            await _productoCuentaRepository.SaveChangesAsync();

            return Response.Success("Producto de cuenta creado correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
    
}