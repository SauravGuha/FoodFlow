using FoodFlow.Api.Controller;
using FoodFlow.Application.Commands.RestaurantCommands.UpdateRestaurant;
using FoodFlow.Application.Queries.RestaurantQueries;
using FoolFlow.Application.Commands.RestaurantCommands.CreateRestaurant;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controllers;

[Route("api/[controller]")]
public class RestaurantController : AppController
{
    /// <summary>
    /// Creates a new restaurant.
    /// </summary>
    /// <param name="command">The command containing restaurant details.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the created restaurant ID in the response.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command, CancellationToken cancellationToken)
    {
        var operationResult = await this.Mediator.Send(command, cancellationToken);
        if (operationResult.Status)
            return CreatedAtAction(nameof(GetRestaurantById), new { id = operationResult.Data }, null);
        else
            return this.ReturnResult(operationResult);
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
        var restaurantInfo = await Mediator.Send(new RestaurantRequest { Id = id }, cancellationToken);
        return this.ReturnResult(restaurantInfo);
    }

    /// <summary>
    /// Retrieves a filtered list of restaurants based on query parameters.
    /// </summary>
    /// <param name="request">The filtered restaurant request containing query parameters.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the filtered list of restaurants or a 404 Not Found if no results are found.</returns>
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredRestaurants([FromQuery] FilteredRestaurantRequest? request)
    {
        var result = await Mediator.Send(request ?? new FilteredRestaurantRequest());
        if (result == null)
        {
            return NotFound();
        }
        // Add custom headers to the response
        if (result.Value != null)
        {
            var extraHeader = result.Value as Dictionary<string, object>;
            if (extraHeader != null)
            {
                foreach (var h in extraHeader)
                {
                    this.Response.Headers[h.Key] = h.Value.ToString();
                }
            }
        }

        return this.ReturnResult(result);
    }

    /// <summary>
    /// Updates an existing restaurant.
    /// </summary>
    /// <param name="command">The command containing updated restaurant details.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the updated restaurant ID in the response.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateRestaurant([FromBody] UpdateRestaurantCommand command, CancellationToken cancellationToken)
    {
        var operationResult = await this.Mediator.Send(command, cancellationToken);
        return this.ReturnResult(operationResult);
    }
}
