
using FoodFlow.Application.Common;
using FoodFlow.Domain.Models.InventoryModels;
using FoodFlow.Domain.Models.RestaurantModels;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Persistence;

public class FoodFlowContext : DbContext, IFoodFlowContext
{
    public FoodFlowContext(DbContextOptions<FoodFlowContext> options) : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; } = null!;

    public DbSet<Cuisine> Cuisines { get; set; } = null!;

    public DbSet<Branch> Branches { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodFlowContext).Assembly);
    }
}