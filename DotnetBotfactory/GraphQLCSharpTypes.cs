using System;
using System.Collections.Generic;
using System.Linq;
using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public record PathPart(string Value, PathPartType Type, bool Enabled = true);

public enum PathPartType
{
    Field,
    Interface,
    Operation
}

public static class GraphQLCSharpTypes
{
    public record SelectionType(string Name, bool IsEnum);
    
    public static string GetIsRequired(string typeRef)
    {
        if (!typeRef.EndsWith("?"))
        {
            return "required ";
        }

        return "";
    }
    
    public static void AddProperty(CaretRef properties, Path path, IObjectOrInterface objectType,
        Renested<IGraphQLSelection> selection, ParseGraphQLSchemaAndOperations metadata,
        CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions, HashSet<string> typesWritten,
        string? interfaceName, CaretRef? jsonDerivedType = null)
    {
        if (selection.Item is GraphQLSelectionGraphQLFieldSelection fieldSelection)
        {
            var field = (objectType.Fields ?? []).FirstOrDefault(x => x.Name == fieldSelection.Name);

            if (fieldSelection.Name is "__typename")
            {
                if (objectType is ParseGraphQLSchemaAndOperationsObjectType)
                {
                    field = new ParseGraphQLSchemaAndOperationsObjectTypeField()
                    {
                        Name = "__typename",
                        Parameters = [],
                        Type = new()
                        {
                            Text = "String!"
                        },
                    };
                }
                else
                {
                    field = new ParseGraphQLSchemaAndOperationsInterfaceTypeField()
                    {
                        Name = "__typename",
                        Parameters = [],
                        Type = new()
                        {
                            Text = "String!"
                        },
                    };
                }
            }
            
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
                var type = GetSelectionType(path + new PathPart(field.Name, PathPartType.Field), selection, field.Type.Text.ToTypeRef(), metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
                
                if (objectType is ParseGraphQLSchemaAndOperationsInterfaceType)
                {
                    GraphQLClient.AddText(properties.Id,
                        $$"""

                          [JsonPropertyName("{{fieldSelection.Name}}")]
                          {{type.Name}} {{fieldSelection.Name.Pascalize()}} { get; set; }

                          """);
                }
                else
                {
                    GraphQLClient.AddText(properties.Id,
                        $$"""

                          [JsonPropertyName("{{fieldSelection.Name}}")]
                          public {{GetIsRequired(type.Name)}} {{type.Name}} {{fieldSelection.Name.Pascalize()}} { get; set; }

                          """);
                }
            }
        }
        else if (selection.Item is GraphQLSelectionGraphQLFragmentSpreadSelection fragmentSpreadSelection)
        {
            var fragment = metadata.Fragments.FirstOrDefault(x => x.Name == fragmentSpreadSelection.FragmentName);
            
            if (fragment is null)
            {
                Imports.Log(new LogEvent() { Level = LogEventLevel.Error, Message = "Cannot find fragment {Fragment}", Args = [fragmentSpreadSelection.FragmentName] });
            }
            else
            {
                var renestedSelections = fragment.DenestedSelections
                    .Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, IGraphQLSelection>(x => x.Depth, (x, _) => x.Item.Selection);
                foreach (var fragmentSelection in renestedSelections)
                {
                    AddProperty(properties, path, objectType, fragmentSelection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
                }
            }
        }
        else if (selection.Item is GraphQLSelectionGraphQLInlineFragmentSelection inlineFragmentSelection)
        {
            
        }
    }
    
    public static SelectionType GetSelectionType(Path path, Renested<IGraphQLSelection> selection, TypeRef type, ParseGraphQLSchemaAndOperations metadata, CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions, HashSet<string> typesWritten, string? interfaceName)
    {
        if (type.Name == "NotNull")
        {
            var x = GetSelectionType(path, selection, type.GenericArguments[0], metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
            var result = x.Name;
            if (result.EndsWith("?"))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return new (result, x.IsEnum);
        }

        if (type.Name == "List")
        {
            var b = GetSelectionType(path, selection, type.GenericArguments[0], metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
            return new ($"List<{b.Name}>?", false);
        }

        if (type.Name == "String")
        {
            return new ("string?", false);
        }

        if (type.Name == "Boolean")
        {
            return new ("bool?", false);
        }

        if (type.Name == "Int")
        {
            return new ("int?", false);
        }

        if (type.Name == "Float")
        {
            return new ("double?", false);
        }

        var enumType = (metadata.Enumerations ?? []).FirstOrDefault(enumType => enumType.Name == type.Name);
        if (enumType is not null)
        {
            return new (enumType.Name.Pascalize() + "?", true);
        }
        
        var objectType = (metadata.ObjectTypes ?? []).FirstOrDefault(objType => objType.Name == type.Name);
        if (objectType is not null)
        {
            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({path.ToString().Pascalize()}))]

                 """);

            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""
                  public partial class {{path.ToString().Pascalize()}}{{CaretRef.New(out var extends)}}
                  {
                      {{CaretRef.New(out var properties)}}
                  }

                  """);

            if (interfaceName is not null)
            {
                GraphQLClient.AddText(extends.Id, $" : {interfaceName.Pascalize()}");
                
                if (!typesWritten.Contains($"{interfaceName.Pascalize()}"))
                {
                    typesWritten.Add($"{interfaceName.Pascalize()}");

                    GraphQLClient.AddText(typeDefinitions.Id,
                        $$"""
                          public partial interface {{interfaceName.Pascalize()}}
                          {
                              {{properties}}
                          }

                          """);
                }
            }

            foreach (var subselection in selection.Children)
            {
                if (subselection.Item is GraphQLSelectionGraphQLFieldSelection fieldSelection)
                {
                    AddProperty(properties, path + new PathPart(fieldSelection.Name.Singularize(), PathPartType.Field, false), objectType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName is null ? null : interfaceName + " " + fieldSelection.Name.Singularize());
                }
                else if (subselection.Item is GraphQLSelectionGraphQLFragmentSpreadSelection fragmentSpreadSelection)
                {
                    var fragment = metadata.Fragments.FirstOrDefault(x =>
                        x.Name == fragmentSpreadSelection.FragmentName);
                    if (fragment is null)
                    {
                        Imports.Log(new LogEvent()
                        {
                            Level = LogEventLevel.Critical,
                            Message = "Cannot find fragment {FragmentName} (path {Path}",
                            Args = [fragmentSpreadSelection.FragmentName, path.ToString()],
                        });
                    }
                    else
                    {
                        interfaceName = $"I{fragment.Name.Pascalize()}";

                        GraphQLClient.AddText(extends.Id, $" : {interfaceName}");
                        CaretRef? interfaceProperties = null;

                        if (!typesWritten.Contains(interfaceName))
                        {
                            typesWritten.Add(interfaceName);

                            GraphQLClient.AddText(typeDefinitions.Id,
                                $$"""
                                  public partial interface {{interfaceName}}
                                  {
                                      {{CaretRef.New(out interfaceProperties)}}
                                  }

                                  """);
                        }
                        
                        var renested = fragment.DenestedSelections.Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, IGraphQLSelection>(x => x.Depth,
                            (item, children) => item.Item.Selection);
                        foreach (var subsubselection in renested)
                        {
                            AddProperty(properties, path, objectType, subsubselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
                        }

                        // if (interfaceProperties is not null)
                        // {
                        //     GraphQLClient.AddText(properties.Id, GraphQLClient.GetCaret(interfaceProperties.Id).Caret!.String);
                        // }
                    }
                }
            }

            return new (path.ToString().Pascalize() + "?", false);
        }

        var fragmentType = (metadata.Fragments ?? []).FirstOrDefault(x => x.Name == type.Name);
        if (fragmentType is not null)
        {
            var outerFragmentObjectType =
                metadata.ObjectTypes?.FirstOrDefault(x => x.Name == fragmentType.TypeCondition);

            if (outerFragmentObjectType is null)
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Critical,
                    Message = "Cannot find type condition {TypeCondition} of fragment {FragmentName} (path {Path})",
                    Args = [fragmentType.TypeCondition, fragmentType.Name, path.ToString()],
                });
                throw new InvalidOperationException();
            }
            
