namespace WSCajaAhorros.Application.Common.Validators;

public static class IdentificationValidator
{
    public static bool EsValida(string numero, int tipo)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return false;

        numero = numero.Trim();

        return tipo switch
        {
            1 => ValidarCedula(numero),
            2 => ValidarRuc(numero),
            3 => ValidarPasaporte(numero),
            _ => false
        };
    }

    private static bool ValidarCedula(string cedula)
    {
        if (cedula.Length != 10 || !cedula.All(char.IsDigit))
            return false;

        int provincia = int.Parse(cedula.Substring(0, 2));
        if (provincia < 1 || provincia > 24)
            return false;

        int tercerDigito = cedula[2] - '0';
        if (tercerDigito >= 6)
            return false;

        int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        int suma = 0;

        for (int i = 0; i < coeficientes.Length; i++)
        {
            int valor = (cedula[i] - '0') * coeficientes[i];
            suma += valor >= 10 ? valor - 9 : valor;
        }

        int digitoVerificador = (10 - (suma % 10)) % 10;
        return digitoVerificador == (cedula[9] - '0');
    }

    private static bool ValidarRuc(string ruc)
    {
        if (ruc.Length != 13 || !ruc.All(char.IsDigit))
            return false;

        if (!ValidarCedula(ruc.Substring(0, 10)))
            return false;

        return ruc.Substring(10, 3) != "000";
    }

    private static bool ValidarPasaporte(string pasaporte)
    {
        return pasaporte.Length >= 5 &&
               pasaporte.Length <= 20 &&
               pasaporte.All(char.IsLetterOrDigit);
    }
}