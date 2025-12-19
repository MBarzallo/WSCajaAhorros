using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Domain.Security;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _dbContext;
    
    public  UsuarioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Usuario>> ObtenerTodos()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    public async Task Crear(Usuario usuario)
    {
        await _dbContext.Usuarios.AddAsync(usuario);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Usuario?> ObtenerPorNombreUsuario(string nombreUsuario)
    {
        var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        return usuario;
    }
}