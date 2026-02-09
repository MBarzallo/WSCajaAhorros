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
    
    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] string? identificacion,
        [FromQuery] string? nombres,
        [FromQuery] bool? activo)
        => Ok(await _sociosService.Listar(identificacion, nombres, activo));

    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(Guid id)
        => Ok(await _sociosService.ObtenerPorId(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(
        Guid id,
        [FromBody] ActualizarSocioRequest request)
        => Ok(await _sociosService.Actualizar(id, request));

    [HttpPut("{id}/activar")]
    public async Task<IActionResult> Activar(Guid id)
        => Ok(await _sociosService.Activar(id));

    [HttpPut("{id}/desactivar")]
    public async Task<IActionResult> Desactivar(Guid id)
        => Ok(await _sociosService.Desactivar(id));
}