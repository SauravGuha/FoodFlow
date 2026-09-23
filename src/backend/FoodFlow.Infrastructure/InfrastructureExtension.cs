

using FoodFlow.Application.Services;
using FoodFlow.Infrastructure.Payment;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Infrastructure;

public static class InfrastructureExtension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
        serviceCollection.AddScoped<IPaymentFactory, RazorpayPaymentFactory>();
        return serviceCollection;
    }
}