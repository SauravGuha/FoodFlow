using FoodFlow.Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using FoodFlow.Domain.Models.InventoryModels;

namespace FoodFlow.Persistence.Repository;

public class ItemRepo : BaseRepository<Item>, IItemRepository
{
    public ItemRepo(FoodFlowContext context) : base(context)
    {
    }
}