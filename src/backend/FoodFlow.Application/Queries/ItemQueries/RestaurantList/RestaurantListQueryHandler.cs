

using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using MediatR;

namespace FoodFlow.Application.Queries.ItemQueries.RestaurantList;

public class RestaurantListQueryHandler : IRequestHandler<RestaurantListQuery, Result<IEnumerable<RestaurantListDto>>>
{
    private readonly IItemRepository itemRepository;

    public RestaurantListQueryHandler(IItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }
    public async Task<Result<IEnumerable<RestaurantListDto>>> Handle(RestaurantListQuery request, CancellationToken cancellationToken)
    {
        var result = await itemRepository.GetRestaurants(cancellationToken);
        return Result<IEnumerable<RestaurantListDto>>.SetSuccess(result, null);
    }
}