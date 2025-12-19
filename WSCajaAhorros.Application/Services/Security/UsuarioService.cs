using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.Common.Security;
using WSCajaAhorros.Application.Common.Validators;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Services.Security;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    
    public  UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Response<List<Usuario>>> ObtenerTodos()
    {
        var usuarios = await _usuarioRepository.ObtenerTodos();
        return Response<List<Usuario>>.Success(usuarios);
    }

    public async Task<Response> CrearUsuario(CrearUsuarioRequest request)
    {
        try
        {
            if (!EmailValidator.EsValido(request.CorreoElectronico))
                return Response.Fail("El correo electronico no es valido");

            if(!UsernameValidator.EsValido(request.NombreUsuario))
                return Response.Fail("El nombre usuario no es valido");
            
            var claveTemporal = PasswordGenerator.GenerarTemporal();
            
            var (hash, salt) = PasswordHasher.Hash(claveTemporal);
            
            var usuario = new Usuario(request.NombreUsuario, request.CorreoElectronico, hash, salt);
            
            await _usuarioRepository.Crear(usuario);
            
            return Response.Success($"Se ha creado el usuario correctamente, la contraseña temporal se envio al correo {request.CorreoElectronico}");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
    
    

}