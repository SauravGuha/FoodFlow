
using FoodFlow.Api.Controller;
using FoodFlow.Application.Commands.OrderCommands;
using FoodFlow.Application.Queries.OrderQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controllers;

[Authorize]
public class OrderController : AppController
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
    {
        var result = await this.Mediator.Send(createOrderCommand, cancellationToken);
        if (result.Status)
            return CreatedAtAction(nameof(GetOrderDetails), new { id = result.Data }, null);
        else
            return this.ReturnResult(result);
    }

    [HttpGet(template: "{id}")]
    public async Task<IActionResult> GetOrderDetails(Guid id, CancellationToken cancellationToken)
    {
        var restaurantInfo = await Mediator.Send(new OrderRequest { Id = id }, cancellationToken);
        return this.ReturnResult(restaurantInfo);
    }
}