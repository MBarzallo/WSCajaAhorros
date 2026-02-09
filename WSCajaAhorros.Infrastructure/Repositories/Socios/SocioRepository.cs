using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Interfaces.Repositories.Socios;
using WSCajaAhorros.Domain.Socios;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories.Socios;

public class SocioRepository : ISocioRepository
{
    private readonly AppDbContext _dbContext;
    
    public SocioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Crear(Socio socio)
    {
        await _dbContext.Socios.AddAsync(socio);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Socio?> ObtenerPorIdentificacion(string identificacion)
    {
        var socio = await _dbContext.Socios.FirstOrDefaultAsync(s=>s.Identificacion.Numero==identificacion);
        return socio;
    }

    public async Task<Socio?> ObtenerPorId(Guid id)
    {
        var socio = await _dbContext.Socios.FirstOrDefaultAsync(s => s.Id == id);
        return socio;
    }
    public async Task<List<Socio>> Listar(
        string? identificacion,
        string? nombres,
        bool? activo)
    {
        var query = _dbContext.Socios.AsQueryable();

        if (!string.IsNullOrWhiteSpace(identificacion))
            query = query.Where(s => s.Identificacion.Numero.Contains(identificacion));

        if (!string.IsNullOrWhiteSpace(nombres))
            query = query.Where(s =>
                (s.Nombres + " " + s.Apellidos).Contains(nombres) ||
                s.RazonSocial!.Contains(nombres));

        if (activo.HasValue)
            query = query.Where(s => s.EstaActivo == activo.Value);

        return await query
            .OrderByDescending(s => s.FechaIngreso)
            .ToListAsync();
    }
    
    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}