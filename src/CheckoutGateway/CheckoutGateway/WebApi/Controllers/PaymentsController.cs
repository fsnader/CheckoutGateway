using CheckoutGateway.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.WebApi.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    /// <summary>
    /// Returns a Payment by UUID
    /// </summary>
    /// <param name="id">Payment UUID</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    public IActionResult GetPaymentByIdAsync(Guid id)
    {
        return Ok("TODO");
    }
    
    /// <summary>
    /// Returns a list of merchant filtered payments
    /// </summary>
    /// <response code="200">List of merchant's requests</response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status200OK)]
    public IActionResult ListPaymentsAsync()
    {
        return Ok("TODO");
    }
    
    
    /// <summary>
    /// Generates and processes a payment
    /// </summary>
    /// <param name="payment"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/payments=
    ///     {
    ///         "amount": 550,
    ///         "currency": "USD",
    ///         "card": {
    ///             "name": "Felipe Nader",
    ///             "number": "4141414141414141",
    ///             "scheme": "VISA",
    ///             "expirationMonth": 11,
    ///             "expirationYear": 2025,
    ///             "cvv": 999
    ///         }
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Processed payment response</response>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
    public IActionResult CreatePaymentAsync([FromBody]CreatePaymentDto payment)
    {
        return Ok("TODO");
    }
}