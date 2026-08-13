using FoodFlow.Application.Common;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Commands.BranchCommands.UpdateBranchStatus;

public class UpdateBranchStatusCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public BranchStatus Status { get; set; }
}
