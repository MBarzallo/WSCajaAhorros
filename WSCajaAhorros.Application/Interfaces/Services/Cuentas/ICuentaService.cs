using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Cuentas;

namespace WSCajaAhorros.Application.Interfaces.Services.Cuentas;

public interface ICuentaService
{
    Task<Response<List<CuentaResponse>>> ObtenerPorSocio(Guid socioId);
    Task<Response> Crear(CrearCuentaRequest request);
    Task<Response> Bloquear(Guid cuentaId);
    Task<Response> Cerrar(Guid cuentaId);
}