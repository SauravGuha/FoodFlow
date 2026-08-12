
using MediatR;
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Application.Commands.CuisineCommands.CreateCuisine;

public class CreateCuisineCommandHandler : IRequestHandler<CreateCuisineCommand, Result<Guid>>
{
    private readonly ICuisineRepository cuisineRepository;
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public CreateCuisineCommandHandler(ICuisineRepository cuisineRepository, IRestaurantRepository restaurantRepository, IFoodFlowContext foodFlowContext)
    {
        this.cuisineRepository = cuisineRepository;
        this.restaurantRepository = restaurantRepository;
        this.foodFlowContext = foodFlowContext;
    }

    public async Task<Result<Guid>> Handle(CreateCuisineCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await this.restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken);
        if (restaurant == null)
        {
            return Result<Guid>.SetError($"Restaurant with id {request.RestaurantId} not found.", 404);
        }

        var cuisine = new Cuisine(request.Name, request.RestaurantId);

        await this.cuisineRepository.AddAsync(cuisine, cancellationToken);
        await this.foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<Guid>.SetSuccess(cuisine.Id, null, 201);
    }
}
