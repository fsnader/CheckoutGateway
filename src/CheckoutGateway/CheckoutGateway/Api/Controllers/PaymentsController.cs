using CheckoutGateway.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.Api.Controllers;

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
    ///     POST /api/payments
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isComplete": true
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