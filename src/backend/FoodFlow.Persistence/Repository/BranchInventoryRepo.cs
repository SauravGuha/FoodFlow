using FoodFlow.Application.Common.Repositories;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.InventoryModels;
using FoodFlow.Domain.Models.RestaurantModels;
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
        var restaurant = this._context.Set<Restaurant>();
        var cuisine = this._context.Set<Cuisine>();
        var branch = this._context.Set<Branch>();

        var result = this._dbSet.Where(e => e.BranchId == branchId)
        .Join(itemTable, bi => bi.ItemId, it => it.Id, (bi, it) => new { bi, it })
        .Join(branch, biit => biit.bi.BranchId, b => b.Id, (biit, b) => new { biit.bi, biit.it, b })
        .Join(restaurant, biitb => biitb.it.RestaurantId, r => r.Id, (biitb, r) => new { biitb.bi, biitb.it, biitb.b, r })
        .Join(cuisine, t => t.it.CuisineId, c => c.Id, (t, c) => new { t.bi, t.it, t.b, t.r, c })
        .Select(finalresult => new ItemBranchInventory
        {
            BranchName = finalresult.b.Name,
            BranchId = finalresult.bi.BranchId,
            Category = finalresult.it.Category,
            CuisineId = finalresult.it.CuisineId,
            CuisineName = finalresult.c.Name,
            Description = finalresult.it.Description,
            InventoryId = finalresult.bi.Id,
            ItemId = finalresult.bi.ItemId,
            ItemName = finalresult.it.Name,
            Price = finalresult.bi.Price,
            Quantity = finalresult.bi.Quantity,
            RestaurantId = finalresult.it.RestaurantId,
            RestaurantName = finalresult.r.Name,
            Sku = finalresult.it.Sku
        });

        return result!;
    }

    public async Task<IEnumerable<ItemBranchInventory>> GetItemBranchInventory(Guid branchId, int quantity)
    {
        var itemTable = this._context.Set<Item>();
        var result = await this._dbSet
        .Join(itemTable, bi => bi.ItemId, it => it.Id, (bi, it) => new ItemBranchInventory
        {
            BranchId = bi.BranchId,
            ItemId = bi.ItemId,
            InventoryId = bi.Id,
            Quantity = bi.Quantity,
            ItemName = it.Name,
            Price = bi.Price
        })
        .Where(bi => bi.Quantity < quantity && bi.BranchId == branchId)
        .ToListAsync();

        return result!;
    }
}