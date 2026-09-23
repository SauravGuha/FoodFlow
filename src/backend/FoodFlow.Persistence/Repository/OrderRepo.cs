using FoodFlow.Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using FoodFlow.Domain.Models.OrderModels;

namespace FoodFlow.Persistence.Repository;

public class OrderRepo : BaseRepository<Order>, IOrderRepository
{
    public OrderRepo(FoodFlowContext context) : base(context)
    {
    }
}