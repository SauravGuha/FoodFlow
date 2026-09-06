
namespace FoodFlow.Application.DTOModels;

public class ItemBranchInventory
{
    public Guid ItemId { get; set; }
    public Guid InventoryId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public decimal Quantity { get; set; }
}