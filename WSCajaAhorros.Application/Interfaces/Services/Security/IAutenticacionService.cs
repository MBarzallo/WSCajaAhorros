using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Security;

namespace WSCajaAhorros.Application.Interfaces.Services.Security;

public interface IAutenticacionService
{
    Task<Response<LoginResponse>> Login(LoginRequest request);
    Task<Response> Activar(Guid usuarioId);
    Task<Response> Desactivar(Guid usuarioId);
}