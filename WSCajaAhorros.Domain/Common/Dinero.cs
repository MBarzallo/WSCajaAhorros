using Microsoft.EntityFrameworkCore;
namespace WSCajaAhorros.Domain.Common;

[Owned]
public sealed class Dinero
{
    
    public decimal Monto { get; }

    private Dinero() { }
    
    public Dinero(decimal monto)
    {
        if (monto < 0)
            throw new ArgumentException("El monto no puede ser negativo");

        Monto = decimal.Round(monto, 2);
    }

    public static Dinero operator +(Dinero a, Dinero b)
        => new(a.Monto + b.Monto);

    public static Dinero operator -(Dinero a, Dinero b)
    {
        if (a.Monto < b.Monto)
            throw new InvalidOperationException("Saldo insuficiente");

        return new Dinero(a.Monto - b.Monto);
    }
}