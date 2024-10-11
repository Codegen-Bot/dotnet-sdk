using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodegenBot;

public class GraphQLRequest<TVariables>
{
    [JsonPropertyName("query")]
    public string Query { get; set; }
    [JsonPropertyName("operationName")]
    public string? OperationName { get; set; }
    [JsonPropertyName("variables")]
    public TVariables? Variables { get; set; }

    public static GraphQLRequest<TVariables> FromJsonString<TVariables>(string requestBody)
    {
        
        return JsonSerializer.Deserialize<GraphQLRequest<TVariables>>(requestBody, new JsonSerializerOptions() { WriteIndented = true });
    }

    public string ToJsonString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
    }
}
