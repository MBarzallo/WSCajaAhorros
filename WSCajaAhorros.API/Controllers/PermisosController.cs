using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.Interfaces.Services.Security;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermisosController : ControllerBase
{
    private readonly IPermisoService _permisoService;

    public PermisosController(IPermisoService permisoService)
    {
        _permisoService = permisoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var response = await _permisoService.ObtenerTodos();
        return Ok(response);
    }
}