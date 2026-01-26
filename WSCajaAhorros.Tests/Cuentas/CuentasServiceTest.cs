using Xunit;
using Moq;
using WSCajaAhorros.Application.Interfaces.Services.Cuentas;
using WSCajaAhorros.Application.Services.Cuentas;
using WSCajaAhorros.Domain.Common;

public class CuentaServiceTests
{
    private readonly Mock<ICuentaRepository> _cuentaRepoMock;
    private readonly CuentaService _service;

    public CuentaServiceTests()
    {
        _cuentaRepoMock = new Mock<ICuentaRepository>();
        _service = new CuentaService(_cuentaRepoMock.Object);
    }

    [Fact]
    public async Task Deposito_Falla_Si_Monto_Es_Cero()
    {
        var result = await _service.Depositar(Guid.NewGuid(), new Dinero(0));

        Assert.False(result.Ok);
    }

    [Fact]
    public async Task Deposito_Exitoso_Cuando_Monto_Es_Valido()
    {
        var result = await _service.Depositar(Guid.NewGuid(), new Dinero(100));

        Assert.True(result.Ok);
    }
}