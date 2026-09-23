
using FoodFlow.Api.Controller;
using FoodFlow.Application.Commands.PaymentCommands.PaymentVerfication;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controllers;

public class PaymentController : AppController
{
    [HttpPost("/verify")]
    public async Task<IActionResult> Verify([FromBody] PaymentVerificationCommand paymentVerification)
    {
        var result = await this.Mediator.Send(paymentVerification);
        return this.ReturnResult(result);
    }
}