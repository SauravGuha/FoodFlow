using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using MediatR;

namespace FoodFlow.Application.Commands.BranchCommands.UpdateBranchStatus;

public class UpdateBranchStatusCommandHandler : IRequestHandler<UpdateBranchStatusCommand, Result<Guid>>
{
    private readonly IBranchRepository branchRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public UpdateBranchStatusCommandHandler(IBranchRepository branchRepository, IFoodFlowContext foodFlowContext)
    {
        this.branchRepository = branchRepository;
        this.foodFlowContext = foodFlowContext;
    }

    public async Task<Result<Guid>> Handle(UpdateBranchStatusCommand request, CancellationToken cancellationToken)
    {
        var branch = await branchRepository.GetByIdAsync(request.Id, cancellationToken);
        if (branch == null)
        {
            return Result<Guid>.SetError($"Branch not found with ID {request.Id}", 404);
        }

        branch.UpdateStatus(request.Status);
        await foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SetSuccess(request.Id, null, 201);
    }
}
