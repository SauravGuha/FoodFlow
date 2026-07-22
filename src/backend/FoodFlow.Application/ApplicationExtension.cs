
using FluentValidation;
using FoodFlow.Application.MediatRPipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Application;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationExtension).Assembly));
        services.AddValidatorsFromAssembly(typeof(ApplicationExtension).Assembly);
        services.AddAutoMapper(typeof(ApplicationExtension).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipeline<,>));
        return services;
    }
}