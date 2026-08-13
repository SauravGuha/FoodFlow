
using FoodFlow.Application.Commands.BranchCommands.CreateBranch;

namespace FoodFlow.Application.Commands.BranchCommands.UpdateBranch;

public class UpdateBranchCommand : CreateBranchCommand
{
    public Guid Id { get; set; }
}