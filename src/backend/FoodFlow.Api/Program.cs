
namespace FoodFlow.Api;

using System.Text.Json.Serialization;
using FoodFlow.Api.Middlewares;
using FoodFlow.Application;
using FoodFlow.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        builder.Services.AddCors(corsOption =>
        {
            corsOption.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //In order for js to access Location header.
                    .WithExposedHeaders("Location");
            });
        });
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "http://localhost:10001/realms/foodflowlocal";
            options.RequireHttpsMetadata = !(builder.Configuration.GetSection("ASPNETCORE_ENVIRONMENT")?.Value == "Development");
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:10001/realms/foodflowlocal",
                ValidateAudience = true,
                ValidAudience = "foodflow-api",
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        app.Services.MigrateDatabase();

        app.Run();
    }
}
