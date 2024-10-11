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
              
              public class GraphQLResponse<T>
              {
                  [JsonPropertyName("data")]
                  public T? Data { get; set; }
              
                  [JsonPropertyName("errors")]
                  public List<GraphQLError>? Errors { get; set; }
              }
              
              public class GraphQLError
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
            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({inputObjectType.Name.Pascalize()}))]

                 """);
            
            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""

                  public class {{inputObjectType.Name.Pascalize()}}
                  {
                      {{CaretRef.New(out var properties)}}
                  }

                  """);

            foreach (var field in inputObjectType.Fields ?? [])
            {
                var type = GetVariableCSharpType(field.Type.Text.ToTypeRef(), out var _);
                GraphQLClient.AddText(properties.Id,
                    $$"""

                      [JsonPropertyName("{{field.Name}}")]
                      public {{GetIsRequired(type)}} {{type}} {{field.Name.Pascalize()}} { get; set; }

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
                  public class {{operation.Name.Pascalize()}}Data
                  {
                      {{CaretRef.New(out var properties)}}
                  }
                  
                  """);

            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""
                  
                  public class {{operation.Name.Pascalize()}}Variables
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
                var type = GetVariableCSharpType(variable.Type.Text.ToTypeRef(), out var enumName);
                GraphQLClient.AddText(parameters.Id, $$"""{{type}} {{variable.Name}}""");

                GraphQLClient.AddText(variables.Id, $$"""
                                                          {{variable.Name.Pascalize()}} = {{variable.Name}}

                                                          """);
                GraphQLClient.AddText(variablePropertyDefinitions.Id, $$"""
                                                                            [JsonPropertyName("{{variable.Name}}")]
                                                                            public {{GetIsRequired(type)}} {{type}} {{variable.Name.Pascalize()}} { get; set; }

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
            var selections = operation.NestedSelection?.ToSelections() ?? [];
            
            foreach (var selection in selections)
            {
                AddSelectionText(properties, path, objectType, selection);
            }
        }

        void AddSelectionText(CaretRef properties, string path, ParseGraphQLSchemaAndOperationsObjectType objectType, Selection selection)
        {
            if (selection.FieldSelection is not null)
            {
                var field = (objectType.Fields ?? []).FirstOrDefault(x => x.Name == selection.FieldSelection.Name);

                if (field is null)
                {
                    Imports.Log(new LogEvent()
                    {
                        Level = LogEventLevel.Error, Message = "Cannot find field {Field}",
                        Args = [selection.FieldSelection.Name]
                    });
                }
                else
                {
                    (string, bool) type = GetSelectionType(path, selection, field.Type.Text.ToTypeRef());

                    GraphQLClient.AddText(properties.Id,
                        $$"""

                          [JsonPropertyName("{{selection.FieldSelection.Name}}")]
                          public {{GetIsRequired(type.Item1)}} {{type.Item1}} {{selection.FieldSelection.Name.Pascalize()}} { get; set; }

                          """);
                }
            }
            else if (selection.FragmentSpreadSelection is not null)
            {
                throw new NotImplementedException("Fragment spread selection is not implemented.");
                // var fragment = metadata.Fragments.FirstOrDefault(x => x.Name == fragmentSpreadSelection.Name);
                //
                // if (fragment is null)
                // {
                //     Imports.Log(new LogEvent() { Level = LogEventLevel.Error, Message = "Cannot find fragment {Fragment}", Args = [fragmentSpreadSelection.Name] });
                // }
                // else
                // {
                //     foreach (var fragmentSelection in fragment.Selections)
                //     {
                //         AddSelectionText(properties, jsonSerializerContextAttributes, path, objectType, fragmentSelection);
                //     }
                // }
            }
        }
        
        (string type, bool isEnum) GetSelectionType(string path, Selection selection, TypeRef type)
        {
            if (type.Name == "NotNull")
            {
                (string, bool) x = GetSelectionType(path, selection, type.GenericArguments[0]);
                var result = x.Item1;
                if (result.EndsWith("?"))
                {
                    result = result.Substring(0, result.Length - 1);
                }

                return (result, x.Item2);
            }

            if (type.Name == "List")
            {
                (string, bool) b = GetSelectionType(path, selection, type.GenericArguments[0]);
                return ($"List<{b.Item1}>?", false);
            }

            if (type.Name == "String")
            {
                return ("string?", false);
            }

            if (type.Name == "Boolean")
            {
                return ("bool?", false);
            }

            if (type.Name == "Int")
            {
                return ("int?", false);
            }

            var enumType = (metadata.Enumerations ?? []).FirstOrDefault(enumType => enumType.Name == type.Name);
            if (enumType is not null)
            {
                return (enumType.Name.Pascalize() + "?", true);
            }
            
            var objectType = (metadata.ObjectTypes ?? []).FirstOrDefault(objType => objType.Name == type.Name);
            if (objectType is not null)
            {
                GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                    $"""
                     [JsonSerializable(typeof({path.Pascalize()}))]

                     """);

                GraphQLClient.AddText(typeDefinitions.Id,
                    $$"""
                      public class {{path.Pascalize()}}
                      {
                          {{CaretRef.New(out var properties)}}
                      }

                      """);
                
                foreach (var subselection in selection.Children)
                {
                    var name = subselection.FieldSelection?.Name ?? subselection.FragmentSpreadSelection!.Name;
                    name = ((string)name).Singularize();
                    AddSelectionText(properties, $"{path} {name}", objectType, subselection);
                }

                return (path.Pascalize() + "?", false);
            }

            GraphQLClient.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);

            return ("???", false);
        }
        
        string GetVariableCSharpType(TypeRef type, out string? enumName)
        {
            if (type.Name == "NotNull")
            {
                var result = GetVariableCSharpType(type.GenericArguments[0], out enumName);
                if (result.EndsWith("?"))
                {
                    result = result.Substring(0, result.Length - 1);
                }

                return result;
            }

            if (type.Name == "List")
            {
                return $"List<{GetVariableCSharpType(type.GenericArguments[0], out enumName)}>?";
            }

            if (type.Name == "String")
            {
                enumName = null;
                return "string?";
            }

            if (type.Name == "Boolean")
            {
                enumName = null;
                return "bool?";
            }

            if (type.Name == "Int")
            {
                enumName = null;
                return "int?";
            }

            var enumType = (metadata.Enumerations ?? []).FirstOrDefault(enumType => enumType.Name == type.Name);
            if (enumType is not null)
            {
                enumName = enumType.Name.Pascalize();
                return enumType.Name.Pascalize() + "?";
            }
            
            var objectType = (metadata.InputObjectTypes ?? []).FirstOrDefault(objType => objType.Name == type.Name);
            if (objectType is not null)
            {
                enumName = null;
                return objectType.Name + "?";
            }

            GraphQLClient.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);
            
            enumName = null;
            return "???";
        }

        string GetIsRequired(string typeRef)
        {
            if (!typeRef.EndsWith("?"))
            {
                return "required ";
            }

            return "";
        }
    }
}
