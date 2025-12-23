using WSCajaAhorros.Application.DTOs.Socios;
using WSCajaAhorros.Domain.Socios;

namespace WSCajaAhorros.Application.Common.Validators;

public static class SocioValidator
{
    public static void ValidarDatosPorTipo(CrearSocioRequest request)
    {
        if ((TipoPersona)request.TipoPersonaId == TipoPersona.Natural)
        {
            if (string.IsNullOrWhiteSpace(request.Nombres))
                throw new ArgumentException("Los nombres son obligatorios para persona natural.");

            if (string.IsNullOrWhiteSpace(request.Apellidos))
                throw new ArgumentException("Los apellidos son obligatorios para persona natural.");

            if (!request.FechaNacimiento.HasValue)
                throw new ArgumentException("La fecha de nacimiento es obligatoria.");
        }

        if ((TipoPersona)request.TipoPersonaId == TipoPersona.Juridica)
        {
            if (string.IsNullOrWhiteSpace(request.RazonSocial))
                throw new ArgumentException("La razón social es obligatoria para persona jurídica.");
        }
    }

    public static void ValidarPrincipal<T>(
        IEnumerable<T> items,
        Func<T, bool> esPrincipal,
        string mensajeError)
    {
        if (items.Count(esPrincipal) > 1)
            throw new ArgumentException(mensajeError);
    }
}