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
                        arguments {
                          name
                          type { text }
                          value
                        }
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
                      }
                      
                      var query = GraphQLClient.ParseGraphQLOperation([new AdditionalFileInput() { FilePath = "tmp.graphql", Content = request.Query } ]).GraphQL.Operations.SingleOrDefault();
                      
                      if (query is null)
                      {
                          return JsonSerializer.Serialize(new GraphQLServerErrorResponse()
                         {
                             Errors = [
                                 "Could not parse GraphQL request query",
                             ],
                         }, GraphQLServerJsonSerializerContext.Default.GraphQLServerErrorResponse);
                      }
                      
                      var result = new JsonObject();
                      
                      {{CaretRef.New(out var handleOperationType)}}
              
                      var resultString = result.ToJsonString();
                      return resultString;
                  }
              
                  {{CaretRef.New(out var serverBody)}}
              }
              
              public class GraphQLServerErrorResponse
              {
                  public required List<string> Errors { get; set; }
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
                      return JsonSerializer.Deserialize(requestBody, GraphQLServerJsonSerializerContext.Default.GraphQLServerRequest);
                  }
              
                  public string ToJsonString()
                  {
                      return JsonSerializer.Serialize(this, GraphQLServerJsonSerializerContext.Default.GraphQLServerRequest);
                  }
              }
              
              public partial class ParseGraphQLOperationOperationNestedSelection
                  : INestedSelection<
                     ParseGraphQLOperationOperationNestedSelectionFieldSelection,
                     ParseGraphQLOperationOperationNestedSelectionFragmentSpreadSelection
                  >
              {
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
                    Query.AddSelectedFields(query.Variables, query.NestedSelection.ToSelections(), result);
                }
                
                """);
            maybeElse = "else ";
            AddObjectTypeResolver(parsedSchema, queryType, null, new TypeRef() { Name = "Query", Text = "Query!", GenericArguments = [] }, serverBody, typeDefinitions);
        }

        var mutationType = parsedSchema.ObjectTypes.FirstOrDefault(x => x.Name == "Mutation");
        
        if (mutationType is not null)
        {
            GraphQLClient.AddText(handleOperationType.Id,
                $$"""
                {{maybeElse}}if (query.OperationType == GraphQLOperationType.MUTATION)
                {
                    Mutation.AddSelectedFields(query.Variables, query.NestedSelection.ToSelections(), result);
                }
                """);
            AddObjectTypeResolver(parsedSchema, mutationType, null, new TypeRef() { Name = "Mutation", Text = "Mutation!", GenericArguments = [] }, serverBody, typeDefinitions);
            maybeElse = "else";
        }

        GraphQLClient.AddText(handleOperationType.Id,
            $$"""
            {{maybeElse}}
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Critical,
                    Message = "Invalid operation type {OperationType}",
                    Args = [query.OperationType.ToString()],
                });
            }
            """);
    }

    private void AddResolver(ParseGraphQLSchemaAndOperations parsedSchema,
        ParseGraphQLSchemaAndOperationsObjectTypeField field, CaretRef parentBody,
        CaretRef typeDefinitions)
    {
        var typeRef = field.Type.Text.ToTypeRef();
        
        var objectType = parsedSchema.ObjectTypes.FirstOrDefault(x => x.Name == typeRef.Name);
        if (objectType is not null)
        {
            AddObjectTypeResolver(parsedSchema, objectType, field, typeRef, parentBody, typeDefinitions);
        }
        
        var enumType = parsedSchema.Enumerations.FirstOrDefault(x => x.Name == typeRef.Name);
        if (enumType is not null)
        {
            AddEnumResolver(parsedSchema, enumType, field, parentBody, typeDefinitions);
        }
        
        AddScalarResolver(parsedSchema, field, parentBody, typeDefinitions);
    }

    private void AddScalarResolver(ParseGraphQLSchemaAndOperations parsedSchema, ParseGraphQLSchemaAndOperationsObjectTypeField field, CaretRef parentBody, CaretRef typeDefinitions)
    {
        var typeRef = field.Type.Text.ToTypeRef();
        var type = GraphQLCSharpTypes.GetVariableCSharpType(typeRef, out var _, parsedSchema);

        if (field.Parameters.Count > 0)
        {
            GraphQLClient.AddText(parentBody.Id,
                $$"""
                  public {{type}} {{field.Name.Pascalize()}}({{CaretRef.New(out var arguments, separator: ", ")}})
                  {
                  }

                  """);

            foreach (var arg in field.Parameters)
            {
                var argType = GraphQLCSharpTypes.GetVariableCSharpType(arg.Type.Text.ToTypeRef(), out var _, parsedSchema);
                GraphQLClient.AddText(arguments.Id, $"{argType} {arg.Name.Camelize()}");
            }
        }
        else
        {
            GraphQLClient.AddText(parentBody.Id,
                $$"""
                  public {{type}} {{field.Name.Pascalize()}} { get; set; }

                  """);
        }
    }

    private void AddEnumResolver(ParseGraphQLSchemaAndOperations parsedSchema,
        ParseGraphQLSchemaAndOperationsEnumeration type, ParseGraphQLSchemaAndOperationsObjectTypeField field, CaretRef parentBody,
        CaretRef typeDefinitions)
    {
        if (field.Parameters.Count > 0)
        {
            GraphQLClient.AddText(parentBody.Id,
                $$"""
                  public {{type.Name}} {{type.Name}}({{CaretRef.New(out var arguments, separator: ", ")}})
                  {
                  }

                  """);

            foreach (var arg in field.Parameters)
            {
                var argType = GraphQLCSharpTypes.GetVariableCSharpType(arg.Type.Text.ToTypeRef(), out var _, parsedSchema);
                GraphQLClient.AddText(arguments.Id, $"{argType} {arg.Name.Camelize()}");
            }
        }
        else
        {
            GraphQLClient.AddText(parentBody.Id,
                $$"""
                  public {{type.Name}} {{type.Name}} { get; }

                  """);
        }

        GraphQLClient.AddText(typeDefinitions.Id,
            $$"""
              public enum {{type.Name.Pascalize()}}
              {
                  {{CaretRef.New(out var body)}}
              }

              """);

        foreach (var value in type.Values)
        {
            GraphQLClient.AddText(body.Id,
                $"""
                {value.Name}
                
                """);
        }
    }
    
    private void AddObjectTypeResolver(ParseGraphQLSchemaAndOperations parsedSchema,
        ParseGraphQLSchemaAndOperationsObjectType type,
        ParseGraphQLSchemaAndOperationsObjectTypeField? field, TypeRef typeRef,
        CaretRef parentBody,
        CaretRef typeDefinitions)
    {
        if (field is not null && field.Parameters.Count > 0)
        {
            GraphQLClient.AddText(parentBody.Id,
                $$"""
                  public {{type.Name}} {{type.Name}}({{CaretRef.New(out var arguments, separator: ", ")}})
                  {
                  }

                  """);

            foreach (var arg in field.Parameters)
            {
                var argType = GraphQLCSharpTypes.GetVariableCSharpType(arg.Type.Text.ToTypeRef(), out var _, parsedSchema);
                GraphQLClient.AddText(arguments.Id, $"{argType} {arg.Name.Camelize()}");
            }
        }
        else
        {
            GraphQLClient.AddText(parentBody.Id,
                $$"""
                  public {{type.Name}} {{type.Name}} { get; } = new();

                  """);
        }

        GraphQLClient.AddText(typeDefinitions.Id,
            $$"""
            public partial class {{type.Name.Pascalize()}}
            {
                public void AddSelectedFields(IReadOnlyList<ParseGraphQLOperationOperationVariable> variables, IReadOnlyList<Selection<
            ParseGraphQLOperationOperationNestedSelectionFieldSelection,
            ParseGraphQLOperationOperationNestedSelectionFragmentSpreadSelection>> selections, JsonObject result)
                {
                    foreach (var selection in selections)
                    {
                        {{CaretRef.New(out var addSelectedFieldsBody)}}
                    }
                    
                }
            
                {{CaretRef.New(out var body)}}
            }
            
            """);

        foreach (var subfield in type.Fields)
        {
            GraphQLClient.AddText(addSelectedFieldsBody.Id,
                $$"""
                if (selection.FieldSelection is not null && selection.FieldSelection.Name == "{{subfield.Name}}")
                {
                    result[selection.FieldSelection.Alias ?? selection.FieldSelection.Name] = {{subfield.Name.Pascalize()}}();
                }
                else if (selection.FragmentSpreadSelection is not null && selection.FragmentSpreadSelection.Name == "{{subfield.Name}}")
                {
                    result[selection.FragmentSpreadSelection.Name] = {{subfield.Name.Pascalize()}}();
                }
                
                """);
            AddResolver(parsedSchema, subfield, body, typeDefinitions);
        }
    }
}
