
using System.Text.Json;
using FoodFlow.MCP.Api;
using FoodFlow.MCP.Common;
using ModelContextProtocol.Protocol;
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
            .WithTools(GetTools2());

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

    private static IEnumerable<McpServerTool> GetTools2()
    {
        yield return GetRestaurantByIdTool();
        yield return CreateRestaurantTool();
    }

    private static McpServerTool GetRestaurantByIdTool()
    {
        var tool = new Tool
        {
            Name = "GetRestaurantById",
            Description = "Get a restaurant by its ID.",
            InputSchema = JsonSerializer.SerializeToElement(new
            {
                type = "object",

                properties = new
                {
                    id = new
                    {
                        type = "string",
                        format = "uuid",
                        description = "The restaurant ID."
                    }
                },

                required = new[]
            {
                                        "id"
                                    }
            })
        };
        var handler = async (
                    IDictionary<string, JsonElement>? arguments,
                    CancellationToken cancellationToken) =>
                {
                    var id = arguments?["id"].ToString();

                    using var client = new HttpClient();

                    var response = await client.GetAsync(
                        $"http://localhost:5243/api/Restaurant/{id}",
                        cancellationToken);

                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync(
                        cancellationToken);
                };
        return new FoodFlowMcpTool(tool, handler);
    }

    private static McpServerTool CreateRestaurantTool()
    {
        var tool = new Tool
        {
            Name = "CreateRestaurant",
            Description = "Creates a new restaurant.",

            InputSchema = JsonSerializer.SerializeToElement(new
            {
                type = "object",

                properties = new
                {
                    name = new
                    {
                        type = "string"
                    },

                    gstNumber = new
                    {
                        type = "string"
                    },

                    fNumber = new
                    {
                        type = "string"
                    },

                    description = new
                    {
                        type = new[] { "null", "string" }
                    },

                    restaurantOwner = new
                    {
                        type = "object",

                        properties = new
                        {
                            name = new
                            {
                                type = "string"
                            },

                            email = new
                            {
                                type = "string"
                            },

                            phoneNumber = new
                            {
                                type = "string"
                            }
                        }
                    }
                }
            })
        };

        var handler = async (
            IDictionary<string, JsonElement>? arguments,
            CancellationToken cancellationToken) =>
        {
            var body = JsonSerializer.Serialize(arguments);

            using var client = new HttpClient();

            using var content = new StringContent(
                body,
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "http://localhost:5243/api/Restaurant",
                content,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(
                cancellationToken);
        };

        return new FoodFlowMcpTool(tool, handler);
    }

}
