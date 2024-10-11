using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodegenBot;

public class GraphQLServer(IServiceProvider services, IRequestExecutorResolver requestExecutorResolver)
{
    private readonly ILogger logger = services.GetRequiredService<ILogger<GraphQLServer>>();

    private async Task<string> GetSchemaAsync()
    {
        var requestExecutor =
            await requestExecutorResolver.GetRequestExecutorAsync(cancellationToken: CancellationToken.None);
        return requestExecutor.Schema.Print();
    }
    
    public string GetGraphQLSchema()
    {
        var task = GetSchemaAsync();
        task.Wait();
        return task.Result;
    }
    
    public string Execute(string requestBody, IServiceProvider services)
    {
        var task = ExecuteAsync(requestBody, services, CancellationToken.None);
        task.Wait();
        return task.Result;
    }
    
    public async Task<string> ExecuteAsync(string requestBody, IServiceProvider services,
        CancellationToken cancellationToken)
    {
        var requestExecutor =
            await requestExecutorResolver.GetRequestExecutorAsync(cancellationToken: cancellationToken);

        var req = GraphQLRequest.FromJsonString(requestBody);

        if (req is null)
        {
            Imports.Log(new LogEvent()
            {
                Level = LogEventLevel.Critical,
                Message = "Failed to parse JSON request in dotnet bot",
                Args = [],
            });
            throw new InvalidOperationException("Failed to parse JSON request in dotnet bot");
        }
        
        // Here services is the outer service provider that is part of the general application,
        // whereas inMemoryGraphQLServerServices is the service provider that HotChocolate uses to resolve all its
        // stuff. This is the place where things registered in inMemoryGraphQLServerServices can be initialized to point
        // at services in the outer scope.
        
        var result =
            (IQueryResult) await requestExecutor.ExecuteAsync(
                ConvertJsonToQueryRequest(req, services), cancellationToken);

        var jsonResult = JsonNode.Parse(result.ToJson());
        
        if (jsonResult is null) 
        {
            Imports.Log(new LogEvent()
            {
                Level = LogEventLevel.Critical,
                Message = "Failed to parse JSON response in dotnet bot",
                Args = [],
            });
            
            throw new InvalidOperationException("Failed to parse JSON response in dotnet bot");
        }

        if (result.Errors is not null && result.Errors.Count > 0)
        {
            foreach (var error in result.Errors)
            {
                var locations = "";
                if (error.Locations is not null)
                {
                    locations = string.Join(", ",
                        error.Locations.Select(location => $"Line {location.Line}:{location.Column}"));
                }

                if (error.Exception is null)
                {
                    logger.LogError(
                        "Failed to process GraphQL request inside dotnet bot; error message is {ErrorMessage}, path is {Path}, location is {Location}, code is {ErrorCode}, request was {Request}",
                        error.Message, error.Path, locations, error.Code, requestBody);
                }
                else
                {
                    logger.LogError(error.Exception,
                        "Failed to process GraphQL request inside dotnet bot; error message is {ErrorMessage}, path is {Path}, location is {Location}, code is {ErrorCode} request was {Request}",
                        error.Message, error.Path, locations, error.Code, requestBody);
                }
            }
        }

        try
        {
            return jsonResult.ToJsonString();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to convert the JSON response");
            throw;
        }
    }

    private static IQueryRequest ConvertJsonToQueryRequest(GraphQLRequest req, IServiceProvider? services = null)
    {
        var query = req.Query;
        var requestBuilder = QueryRequestBuilder.New()
            .SetQuery(query);
        if (services is not null)
        {
            requestBuilder = requestBuilder.SetServices(services);
        }

        if (req.Variables is not null)
        {
            foreach (var variable in req.Variables)
            {
                object? getValue(object? obj)
                {
                    if (obj is JsonElement jsonElement)
                    {
                        if (jsonElement.ValueKind == JsonValueKind.String)
                        {
                            return jsonElement.GetString()!;
                        }

                        if (jsonElement.ValueKind == JsonValueKind.Number)
                        {
                            if (jsonElement.GetRawText().Contains("."))
                            {
                                return jsonElement.GetDouble();
                            }
                            else
                            {
                                return jsonElement.GetInt32();
                            }
                        }

                        if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            return jsonElement.EnumerateArray().Select(x => getValue(x)).ToArray();
                        }

                        if (jsonElement.ValueKind is JsonValueKind.Object)
                        {
                            var result = new Dictionary<string, object?>();
                            foreach (var prop in jsonElement.EnumerateObject())
                            {
                                result[prop.Name] = getValue(prop.Value);
                            }

                            return result;
                        }
                    }

                    if (obj is null)
                    {
                        return null;
                    }

                    throw new NotImplementedException($"Parameters of type {obj} are not supported yet");
                }

                requestBuilder = requestBuilder.AddVariableValue(variable.Key, getValue(variable.Value));
            }
        }

        var request = requestBuilder.Create();
        return request;
    }
}

internal class GraphQLRequest
{
    [JsonPropertyName("query")]
    public required string Query { get; set; }
    [JsonPropertyName("operationName")]
    public string? OperationName { get; set; }
    [JsonPropertyName("variables")]
    public Dictionary<string, object?>? Variables { get; set; }

    public static GraphQLRequest? FromJsonString(string requestBody)
    {
        return JsonSerializer.Deserialize(requestBody, GraphQLRequestJsonSerializerContext.Default.GraphQLRequest);
    }

    public string ToJsonString()
    {
        return JsonSerializer.Serialize(this, GraphQLRequestJsonSerializerContext.Default.GraphQLRequest);
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(GraphQLRequest))]
internal partial class GraphQLRequestJsonSerializerContext : JsonSerializerContext
{
    
}