using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Services.Security;

public class PermisoService: IPermisoService
{
    private readonly IPermisoRepository _permisoRepository;

    public PermisoService(IPermisoRepository permisoRepository)
    {
        _permisoRepository = permisoRepository;
    }

    public async Task<Response<List<Permiso>>> ObtenerTodos()
    {
        var permisos = await _permisoRepository.ObtenerTodos();
        return Response<List<Permiso>>.Success(permisos);
    }
}