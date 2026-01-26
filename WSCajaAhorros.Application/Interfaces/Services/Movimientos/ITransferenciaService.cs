using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Movimientos;

namespace WSCajaAhorros.Application.Interfaces.Services.Movimientos;

public interface ITransferenciaService
{
    Task<Response> Transferir(CrearTransferenciaRequest request);
}