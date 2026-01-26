namespace WSCajaAhorros.Application.DTOs.Socios;

public class CrearSocioRequest
{
    public int TipoPersonaId { get; set; } = 1;
    
    public string IdentificacionNumero { get; set; } = string.Empty;
    public int TipoIdentificacionId { get; set; }
    
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }
    public DateOnly? FechaNacimiento { get; set; }

    public string? RazonSocial { get; set; }
    public string? NombreComercial { get; set; }
    public DateOnly? FechaConstitucion { get; set; }
    
    public List<CrearTelefonoDto> Telefonos { get; set; } = new();
    public List<CrearCorreoDto> Correos { get; set; } = new();
    public List<CrearDireccionDto> Direcciones { get; set; } = new();
}

public class CrearTelefonoDto
{
    public string Numero { get; set; } = string.Empty;
    public string Etiqueta { get; set; }
    public bool EsPrincipal { get; set; }
}

public class CrearCorreoDto
{
    public string Email { get; set; } = string.Empty;
    public string Etiqueta { get; set; } = string.Empty;
    public bool EsPrincipal { get; set; }
}

public class CrearDireccionDto
{
    public bool EsPrincipal { get; set; }
    public string CallePrincipal { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public string Pais  { get; set; } = string.Empty;
    public string CalleSecundaria { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;
    public string Etiqueta { get; set; } = string.Empty;
}