using WSCajaAhorros.Domain.Common;

namespace WSCajaAhorros.Domain.Contabilidad;

public class AsientoContable
{
    public Guid Id { get; private set; }

    public DateTime FechaContable { get; private set; }
    public string Descripcion { get; private set; }

    public Dinero Monto { get; private set; }

    public TipoOperacionContable TipoOperacion { get; private set; }  
    public Guid ReferenciaOperacionId { get; private set; } 

    public Guid UsuarioId { get; private set; }          
    public EstadoAsientoContable Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    protected AsientoContable() { }

    public AsientoContable(
        string descripcion,
        Dinero monto,
        TipoOperacionContable tipoOperacion,
        Guid referenciaOperacionId,
        Guid usuarioId)
    {
        Id = Guid.NewGuid();
        FechaContable = DateTime.UtcNow;
        FechaCreacion = DateTime.UtcNow;

        Descripcion = descripcion;
        Monto = monto;
        TipoOperacion = tipoOperacion;
        ReferenciaOperacionId = referenciaOperacionId;
        UsuarioId = usuarioId;

        Estado = EstadoAsientoContable.Generado;
    }

    public void MarcarComoExportado()
    {
        Estado = EstadoAsientoContable.Exportado;
    }

    public void Anular(string motivo)
    {
        Estado = EstadoAsientoContable.Anulado;
        Descripcion = $"{Descripcion} | ANULADO: {motivo}";
    }
}