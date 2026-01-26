namespace WSCajaAhorros.Application.DTOs.Movimientos;

public class CrearTransferenciaRequest
{
    public Guid CuentaOrigenId { get; set; }
    public Guid CuentaDestinoId { get; set; }
    public decimal Monto { get; set; }
    public string Observacion { get; set; } = string.Empty;
}