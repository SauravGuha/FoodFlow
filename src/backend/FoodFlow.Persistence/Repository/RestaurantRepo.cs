
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Persistence.Repository;

public class RestaurantRepo : BaseRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepo(FoodFlowContext context) : base(context)
    {
    }

    public Task<Restaurant?> GetByGstNumberAsync(string gstNumber, CancellationToken cancellationToken = default)
    {
        return this._dbSet.AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);
    }
}