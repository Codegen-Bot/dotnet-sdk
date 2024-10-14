using System;
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
    
    public static void AddSelectionText(CaretRef properties, string path, ParseGraphQLSchemaAndOperationsObjectType objectType, Renested<ISelection> selection, ParseGraphQLSchemaAndOperations metadata, CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions)
    {
        if (selection.Item.FieldSelection1 is not null)
        {
            var field = (objectType.Fields ?? []).FirstOrDefault(x => x.Name == selection.Item.FieldSelection1.Name);

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
                var type = GetSelectionType(path, selection, field.Type.Text.ToTypeRef(), metadata, jsonSerializerContextAttributes, typeDefinitions);

                GraphQLClient.AddText(properties.Id,
                    $$"""

                      [JsonPropertyName("{{selection.Item.FieldSelection1.Name}}")]
                      public {{GetIsRequired(type.Name)}} {{type.Name}} {{selection.Item.FieldSelection1.Name.Pascalize()}} { get; set; }

                      """);
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
                    AddSelectionText(properties, path, objectType, fragmentSelection, metadata, jsonSerializerContextAttributes, typeDefinitions);
                }
            }
        }
        else if (selection.Item.InlineFragmentSelection1 is not null)
        {
            
        }
    }
    
    public static SelectionType GetSelectionType(string path, Renested<ISelection> selection, TypeRef type, ParseGraphQLSchemaAndOperations metadata, CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions)
    {
        if (type.Name == "NotNull")
        {
            var x = GetSelectionType(path, selection, type.GenericArguments[0], metadata, jsonSerializerContextAttributes, typeDefinitions);
            var result = x.Name;
            if (result.EndsWith("?"))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return new (result, x.IsEnum);
        }

        if (type.Name == "List")
        {
            var b = GetSelectionType(path, selection, type.GenericArguments[0], metadata, jsonSerializerContextAttributes, typeDefinitions);
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
                  public partial class {{path.Pascalize()}}
                  {
                      {{CaretRef.New(out var properties)}}
                  }

                  """);
            
            foreach (var subselection in selection.Children)
            {
                if (subselection.Item.FieldSelection1 is not null)
                {
                    AddSelectionText(properties, path + " " + subselection.Item.FieldSelection1.Name.Singularize(), objectType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions);
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
                        throw new NotImplementedException("Nope");
                        // var renested = fragment.DenestedSelections.Renest(x => x.Depth,
                        //     (item, children) =>
                        //     {
                        //         return new Selection()
                        //         {
                        //             Item = new ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItem()
                        //             {
                        //                 FieldSelection = item.Item.FieldSelection,
                        //             },
                        //         };
                        //     });
                        // foreach (var subsubselection in renested)
                        // {
                        //     AddSelectionText(properties, path, objectType, subsubselection, metadata, jsonSerializerContextAttributes, typeDefinitions);
                        // }
                    }
                }
                else if (subselection.Item.InlineFragmentSelection1 is not null)
                {
                    
                }
            }

            return new (path.Pascalize() + "?", false);
        }

        GraphQLClient.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);

        return new ("???", false);
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

        GraphQLClient.Log(LogSeverity.ERROR, "Don't know how to process type {Type}", [type.Text]);
        
        enumName = null;
        return "???";
    }
}