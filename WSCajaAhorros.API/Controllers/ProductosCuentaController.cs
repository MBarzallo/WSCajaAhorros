using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Cuentas;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosCuentaController : ControllerBase
{
    private readonly IProductoCuentaService _productoCuentaService;

    public ProductosCuentaController(IProductoCuentaService productoCuentaService)
    {
        _productoCuentaService = productoCuentaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var response = await _productoCuentaService.ObtenerTodos();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar([FromBody] CrearProductoCuentaRequest request)
    {
        var response = await _productoCuentaService.Agregar(request);
        return Ok(response);
    }
}