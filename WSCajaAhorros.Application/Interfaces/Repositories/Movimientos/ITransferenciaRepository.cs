using WSCajaAhorros.Domain.Movimientos;

namespace WSCajaAhorros.Application.Interfaces.Repositories.Movimientos;

public interface ITransferenciaRepository
{
    Task Agregar(Transferencia transferencia);
}