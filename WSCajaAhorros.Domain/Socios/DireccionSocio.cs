using WSCajaAhorros.Domain.Common;

namespace WSCajaAhorros.Domain.Socios;

public class DireccionSocio
{
    public Guid Id { get; private set; }
    public Guid SocioId { get; private set; }

    public Direccion Direccion { get; private set; } = null!;
    public string? Etiqueta { get; private set; } // "Domicilio", "Trabajo", "Matriz"
    public bool EsPrincipal { get; private set; }
    public bool EstaActiva { get; private set; }

    protected DireccionSocio() { }

    public DireccionSocio(Direccion direccion, string? etiqueta = null, bool esPrincipal = false)
    {
        Id = Guid.NewGuid();
        Direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
        Etiqueta = string.IsNullOrWhiteSpace(etiqueta) ? null : etiqueta.Trim();
        EsPrincipal = esPrincipal;
        EstaActiva = true;
    }

    public void Desactivar() => EstaActiva = false;
    public void MarcarPrincipal() => EsPrincipal = true;
    public void DesmarcarPrincipal() => EsPrincipal = false;
}