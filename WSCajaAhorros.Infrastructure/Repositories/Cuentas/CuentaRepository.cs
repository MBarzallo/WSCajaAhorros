using Microsoft.EntityFrameworkCore;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Domain.Cuentas;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories.Cuentas;

public class CuentaRepository : ICuentaRepository
{
    private readonly AppDbContext _context;
     
    public CuentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cuenta?> ObtenerPorIdAsync(Guid cuentaId)
    {
        return await _context.Cuentas
            .FirstOrDefaultAsync(c => c.Id == cuentaId);
    }

    public async Task<Cuenta?> ObtenerPorNumeroAsync(string numeroCuenta)
    {
        return await _context.Cuentas
            .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
    }

    public async Task<List<Cuenta>> ObtenerPorSocioAsync(Guid socioId)
    {
        return await _context.Cuentas
            .Where(c => c.SocioId == socioId)
            .ToListAsync();
    }

    public async Task<bool> ExisteNumeroCuentaAsync(string numeroCuenta)
    {
        return await _context.Cuentas
            .AnyAsync(c => c.NumeroCuenta == numeroCuenta);
    }

    public async Task AgregarAsync(Cuenta cuenta)
    {
        await _context.Cuentas.AddAsync(cuenta);
    }

    public Task ActualizarAsync(Cuenta cuenta)
    {
        _context.Cuentas.Update(cuenta);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}