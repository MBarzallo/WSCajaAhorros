using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Movimientos;
using WSCajaAhorros.Application.Interfaces.Services.Movimientos;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientosService _movimientoService;

    public MovimientosController(IMovimientosService movimientoService)
    {
        _movimientoService = movimientoService;
    }

    [HttpPost("deposito")]
    public async Task<IActionResult> Deposito(
        [FromBody] CrearMovimientoRequest request)
    {
        var usuarioId = Guid.NewGuid(); // luego lo sacas del JWT
        var response = await _movimientoService.Deposito(request, usuarioId);
        return Ok(response);
    }

    [HttpPost("retiro")]
    public async Task<IActionResult> Retiro(
        [FromBody] CrearMovimientoRequest request)
    {
        var usuarioId = Guid.NewGuid();
        var response = await _movimientoService.Retiro(request, usuarioId);
        return Ok(response);
    }

    [HttpGet("cuenta/{cuentaId:guid}")]
    public async Task<IActionResult> ObtenerPorCuenta(Guid cuentaId)
    {
        var response = await _movimientoService.ObtenerPorCuenta(cuentaId);
        return Ok(response);
    }
}