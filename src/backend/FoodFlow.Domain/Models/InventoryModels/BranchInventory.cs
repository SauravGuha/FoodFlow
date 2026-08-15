
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Domain.Models.InventoryModels;

public class BranchInventory : BaseModel
{
    public BranchInventory(Guid itemId, Guid branchId)
    {
        this.ItemId = itemId;
        this.BranchId = branchId;
    }

    public byte[] RowVersion { get; private set; } = [];

    public Guid ItemId { get; private set; }

    public Guid BranchId { get; private set; }

    public int Quantity { get; private set; } = 0;

    public void AddQuantity(int value)
    {
        if (value < 1)
            throw new ArgumentException($"Qunatity to add cannot be less than 1");
        this.Quantity += value;
    }

    public void RemoveQuantity(int value)
    {
        if (Quantity >= value)
        {
            this.Quantity -= value;
        }
        else
        {
            throw new ArgumentException($"Cannot order {value}");
        }
    }
}