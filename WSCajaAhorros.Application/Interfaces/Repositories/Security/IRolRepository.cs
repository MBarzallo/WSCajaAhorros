using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Interfaces.Security;

public interface IRolRepository
{
    Task<List<Rol>> ObtenerTodos();
    Task<Rol?> ObtenerPorId(Guid id);
    Task Agregar(Rol rol);
    Task AgregarRolPermiso(RolPermiso permiso);
}