namespace WSCajaAhorros.Application.DTOs.Cuentas;

public class CrearProductoCuentaRequest
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int TipoProductoCuentaId { get; set; }

    public decimal TasaInteres { get; set; }
    public bool PermiteRetiros { get; set; }
    public bool PermiteTransferencias { get; set; }
    public decimal SaldoMinimo { get; set; }
}