using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Domain.Security;

namespace WSCajaAhorros.Application.Interfaces.Services.Security;

public interface IRolService
{
    Task<Response<List<Rol>>> ObtenerTodos();

    Task<Response> Agregar(CrearRolRequest request);
    Task<Response> AgregarRolPermiso(CrearRolPermisoRequest request);
}