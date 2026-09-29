
using FoodFlow.MCP.Api;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace FoodFlow.MCP;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        // builder.Services.AddOpenApi();

        builder.Services.AddSingleton<FoodFlowApiOpenApiData>();
        builder.Services.AddMcpServer()
        .WithHttpTransport(options =>
            {
                // Stateless mode is recommended for servers that don't need
                // server-to-client requests like sampling or elicitation.
                // See the Sessions documentation for details.
                options.Stateless = true;
            })
            .WithTools(GetTools());

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
            app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapMcp("/mcp");

        app.MapGet("/openapi", async (FoodFlowApiOpenApiData openApiData) =>
        {
            await openApiData.ReadOpenApiData();
            return Results.Ok();
        });

        app.Run();
    }

    private static IEnumerable<McpServerTool> GetTools()
    {
        yield return McpServerTool.Create(
            AIFunctionFactory.Create(
                async (string id) =>
                {
                    using var client = new HttpClient();

                    var response = await client.GetAsync(
                        $"http://localhost:5243/api/Restaurant/{id}");

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                },
                new AIFunctionFactoryOptions
                {
                    Name = "GetRestaurantById",
                    Description = "Get a restaurant by its ID.",

                }));
    }
}
