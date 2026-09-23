using FoodFlow.Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using FoodFlow.Domain.Models.OrderModels;

namespace FoodFlow.Persistence.Repository;

public class OrderItemRepo : BaseRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepo(FoodFlowContext context) : base(context)
    {
    }
}