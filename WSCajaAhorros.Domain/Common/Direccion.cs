using Microsoft.EntityFrameworkCore;
namespace WSCajaAhorros.Domain.Common;

[Owned]
public sealed class Direccion
{
    public Direccion(){}
    public string Linea1 { get; }
    public string? Linea2 { get; }
    public string Ciudad { get; }
    public string Provincia { get; }
    public string Pais { get; }
    public string? Referencia { get; }

    public Direccion(string linea1, string ciudad, string provincia, string pais, string? linea2 = null, string? referencia = null)
    {
        if (string.IsNullOrWhiteSpace(linea1)) throw new ArgumentException("La dirección (línea 1) es obligatoria.");
        if (string.IsNullOrWhiteSpace(ciudad)) throw new ArgumentException("La ciudad es obligatoria.");
        if (string.IsNullOrWhiteSpace(provincia)) throw new ArgumentException("La provincia es obligatoria.");
        if (string.IsNullOrWhiteSpace(pais)) throw new ArgumentException("El país es obligatorio.");

        Linea1 = linea1.Trim();
        Linea2 = string.IsNullOrWhiteSpace(linea2) ? null : linea2.Trim();
        Ciudad = ciudad.Trim();
        Provincia = provincia.Trim();
        Pais = pais.Trim();
        Referencia = string.IsNullOrWhiteSpace(referencia) ? null : referencia.Trim();
    }
}