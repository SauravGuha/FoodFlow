using FoodFlow.Application.Commands.RestaurantCommands.UpdateRestaurantStatus;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using MediatR;

namespace FoodFlow.Application.Commands.RestaurantCommands.UpdateRestaurantStatus;

public class UpdateRestaurantStatusCommandHandler : IRequestHandler<UpdateRestaurantStatusCommand, Result<Guid>>
{
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public UpdateRestaurantStatusCommandHandler(IRestaurantRepository restaurantRepository,
    IFoodFlowContext foodFlowContext)
    {
        this.restaurantRepository = restaurantRepository;
        this.foodFlowContext = foodFlowContext;
    }
    public async Task<Result<Guid>> Handle(UpdateRestaurantStatusCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await this.restaurantRepository.GetByIdAsync(request.Id, cancellationToken);
        if (restaurant == null)
        {
            return Result<Guid>.SetError($"Restaurant not found with ID {request.Id}", 404);
        }

        restaurant.UpdateStatus(request.Status);
        await foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SetSuccess(request.Id, null, 201);
    }
}