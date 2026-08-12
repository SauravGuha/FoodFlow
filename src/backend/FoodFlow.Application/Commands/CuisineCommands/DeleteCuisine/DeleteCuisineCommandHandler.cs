
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;
using MediatR;

namespace FoodFlow.Application.Commands.CuisineCommands.DeleteCuisine;

public class DeleteCuisineCommandHandler : IRequestHandler<DeleteCuisineCommand, Result<bool>>
{
    private readonly ICuisineRepository cuisineRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public DeleteCuisineCommandHandler(ICuisineRepository cuisineRepository, IFoodFlowContext foodFlowContext)
    {
        this.cuisineRepository = cuisineRepository;
        this.foodFlowContext = foodFlowContext;
    }

    public async Task<Result<bool>> Handle(DeleteCuisineCommand request, CancellationToken cancellationToken)
    {
        var cuisine = await this.cuisineRepository.GetByIdAsync(request.CuisineId, cancellationToken);
        if (cuisine == null)
        {
            return Result<bool>.SetError($"Cuisine with id {request.CuisineId} not found.", 404);
        }

        if (cuisine.RestaurantId != request.RestaurantId)
        {
            return Result<bool>.SetError("Cuisine does not belong to the specified restaurant.", 400);
        }

        await this.cuisineRepository.DeleteAsync(cuisine, cancellationToken);
        await this.foodFlowContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.SetSuccess(true, null, 204);
    }
}
