namespace WSCajaAhorros.Domain.Productos;

public class ProductoCuenta
{
    public Guid Id { get; private set; }
    public string Codigo { get; private set; }          // AHV, CTE, PF, etc.
    public string Nombre { get; private set; }
    public TipoProductoCuenta Tipo { get; private set; }

    public decimal TasaInteres { get; private set; }
    public bool PermiteRetiros { get; private set; }
    public bool PermiteTransferencias { get; private set; }
    public decimal SaldoMinimo { get; private set; }

    protected ProductoCuenta() { }

    public ProductoCuenta(
        string codigo,
        string nombre,
        TipoProductoCuenta tipo,
        decimal tasaInteres,
        bool permiteRetiros,
        bool permiteTransferencias,
        decimal saldoMinimo)
    {
        Id = Guid.NewGuid();
        Codigo = codigo;
        Nombre = nombre;
        Tipo = tipo;
        TasaInteres = tasaInteres;
        PermiteRetiros = permiteRetiros;
        PermiteTransferencias = permiteTransferencias;
        SaldoMinimo = saldoMinimo;
    }
}