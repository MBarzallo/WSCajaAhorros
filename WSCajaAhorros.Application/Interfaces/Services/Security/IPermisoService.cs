using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Interfaces.Services.Security;

public interface IPermisoService
{
    Task<Response<List<Permiso>>> ObtenerTodos();
}