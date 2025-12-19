namespace WSCajaAhorros.Domain.Security;

public class RolPermiso
{
    public Guid RolId { get; private set; }
    public Rol Rol { get; private set; } = null!;

    public Guid PermisoId { get; private set; }
    public Permiso Permiso { get; private set; } = null!;

    protected RolPermiso() { }

    public RolPermiso(Rol rol, Permiso permiso)
    {
        Rol = rol;
        RolId = rol.Id;

        Permiso = permiso;
        PermisoId = permiso.Id;
    }
}