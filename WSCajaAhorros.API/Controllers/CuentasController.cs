using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Cuentas;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaService _cuentaService;

    public CuentasController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    // ============================
    // Obtener cuentas por socio
    // ============================
    [HttpGet("socio/{socioId:guid}")]
    public async Task<IActionResult> ObtenerPorSocio(Guid socioId)
    {
        var response = await _cuentaService.ObtenerPorSocio(socioId);
        return Ok(response);
    }

    // ============================
    // Crear cuenta
    // ============================
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearCuentaRequest request)
    {
        var response = await _cuentaService.Crear(request);
        return Ok(response);
    }

    // ============================
    // Bloquear cuenta
    // ============================
    [HttpPut("{cuentaId:guid}/bloquear")]
    public async Task<IActionResult> Bloquear(Guid cuentaId)
    {
        var response = await _cuentaService.Bloquear(cuentaId);
        return Ok(response);
    }

    // ============================
    // Cerrar cuenta
    // ============================
    [HttpPut("{cuentaId:guid}/cerrar")]
    public async Task<IActionResult> Cerrar(Guid cuentaId)
    {
        var response = await _cuentaService.Cerrar(cuentaId);
        return Ok(response);
    }
}