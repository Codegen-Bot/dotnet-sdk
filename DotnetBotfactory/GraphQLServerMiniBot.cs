using System.IO;
using System.Linq;
using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class GraphQLServerMiniBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLClient.GetConfiguration().Configuration;

        if (!configuration.ProvideApi)
        {
            return;
        }
        
        var rootNamespace = configuration.ProjectName.Replace("-", " ").Pascalize();

        var botSpec = GraphQLClient.GetSchema($"{configuration.OutputPath}/bot.json").BotSpec;

        var schemaFilePath =  botSpec?.ProvidedSchemaPath;

        if (schemaFilePath is null)
        {
            Imports.Log(new LogEvent()
            {
                Level = LogEventLevel.Critical,
                Message = "bot.json did not have a providedSchema setting",
                Args = []
            });
            return;
        }

        var schema = GraphQLClient.ReadTextFile(Path.Combine(configuration.OutputPath, schemaFilePath).Replace("\\", "/")).ReadTextFile;

        if (schema is null)
        {
            Imports.Log(new LogEvent()
            {
                Level = LogEventLevel.Critical,
                Message = "bot.json contained a providedSchema setting but the file did not exist",
                Args = []
            });
            return;
        }

        var metadata = GraphQLClient.ParseGraphQLSchemaAndOperations([new AdditionalFileInput()
            {
                FilePath = schemaFilePath!,
                Content = schema!,
            }]).GraphQL;

        var parsedSchema = GraphQLClient.ParseGraphQLSchemaAndOperations([new AdditionalFileInput()
        {
            FilePath = schemaFilePath!,
            Content = schema,
        }]).GraphQL;

        GraphQLClient.AddTextByTags([
            new CaretTagInput()
            {
                Name = "location",
                Value = "bot.json/dependencies",
            },
            new CaretTagInput()
            {
                Name = "botId",
                Value = configuration.Id
            },
        ], $$"""
           "bot://parse/graphql": "1.0.0",
           
           """);
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/defaultServerOperations.graphql",
            $$""""
              
              query ParseGraphQLOperation($graphql: [AdditionalFileInput!]!) {
                graphQL(additionalFiles: $graphql) {
                  operations {
                    name
                    operationType
                    text
                    variables {
                      name
                      type {
                        text
                      }
                    }
                    nestedSelection {
                      depth
                      fieldSelection {
                        name
                        alias
                      }
                      fragmentSpreadSelection {
                        name
                      }
                    }
                  }
                }
              }
              
              """");

        GraphQLClient.AddFile($"{configuration.OutputPath}/GraphQLServer.cs",
            $$""""
              using System;
              using System.Collections.Generic;
              using System.Linq;
              using System.Text.Json;
              using System.Text.Json.Nodes;
              using System.Text.Json.Serialization;
              using System.Runtime.Serialization;
              using CodegenBot;

              namespace {{rootNamespace}};

              [JsonSerializable(typeof(GraphQLServerErrorResponse))]
              [JsonSerializable(typeof(GraphQLServerRequest))]
              {{CaretRef.New(out var jsonSerializerContextAttributes)}}
              public partial class GraphQLServerJsonSerializerContext : JsonSerializerContext
              {
              }

              public partial class GraphQLServer
              {
                  public string Execute(string requestBody)
                  {
                      var request = GraphQLServerRequest.FromJsonString(requestBody);
                      
                      if (request is null)
                      {
                          return JsonSerializer.Serialize(new GraphQLServerErrorResponse()
                         {
                             Errors = [
                                 "Could not parse GraphQL request JSON",
                             ],
                         }, GraphQLServerJsonSerializerContext.Default.GraphQLServerErrorResponse);
                         return;
                      }
                      
                      var query = GraphQLClient.ParseGraphQLOperation(request.Query).GraphQL.Operations.SingleOrDefault();
                      
                      if (request is null)
                      {
                          return JsonSerializer.Serialize(new GraphQLServerErrorResponse()
                         {
                             Errors = [
                                 "Could not parse GraphQL request query",
                             ],
                         }, GraphQLServerJsonSerializerContext.Default.GraphQLServerErrorResponse);
                         return;
                      }
                      
                      var result = new JsonObject();
                      
                      {{CaretRef.New(out var handleOperationType)}}
                      else
                      {
                          Imports.Log(new LogEvent()
                          {
                              Level = LogEventLevel.Critical,
                              Message = "Invalid operation type {OperationType}",
                              Args = [query.OperationType.ToString()],
                          });
                      }
                  }
              
              {{CaretRef.New(out var serverBody)}}
              }
              
              public class GraphQLServerErrorResponse
              {
                  public List<string> Errors { get; set; }
              }
              
              public class GraphQLServerRequest
              {
                  [JsonPropertyName("query")]
                  public required string Query { get; set; }
                  [JsonPropertyName("operationName")]
                  public string? OperationName { get; set; }
                  [JsonPropertyName("variables")]
                  public Dictionary<string, object?>? Variables { get; set; }
              
                  public static GraphQLServerRequest? FromJsonString(string requestBody)
                  {
                      return JsonSerializer.Deserialize(requestBody, GraphQLServerRequestJsonSerializerContext.Default.GraphQLRequest);
                  }
              
                  public string ToJsonString()
                  {
                      return JsonSerializer.Serialize(this, GraphQLServerRequestJsonSerializerContext.Default.GraphQLRequest);
                  }
              }
              
              {{CaretRef.New(out var typeDefinitions)}}

              """");

        var queryType = parsedSchema.ObjectTypes.FirstOrDefault(x => x.Name == "Query");
        
        var maybeElse = "";
        if (queryType is not null)
        {
            GraphQLClient.AddText(handleOperationType.Id,
                """
                if (query.OperationType == GraphQLOperationType.QUERY)
                {
                    Query.AddSelectedFields(result, query.Variables, query.FieldSelection, query.FragmentSpreadSelection);
                }
                
                """);
            maybeElse = "else ";
            AddAddSelectedFields(parsedSchema, queryType, new TypeRef() { Name = "Query", Text = "Query!", GenericArguments = [] }, serverBody, typeDefinitions);
        }

        var mutationType = parsedSchema.ObjectTypes.FirstOrDefault(x => x.Name == "Mutation");
        
        if (mutationType is not null)
        {
            GraphQLClient.AddText(handleOperationType.Id,
                $$"""
                {{maybeElse}}if (query.OperationType == GraphQLOperationType.MUTATION)
                {
                    Mutation.AddSelectedFields(result, query.Variables, query.FieldSelection, query.FragmentSpreadSelection);
                }
                """);
            AddAddSelectedFields(parsedSchema, mutationType, new TypeRef() { Name = "Mutation", Text = "Mutation!", GenericArguments = [] }, serverBody, typeDefinitions);
        }
    }

    private void AddAddSelectedFields(ParseGraphQLSchemaAndOperations parsedSchema,
        ParseGraphQLSchemaAndOperationsObjectType type, TypeRef typeRef, CaretRef parentBody,
        CaretRef typeDefinitions)
    {
        GraphQLClient.AddText(parentBody.Id,
            $$"""
            public {{type.Name}} { get; set; }
            
            """);

        GraphQLClient.AddText(typeDefinitions.Id,
            $$"""
            public partial class {{type.Name.Pascalize()}}
            {
                public void AddSelectedFields()
                {
                }
            
                {{CaretRef.New(out var body)}}
            }
            
            """);
        
        
    }
}
