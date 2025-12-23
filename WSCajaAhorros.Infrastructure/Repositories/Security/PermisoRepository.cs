using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Domain.Security;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories;

public class PermisoRepository : IPermisoRepository
{
    private readonly AppDbContext _dbContext;

    public PermisoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Permiso>> ObtenerTodos()
    {
        return await _dbContext.Permisos.ToListAsync();
    }

    public async Task<Permiso?> ObtenerPorId(Guid id)
    {
        return await _dbContext.Permisos.FirstOrDefaultAsync(p => p.Id == id);
    }
    
}