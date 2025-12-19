namespace WSCajaAhorros.Application.Common;

public class Response<T>
{
    public bool Ok { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;

    public static Response<T> Success(T? data, string message = "Operación exitosa")
        => new()
        {
            Ok = true,
            Data = data,
            Message = message
        };

    public static Response<T> Fail(string message)
        => new()
        {
            Ok = false,
            Data = default,
            Message = message
        };
}

public class Response
{
    public bool Ok { get; set; }
    public string Message { get; set; } = string.Empty;

    public static Response Success(string message = "Operación exitosa")
        => new()
        {
            Ok = true,
            Message = message
        };

    public static Response Fail(string message)
        => new()
        {
            Ok = false,
            Message = message
        };
}