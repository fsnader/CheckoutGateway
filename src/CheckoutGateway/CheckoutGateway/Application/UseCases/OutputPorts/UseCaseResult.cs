namespace CheckoutGateway.Application.UseCases.OutputPorts;

public class UseCaseResult<T>
{
    private UseCaseResult(ErrorType errorType, string message)
    {
        Error = errorType;
        ErrorMessage = message;
    }

    private UseCaseResult(T result)
    {
        Result = result;
        Error = null;
        ErrorMessage = null;
    }
    
    public T? Result { get; }
    public ErrorType? Error { get; }
    public string? ErrorMessage { get; }

    public static UseCaseResult<T> Success(T result) => new UseCaseResult<T>(result);

    public static UseCaseResult<T> NotFound(string errorMessage = "Entity not found") =>
        new UseCaseResult<T>(ErrorType.NotFound, errorMessage);
    
    public static UseCaseResult<T> BadRequest(string errorMessage = "Please provide all required fields") =>
        new UseCaseResult<T>(ErrorType.BadRequest, errorMessage);

    public static UseCaseResult<T> Unauthorized(string errorMessage = "Unauthorized") =>
        new UseCaseResult<T>(ErrorType.Unauthorized, errorMessage);
}