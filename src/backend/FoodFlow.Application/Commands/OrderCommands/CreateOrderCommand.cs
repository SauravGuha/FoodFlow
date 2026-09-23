
using FoodFlow.Application.Common;
using FoodFlow.Application.DTOModels;
using FoodFlow.Domain.Models.OrderModels;
using MediatR;

namespace FoodFlow.Application.Commands.OrderCommands;

public class CreateOrderCommand : IRequest<Result<Guid>>
{
    public OrderStatus Status { get; private set; }
    public AddressDto DeliveryAddress { get; set; } = new();

    public AddressDto BillingAddress { get; set; } = new();
    public decimal ShippingCost { get; set; }
    public Guid BranchId { get; set; }
    public required List<OrderItemDto> OrderItems { get; set; }
}