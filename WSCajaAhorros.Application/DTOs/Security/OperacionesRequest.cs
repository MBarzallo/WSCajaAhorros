using WSCajaAhorros.Domain.Movimientos;

namespace WSCajaAhorros.Application.DTOs.Security;

public class OperacionesRequest
{
    public Decimal Monto { get; set; }
    public TipoMovimiento TipoMovimiento{ get; set; }
}