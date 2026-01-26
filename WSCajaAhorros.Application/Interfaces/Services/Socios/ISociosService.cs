using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Socios;

namespace WSCajaAhorros.Application.Interfaces.Services.Socios;

public interface ISociosService
{
    Task<Response> Crear(CrearSocioRequest request);
}