using Xunit;
using Moq;
using WSCajaAhorros.Application.Common.Security;
using WSCajaAhorros.Application.Services.Security;
using WSCajaAhorros.Application.DTOs.Security;
using WSCajaAhorros.Application.Interfaces.Security;
using WSCajaAhorros.Domain.Security;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
    private readonly Mock<JwtService> _jwtServiceMock;
    private readonly UsuarioService _service;

    public UsuarioServiceTests()
    {
        _usuarioRepoMock = new Mock<IUsuarioRepository>();
        _jwtServiceMock = new Mock<JwtService>();

        _service = new UsuarioService(
            _usuarioRepoMock.Object,
            _jwtServiceMock.Object
        );
    }

    [Fact]
    public async Task Login_Falla_Cuando_Usuario_No_Existe()
    {
        _usuarioRepoMock
            .Setup(r => r.ObtenerPorNombreUsuario(It.IsAny<string>()))
            .ReturnsAsync((Usuario)null);

        var request = new LoginRequest
        {
            NombreUsuario = "admin",
            Contrasena = "1234"
        };

        var result = await _service.Login(request);

        Assert.False(result.Ok);
    }

    [Fact]
    public async Task Login_Exitoso_Cuando_Credenciales_Son_Validas()
    {
        var usuario = new Usuario("admin", "admin@coop.com", "hash", "salt");

        _usuarioRepoMock
            .Setup(r => r.ObtenerPorNombreUsuario("admin"))
            .ReturnsAsync(usuario);

        _jwtServiceMock
            .Setup(j => j.GenerarToken(It.IsAny<Usuario>(), It.IsAny<IEnumerable<string>>()))
            .Returns("fake-jwt");

        var request = new LoginRequest
        {
            NombreUsuario = "admin",
            Contrasena = "1234"
        };

        var result = await _service.Login(request);

        Assert.True(result.Ok);
        Assert.NotNull(result.Data);
    }
}