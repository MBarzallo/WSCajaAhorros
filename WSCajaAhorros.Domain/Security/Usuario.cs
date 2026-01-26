namespace WSCajaAhorros.Domain.Security;

public class Usuario
{
    public Guid Id { get; private set; }

    public string NombreUsuario { get; private set; } = null!;
    public string CorreoElectronico { get; private set; } = null!;

    public string HashContrasena { get; private set; } = null!;
    public string SaltContrasena { get; private set; } = null!;

    public bool EstaActivo { get; private set; }
    public bool MfaHabilitado { get; private set; }

    public DateTime FechaCreacion { get; private set; }
    public DateTime? UltimoInicioSesion { get; private set; }

    private readonly List<UsuarioRol> _roles = new();
    public IReadOnlyCollection<UsuarioRol> Roles => _roles.AsReadOnly();

    private readonly List<UsuarioAccesoHorario> _accesosHorarios = new();
    public IReadOnlyCollection<UsuarioAccesoHorario> AccesosHorarios => _accesosHorarios.AsReadOnly();

    protected Usuario() { }

    public Usuario(
        string nombreUsuario,
        string correoElectronico,
        string hashContrasena,
        string saltContrasena)
    {
        Id = Guid.NewGuid();
        NombreUsuario = nombreUsuario;
        CorreoElectronico = correoElectronico;
        HashContrasena = hashContrasena;
        SaltContrasena = saltContrasena;

        EstaActivo = true;
        MfaHabilitado = false;
        FechaCreacion = DateTime.UtcNow;
    }

    public void AgregarRol(UsuarioRol usuarioRol)
    {
        _roles.Add(usuarioRol);
    }

    public void RegistrarInicioSesion()
    {
        UltimoInicioSesion = DateTime.UtcNow;
    }

    public void Desactivar() => EstaActivo = false;
    public void Activar() => EstaActivo = true;

    public void HabilitarMfa() => MfaHabilitado = true;
}