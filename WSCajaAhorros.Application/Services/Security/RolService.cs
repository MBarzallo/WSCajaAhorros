using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Services.Security;

public class RolService : IRolService
{
    private readonly IRolRepository _rolRepository;
    
    public RolService(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }

    public async Task<Response<List<Rol>>> ObtenerTodos()
    {
        var roles = await _rolRepository.ObtenerTodos();
        return Response<List<Rol>>.Success(roles);
    }

    public async Task<Response> Agregar(CrearRolRequest request)
    {
        if (string.IsNullOrEmpty(request.Codigo))
            return Response.Fail("Necesita agregar un codigo al rol");

        if (string.IsNullOrEmpty(request.Descripcion))
            return Response.Fail("Necesita agregar un descripcion al rol");

        try
        {
            Rol rol = new Rol(request.Codigo, request.Descripcion);
            await _rolRepository.Agregar(rol);
            return Response.Success("Se agrego el rol correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
}