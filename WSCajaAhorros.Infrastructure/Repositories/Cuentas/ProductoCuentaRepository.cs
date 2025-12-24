using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Domain.Productos;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories.Cuentas;

public class ProductoCuentaRepository : IProductoCuentaRepository
{
    private readonly AppDbContext _context;

    public ProductoCuentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductoCuenta>> ObtenerTodos()
        => await _context.ProductoCuentas.ToListAsync();

    public async Task<ProductoCuenta?> ObtenerPorId(Guid id)
        => await _context.ProductoCuentas.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<bool> ExisteCodigo(string codigo)
        => await _context.ProductoCuentas.AnyAsync(p => p.Codigo == codigo);

    public async Task Agregar(ProductoCuenta productoCuenta)
    {
        await _context.ProductoCuentas.AddAsync(productoCuenta);
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}