using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Socios;
using WSCajaAhorros.Domain.Socios;

namespace WSCajaAhorros.Application.Interfaces.Services.Socios;

public interface ISociosService
{
    Task<Response> Crear(CrearSocioRequest request);
    Task<Response<List<Socio>>> Listar(
        string? identificacion,
        string? nombres,
        bool? activo);

    Task<Response<Socio>> ObtenerPorId(Guid id);

    Task<Response> Actualizar(Guid id, ActualizarSocioRequest request);

    Task<Response> Activar(Guid id);
    Task<Response> Desactivar(Guid id);
}