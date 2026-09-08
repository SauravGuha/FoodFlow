


namespace FoodFlow.Domain.Models.InventoryModels;

public class BranchInventory : BaseModel
{
    public BranchInventory(Guid itemId, Guid branchId, decimal price)
    {
        this.ItemId = itemId;
        this.BranchId = branchId;
        this.Price = price;
    }

    public byte[] RowVersion { get; private set; } = [];

    public Guid ItemId { get; private set; }

    public Guid BranchId { get; private set; }

    public int Quantity { get; private set; } = 0;

    public decimal Price { get; private set; } = 0;

    public void SetPrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be less than 0");
        this.Price = price;
    }

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