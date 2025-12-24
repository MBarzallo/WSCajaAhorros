using WSCajaAhorros.Domain.Socios;

namespace WSCajaAhorros.Application.Interfaces.Repositories.Socios;

public interface ISocioRepository
{
    Task Crear(Socio socio);
    Task<Socio?> ObtenerPorIdentificacion(string identificacion);
    Task<Socio?> ObtenerPorId(Guid id);
    Task SaveChangesAsync();
}