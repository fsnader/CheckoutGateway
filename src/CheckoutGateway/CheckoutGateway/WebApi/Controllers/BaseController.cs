using CheckoutGateway.Application.UseCases.OutputPorts;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.WebApi.Controllers;

public class BaseController : ControllerBase
{
    private IActionResult ErrorResult<T>(UseCaseResult<T> result) =>
        result.Error switch
        {
            ErrorType.NotFound => NotFound(ErrorObject(result.ErrorMessage)),
            ErrorType.BadRequest => BadRequest(ErrorObject(result.ErrorMessage)),
            ErrorType.Rejected => StatusCode(StatusCodes.Status406NotAcceptable, ErrorObject(result.ErrorMessage)),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };

    private static object ErrorObject(string? message) => new { Message = message };
    
    protected IActionResult UseCaseActionResult<T, TOutput>(UseCaseResult<T> result,
        Func<T, TOutput> converter) =>
        result.Result is null 
            ? ErrorResult(result) 
            : Ok(converter(result.Result));
}