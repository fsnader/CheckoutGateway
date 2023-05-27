using CheckoutGateway.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutGateway.Api.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetPaymentByIdAsync(string id)
    {
        return Ok("TODO");
    }
    
    [HttpGet]
    public IActionResult ListPaymentsAsync()
    {
        return Ok("TODO");
    }
    
    /// <summary>
    /// Generates a payment in the gateway
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
    /// <response code="201">Returns the processed payment request</response>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
    public IActionResult CreatePaymentAsync([FromBody]CreatePaymentDto payment)
    {
        return Ok("TODO");
    }
}