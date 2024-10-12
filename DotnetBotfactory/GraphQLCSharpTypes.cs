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
    
    public static void AddSelectionText(CaretRef properties, string path, ParseGraphQLSchemaAndOperationsObjectType objectType, Selection selection, ParseGraphQLSchemaAndOperations metadata, CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions)
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
                var type = GetSelectionType(path, selection, field.Type.Text.ToTypeRef(), metadata, jsonSerializerContextAttributes, typeDefinitions);

                GraphQLClient.AddText(properties.Id,
                    $$"""

                      [JsonPropertyName("{{selection.FieldSelection.Name}}")]
                      public {{GetIsRequired(type.Name)}} {{type.Name}} {{selection.FieldSelection.Name.Pascalize()}} { get; set; }

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
    
    public static SelectionType GetSelectionType(string path, Selection selection, TypeRef type, ParseGraphQLSchemaAndOperations metadata, CaretRef jsonSerializerContextAttributes, CaretRef typeDefinitions)
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
                  public class {{path.Pascalize()}}
                  {
                      {{CaretRef.New(out var properties)}}
                  }

                  """);
            
            foreach (var subselection in selection.Children)
            {
                var name = subselection.FieldSelection?.Name ?? subselection.FragmentSpreadSelection!.Name;
                name = ((string)name).Singularize();
                AddSelectionText(properties, $"{path} {name}", objectType, subselection, metadata, jsonSerializerContextAttributes, typeDefinitions);
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