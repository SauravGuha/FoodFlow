
using FoodFlow.Application.Commands.ItemCommands;
using FoodFlow.Application.Queries.ItemQueries;
using FoodFlow.Application.Queries.ItemQueries.FilteredItem;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controller;

[Route("api/[controller]")]
public class ItemController : AppController
{
    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="command">The command containing item details.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the created item ID in the response.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
    {
        var operationResult = await Mediator.Send(command, cancellationToken);
        if (operationResult.Status)
            return CreatedAtAction(nameof(GetItembyId), new { id = operationResult.Data }, null);
        else
            return ReturnResult(operationResult);
    }

    /// <summary>
    /// Retrieves an item by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the item.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the item details if found, otherwise returns a 404 Not Found.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItembyId(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetItemByIdQuery { Id = id }, cancellationToken);
        return ReturnResult(result);
    }


    [HttpGet(template: "filtered")]
    public async Task<IActionResult> GetFilteredItem([FromBody] FilteredItemRequest? request, CancellationToken token)
    {
        var result = await Mediator.Send(request ?? new FilteredItemRequest());
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
}