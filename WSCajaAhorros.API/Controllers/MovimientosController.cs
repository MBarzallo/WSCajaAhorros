using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost("deposito")]
    public async Task<IActionResult> Deposito(
        [FromBody] CrearMovimientoRequest request)
    {
        var response = await _movimientoService.Deposito(request);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("retiro")]
    public async Task<IActionResult> Retiro(
        [FromBody] CrearMovimientoRequest request)
    {
        var response = await _movimientoService.Retiro(request);
        return Ok(response);
    }

    [HttpGet("cuenta/{cuentaId:guid}")]
    public async Task<IActionResult> ObtenerPorCuenta(Guid cuentaId)
    {
        var response = await _movimientoService.ObtenerPorCuenta(cuentaId);
        return Ok(response);
    }
}