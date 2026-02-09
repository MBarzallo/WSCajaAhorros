using WSCajaAhorros.Domain.Common;

namespace WSCajaAhorros.Domain.Movimientos;

public class Movimiento
{
    public Guid Id { get; private set; }

    public Guid CuentaId { get; private set; }

    public TipoMovimiento Tipo { get; private set; }

    public Dinero Monto { get; private set; }

    public DateTime FechaOperacion { get; private set; }
    public Guid UsuarioId { get; private set; }        
    public string Canal { get; private set; }          
    public string? DireccionIp { get; private set; }

    public string Descripcion { get; private set; }
    public Guid? TransferenciaId { get; private set; } 
    public Guid? AsientoContableId { get; private set; }

    protected Movimiento() { }

    public Movimiento(
        Guid cuentaId,
        TipoMovimiento tipo,
        Dinero monto,
        Guid usuarioId,
        string canal,
        string descripcion,
        string? direccionIp = null,
        Guid? transferenciaId = null)
    {
        Id = Guid.NewGuid();
        CuentaId = cuentaId;
        Tipo = tipo;
        Monto = monto;
        UsuarioId = usuarioId;
        Canal = canal;
        Descripcion = descripcion;
        DireccionIp = direccionIp;
        TransferenciaId = transferenciaId;
        FechaOperacion = DateTime.UtcNow;
    }
    
    public static Movimiento Crear(
        Guid cuentaId,
        TipoMovimiento tipo,
        Dinero monto,
        Guid usuarioId,
        string canal,
        string descripcion,
        Guid? transferenciaId = null
    )
    {
        return new Movimiento(
            cuentaId,
            tipo,
            monto,
            usuarioId,
            canal,
            descripcion,
            direccionIp: null,
            transferenciaId: transferenciaId
        );
    }

}