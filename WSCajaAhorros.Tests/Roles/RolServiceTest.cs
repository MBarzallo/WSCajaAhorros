using Moq;
using WSCajaAhorros.Application.Services.Security;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Security;

public class RolServiceTests
{
    private readonly Mock<IRolRepository> _rolRepoMock;
    private readonly RolService _service;

    public RolServiceTests()
    {
        _rolRepoMock = new Mock<IRolRepository>();
        _service = new RolService(_rolRepoMock.Object);
    }

    [Fact]
    public async Task CrearRol_Falla_Si_Codigo_Esta_Vacio()
    {
        var request = new CrearRolRequest
        {
            Codigo = "",
            Descripcion = "Administrador"
        };

        var result = await _service.Agregar(request);

        Assert.False(result.Ok);
    }

    [Fact]
    public async Task CrearRol_Exitoso_Cuando_Datos_Son_Correctos()
    {
        var request = new CrearRolRequest
        {
            Codigo = "ADMIN",
            Descripcion = "Administrador del sistema"
        };

        var result = await _service.Agregar(request);

        Assert.True(result.Ok);
    }
}