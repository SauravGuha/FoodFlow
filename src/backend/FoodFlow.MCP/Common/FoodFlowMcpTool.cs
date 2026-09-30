
using System.Collections.ObjectModel;
using System.Text.Json;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;

namespace FoodFlow.MCP.Common;

public class FoodFlowMcpTool : McpServerTool
{
    public FoodFlowMcpTool(Tool tool,
    Func<IDictionary<string, JsonElement>, CancellationToken, Task<string>> handler)
    {
        this.ProtocolTool = tool;
        this.handler = handler;
    }
    public override Tool ProtocolTool { get; }

    private Func<IDictionary<string, JsonElement>, CancellationToken, Task<string>> handler;

    public override IReadOnlyList<object> Metadata { get; } = [];

    public override async ValueTask<CallToolResult> InvokeAsync(RequestContext<CallToolRequestParams> request,
    CancellationToken cancellationToken = default)
    {
        var arguments = request.Params?.Arguments;

        var result = await handler(
            arguments,
            cancellationToken);

        return new CallToolResult
        {
            Content =
            [
                new TextContentBlock
                {
                    Text = result
                }
            ]
        };
    }
}