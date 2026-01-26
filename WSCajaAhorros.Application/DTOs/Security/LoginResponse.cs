namespace WSCajaAhorros.Application.DTOs.Security;

public class LoginResponse
{
    public string AccessToken { get; set; } = null;
    public int ExpiresIn { get; set; }
    public UsuarioSesion Usuario { get; set; } = null!;
}

public class UsuarioSesion
{
    public Guid Id { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public List<string> Roles { get; set; } = new();

}