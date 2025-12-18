namespace WSCajaAhorros.Domain.Socios;

public class CorreoSocio
{
    public Guid Id { get; private set; }
    public Guid SocioId { get; private set; }

    public string CorreoElectronico { get; private set; } = null!;
    public string? Etiqueta { get; private set; } // "Personal", "Trabajo"
    public bool EsPrincipal { get; private set; }
    public bool EstaActivo { get; private set; }

    protected CorreoSocio() { }

    public CorreoSocio(string correoElectronico, string? etiqueta = null, bool esPrincipal = false)
    {
        if (string.IsNullOrWhiteSpace(correoElectronico))
            throw new ArgumentException("El correo es obligatorio.");

        Id = Guid.NewGuid();
        CorreoElectronico = correoElectronico.Trim();
        Etiqueta = string.IsNullOrWhiteSpace(etiqueta) ? null : etiqueta.Trim();
        EsPrincipal = esPrincipal;
        EstaActivo = true;
    }

    public void Desactivar() => EstaActivo = false;
    public void MarcarPrincipal() => EsPrincipal = true;
    public void DesmarcarPrincipal() => EsPrincipal = false;
}