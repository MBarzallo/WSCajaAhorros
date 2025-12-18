using WSCajaAhorros.Domain.Common;

namespace WSCajaAhorros.Domain.Movimientos;

public class Transferencia
{
    public Guid Id { get; private set; }

    public Guid CuentaOrigenId { get; private set; }
    public Guid CuentaDestinoId { get; private set; }

    public Dinero Monto { get; private set; }

    public Guid UsuarioId { get; private set; }     
    public DateTime FechaOperacion { get; private set; }
    public string Canal { get; private set; }       
    public string? DireccionIp { get; private set; }
    public string Observacion { get; private set; }
    public string CodigoOperacion { get; private set; } 

    protected Transferencia() { }

    public Transferencia(
        Guid cuentaOrigenId,
        Guid cuentaDestinoId,
        Dinero monto,
        Guid usuarioId,
        string canal,
        string observacion,
        string? direccionIp = null)
    {
        Id = Guid.NewGuid();
        CuentaOrigenId = cuentaOrigenId;
        CuentaDestinoId = cuentaDestinoId;
        Monto = monto;
        UsuarioId = usuarioId;
        Canal = canal;
        Observacion = observacion;
        DireccionIp = direccionIp;
        FechaOperacion = DateTime.UtcNow;

        CodigoOperacion = GenerarCodigo();
    }

    private static string GenerarCodigo()
        => $"TRF-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..6]}";
}