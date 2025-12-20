using Microsoft.EntityFrameworkCore;
namespace WSCajaAhorros.Domain.Socios;

[Owned]
public sealed class Identificacion
{
    public Identificacion(){}
    public TipoIdentificacion Tipo { get; }
    public string Numero { get; }

    public Identificacion(TipoIdentificacion tipo, string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("El número de identificación es obligatorio.");

        Tipo = tipo;
        Numero = numero.Trim();
    }
}