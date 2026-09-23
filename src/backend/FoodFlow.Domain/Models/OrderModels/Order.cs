using FoodFlow.Domain.Models.CustomerModels;
using FoodFlow.Domain.Models.RestaurantModels;

namespace FoodFlow.Domain.Models.OrderModels;

public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}

public class Order : BaseModel
{
    public Order() { }
    public Order(Guid customerId, decimal shippingCost, Guid branchId,
    Address billingAddress, Address deliveryAddress, OrderStatus status = OrderStatus.Pending)
    {
        CustomerId = customerId;
        ShippingCost = shippingCost;
        BranchId = branchId;
        BillingAddress = billingAddress;
        DeliveryAddress = deliveryAddress;
        Status = status;
    }
    public Guid CustomerId { get; private set; }

    public virtual Customer Customer { get; private set; } = null!;

    public decimal ShippingCost { get; private set; }
    public Guid BranchId { get; private set; }

    public virtual Branch Branch { get; private set; } = null!;

    public OrderStatus Status { get; private set; }
    public Address BillingAddress { get; private set; }

    public Address DeliveryAddress { get; private set; }

    private List<OrderItem> _orderItems = new List<OrderItem>();

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }

    public void RemoveOrderItem(OrderItem orderItem)
    {
        _orderItems.Remove(orderItem);
    }

    public decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var item in OrderItems)
        {
            totalPrice += item.CalculateTotal();
        }
        return totalPrice + ShippingCost;
    }

}