            interfaceName = $"I{fragmentType.Name}";

            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({interfaceName}))]

                 """);

            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""
                  public partial class {{path.ToString().Pascalize()}} : {{interfaceName}}
                  {
                      {{CaretRef.New(out var properties)}}
                  }

                  """);

            if (!typesWritten.Contains(interfaceName))
            {
                typesWritten.Add(interfaceName);

                GraphQLClient.AddText(typeDefinitions.Id,
                    $$"""
                      public partial interface {{interfaceName}}
                      {
                          {{properties}}
                      }

                      """);
            }
            
            foreach (var subselection in selection.Children)
            {
                if (subselection.Item is GraphQLSelectionGraphQLFieldSelection fieldSelection)
                {
                    AddProperty(properties, path + new PathPart(fieldSelection.Name.Singularize(), PathPartType.Field, false), outerFragmentObjectType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName is null ? null : interfaceName + " " + fieldSelection.Name.Singularize());
                }
                else if (subselection.Item is GraphQLSelectionGraphQLFragmentSpreadSelection fragmentSpreadSelection)
                {
                    var fragment = metadata.Fragments!.FirstOrDefault(x =>
                        x.Name == fragmentSpreadSelection.FragmentName);
                    if (fragment is null)
                    {
                        Imports.Log(new LogEvent()
                        {
                            Level = LogEventLevel.Critical,
                            Message = "Cannot find fragment {FragmentName} (path {Path}",
                            Args = [fragmentSpreadSelection.FragmentName, path.ToString()],
                        });
                    }
                    else
                    {
                        var fragmentObjectType =
                            metadata.ObjectTypes?.FirstOrDefault(x => x.Name == fragment.TypeCondition);

                        if (fragmentObjectType is null)
                        {
                            Imports.Log(new LogEvent()
                            {
                                Level = LogEventLevel.Critical,
                                Message = "Cannot find the type condition {TypeCondition} on fragment {FragmentName} (path {Path})",
                                Args = [fragment.TypeCondition, fragmentSpreadSelection.FragmentName, path.ToString()],
                            });
                        }
                        else
                        {
                            var renested = fragment.DenestedSelections.Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, IGraphQLSelection>(x => x.Depth,
                                (item, children) => item.Item.Selection);
                            foreach (var subsubselection in renested)
                            {
                                AddProperty(properties, path, fragmentObjectType, subsubselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
                            }
                        }
                    }
                }
            }

            return new (interfaceName + "?", false);
        }
        
        var interfaceType = (metadata.InterfaceTypes ?? []).FirstOrDefault(x => x.Name == type.Name);
        if (interfaceType is not null)
        {
            interfaceName = $"I{interfaceType.Name.Pascalize()}";
            
            GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                $"""
                 [JsonSerializable(typeof({interfaceName}))]

                 """);

            if (!typesWritten.Contains(interfaceName))
            {
                typesWritten.Add(interfaceName);
                GraphQLClient.AddText(typeDefinitions.Id,
                    $$"""
                      [JsonPolymorphic(TypeDiscriminatorPropertyName = "__typename")]
                      {{CaretRef.New(out var jsonDeriveTypes)}}
                      public partial interface {{interfaceName}}
                      {
                          {{CaretRef.New(out var properties)}}
                      }

                      """);

                path = new Path(new PathPart(interfaceName.Substring(1), PathPartType.Interface));
                
                foreach (var subselection in selection.Children)
                {
                    if (subselection.Item is GraphQLSelectionGraphQLFieldSelection fieldSelection)
                    {
                        AddProperty(properties, path + new PathPart(fieldSelection.Name.Singularize(), PathPartType.Field, false), interfaceType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName is null ? null : interfaceName + " " + fieldSelection.Name.Singularize());
                    }
                    else if (subselection.Item is GraphQLSelectionGraphQLFragmentSpreadSelection fragmentSpreadSelection)
                    {
                        var fragment = metadata.Fragments!.FirstOrDefault(x =>
                            x.Name == fragmentSpreadSelection.FragmentName);
                        if (fragment is null)
                        {
                            Imports.Log(new LogEvent()
                            {
                                Level = LogEventLevel.Critical,
                                Message = "Cannot find fragment {FragmentName} (path {Path}",
                                Args = [fragmentSpreadSelection.FragmentName, path.ToString()],
                            });
                        }
                        else
                        {
                            var renested = fragment.DenestedSelections.Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, IGraphQLSelection>(x => x.Depth,
                                (item, children) => item.Item.Selection);
                            foreach (var subsubselection in renested)
                            {
                                AddProperty(properties, path, interfaceType, subsubselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
                            }
                        }
                    }
                    else if (subselection.Item is GraphQLSelectionGraphQLInlineFragmentSelection inlineFragmentSelection)
                    {
                        var inlineObjectType = metadata.ObjectTypes!.FirstOrDefault(x =>
                            x.Name == inlineFragmentSelection.TypeName);

                        if (inlineObjectType is null)
                        {
                            Imports.Log(new LogEvent()
                            {
                                Level = LogEventLevel.Critical,
                                Message = "Cannot find object type {ObjectType} (path {Path})",
                                Args = [inlineFragmentSelection.TypeName, path.ToString()],
                            });
                            throw new InvalidOperationException();
                        }

                        GraphQLClient.AddText(jsonDeriveTypes.Id,
                            $$"""
                            [JsonDerivedType(typeof({{(path + inlineObjectType.Name).Pascalize()}}), "{{inlineFragmentSelection.TypeName}}")]
                            
                            """);
                        
                        GraphQLClient.AddText(typeDefinitions.Id,
                            $$"""
                              public partial class {{(path + inlineObjectType.Name).Pascalize()}} : {{interfaceName}}
                              {
                                  {{CaretRef.New(out var innerProperties)}}
                              }

                              """);

                        foreach (var othersubselection in selection.Children)
                        {
                            if (othersubselection.Item is GraphQLSelectionGraphQLFieldSelection innerFieldSelection)
                            {
                                AddProperty(innerProperties, path + new PathPart(innerFieldSelection.Name.Singularize(), PathPartType.Field, false),
                                    inlineObjectType, othersubselection, metadata, jsonSerializerContextAttributes, typeDefinitions,
                                    typesWritten,
                                    null);
                            }
                        }
                        
                        foreach (var subsubselection in subselection.Children)
                        {
                            AddProperty(innerProperties, path, inlineObjectType, subsubselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, null);
                        }
                    }
                }
            }

            return new (interfaceName + "?", false);
        }
        
        GraphQLClient.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);

        return new ("object", false);
    }
    
    public static string GetVariableCSharpType(TypeRef type, out string? enumName, ParseGraphQLSchemaAndOperations metadata)
    {
        if (type.Name == "NotNull")
        {
            var result = GetVariableCSharpType(type.GenericArguments[0], out enumName, metadata);
            if (result.EndsWith("?"))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        if (type.Name == "List")
        {
            return $"List<{GetVariableCSharpType(type.GenericArguments[0], out enumName, metadata)}>?";
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

        if (type.Name == "Float")
        {
            enumName = null;
            return "double?";
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

        var interfaceType = metadata.InterfaceTypes.FirstOrDefault(x => x.Name == type.Name);
        if (interfaceType is not null)
        {
            enumName = null;
            return "I" + interfaceType.Name + "?";
        }
        
        GraphQLClient.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);
        
        enumName = null;
        return "object";
    }
}