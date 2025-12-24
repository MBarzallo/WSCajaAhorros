using Microsoft.AspNetCore.Mvc;
using WSCajaAhorros.Application.DTOs.Movimientos;
using WSCajaAhorros.Application.Services.Movimientos;

namespace WSCajaAhorros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferenciasController : ControllerBase
{
    private readonly TransferenciaService _transferenciaService;

    public TransferenciasController(TransferenciaService transferenciaService)
    {
        _transferenciaService = transferenciaService;
    }

    [HttpPost]
    public async Task<IActionResult> Transferir([FromBody] CrearTransferenciaRequest request)
    {
        var usuarioId = Guid.NewGuid(); // luego sale del JWT
        var response = await _transferenciaService.Transferir(request, usuarioId);
        return Ok(response);
    }
}