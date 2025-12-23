using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Application.Interfaces.Services.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Services.Security;

public class RolService : IRolService
{
    private readonly IRolRepository _rolRepository;
    private readonly IPermisoRepository _permisoRepository;
    
    public RolService(IRolRepository rolRepository,  IPermisoRepository permisoRepository)
    {
        _rolRepository = rolRepository;
        _permisoRepository = permisoRepository;
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

    public async Task<Response> AgregarRolPermiso(CrearRolPermisoRequest request)
    {
        try
        {
            Rol? rol = await _rolRepository.ObtenerPorId(request.RolId);
            if (rol == null)
                return Response.Fail("No existe un rol con ese codigo");

            Permiso? permiso = await _permisoRepository.ObtenerPorId(request.PermisoId);
            if (permiso == null)
                return Response.Fail("No existe un permiso con ese rol");

            RolPermiso rolPermiso = new RolPermiso(rol, permiso);

            await _rolRepository.AgregarRolPermiso(rolPermiso);
            return Response.Success("Se agrego el permiso al rol");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
}