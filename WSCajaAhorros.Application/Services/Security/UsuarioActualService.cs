using WSCajaAhorros.Application.Interfaces.Services.Security;
using Microsoft.AspNetCore.Http;

namespace WSCajaAhorros.Application.Services.Security;

public class UsuarioActualService : IUsuarioActualService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioActualService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid ObtenerUsuarioId()
    {
        var claim = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst("usuarioId");

        if (claim == null)
            throw new UnauthorizedAccessException("Usuario no autenticado");

        return Guid.Parse(claim.Value);
    }
}