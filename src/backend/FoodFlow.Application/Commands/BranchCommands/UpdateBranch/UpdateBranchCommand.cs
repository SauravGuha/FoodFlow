
using FoodFlow.Application.Commands.BranchCommands.CreateBranch;

namespace FoodFlow.Application.Commands.BranchCommands.UpdateBranch;

// UpdateBranchCommand represents the action to update a branch's details.
// This command handles updating name, contact information, and operating hours.
public class UpdateBranchCommand : CreateBranchCommand
{
    public Guid Id { get; set; }
}
