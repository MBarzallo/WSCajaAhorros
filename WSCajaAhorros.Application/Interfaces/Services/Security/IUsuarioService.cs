using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Interfaces.Services.Security;

public interface IUsuarioService
{
    Task<Response<List<Usuario>>> ObtenerTodos();
    Task<Response> CrearUsuario(CrearUsuarioRequest request);
    Task<Response> CrearUsuarioRol(Guid userId, CrearUsuarioRolRequest request);
    Task<Response> Activar(Guid userId);
    Task<Response> Desactivar(Guid userId);
}