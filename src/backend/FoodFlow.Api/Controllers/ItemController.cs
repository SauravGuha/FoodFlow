using FoodFlow.Application.Commands.ItemCommands;
using FoodFlow.Application.Queries.ItemQueries;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controller;

[Route("api/[controller]")]
public class ItemController : AppController
{

    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
    {
        var operationResult = await Mediator.Send(command, cancellationToken);
        if (operationResult.Status)
            return CreatedAtAction(nameof(GetItembyId), new { id = operationResult.Data }, null);
        else
            return ReturnResult(operationResult);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItembyId(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetItemByIdQuery { Id = id }, cancellationToken);
        return ReturnResult(result);
    }
}