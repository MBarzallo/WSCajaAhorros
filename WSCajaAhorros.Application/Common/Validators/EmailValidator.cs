using System.Text.RegularExpressions;

namespace WSCajaAhorros.Application.Common.Validators;

public class EmailValidator
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public static bool EsValido(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return EmailRegex.IsMatch(email);
    }
}