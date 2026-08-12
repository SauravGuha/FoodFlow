
using FoodFlow.Api.Controller;
using FoodFlow.Application.Commands.BranchCommands.CreateBranch;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.Api.Controllers;

[Route("api/[controller]")]
public class BranchController : AppController
{
    /// <summary>
    /// Creates a new branch for a restaurant.
    /// </summary>
    /// <param name="command">The command containing branch details.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the created branch ID in the response.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchCommand command, CancellationToken cancellationToken)
    {
        var operationResult = await Mediator.Send(command, cancellationToken);
        if (operationResult.Status)
            return CreatedAtAction(nameof(CreateBranch), new { id = operationResult.Data }, null);
        else
            return ReturnResult(operationResult);
    }
}