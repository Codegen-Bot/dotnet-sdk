using System;
using System.Collections.Generic;
using System.Linq;
using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

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
    
    public static void AddProperty(CaretRef properties, string path, IObjectOrInterface objectType,
        Renested<ISelection> selection, ParseGraphQLSchemaAndOperations metadata,
        CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions, HashSet<string> typesWritten,
        string? interfaceName, CaretRef? jsonDerivedType = null)
    {
        if (selection.Item.FieldSelection1 is not null)
        {
            var field = (objectType.Fields1 ?? []).FirstOrDefault(x => x.Name1 == selection.Item.FieldSelection1.Name);

            if (selection.Item.FieldSelection1.Name is "__typename")
            {
                field = new ParseGraphQLSchemaAndOperationsObjectTypeField()
                {
                    Name = "__typename",
                    Parameters = [],
                    Type = new ParseGraphQLSchemaAndOperationsObjectTypeFieldType()
                    {
                        Text = "String!"
                    },
                };
            }
            
            if (field is null)
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Error, Message = "Cannot find field {Field}",
                    Args = [selection.Item.FieldSelection1.Name]
                });
            }
            else
            {
                var type = GetSelectionType(path, selection, field.Type1.Text1.ToTypeRef(), metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);

                if (objectType is ParseGraphQLSchemaAndOperationsInterfaceType)
                {
                    GraphQLClient.AddText(properties.Id,
                        $$"""

                          {{type.Name}} {{selection.Item.FieldSelection1.Name.Pascalize()}} { get; set; }

                          """);
                }
                else
                {
                    GraphQLClient.AddText(properties.Id,
                        $$"""

                          [JsonPropertyName("{{selection.Item.FieldSelection1.Name}}")]
                          public {{GetIsRequired(type.Name)}} {{type.Name}} {{selection.Item.FieldSelection1.Name.Pascalize()}} { get; set; }

                          """);
                }
            }
        }
        else if (selection.Item.FragmentSpreadSelection1 is not null)
        {
            var fragment = metadata.Fragments.FirstOrDefault(x => x.Name == selection.Item.FragmentSpreadSelection1.FragmentName);
            
            if (fragment is null)
            {
                Imports.Log(new LogEvent() { Level = LogEventLevel.Error, Message = "Cannot find fragment {Fragment}", Args = [selection.Item.FragmentSpreadSelection1.FragmentName] });
            }
            else
            {
                var renestedSelections = fragment.DenestedSelections
                    .Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, ISelection>(x => x.Depth, (x, _) => x);
                foreach (var fragmentSelection in renestedSelections)
                {
                    AddProperty(properties, path, objectType, fragmentSelection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
                }
            }
        }
        else if (selection.Item.InlineFragmentSelection1 is not null)
        {
            
        }
    }
    
    public static SelectionType GetSelectionType(string path, Renested<ISelection> selection, TypeRef type, ParseGraphQLSchemaAndOperations metadata, CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions, HashSet<string> typesWritten, string? interfaceName)
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
                 [JsonSerializable(typeof({path.Pascalize()}))]

                 """);

            GraphQLClient.AddText(typeDefinitions.Id,
                $$"""
                  public partial class {{path.Pascalize()}}{{CaretRef.New(out var extends)}}
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
                if (subselection.Item.FieldSelection1 is not null)
                {
                    AddProperty(properties, path + " " + subselection.Item.FieldSelection1.Name.Singularize(), objectType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName is null ? null : interfaceName + " " + subselection.Item.FieldSelection1.Name.Singularize());
                }
                else if (subselection.Item.FragmentSpreadSelection1 is not null)
                {
                    var fragment = metadata.Fragments.FirstOrDefault(x =>
                        x.Name == subselection.Item.FragmentSpreadSelection1.FragmentName);
                    if (fragment is null)
                    {
                        Imports.Log(new LogEvent()
                        {
                            Level = LogEventLevel.Critical,
                            Message = "Cannot find fragment {FragmentName} (path {Path}",
                            Args = [subselection.Item.FragmentSpreadSelection1.FragmentName, path],
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
                        
                        var renested = fragment.DenestedSelections.Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, ISelection>(x => x.Depth,
                            (item, children) => item);
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

            return new (path.Pascalize() + "?", false);
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
                    Args = [fragmentType.TypeCondition, fragmentType.Name, path],
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
                  public partial class {{path.Pascalize()}} : {{interfaceName}}
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
                if (subselection.Item.FieldSelection1 is not null)
                {
                    AddProperty(properties, path + " " + subselection.Item.FieldSelection1.Name.Singularize(), outerFragmentObjectType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName is null ? null : interfaceName + " " + subselection.Item.FieldSelection1.Name.Singularize());
                }
                else if (subselection.Item.FragmentSpreadSelection1 is not null)
                {
                    var fragment = metadata.Fragments!.FirstOrDefault(x =>
                        x.Name == subselection.Item.FragmentSpreadSelection1.FragmentName);
                    if (fragment is null)
                    {
                        Imports.Log(new LogEvent()
                        {
                            Level = LogEventLevel.Critical,
                            Message = "Cannot find fragment {FragmentName} (path {Path}",
                            Args = [subselection.Item.FragmentSpreadSelection1.FragmentName, path],
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
                                Args = [fragment.TypeCondition, subselection.Item.FragmentSpreadSelection1.FragmentName, path],
                            });
                        }
                        else
                        {
                            var renested = fragment.DenestedSelections.Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, ISelection>(x => x.Depth,
                                (item, children) => item);
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
                      [JsonPolymorphic]
                      {{CaretRef.New(out var jsonDeriveTypes)}}
                      public partial interface {{interfaceName}}
                      {
                          {{CaretRef.New(out var properties)}}
                      }

                      """);

                path = interfaceName.Substring(1);
                
                foreach (var subselection in selection.Children)
                {
                    if (subselection.Item.FieldSelection1 is not null)
                    {
                        AddProperty(properties, path + " " + subselection.Item.FieldSelection1.Name.Singularize(), interfaceType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName is null ? null : interfaceName + " " + subselection.Item.FieldSelection1.Name.Singularize());
                    }
                    else if (subselection.Item.FragmentSpreadSelection1 is not null)
                    {
                        var fragment = metadata.Fragments!.FirstOrDefault(x =>
                            x.Name == subselection.Item.FragmentSpreadSelection1.FragmentName);
                        if (fragment is null)
                        {
                            Imports.Log(new LogEvent()
                            {
                                Level = LogEventLevel.Critical,
                                Message = "Cannot find fragment {FragmentName} (path {Path}",
                                Args = [subselection.Item.FragmentSpreadSelection1.FragmentName, path],
                            });
                        }
                        else
                        {
                            var renested = fragment.DenestedSelections.Renest<ParseGraphQLSchemaAndOperationsFragmentDenestedSelection, ISelection>(x => x.Depth,
                                (item, children) => item);
                            foreach (var subsubselection in renested)
                            {
                                AddProperty(properties, path, interfaceType, subsubselection, metadata, jsonSerializerContextAttributes, typeDefinitions, typesWritten, interfaceName);
                            }
                        }
                    }
                    else if (subselection.Item.InlineFragmentSelection1 is not null)
                    {
                        var inlineObjectType = metadata.ObjectTypes!.FirstOrDefault(x =>
                            x.Name == subselection.Item.InlineFragmentSelection1.TypeName);

                        if (inlineObjectType is null)
                        {
                            Imports.Log(new LogEvent()
                            {
                                Level = LogEventLevel.Critical,
                                Message = "Cannot find object type {ObjectType} (path {Path})",
                                Args = [subselection.Item.InlineFragmentSelection1.TypeName, path],
                            });
                            throw new InvalidOperationException();
                        }

                        GraphQLClient.AddText(jsonDeriveTypes.Id,
                            $$"""
                            [JsonDerivedType(typeof({{(path + inlineObjectType.Name).Pascalize()}}), "{{subselection.Item.InlineFragmentSelection1.TypeName}}")]
                            
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
                            if (othersubselection.Item.FieldSelection1 is not null)
                            {
                                AddProperty(innerProperties, path + " " + othersubselection.Item.FieldSelection1.Name.Singularize(),
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