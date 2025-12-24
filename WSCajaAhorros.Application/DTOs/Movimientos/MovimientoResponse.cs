namespace WSCajaAhorros.Application.DTOs.Movimientos;

public class MovimientoResponse
{
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}