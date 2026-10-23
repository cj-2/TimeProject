namespace TimeProject.Domain.Errors;

public enum ErrorTypes
{
    NotFound,
    BadRequest,
    Unauthorized,
    ServerError,
    Forbidden
}

public static class ErrorTypesFormat
{
    public static string Message(this ErrorTypes type, string message)
    {
        var value = type switch
        {
            ErrorTypes.NotFound => "not_found",
            ErrorTypes.BadRequest => "bad_request",
            ErrorTypes.Unauthorized => "unauthorized",
            ErrorTypes.ServerError => "server_erro",
            ErrorTypes.Forbidden => "forbidden",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        return value + ":" + message;
    }
}