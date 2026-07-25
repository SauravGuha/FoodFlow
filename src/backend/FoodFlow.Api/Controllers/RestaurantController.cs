
using FoodFlow.Application.Queries.RestaurantQueries;
using FoolFlow.Application.Commands.RestaurantCommands.CreateRestaurant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IMediator mediator;

    public RestaurantController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command, CancellationToken cancellationToken)
    {
        var restaurantId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurantId }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById(Guid id, CancellationToken cancellationToken)
    {
        var restaurantInfo = await mediator.Send(new RestaurantRequest { Id = id }, cancellationToken);
        return Ok(restaurantInfo);
    }
}