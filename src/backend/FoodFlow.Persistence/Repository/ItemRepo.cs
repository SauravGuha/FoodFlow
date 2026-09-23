using FoodFlow.Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using FoodFlow.Domain.Models.InventoryModels;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Persistence.Repository;

public class ItemRepo : BaseRepository<Item>, IItemRepository
{
    public ItemRepo(FoodFlowContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RestaurantListDto>> GetRestaurants(CancellationToken cancellationToken)
    {
        var restaurantSet = this._context.Set<Restaurant>();
        return await restaurantSet.Select(e => new RestaurantListDto
        {
            Id = e.Id,
            Name = e.Name
        }).ToListAsync(cancellationToken);
    }
}