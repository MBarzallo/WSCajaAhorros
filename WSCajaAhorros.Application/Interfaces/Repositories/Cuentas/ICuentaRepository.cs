using WSCajaAhorros.Domain.Cuentas;

namespace WSCajaAhorros.Application.Interfaces.Services.Cuentas;

public interface ICuentaRepository
{
    Task<Cuenta?> ObtenerPorIdAsync(Guid cuentaId);
    Task<Cuenta?> ObtenerPorNumeroAsync(string numeroCuenta);

    Task<List<Cuenta>> ObtenerPorSocioAsync(Guid socioId);

    Task<bool> ExisteNumeroCuentaAsync(string numeroCuenta);

    Task AgregarAsync(Cuenta cuenta);

    Task ActualizarAsync(Cuenta cuenta);

    Task SaveChangesAsync();
}