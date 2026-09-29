
using Microsoft.OpenApi;

namespace FoodFlow.MCP.Api;

public class FoodFlowApiOpenApiData
{
    private string foodFlowOpenApiUrl;

    public FoodFlowApiOpenApiData(IConfiguration configuration)
    {
        this.foodFlowOpenApiUrl = configuration.GetSection("FoodFlow:OpenApiUrl").Value;
        if (string.IsNullOrEmpty(this.foodFlowOpenApiUrl))
        {
            throw new ArgumentNullException("FoodFlow:OpenApiUrl",
            "FoodFlow:OpenApiUrl is not configured in appsettings.json");
        }
    }
    public async Task ReadOpenApiData()
    {
        var specs = await OpenApiDocument.LoadAsync(this.foodFlowOpenApiUrl);
        foreach (var path in specs.Document?.Paths)
        {
            Console.WriteLine($"Path: {path.Key}");
            foreach (var operation in path.Value.Operations)
            {
                Console.WriteLine($"  Operation: {operation.Key}");
                Console.WriteLine($"    Summary: {operation.Value.Summary}");
                Console.WriteLine($"    Description: {operation.Value.Description}");
            }
        }
    }
}