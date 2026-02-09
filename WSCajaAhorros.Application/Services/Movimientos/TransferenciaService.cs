using WSCajaAhorros.Application.Common;
using WSCajaAhorros.Application.DTOs.Movimientos;
using WSCajaAhorros.Application.Interfaces.Repositories.Movimientos;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Application.Interfaces.Services.Movimientos;
using WSCajaAhorros.Application.Interfaces.Services.Security;
using WSCajaAhorros.Domain.Common;
using WSCajaAhorros.Domain.Cuentas;
using WSCajaAhorros.Domain.Movimientos;

namespace WSCajaAhorros.Application.Services.Movimientos;

public class TransferenciaService : ITransferenciaService
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMovimientoRepository _movimientoRepository;
    private readonly ITransferenciaRepository _transferenciaRepository;
    private readonly IUsuarioActualService _usuarioActual;
    

    public TransferenciaService(
        ICuentaRepository cuentaRepository,
        IMovimientoRepository movimientoRepository,
        ITransferenciaRepository transferenciaRepository,
        IUsuarioActualService usuarioActual)
    {
        _cuentaRepository = cuentaRepository;
        _movimientoRepository = movimientoRepository;
        _transferenciaRepository = transferenciaRepository;
        _usuarioActual = usuarioActual;
    }

    public async Task<Response> Transferir(CrearTransferenciaRequest request)
    {
        if (request.Monto <= 0)
            return Response.Fail("El monto debe ser mayor a cero");

        if (request.CuentaOrigenId == request.CuentaDestinoId)
            return Response.Fail("La cuenta origen y destino no pueden ser la misma");

        try
        {
            Guid usuarioId = _usuarioActual.ObtenerUsuarioId();
            await _cuentaRepository.EjecutarTransaccionAsync(async () =>
            {
                var cuentaOrigen = await _cuentaRepository.ObtenerPorIdAsync(request.CuentaOrigenId);
                if (cuentaOrigen == null)
                    throw new Exception("La cuenta origen no existe");

                var cuentaDestino = await _cuentaRepository.ObtenerPorIdAsync(request.CuentaDestinoId);
                if (cuentaDestino == null)
                    throw new Exception("La cuenta destino no existe");

                if (cuentaOrigen.Estado != EstadoCuenta.Activa)
                    throw new Exception("La cuenta origen no está activa");

                if (cuentaDestino.Estado != EstadoCuenta.Activa)
                    throw new Exception("La cuenta destino no está activa");

                var monto = new Dinero(request.Monto);

                // Debitar y acreditar
                cuentaOrigen.Debitar(monto);
                cuentaDestino.Acreditar(monto);

                // Crear transferencia
                var transferencia = new Transferencia(
                    cuentaOrigen.Id,
                    cuentaDestino.Id,
                    new Dinero(request.Monto),
                    usuarioId,
                    "VENTANILLA",
                    request.Observacion
                );

                await _transferenciaRepository.Agregar(transferencia);

                var montoSalida = new Dinero(request.Monto);
                var montoEntrada = new Dinero(request.Monto);

                // Movimiento salida
                await _movimientoRepository.Agregar(
                    Movimiento.Crear(
                        cuentaOrigen.Id,
                        TipoMovimiento.TransferenciaSalida,
                        new Dinero(request.Monto),
                        usuarioId,
                        "VENTANILLA",
                        $"Transferencia a cuenta {cuentaDestino.NumeroCuenta}",
                        transferencia.Id
                    )
                );

// Movimiento entrada
                await _movimientoRepository.Agregar(
                    Movimiento.Crear(
                        cuentaDestino.Id,
                        TipoMovimiento.TransferenciaEntrada,
                        new Dinero(request.Monto),
                        usuarioId,
                        "VENTANILLA",
                        $"Transferencia desde cuenta {cuentaOrigen.NumeroCuenta}",
                        transferencia.Id
                    )
                );

            });

            return Response.Success("Transferencia realizada correctamente");
        }
        catch (Exception ex)
        {
            return Response.Fail(ex.Message);
        }
    }
}