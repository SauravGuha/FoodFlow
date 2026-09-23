
using AutoMapper;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Commands.BranchCommands.UpdateBranch;

// UpdateBranchCommandHandler implements the action for updating branch details.
// This handler processes updates to name, contact info, and operating hours.
public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, Result<Guid>>
{
    private readonly IBranchRepository branchRepository;
    private readonly IFoodFlowContext foodFlowContext;
    private readonly IMapper mapper;

    public UpdateBranchCommandHandler(IBranchRepository branchRepository, IFoodFlowContext foodFlowContext,
    IMapper mapper)
    {
        this.branchRepository = branchRepository;
        this.foodFlowContext = foodFlowContext;
        this.mapper = mapper;
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
        var t = mapper.Map<OperatingHours>(request.OperatingHours).Schedule!;
        branch.UpdateOperatingHours(t);

        await this.branchRepository.UpdateAsync(branch, cancellationToken);
        await this.foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SetSuccess(branch.Id, null, 201);
    }
}