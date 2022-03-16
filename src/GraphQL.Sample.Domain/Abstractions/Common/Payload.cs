namespace GraphQL.Sample.Domain.Abstractions.Common;

public abstract class Payload
{
    protected Payload(IReadOnlyList<ApiError>? errors = null)
    {
        Errors = errors;
    }

    public IReadOnlyList<ApiError> Errors { get; set; }
}

public class ApiError
{
    public ApiError(string message, string code)
    {
        Message = message;
        Code = code;
    }

    public string Message { get; }
    public string Code { get; }
}