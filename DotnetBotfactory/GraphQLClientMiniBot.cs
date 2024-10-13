using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class GraphQLClientMiniBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLClient.GetConfiguration().Configuration;
        
        var rootNamespace = configuration.ProjectName.Replace("-", " ").Pascalize();

        var files = GraphQLClient.GetFiles(["**/*.graphql"], []).Files;

        var schema = GraphQLClient.GetSchema($"{configuration.OutputPath}/bot.json");

        string? schemaPath = null;
        if (schema.BotSpec?.ConsumedSchemaPath is not null && schema.BotSchema is not null)
        {
            schemaPath = Path.Combine(configuration.OutputPath, schema.BotSpec?.ConsumedSchemaPath!)
                .Replace("\\", "/");
            GraphQLClient.AddFile(schemaPath,
                schema.BotSchema!);
        }

        files ??= new();
        if (schemaPath is not null && !files.Any(file => file.Path == schemaPath))
        {
            files.Add(new GetFiles() { Path = schemaPath, Kind = FileKind.TEXT });
        }

        var fileContentses = new List<AdditionalFileInput>();
        
        foreach (var file in files)
        {
            string? fileContents = null;
            if (file.Path == schemaPath)
            {
                fileContents = schema.BotSchema;
            }
            
            if (fileContents is null)
            {
                fileContents = GraphQLClient.ReadTextFileWithVersion(file.Path, FileVersion.HEAD).ReadTextFile
                               ?? GraphQLClient.ReadTextFile(file.Path).ReadTextFile;
            }
            
            if (fileContents is null)
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Error,
                    Message = "File {File} was detected as a GraphQL file but it could not be read",
                    Args = [file.Path],
                });
                continue;
            }

            fileContentses.Add(new AdditionalFileInput()
            {
                FilePath = file.Path,
                Content = fileContents,
            });
        }

        var typeNames = new HashSet<string>();

        bool TypeNameAlreadyExists(string name)
        {
            if (typeNames.Contains(name))
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Critical,
                    Message = "Type {Type} was already generated",
                    Args = [name],
                });
                return true;
            }

            typeNames.Add(name);
            return false;
        }
        
        var metadata = GraphQLClient.ParseGraphQLSchemaAndOperations(fileContentses).GraphQL;
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/GraphQLClient.cs",
            $$""""
              using System;
              using System.Collections.Generic;
              using System.Linq;
              using System.Text.Json;
              using System.Text.Json.Serialization;
              using System.Runtime.Serialization;
              using CodegenBot;
              
              namespace {{rootNamespace}};
              
              public partial class GraphQLResponse<T>
              {
                  [JsonPropertyName("data")]
                  public T? Data { get; set; }
              
                  [JsonPropertyName("errors")]
                  public List<GraphQLError>? Errors { get; set; }
              }
              
              public partial class GraphQLError
              {
                  [JsonPropertyName("message")]
                  public required string Message { get; set; }
              }
              
              [JsonSerializable(typeof(GraphQLError))]
              {{CaretRef.New(out var jsonSerializerContextAttributes)}}
              public partial class GraphQLClientJsonSerializerContext : JsonSerializerContext
              {
              }
              
              public static partial class GraphQLClient
              {
              {{CaretRef.New(out var operations)}}
              }
              
              {{CaretRef.New(out var typeDefinitions)}}

              """");

        foreach (var enumType in (metadata.Enumerations ?? []).OrderBy(x => x.Name))
        {
            if (TypeNameAlreadyExists(enumType.Name))
            {
                continue;
            }
            
            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                [JsonSerializable(typeof({enumType.Name.Pascalize()}))]
                
                """);
            
            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""

                  [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
                  public enum {{enumType.Name.Pascalize()}}
                  {
                      {{CaretRef.New(out var enumValues)}}
                  }

                  """);

            foreach (var value in enumType.Values ?? [])
            {
                GraphQLClient.AddText(enumValues.Id, $$"""
                                                           [EnumMember(Value = "{{value.Name}}")]
                                                           {{value.Name}},
                                                           """);
            }
        }
        
        foreach (var inputObjectType in (metadata.InputObjectTypes ?? []).OrderBy(x => x.Name))
        {
            if (TypeNameAlreadyExists(inputObjectType.Name))
            {
                continue;
            }
            
            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({inputObjectType.Name.Pascalize()}))]

                 """);
            
            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""

                  public partial class {{inputObjectType.Name.Pascalize()}}
                  {
                      {{CaretRef.New(out var properties)}}
                  }

                  """);

            foreach (var field in inputObjectType.Fields ?? [])
            {
                var type = GraphQLCSharpTypes.GetVariableCSharpType(field.Type.Text.ToTypeRef(), out var _, metadata);
                GraphQLClient.AddText(properties.Id,
                    $$"""

                      [JsonPropertyName("{{field.Name}}")]
                      public {{GraphQLCSharpTypes.GetIsRequired(type)}} {{type}} {{field.Name.Pascalize()}} { get; set; }

                      """);
            }
        }

        foreach (var operation in (metadata.Operations ?? []).OrderBy(x => x.Name))
        {
            if (string.IsNullOrWhiteSpace(operation.Name))
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Warning,
                    Message = "This operation has no name, so code won't be generated for it: {Operation}",
                    Args = [operation.Text],
                });
                continue;
            }
            
            if (TypeNameAlreadyExists(operation.Name.Pascalize() + "Variables"))
            {
                continue;
            }
            
            if (TypeNameAlreadyExists(operation.Name.Pascalize() + "Data"))
            {
                continue;
            }
            
            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({operation.Name.Pascalize()}Variables))]
                 [JsonSerializable(typeof({operation.Name.Pascalize()}Data))]
                 [JsonSerializable(typeof(GraphQLResponse<{operation.Name.Pascalize()}Data>))]

                 """);

            GraphQLClient.AddText(operations.Id,
                $$""""
                   public static {{operation.Name.Pascalize()}}Data {{operation.Name}}({{CaretRef.New(out var parameters, separator: ", ")}})
                   {
                       var request = new GraphQLRequest<{{operation.Name.Pascalize()}}Variables>
                       {
                           Query = """
                               {{CaretRef.New(out var queryText, separator: "", indentation: "            ")}}
                               """,
                           OperationName = "{{operation.Name}}",
                           Variables = new {{operation.Name.Pascalize()}}Variables()
                           {
                               {{CaretRef.New(out var variables, separator: ", ")}}
                           },
                       };
                   
                       var response = Imports.GraphQL(request, GraphQLClientJsonSerializerContext.Default.GraphQLRequest{{operation.Name.Pascalize()}}Variables);
                       var result = JsonSerializer.Deserialize<GraphQLResponse<{{operation.Name.Pascalize()}}Data>>(response, GraphQLClientJsonSerializerContext.Default.GraphQLResponse{{operation.Name.Pascalize()}}Data);
                       return result?.Data ?? throw new InvalidOperationException("Received null data for request {{operation.Name.Pascalize()}}.");
                   }
                   """");

            GraphQLClient.AddText(queryText.Id, operation.Text);

            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""
                  public partial class {{operation.Name.Pascalize()}}Data
                  {
                      {{CaretRef.New(out var properties)}}
                  }
                  
                  """);

            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""
                  
                  public partial class {{operation.Name.Pascalize()}}Variables
                  {
                  {{CaretRef.New(out var variablePropertyDefinitions)}}
                  }
                  
                  """);
            
            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof(GraphQLRequest<{operation.Name.Pascalize()}Variables>))]

                 """);

            foreach (var variable in operation.Variables ?? [])
            {
                var type = GraphQLCSharpTypes.GetVariableCSharpType(variable.Type.Text.ToTypeRef(), out var enumName, metadata);
                GraphQLClient.AddText(parameters.Id, $$"""{{type}} {{variable.Name}}""");

                GraphQLClient.AddText(variables.Id, $$"""
                                                          {{variable.Name.Pascalize()}} = {{variable.Name}}

                                                          """);
                GraphQLClient.AddText(variablePropertyDefinitions.Id, $$"""
                                                                            [JsonPropertyName("{{variable.Name}}")]
                                                                            public {{GraphQLCSharpTypes.GetIsRequired(type)}} {{type}} {{variable.Name.Pascalize()}} { get; set; }

                                                                            """);
            }

            var rootType =
                (metadata.ObjectTypes ?? []).FirstOrDefault(objType => objType.Name.Equals(operation.OperationType.ToString(), StringComparison.OrdinalIgnoreCase));

            if (rootType is null)
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Error, Message = "Cannot find root type {RootType}. Please rerun `codegen.bot schema` to update the GraphQL schema. If that doesn't work, verify the dependencies in your bot.json are correct.",
                    Args = [operation.OperationType.ToString()]
                });
            }
            else
            {
                AddText(operation, properties, jsonSerializerContextAttributes, operation.Name, rootType);
            }
        }
        
        void AddText(ParseGraphQLSchemaAndOperationsOperation operation, CaretRef properties, CaretRef jsonSerializerContextAttributes, string path,
            ParseGraphQLSchemaAndOperationsObjectType objectType)
        {
            var selections = operation.DenestedSelections?.ToSelections() ?? [];
            
            foreach (var selection in selections)
            {
                GraphQLCSharpTypes.AddSelectionText(properties, path, objectType, selection, metadata, jsonSerializerContextAttributes, typeDefinitions);
            }
        }
    }
}
