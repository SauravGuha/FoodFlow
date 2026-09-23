
namespace FoodFlow.Domain.Models.OrderModels;

public class OrderItem : BaseModel
{
    public Guid OrderId { get; private set; }
    public Guid BranchInventoryId { get; private set; }
    public int Quantity { get; private set; }

    public string ItemName { get; private set; } = null!;

    public string Sku { get; private set; } = null!;

    public decimal UnitPrice { get; private set; }

    public string? TaxCode { get; private set; }

    public decimal DiscountPercent { get; private set; }

    public decimal TaxPercent { get; private set; }

    public OrderItem() { }

    public OrderItem(Guid branchInventoryId, int quantity, string itemName, decimal unitPrice, string sku,
        decimal discountPercent = 0, decimal taxPercent = 0, string? taxCode = null)
    {
        BranchInventoryId = branchInventoryId;
        Quantity = quantity;
        ItemName = itemName;
        UnitPrice = unitPrice;
        Sku = sku;
        TaxCode = taxCode;
        DiscountPercent = discountPercent;
        TaxPercent = taxPercent;

        if (discountPercent > 100)
            throw new ArgumentException("Discount percent cannot be greater than 100.");

        if (taxPercent > 100)
            throw new ArgumentException("Tax percent cannot be greater than 100.");

        if (discountPercent < 0 || taxPercent < 0)
            throw new ArgumentException("Discount and tax percentages cannot be negative.");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (sku == null || itemName == null)
            throw new ArgumentException("Item name and SKU cannot be null.");

    }

    public decimal CalculateTotal()
    {
        decimal total = UnitPrice * Quantity;
        total -= total * DiscountPercent / 100;
        total += total * TaxPercent / 100;

        return Math.Round(total, 2);
    }
}