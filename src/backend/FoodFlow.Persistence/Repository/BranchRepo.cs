
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Persistence.Repository;

public class BranchRepo : BaseRepository<Branch>, IBranchRepository
{
    public BranchRepo(FoodFlowContext context) : base(context)
    {
    }
}
