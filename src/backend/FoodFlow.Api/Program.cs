
namespace FoodFlow.Api;

using System.Text.Json.Serialization;
using FoodFlow.Api.Middlewares;
using FoodFlow.Application;
using FoodFlow.Persistence;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddProblemDetails();

        builder.Services.AddScoped<ExceptionMiddleware>();
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        app.Services.MigrateDatabase();

        app.Run();
    }
}
