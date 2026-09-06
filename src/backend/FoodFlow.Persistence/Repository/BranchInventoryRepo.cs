using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.InventoryModels;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Persistence.Repository;

public class BranchInventoryRepo : BaseRepository<BranchInventory>, IBranchInventoryRepository
{
    public BranchInventoryRepo(FoodFlowContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ItemBranchInventory>> GetItemBranchInventory(Guid branchId)
    {
        var itemTable = this._context.Set<Item>();
        var result = await this._dbSet.Join(itemTable, bi => bi.ItemId, it => it.Id, (bi, it) => new ItemBranchInventory
        {
            BranchId = bi.BranchId,
            ItemId = bi.ItemId,
            InventoryId = bi.Id,
            Quantity = bi.Quantity,
            ItemName = it.Name
        }).ToListAsync();

        return result!;
    }
}