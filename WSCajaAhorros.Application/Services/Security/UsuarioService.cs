using WSCajaAhorros.Application.Common;
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
    
    

}