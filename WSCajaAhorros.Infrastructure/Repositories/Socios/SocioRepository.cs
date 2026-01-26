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
    
    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}