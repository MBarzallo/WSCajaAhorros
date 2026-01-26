using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Interfaces.Services.Security;

public interface IOperacionesService
{
    Task<Response> Depositar(OperacionesRequest request);
    Task<Response> Retirar(OperacionesRequest request);
}