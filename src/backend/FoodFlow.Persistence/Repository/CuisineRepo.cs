
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Persistence.Repository;

public class CuisineRepo : BaseRepository<Cuisine>, ICuisineRepository
{
    public CuisineRepo(FoodFlowContext context) : base(context)
    {
    }
}
