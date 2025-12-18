namespace WSCajaAhorros.Domain.Security;

public class Permiso
{
    public Guid Id { get; private set; }
    public string Codigo { get; private set; } = null!;
    public string Descripcion { get; private set; } = null!;

    private readonly List<RolPermiso> _roles = new();
    public IReadOnlyCollection<RolPermiso> Roles => _roles.AsReadOnly();

    protected Permiso() { }

    public Permiso(string codigo, string descripcion)
    {
        Id = Guid.NewGuid();
        Codigo = codigo;
        Descripcion = descripcion;
    }
}