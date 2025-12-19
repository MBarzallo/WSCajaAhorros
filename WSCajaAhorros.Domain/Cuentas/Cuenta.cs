using WSCajaAhorros.Domain.Common;

namespace WSCajaAhorros.Domain.Cuentas;

public class Cuenta
{
    public Guid Id { get; private set; }
    public string NumeroCuenta { get; private set; }

    public Guid SocioId { get; private set; }        // FK al socio
    public Guid ProductoCuentaId { get; private set; }

    public Dinero Saldo { get; private set; }
    public EstadoCuenta Estado { get; private set; }

    public DateTime FechaApertura { get; private set; }

    protected Cuenta() { }

    public Cuenta(
        string numeroCuenta,
        Guid socioId,
        Guid productoCuentaId)
    {
        Id = Guid.NewGuid();
        NumeroCuenta = numeroCuenta;
        SocioId = socioId;
        ProductoCuentaId = productoCuentaId;
        Saldo = new Dinero(0);
        Estado = EstadoCuenta.Activa;
        FechaApertura = DateTime.UtcNow;
    }

    public void Acreditar(Dinero monto)
    {
        Saldo = Saldo + monto;
    }

    public void Debitar(Dinero monto)
    {
        Saldo = Saldo - monto;
    }

    public void Bloquear()
    {
        Estado = EstadoCuenta.Bloqueada;
    }

    public void Cerrar()
    {
        Estado = EstadoCuenta.Cerrada;
    }
}