using System.Security.Cryptography;

namespace WSCajaAhorros.Application.Common.Security;

public class PasswordHasher
{
    private const int SaltSize = 16;        
    private const int KeySize = 32;         
    private const int Iterations = 100_000; 

    public static (string Hash, string Salt) Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("La contraseña no puede ser vacía");

        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256
        );

        var hashBytes = pbkdf2.GetBytes(KeySize);

        return (
            Convert.ToBase64String(hashBytes),
            Convert.ToBase64String(saltBytes)
        );
    }

    public static bool Verify(
        string password,
        string storedHash,
        string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var hashBytes = Convert.FromBase64String(storedHash);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256
        );

        var computedHash = pbkdf2.GetBytes(KeySize);

        return CryptographicOperations.FixedTimeEquals(
            computedHash,
            hashBytes
        );
    }
}