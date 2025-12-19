using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController:ControllerBase
{
    private readonly IRolService _rolService;

    public RolesController(IRolService rolService)
    {
        _rolService = rolService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var response = await _rolService.ObtenerTodos();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar([FromBody] CrearRolRequest request)
    {
        var response = await _rolService.Agregar(request);
        return Ok(response);
    }
}