using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperacionesController:ControllerBase
{
    private readonly IOperacionesService _OperacionesService;

    public OperacionesController(IOperacionesService OperacionesService)
    {
        _OperacionesService = OperacionesService;
    }

    [HttpPost("depositar")]
    public async Task<IActionResult> Depositar([FromBody] OperacionesRequest request)
    {
        var response = await _OperacionesService.Retirar(request);
        return Ok(response);
    }

    
    [HttpPost("retirar")]
    public async Task<IActionResult> Returar([FromBody] OperacionesRequest request)
    {
        var response = await _OperacionesService.Depositar(request);
        return Ok(response);
    }
}