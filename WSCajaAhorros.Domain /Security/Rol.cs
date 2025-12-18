namespace WSCajaAhorros.Domain.Security;

public class Rol
{
    public Guid Id { get; private set; }
    public string Codigo { get; private set; } = null!;
    public string Descripcion { get; private set; } = null!;

    private readonly List<UsuarioRol> _usuarios = new();
    public IReadOnlyCollection<UsuarioRol> Usuarios => _usuarios.AsReadOnly();

    private readonly List<RolPermiso> _permisos = new();
    public IReadOnlyCollection<RolPermiso> Permisos => _permisos.AsReadOnly();

    protected Rol() { }

    public Rol(string codigo, string descripcion)
    {
        Id = Guid.NewGuid();
        Codigo = codigo;
        Descripcion = descripcion;
    }
}