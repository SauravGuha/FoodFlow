

using FoodFlow.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Infrastructure;

public static class InfrastructureExtension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
        return serviceCollection;
    }
}