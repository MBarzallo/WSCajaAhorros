using System.Security.Cryptography;
using System.Text;

namespace WSCajaAhorros.Application.Common.Security;

public class PasswordGenerator
{
    private const string Mayusculas = "ABCDEFGHJKLMNPQRSTUVWXYZ";
    private const string Minusculas = "abcdefghijkmnopqrstuvwxyz";
    private const string Numeros = "23456789";
    private const string Simbolos = "@#$%&*!";

    private static readonly string Todos =
        Mayusculas + Minusculas + Numeros + Simbolos;

    public static string GenerarTemporal(int longitud = 8)
    {
        if (longitud < 8)
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres");

        var password = new StringBuilder();

        password.Append(GetRandomChar(Mayusculas));
        password.Append(GetRandomChar(Minusculas));
        password.Append(GetRandomChar(Numeros));
        password.Append(GetRandomChar(Simbolos));

        for (int i = password.Length; i < longitud; i++)
            password.Append(GetRandomChar(Todos));

        return Mezclar(password.ToString());
    }

    private static char GetRandomChar(string source)
    {
        var index = RandomNumberGenerator.GetInt32(source.Length);
        return source[index];
    }

    private static string Mezclar(string input)
    {
        var chars = input.ToCharArray();

        for (int i = chars.Length - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (chars[i], chars[j]) = (chars[j], chars[i]);
        }

        return new string(chars);
    }
}