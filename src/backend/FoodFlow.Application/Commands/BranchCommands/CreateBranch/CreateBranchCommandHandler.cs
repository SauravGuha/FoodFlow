using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Commands.BranchCommands.CreateBranch;

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, Result<Guid>>
{
    private readonly IBranchRepository branchRepository;
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public CreateBranchCommandHandler(IBranchRepository branchRepository, IRestaurantRepository restaurantRepository, IFoodFlowContext foodFlowContext)
    {
        this.branchRepository = branchRepository;
        this.restaurantRepository = restaurantRepository;
        this.foodFlowContext = foodFlowContext;
    }

    public async Task<Result<Guid>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await this.restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken);
        if (restaurant == null)
        {
            return Result<Guid>.SetError($"Restaurant with id {request.RestaurantId} not found.", 404);
        }

        var address = new Address(request.Street, request.City, request.State, request.ZipCode, request.Country);
        var operatingHours = new OperatingHours(request.OperatingHours);

        var branch = new Branch(request.RestaurantId, request.Name, address, request.PhoneNumber, request.Email, operatingHours);

        await this.branchRepository.AddAsync(branch, cancellationToken);
        await this.foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SetSuccess(branch.Id, null, 201);
    }
}
