using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.Common.Security;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;

namespace WSCajaAhorros.Application.Services.Security;

public class AutenticacionService : IAutenticacionService
{
    
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly JwtService _jwtService;
    
    public AutenticacionService(IUsuarioRepository usuarioRepository,  JwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<Response<LoginResponse>> Login(LoginRequest request)
    {
        try
        {
            var usuario = await _usuarioRepository.ObtenerPorNombreUsuario(request.NombreUsuario);
            if (usuario == null)
                return Response<LoginResponse>.Fail("Usuario o contraseña incorrectos");

            if (!PasswordHasher.Verify(request.Contrasena, usuario.HashContrasena, usuario.SaltContrasena))
                return Response<LoginResponse>.Fail("Usuario o contraseña incorrectos");

            var jwt = _jwtService.GenerarToken(usuario, usuario.Roles.Select(r => r.Rol.Codigo));
            
            usuario.RegistrarInicioSesion();

            await _usuarioRepository.SaveChangesAsync();

            var loginResponse = new LoginResponse()
            {
                AccessToken = jwt,
                ExpiresIn = 15 * 60,
                Usuario = new UsuarioSesion()
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.NombreUsuario,
                    Roles = usuario.Roles.Select(r => r.Rol.Codigo).ToList(),
                }
            };
            
            return Response<LoginResponse>.Success(loginResponse);
        }
        catch (Exception ex)
        {
            return Response<LoginResponse>.Fail(ex.Message);
        }
    }

    public async Task<Response> Activar(Guid usuarioId)
    {
        try
        {
            var usuario = await  _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null)
                return Response.Fail("El  usuario no existe");
            
            
            usuario.Activar();
            
            await _usuarioRepository.SaveChangesAsync();
            
            return Response.Success();
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
    
    public async Task<Response> Desactivar(Guid usuarioId)
    {
        try
        {
            var usuario = await  _usuarioRepository.ObtenerPorId(usuarioId);
            if (usuario == null)
                return Response.Fail("El  usuario no existe");
            
            
            usuario.Desactivar();
            
            await _usuarioRepository.SaveChangesAsync();
            
            return Response.Success();
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
}