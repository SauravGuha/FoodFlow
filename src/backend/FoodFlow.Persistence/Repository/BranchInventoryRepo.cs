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
        var result = restaurant.Join(branch, r => r.Id, b => b.RestaurantId, (r, b) => new { r, b })
        .Where(rb => rb.b.Id == branchId)
        .Join(itemTable, rb => rb.r.Id, i => i.RestaurantId, (rb, i) => new { rb.r, rb.b, i })
        .Join(this._dbSet, rbi => rbi.i.Id, bi => bi.ItemId, (rbi, bi) => new { rbi.r, rbi.b, rbi.i, bi })
        .Join(cuisine, rbii => rbii.i.CuisineId, c => c.Id, (rbbii, c) => new { rbbii.r, rbbii.b, rbbii.i, rbbii.bi, c })
        .Select(finalResult => new ItemBranchInventory
        {
            ItemId = finalResult.i.Id,
            BranchId = finalResult.b.Id,
            BranchName = finalResult.b.Name,
            InventoryId = finalResult.bi.Id,
            ItemName = finalResult.i.Name,
            Description = finalResult.i.Description,
            Sku = finalResult.i.Sku,
            Category = finalResult.i.Category,
            Price = finalResult.bi.Price,
            Quantity = finalResult.bi.Quantity,
            CuisineId = finalResult.c.Id,
            CuisineName = finalResult.c.Name,
            RestaurantId = finalResult.r.Id,
            RestaurantName = finalResult.r.Name
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