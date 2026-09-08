
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.InventoryModels;
using MediatR;

namespace FoodFlow.Application.Commands.ItemCommands.CreateItem;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<Guid>>
{
    private readonly IItemRepository itemRepository;
    private readonly ICuisineRepository cuisineRepository;
    private readonly IFoodFlowContext foodFlowContext;

    public CreateItemCommandHandler(IItemRepository itemRepository, ICuisineRepository cuisineRepository, IFoodFlowContext foodFlowContext)
    {
        this.itemRepository = itemRepository;
        this.cuisineRepository = cuisineRepository;
        this.foodFlowContext = foodFlowContext;
    }
    public async Task<Result<Guid>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        //sku for a restaurant cannot be duplicate
        var itemCount = await this.itemRepository.GetQueryCount(e => e.RestaurantId == request.RestaurantId &&
        e.Sku == request.Sku, e => e.CreatedAt, cancellationToken);
        if (itemCount > 0)
            return Result<Guid>.SetError("Cannot have same sku for the restaurant", 409);
        else
        {
            //Cuisine should be for the request RestaurantId else send error
            var cuisine = await cuisineRepository.GetByIdAsync(request.CuisineId);
            if (cuisine == null || cuisine.RestaurantId != request.RestaurantId)
                return Result<Guid>.SetError("Invalid or invalid cuisine", 400);
            var item = new Item(request.Name, request.Description, request.Sku, request.RestaurantId, request.CuisineId);
            await this.itemRepository.AddAsync(item, cancellationToken);
            await this.foodFlowContext.SaveChangesAsync(cancellationToken);
            return Result<Guid>.SetSuccess(item.Id, 201);
        }
    }
}