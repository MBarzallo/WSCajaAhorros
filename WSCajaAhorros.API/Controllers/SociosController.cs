using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Socios;
using WSCajaAhorros.Application.Interfaces.Services.Socios;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SociosController :ControllerBase
{
    
    private readonly ISociosService _sociosService;

    public SociosController(ISociosService sociosService)
    {
        _sociosService = sociosService;
    }

    [HttpPost]
    public async Task<IActionResult> CrearSocio([FromBody] CrearSocioRequest socio)
    {
        var response = await _sociosService.Crear(socio);
        return Ok(response);
    }
}