namespace WSCajaAhorros.Domain.Socios;

public class Socio
{
    public Guid Id { get; private set; }

    public TipoPersona TipoPersona { get; private set; }
    public Identificacion Identificacion { get; private set; } = null!;

    public string? Nombres { get; private set; }
    public string? Apellidos { get; private set; }
    public DateOnly? FechaNacimiento { get; private set; }

    public string? RazonSocial { get; private set; }
    public string? NombreComercial { get; private set; }
    public DateOnly? FechaConstitucion { get; private set; }

    public bool EstaActivo { get; private set; }    
    public DateTime FechaIngreso { get; private set; }
    public DateTime? FechaActualizacion { get; private set; }

    private readonly List<TelefonoSocio> _telefonos = new();
    public IReadOnlyCollection<TelefonoSocio> Telefonos => _telefonos.AsReadOnly();

    private readonly List<CorreoSocio> _correos = new();
    public IReadOnlyCollection<CorreoSocio> Correos => _correos.AsReadOnly();

    private readonly List<DireccionSocio> _direcciones = new();
    public IReadOnlyCollection<DireccionSocio> Direcciones => _direcciones.AsReadOnly();

    protected Socio() { }

    private Socio(TipoPersona tipoPersona, Identificacion identificacion)
    {
        Id = Guid.NewGuid();
        TipoPersona = tipoPersona;
        Identificacion = identificacion;

        EstaActivo = true;
        FechaIngreso = DateTime.UtcNow;
    }

    //  Fábricas: obligan a crear bien según tipo
    public static Socio CrearNatural(
        Identificacion identificacion,
        string nombres,
        string apellidos,
        DateOnly fechaNacimiento)
    {
        if (string.IsNullOrWhiteSpace(nombres)) throw new ArgumentException("Nombres obligatorios.");
        if (string.IsNullOrWhiteSpace(apellidos)) throw new ArgumentException("Apellidos obligatorios.");

        var socio = new Socio(TipoPersona.Natural, identificacion);
        socio.Nombres = nombres.Trim();
        socio.Apellidos = apellidos.Trim();
        socio.FechaNacimiento = fechaNacimiento;
        return socio;
    }

    public static Socio CrearJuridica(
        Identificacion identificacion,
        string razonSocial,
        string? nombreComercial,
        DateOnly? fechaConstitucion)
    {
        if (string.IsNullOrWhiteSpace(razonSocial)) throw new ArgumentException("Razón social obligatoria.");

        var socio = new Socio(TipoPersona.Juridica, identificacion);
        socio.RazonSocial = razonSocial.Trim();
        socio.NombreComercial = string.IsNullOrWhiteSpace(nombreComercial) ? null : nombreComercial.Trim();
        socio.FechaConstitucion = fechaConstitucion;
        return socio;
    }

    //  Operaciones del agregado (mantener consistencia)
    public void Desactivar()
    {
        EstaActivo = false;
        FechaActualizacion = DateTime.UtcNow;
    }

    public void Activar()
    {
        EstaActivo = true;
        FechaActualizacion = DateTime.UtcNow;
    }

    public void AgregarTelefono(TelefonoSocio telefono)
    {
        if (telefono is null) throw new ArgumentNullException(nameof(telefono));

        if (telefono.EsPrincipal)
            foreach (var t in _telefonos) t.DesmarcarPrincipal();

        _telefonos.Add(telefono);
        FechaActualizacion = DateTime.UtcNow;
    }

    public void AgregarCorreo(CorreoSocio correo)
    {
        if (correo is null) throw new ArgumentNullException(nameof(correo));

        if (correo.EsPrincipal)
            foreach (var c in _correos) c.DesmarcarPrincipal();

        _correos.Add(correo);
        FechaActualizacion = DateTime.UtcNow;
    }

    public void AgregarDireccion(DireccionSocio direccion)
    {
        if (direccion is null) throw new ArgumentNullException(nameof(direccion));

        if (direccion.EsPrincipal)
            foreach (var d in _direcciones) d.DesmarcarPrincipal();

        _direcciones.Add(direccion);
        FechaActualizacion = DateTime.UtcNow;
    }
}