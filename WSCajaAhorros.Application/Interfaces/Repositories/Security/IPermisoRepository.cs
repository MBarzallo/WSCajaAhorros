using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Interfaces.Security;

public interface IPermisoRepository
{
    Task<List<Permiso>> ObtenerTodos();
    Task<Permiso?> ObtenerPorId(Guid id);
}