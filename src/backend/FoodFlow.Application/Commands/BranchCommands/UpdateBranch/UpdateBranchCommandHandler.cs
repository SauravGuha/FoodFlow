
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Commands.BranchCommands.UpdateBranch;

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, Result<Guid>>
{
    private readonly IBranchRepository branchRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public UpdateBranchCommandHandler(IBranchRepository branchRepository, IFoodFlowContext foodFlowContext)
    {
        this.branchRepository = branchRepository;
        this.foodFlowContext = foodFlowContext;
    }
    public async Task<Result<Guid>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await this.branchRepository.GetByIdAsync(request.Id, cancellationToken);
        if (branch == null)
        {
            return Result<Guid>.SetError($"Branch with id {request.Id} not found.", 404);
        }
        branch.UpdateName(request.Name);
        branch.UpdatePhoneNumber(request.PhoneNumber);
        branch.UpdateEmail(request.Email);
        var address = new Address(request.Street, request.City, request.State, request.ZipCode, request.Country);
        branch.UpdateAddress(address);
        branch.UpdateOperatingHours(request.OperatingHours);

        await this.branchRepository.UpdateAsync(branch, cancellationToken);
        await this.foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SetSuccess(branch.Id, null, 201);
    }
}