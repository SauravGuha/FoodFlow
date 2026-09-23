
namespace FoodFlow.Application.DTOModels;

public class OrderItemDto
{
    public Guid BranchInventoryId { get; set; }
    public int Quantity { get; set; }

    public string ItemName { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public string? TaxCode { get; set; }

    public decimal DiscountPercent { get; set; }

    public decimal TaxPercent { get; set; }
}