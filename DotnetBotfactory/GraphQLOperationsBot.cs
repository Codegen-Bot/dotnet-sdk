using System;
using System.IO;
using System.Linq;
using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class GraphQLOperationsBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLOperations.GetConfiguration().Configuration;
        
        var rootNamespace = configuration.ProjectName.Replace("-", " ").Pascalize();

        var service = new GraphQLMetadataExtractionService();

        var files = GraphQLOperations.GetFiles(["**/*.graphql"], []).Files;

        var metadata = new GraphQLMetadata();
        
        Imports.Log(new LogEvent() { Level = LogEventLevel.Information, Message = "Found {FileCount} GraphQL files", Args = [files?.Count.ToString() ?? "0"] });
        
        foreach (var file in files ?? [])
        {
            var fileContents = GraphQLOperations.GetFileContents(file.Path, FileVersion.HEAD).ReadTextFile;
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
            service.Extract(fileContents, metadata);
            
            Imports.Log(new LogEvent()
            {
                Level = LogEventLevel.Debug, Message = "Found GraphQL file {FilePath}: {Length} characters",
                Args = [file.Path, fileContents.Length.ToString()]
            });
        }
        
        GraphQLOperations.AddFile($"{configuration.OutputPath}/GraphQLOperations.cs",
            $$""""
              using System;
              using System.Collections.Generic;
              using System.Linq;
              using System.Text.Json;
              using System.Text.Json.Serialization;
              using CodegenBot;
              
              namespace {{rootNamespace}};
              
              public static partial class GraphQLOperations
              {
              {{CaretRef.New(out var operations)}}
              }
              
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
                  public string Message { get; set; }
              }
              
              [JsonSerializable(typeof(GraphQLError))]
              {{CaretRef.New(out var jsonSerializerContextAttributes)}}
              public partial class GraphQLOperationsJsonSerializerContext : JsonSerializerContext
              {
              }
              
              {{CaretRef.New(out var typeDefinitions)}}

              """");

        // TODO - do we do this?
        //[JsonSerializable(typeof(GraphQLResponse<>))]
        foreach (var objectType in metadata.ObjectTypes)
        {
            Imports.Log(new LogEvent()
            {
                Level = LogEventLevel.Debug, Message = "Found object type {Type}",
                Args = [objectType.Name]
            });
        }
        
        foreach (var enumType in metadata.Enumerations)
        {
            GraphQLOperations.AddText(jsonSerializerContextAttributes.Id,
                $"""
                [JsonSerializable(typeof({enumType.Name.Pascalize()}))]
                
                """);
            
            GraphQLOperations.AddText(typeDefinitions.Id,
                $$"""

                  public enum {{enumType.Name.Pascalize()}}
                  {
                      {{CaretRef.New(out var enumValues, separator: ", ")}}
                  }

                  """);
            
            foreach (var value in enumType.Values)
            {
                GraphQLOperations.AddText(enumValues.Id, value.Name);
            }
        }
        
        foreach (var inputObjectType in metadata.InputObjectTypes)
        {
            GraphQLOperations.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({inputObjectType.Name.Pascalize()}))]

                 """);
            
            GraphQLOperations.AddText(typeDefinitions.Id,
                $$"""

                  public class {{inputObjectType.Name.Pascalize()}}
                  {
                      {{CaretRef.New(out var properties)}}
                  }

                  """);

            foreach (var field in inputObjectType.Fields)
            {
                GraphQLOperations.AddText(properties.Id,
                    $$"""

                      [JsonPropertyName("{{field.Name}}")]
                      public {{GetVariableCSharpType(field.Type, out var _)}} {{field.Name.Pascalize()}} { get; set; }

                      """);
            }
        }

        foreach (var operation in metadata.Operations)
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
            
            GraphQLOperations.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({operation.Name.Pascalize()}Variables))]
                 [JsonSerializable(typeof({operation.Name.Pascalize()}Data))]
                 [JsonSerializable(typeof(GraphQLResponse<{operation.Name.Pascalize()}Data>))]

                 """);

            GraphQLOperations.AddText(operations.Id,
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
                   
                       var response = Imports.GraphQL(request);
                       var result = JsonSerializer.Deserialize<GraphQLResponse<{{operation.Name.Pascalize()}}Data>>(response, GraphQLOperationsJsonSerializationContext.GraphQLResponseOf{{operation.Name.Pascalize()}}Data);
                       return result?.Data;
                   }
                   """");

            GraphQLOperations.AddText(queryText.Id, operation.Text);

            GraphQLOperations.AddText(typeDefinitions.Id,
                $$"""
                  public class {{operation.Name.Pascalize()}}Data
                  {
                      {{CaretRef.New(out var properties)}}
                  }
                  
                  """);

            GraphQLOperations.AddText(typeDefinitions.Id,
                $$"""
                  
                  public class {{operation.Name.Pascalize()}}Variables
                  {
                  {{CaretRef.New(out var variablePropertyDefinitions)}}
                  }
                  
                  """);
            
            foreach (var variable in operation.Variables)
            {
                var type = GetVariableCSharpType(variable.Type, out var isEnum);
                GraphQLOperations.AddText(parameters.Id, $$"""{{type}} {{variable.Name}}""");

                if (isEnum)
                {
                    GraphQLOperations.AddText(variables.Id, $$"""
                                                              {{variable.Name.Pascalize()}} = {{variable.Name}}

                                                              """);
                    GraphQLOperations.AddText(variablePropertyDefinitions.Id, $$"""
                                                              [JsonConverter(typeof(JsonStringEnumConverter))]
                                                              [JsonPropertyName("{{variable.Name}}")]
                                                              public {{type}} {{variable.Name.Pascalize()}} { get; set; }

                                                              """);
                }
                else
                {
                    GraphQLOperations.AddText(variables.Id, $$"""
                                                              {{variable.Name.Pascalize()}} = {{variable.Name}}

                                                              """);
                    GraphQLOperations.AddText(variablePropertyDefinitions.Id, $$"""
                                                                                [JsonPropertyName("{{variable.Name}}")]
                                                                                public {{type}} {{variable.Name.Pascalize()}} { get; set; }

                                                                                """);
                }
            }

            var rootType =
                metadata.ObjectTypes.FirstOrDefault(objType => objType.Name.Equals(operation.OperationType.ToString(), StringComparison.OrdinalIgnoreCase));

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
        
        void AddText(GraphQLOperation operation, CaretRef properties, CaretRef jsonSerializerContextAttributes, string path,
            GraphQLObjectType objectType)
        {
            foreach (var selection in operation.Selections)
            {
                AddSelectionText(properties, jsonSerializerContextAttributes, path, objectType, selection);
            }
        }

        void AddSelectionText(CaretRef properties, CaretRef jsonSerializerContextAttributes, string path, GraphQLObjectType objectType,
            IGraphQLSelection selection)
        {
            if (selection is GraphQLFieldSelection fieldSelection)
            {
                var field = objectType.Fields.FirstOrDefault(x => x.Name == fieldSelection.Name);

                if (field is null)
                {
                    Imports.Log(new LogEvent()
                    {
                        Level = LogEventLevel.Error, Message = "Cannot find field {Field}",
                        Args = [fieldSelection.Name]
                    });
                }
                else
                {
                    var type = GetSelectionType(path, fieldSelection, field.Type, out var isEnum);

                    if (isEnum)
                    {
                        GraphQLOperations.AddText(properties.Id,
                            $$"""

                              [JsonConverter(typeof(JsonStringEnumConverter))]
                              [JsonPropertyName("{{selection.Name}}")]
                              public {{type}} {{(fieldSelection.Alias ?? fieldSelection.Name).Pascalize()}} { get; set; }

                              """);
                    }
                    else
                    {
                        GraphQLOperations.AddText(properties.Id,
                            $$"""

                              [JsonPropertyName("{{selection.Name}}")]
                              public {{type}} {{(fieldSelection.Alias ?? fieldSelection.Name).Pascalize()}} { get; set; }

                              """);
                    }
                }
            }
            else if (selection is GraphQLFragmentSpreadSelection fragmentSpreadSelection)
            {
                var fragment = metadata.Fragments.FirstOrDefault(x => x.Name == fragmentSpreadSelection.Name);

                if (fragment is null)
                {
                    Imports.Log(new LogEvent() { Level = LogEventLevel.Error, Message = "Cannot find fragment {Fragment}", Args = [fragmentSpreadSelection.Name] });
                }
                else
                {
                    foreach (var fragmentSelection in fragment.Selections)
                    {
                        AddSelectionText(properties, jsonSerializerContextAttributes, path, objectType, fragmentSelection);
                    }
                }
            }
        }
        
        string GetSelectionType(string path, GraphQLFieldSelection selection, TypeRef type, out bool isEnum)
        {
            isEnum = false;
            if (type.Name == "NotNull")
            {
                var result = GetSelectionType(path, selection, type.GenericArguments[0], out isEnum);
                if (result.EndsWith("?"))
                {
                    result = result.Substring(0, result.Length - 1);
                }

                return result;
            }

            if (type.Name == "List")
            {
                return $"List<{GetSelectionType(path, selection, type.GenericArguments[0], out isEnum)}>?";
            }

            if (type.Name == "String")
            {
                return "string?";
            }

            if (type.Name == "Boolean")
            {
                return "bool?";
            }

            if (type.Name == "Integer")
            {
                return "int?";
            }

            var enumType = metadata.Enumerations.FirstOrDefault(enumType => enumType.Name == type.Name);
            if (enumType is not null)
            {
                isEnum = true;
                return enumType.Name.Pascalize();
            }
            
            var objectType = metadata.ObjectTypes.FirstOrDefault(objType => objType.Name == type.Name);
            if (objectType is not null)
            {
                GraphQLOperations.AddText(jsonSerializerContextAttributes.Id,
                    $"""
                     [JsonSerializable(typeof({path.Pascalize()}))]

                     """);

                GraphQLOperations.AddText(typeDefinitions.Id,
                    $$"""
                      public class {{path.Pascalize()}}
                      {
                          {{CaretRef.New(out var properties)}}
                      }

                      """);

                foreach (var subselection in selection.Selections)
                {
                    AddSelectionText(properties, jsonSerializerContextAttributes, path + " " + (selection.Alias ?? selection.Name), objectType, subselection);
                }
                
                return path.Pascalize() + "?";
            }

            GraphQLOperations.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);
            
            return "???";
        }

        string GetVariableCSharpType(TypeRef type, out bool isEnum)
        {
            if (type.Name == "NotNull")
            {
                var result = GetVariableCSharpType(type.GenericArguments[0], out isEnum);
                if (result.EndsWith("?"))
                {
                    result = result.Substring(0, result.Length - 1);
                }

                return result;
            }

            if (type.Name == "List")
            {
                return $"List<{GetVariableCSharpType(type.GenericArguments[0], out isEnum)}>?";
            }

            if (type.Name == "String")
            {
                isEnum = false;
                return "string";
            }

            if (type.Name == "Boolean")
            {
                isEnum = false;
                return "bool";
            }

            if (type.Name == "Integer")
            {
                isEnum = false;
                return "int";
            }

            var enumType = metadata.Enumerations.FirstOrDefault(enumType => enumType.Name == type.Name);
            if (enumType is not null)
            {
                isEnum = true;
                return enumType.Name.Pascalize();
            }
            
            var objectType = metadata.InputObjectTypes.FirstOrDefault(objType => objType.Name == type.Name);
            if (objectType is not null)
            {
                isEnum = false;
                return objectType.Name;
            }

            GraphQLOperations.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);
            
            isEnum = false;
            return "???";
        }
    }
}
