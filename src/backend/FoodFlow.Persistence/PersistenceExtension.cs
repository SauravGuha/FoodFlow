
using FoodFlow.Application.Common;
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Persistence;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FoodFlowContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRestaurantRepository, RestaurantRepo>();
        services.AddScoped<ICuisineRepository, CuisineRepo>();
        services.AddScoped<IBranchRepository, BranchRepo>();
        services.AddScoped<IFoodFlowContext>(sp => sp.GetRequiredService<FoodFlowContext>());

        return services;
    }

    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FoodFlowContext>();
        context.Database.Migrate();
    }
}