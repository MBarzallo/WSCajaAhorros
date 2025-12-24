using WSCajaAhorros.Application.Interfaces.Repositories.Movimientos;
using WSCajaAhorros.Domain.Movimientos;
using WSCajaAhorros.Infrastructure.Persistence;

namespace WSCajaAhorros.Infrastructure.Repositories.Movimientos;

public class TransferenciaRepository :ITransferenciaRepository
{
    private readonly AppDbContext _context;

    public TransferenciaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Agregar(Transferencia transferencia)
    {
        await _context.Transferencias.AddAsync(transferencia);
    }
}