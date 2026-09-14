
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.InventoryModels;


namespace FoodFlow.Application.Common.Repositories;

public interface IItemRepository : IBaseRepository<Item>
{
    public Task<IEnumerable<RestaurantListDto>> GetRestaurants(CancellationToken cancellationToken);
}