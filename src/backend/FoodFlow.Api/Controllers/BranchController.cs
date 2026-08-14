
using FoodFlow.Api.Controller;
using FoodFlow.Application.Commands.BranchCommands.CreateBranch;
using FoodFlow.Application.Commands.BranchCommands.UpdateBranch;
using FoodFlow.Application.Commands.BranchCommands.UpdateBranchStatus;
using FoodFlow.Application.Queries.BranchQueries;
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
            return CreatedAtAction(nameof(GetBranchById), new { id = operationResult.Data }, null);
        else
            return ReturnResult(operationResult);
    }

    /// <summary>
    /// Retrieves a branch by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the branch.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the branch details if found, otherwise returns a 404 Not Found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranchById(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new BranchRequest { Id = id }, cancellationToken);
        return ReturnResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchCommand updateBranchCommand, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(updateBranchCommand, cancellationToken);
        return ReturnResult(result);
    }

    /// <summary>
    /// Updates the status of an existing branch.
    /// </summary>
    /// <param name="updateRequest">The command containing branch ID and new status.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>Returns the updated branch ID in the response.</returns>
    [HttpPatch]
    public async Task<IActionResult> UpdateBranchStatus([FromBody] UpdateBranchStatusCommand updateRequest, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(updateRequest, cancellationToken);
        return ReturnResult(result);
    }
}