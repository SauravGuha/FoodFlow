
using FoodFlow.Domain.Models.InventoryModels;

namespace FoodFlow.Application.DTOModels;

public class ItemBranchInventory
{
    public Guid ItemId { get; set; }
    public Guid InventoryId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public decimal Quantity { get; set; }

    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public FoodCategory Category { get; set; }
    public Guid CuisineId { get; set; }
    public string CuisineName { get; set; }
    public Guid RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string BranchName { get; set; }
}