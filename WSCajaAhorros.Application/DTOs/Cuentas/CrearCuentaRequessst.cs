namespace WSCajaAhorros.Application.DTOs.Cuentas;

public class CrearCuentaRequest
{
    public Guid SocioId { get; set; }
    public Guid ProductoCuentaId { get; set; }
}