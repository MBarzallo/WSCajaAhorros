namespace WSCajaAhorros.Domain.Security;

public class UsuarioRol
{
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public Guid RolId { get; private set; }
    public Rol Rol { get; private set; } = null!;

    protected UsuarioRol() { }

    public UsuarioRol(Usuario usuario, Rol rol)
    {
        Usuario = usuario;
        UsuarioId = usuario.Id;

        Rol = rol;
        RolId = rol.Id;
    }
}