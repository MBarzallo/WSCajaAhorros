using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly  IUsuarioService  _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var response = await _usuarioService.ObtenerTodos();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearUsuario([FromBody] CrearUsuarioRequest request)
    {
        var response = await _usuarioService.CrearUsuario(request);
        return Ok(response);
    }

    [HttpPost("rol/{userId}")]
    public async Task<IActionResult> AsignarRol(Guid userId, [FromBody] CrearUsuarioRolRequest request)
    {
        var response = await _usuarioService.CrearUsuarioRol(userId, request);
        return Ok(response);
    }
}