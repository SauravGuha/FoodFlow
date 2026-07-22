
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
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {
        var restaurantId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurantId }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById(Guid id)
    {
        // Implementation for retrieving a restaurant by ID would go here.
        return Ok();
    }
}