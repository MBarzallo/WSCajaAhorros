namespace WSCajaAhorros.Application.DTOs.Cuentas;

public class CuentaResponse
{
    public Guid Id { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaApertura { get; set; }
}