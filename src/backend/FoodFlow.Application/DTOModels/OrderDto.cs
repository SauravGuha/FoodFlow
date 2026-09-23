
using FoodFlow.Domain.Models.OrderModels;

namespace FoodFlow.Application.DTOModels;

public class OrderDto
{

    public CustomerDto Customer { get; set; } = null!;

    public decimal ShippingCost { get; set; }
    public Guid BranchId { get; set; }

    public BranchDto Branch { get; set; } = null!;

    public OrderStatus Status { get; set; }
    public AddressDto BillingAddress { get; set; }

    public AddressDto DeliveryAddress { get; set; }

    public List<OrderItemDto> OrderItemDtos { get; set; } = new List<OrderItemDto>();
}