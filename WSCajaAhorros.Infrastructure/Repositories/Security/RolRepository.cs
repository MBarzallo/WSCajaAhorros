using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Domain.Security;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories;

public class RolRepository: IRolRepository
{
    private readonly AppDbContext _dbContext;
    
    public RolRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Rol>> ObtenerTodos()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task<Rol?> ObtenerPorId(Guid id)
    {
        return await _dbContext.Roles.FindAsync(id);
    }

    public async Task Agregar(Rol rol)
    {
        _dbContext.Roles.Add(rol);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AgregarRolPermiso(RolPermiso permiso)
    {
        _dbContext.RolPermisos.Add(permiso);
        await _dbContext.SaveChangesAsync();
    }
    
    
}