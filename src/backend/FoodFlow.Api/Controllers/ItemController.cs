
using FoodFlow.Application.Commands.AddStock;
using FoodFlow.Application.Commands.InventoryCommands.AddBranchInventory;
using FoodFlow.Application.Commands.ItemCommands.CreateItem;
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

    /// <summary>
    /// Retrieves a filtered list of items based on the provided request.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Creates an inventory item for a branch.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost(template: "iteminventory")]
    public async Task<IActionResult> CreateItemInventory([FromBody] AddBranchInventoryCommand command, CancellationToken token)
    {
        var operationResult = await Mediator.Send(command, token);
        if (operationResult.Status)
            return CreatedAtAction(nameof(GetItembyId), new { id = command.ItemId }, null);
        else
            return ReturnResult(operationResult);
    }

    /// <summary>
    /// Updates the stock of a branch for an item.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut(template: "iteminventory")]
    public async Task<IActionResult> AddBranchStock([FromBody] UpdateStockCommand command, CancellationToken token)
    {
        var operationResult = await Mediator.Send(command, token);
        if (operationResult.Status)
            return CreatedAtAction(nameof(GetItembyId), new { id = command.ItemId }, null);
        else
            return ReturnResult(operationResult);
    }

    /// <summary>
    /// Removes the stock of a branch for an item.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut(template: "removeitembranchstock")]
    public async Task<IActionResult> RemoveBranchStock([FromBody] UpdateStockCommand command, CancellationToken token)
    {
        command.Quantity = -command.Quantity;
        var operationResult = await Mediator.Send(command, token);
        if (operationResult.Status)
            return CreatedAtAction(nameof(GetItembyId), new { id = command.ItemId }, null);
        else
            return ReturnResult(operationResult);
    }

}