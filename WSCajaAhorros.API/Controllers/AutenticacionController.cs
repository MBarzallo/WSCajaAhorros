using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutenticacionController : ControllerBase
{
    private readonly IAutenticacionService _autenticacionService;

    public AutenticacionController(IAutenticacionService autenticacionService)
    {
        _autenticacionService = autenticacionService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _autenticacionService.Login(request);
        return Ok(response);
    }

    [HttpPut("activar/{usuarioId}")]
    public async Task<IActionResult> Activar(Guid usuarioId)
    {
        var response = await _autenticacionService.Activar(usuarioId);
        return Ok(response);
    }
    
    [HttpPut("desactivar/{usuarioId}")]
    public async Task<IActionResult> Desactivar(Guid usuarioId)
    {
        var response = await _autenticacionService.Desactivar(usuarioId);
        return Ok(response);
    }
}