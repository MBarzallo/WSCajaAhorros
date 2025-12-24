using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Interfaces.Repositories.Movimientos;
using WSCajaAhorros.Domain.Movimientos;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories.Movimientos;

public class MovimientoRepository : IMovimientoRepository
{
    private readonly AppDbContext _context;

    public MovimientoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Agregar(Movimiento movimiento)
    {
        await _context.Movimientos.AddAsync(movimiento);
    }

    public async Task<List<Movimiento>> ObtenerPorCuenta(Guid cuentaId)
    {
        return await _context.Movimientos
            .Where(m => m.CuentaId == cuentaId)
            .OrderByDescending(m => m.FechaOperacion)
            .ToListAsync();
    }

    public async Task<List<Movimiento>> ObtenerUltimos(Guid cuentaId, int cantidad)
    {
        return await _context.Movimientos
            .Where(m => m.CuentaId == cuentaId)
            .OrderByDescending(m => m.FechaOperacion)
            .Take(cantidad)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
    
    public async Task GuardarCambiosAsync()
        => await _context.SaveChangesAsync();

    public async Task EjecutarTransaccionAsync(Func<Task> operacion)
    {
        using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            await operacion();
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}