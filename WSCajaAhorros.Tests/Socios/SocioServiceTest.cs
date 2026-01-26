using Xunit;
using Moq;
using WSCajaAhorros.Application.Services.Socios;
using WSCajaAhorros.Application.DTOs.Socios;
using WSCajaAhorros.Application.Interfaces.Repositories.Socios;

public class SocioServiceTests
{
    private readonly Mock<ISocioRepository> _socioRepoMock;
    private readonly SociosService _service;

    public SocioServiceTests()
    {
        _socioRepoMock = new Mock<ISocioRepository>();
        _service = new SociosService(_socioRepoMock.Object);
    }

    [Fact]
    public async Task CrearSocio_Falla_Si_Identificacion_No_Es_Valida()
    {
        var request = new CrearSocioRequest
        {
            IdentificacionNumero = "123",
            TipoIdentificacionId = 1
        };

        var result = await _service.Crear(request);

        Assert.False(result.Ok);
    }

    [Fact]
    public async Task CrearSocio_Exitoso_Cuando_Datos_Son_Correctos()
    {
        var request = new CrearSocioRequest
        {
            TipoPersonaId = 1,
            IdentificacionNumero = "0102030405",
            TipoIdentificacionId = 1,
            Nombres = "Juan",
            Apellidos = "Perez"
        };

        var result = await _service.Crear(request);

        Assert.True(result.Ok);
    }
}