namespace API.Errors;

public class ApiResponse
{
    public int StatusCode { get; init; }

    public string Message { get; init; }

    public ApiResponse(
        int    statusCode,
        string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    private string GetDefaultMessageForStatusCode(
        int statusCode)
    {
        return statusCode switch
            {
                400 => "Bad request",
                401 => "Unauthorized",
                404 => "Nada",
                500 => "Errors",
                _   => null,
            };
    }
}
