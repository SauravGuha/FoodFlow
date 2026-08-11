using FoodFlow.Application.DTOModels;
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

    /// <summary>
    /// Creates a new restaurant.
    /// </summary>
    /// <param name="command">The command containing restaurant details.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the created restaurant ID in the response.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command, CancellationToken cancellationToken)
    {
        var restaurantId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurantId }, null);
    }

    /// <summary>
    /// Retrieves a restaurant by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the restaurant.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the restaurant details if found, otherwise returns a 404 Not Found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById(Guid id, CancellationToken cancellationToken)
    {
        var restaurantInfo = await mediator.Send(new RestaurantRequest { Id = id }, cancellationToken);
        return Ok(restaurantInfo);
    }

    /// <summary>
    /// Retrieves a filtered list of restaurants based on query parameters.
    /// </summary>
    /// <param name="request">The filtered restaurant request containing query parameters.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the filtered list of restaurants or a 404 Not Found if no results are found.</returns>
    [HttpGet("filtered")]
    public async Task<ActionResult<List<RestaurantDto>>> GetFilteredRestaurants([FromQuery] FilteredRestaurantRequest? request)
    {
        var result = await mediator.Send(request ?? new FilteredRestaurantRequest());
        if (result == null)
        {
            return NotFound();
        }
        // Add custom headers to the response
        this.Response.Headers.Append("F-Total-Count", result.Values!.ToString());

        return Ok(result.Data);
    }
}