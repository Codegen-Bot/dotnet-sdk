using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace CodegenBot;

public class GraphQLRequest<TVariables>
{
    [JsonPropertyName("query")]
    public required string Query { get; set; }
    [JsonPropertyName("operationName")]
    public string? OperationName { get; set; }
    [JsonPropertyName("variables")]
    public TVariables? Variables { get; set; }

    public static GraphQLRequest<TVariables>? FromJsonString(string requestBody, JsonTypeInfo<GraphQLRequest<TVariables>> jsonTypeInfo)
    {
        return JsonSerializer.Deserialize(requestBody, jsonTypeInfo);
    }

    public string ToJsonString(JsonTypeInfo<GraphQLRequest<TVariables>> jsonTypeInfo)
    {
        return JsonSerializer.Serialize(this, jsonTypeInfo);
    }
}
