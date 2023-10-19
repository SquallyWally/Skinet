namespace API.Errors;

public class ApiException : ApiResponse
{
    public string Details { get; init; }

    public ApiException(
        int    statusCode,
        string message = null,
        string details = null)
        : base(
            statusCode,
            message)
    {
        Details = details;
    }
}
