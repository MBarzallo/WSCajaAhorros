namespace WSCajaAhorros.Application.Common.Validators;

public static class UsernameValidator
{
    public static bool EsValido(string username)
    {
        if (string.IsNullOrEmpty(username))
            return false;
        
        return username.Length >= 3 && username.Length <= 16;
    }
}