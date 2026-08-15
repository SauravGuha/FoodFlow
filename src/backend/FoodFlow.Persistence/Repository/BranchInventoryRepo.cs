using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models.InventoryModels;

namespace FoodFlow.Persistence.Repository;
public class BranchInventoryRepo : BaseRepository<BranchInventory>, IBranchInventoryRepository
{
    public BranchInventoryRepo(FoodFlowContext context) : base(context)
    {
    }
}