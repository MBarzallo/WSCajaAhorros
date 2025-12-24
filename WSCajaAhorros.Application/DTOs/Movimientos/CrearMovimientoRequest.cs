namespace WSCajaAhorros.Application.DTOs.Movimientos;

public class CrearMovimientoRequest
{
    public Guid CuentaId { get; set; }
    public decimal Monto { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}