
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Application.Common.Repositories;

public interface IRestaurantRepository : IBaseRepository<Restaurant>
{
    Task<Restaurant?> GetByGstNumberAsync(string gstNumber, CancellationToken cancellationToken = default);
}