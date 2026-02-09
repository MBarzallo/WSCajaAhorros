namespace WSCajaAhorros.Application.DTOs.Socios;

public class ActualizarSocioRequest
{
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }

    public string? RazonSocial { get; set; }
    public string? NombreComercial { get; set; }
}