using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.InventoryModels;

namespace FoodFlow.Application.Common.Repositories;

public interface IBranchInventoryRepository : IBaseRepository<BranchInventory>
{
    public Task<IEnumerable<ItemBranchInventory>> GetItemBranchInventory(Guid branchId);
}