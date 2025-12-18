namespace WSCajaAhorros.Domain.Socios;

public class TelefonoSocio
{
    public Guid Id { get; private set; }
    public Guid SocioId { get; private set; }

    public string Numero { get; private set; } = null!;
    public string? Etiqueta { get; private set; } // "Personal", "Trabajo", etc.
    public bool EsPrincipal { get; private set; }
    public bool EstaActivo { get; private set; }

    protected TelefonoSocio() { }

    public TelefonoSocio(string numero, string? etiqueta = null, bool esPrincipal = false)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("El número de teléfono es obligatorio.");

        Id = Guid.NewGuid();
        Numero = numero.Trim();
        Etiqueta = string.IsNullOrWhiteSpace(etiqueta) ? null : etiqueta.Trim();
        EsPrincipal = esPrincipal;
        EstaActivo = true;
    }

    public void Desactivar() => EstaActivo = false;
    public void MarcarPrincipal() => EsPrincipal = true;
    public void DesmarcarPrincipal() => EsPrincipal = false;
}