
using AutoMapper;
using FoodFlow.Application.Commands.RestaurantCommands.UpdateRestaurant;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Handlers.RestaurantCommands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, Result<Guid>>
{
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IFoodFlowContext foodFlowContext;
    private readonly IMapper mapper;

    public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IFoodFlowContext foodFlowContext, IMapper mapper)
    {
        this.restaurantRepository = restaurantRepository;
        this.foodFlowContext = foodFlowContext;
        this.mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await this.restaurantRepository.GetByIdAsync(request.Id, cancellationToken);
        if (restaurant == null)
        {
            return Result<Guid>.SetError($"Restaurant not found with ID {request.Id}", 404);
        }
        restaurant.UpdateDescription(request.Description);
        restaurant.UpdateFNumber(request.FNumber);
        restaurant.UpdateGstNumber(request.Gst);
        restaurant.UpdateName(request.Name);
        restaurant.UpdateRestaurantOwner(mapper.Map<RestaurantOwner>(request.RestaurantOwner));

        await this.foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SetSuccess(restaurant.Id, null, 201);
    }
}