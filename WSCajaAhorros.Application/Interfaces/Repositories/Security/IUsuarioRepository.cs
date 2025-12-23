using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Interfaces.Security;

public interface IUsuarioRepository
{
    Task<List<Usuario>> ObtenerTodos();
    Task Crear(Usuario usuario);
    Task<Usuario?> ObtenerPorNombreUsuario(string nombreUsuario);
    Task<Usuario?> ObtenerPorId(Guid id);
    Task SaveChangesAsync();
}