using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class CopyBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLOperations.GetConfiguration().Configuration;
        var copybots = configuration.Copybots ?? [];

        var fields = new HashSet<string>();
        
        foreach (var copybot in copybots)
        {
            foreach (var field in copybot.FieldDefinitions ?? [])
            {
                fields.Add(field.FieldName);
            }
            
            GraphQLOperations.AddFile($"{configuration.OutputPath}/{copybot.Name}.cs",
                $$""""
                  using Humanizer;
                  
                  namespace {{configuration.ProjectName}};
                  
                  public class {{copybot.Name}} : IMiniBot
                  {
                      public void Execute()
                      {
                          var configuration = GraphQLOperations.GetConfiguration().Configuration;
                          
                          {{CaretRef.New(out var execute, separator: "", indentation: "        ")}}
                      }
                  }
                  """");

            GraphQLOperations.AddTextByTags(
                [
                    new CaretTagInput() { Name = "outputPath", Value = configuration.OutputPath },
                    new CaretTagInput() { Name = "location", Value = "Exports.cs/miniBots" },
                ],
                $$"""
                new {{copybot.Name}}(),
                """
            );

            var files = GraphQLOperations.GetFiles(copybot.Whitelist ?? [], []).Files ?? [];

            if (files.Count == 0)
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Warning,
                    Message = "No files were found to generate a copybot from",
                    Args = [],
                });
            }

            files = files.Where(file => file.Path.StartsWith(copybot.InputDirectory)).ToList();

            if (files.Count == 0)
            {
                Imports.Log(new LogEvent()
                {
                    Level = LogEventLevel.Warning,
                    Message = "No files in inputDirectory were found to generate a copybot from",
                    Args = [],
                });
            }
            
            foreach(var file in files)
            {
                if (!file.Path.StartsWith(copybot.InputDirectory))
                {
                    continue;
                }
                
                if (file.Kind == FileKind.BINARY)
                {
                    Imports.Log(new LogEvent()
                    {
                        Level = LogEventLevel.Warning,
                        Message = "File {FilePath} is binary; skipping",
                        Args = [file.Path],
                    });
                    continue;
                }
                
                var relativePath = file.Path.Replace(copybot.InputDirectory, "");
                
                var fileContents = GraphQLOperations.ReadTextFile(file.Path);

                var dollarSigns = string.Join("", Enumerable.Repeat('$', Math.Max(MaxConsecutiveOccurrences(fileContents.ReadTextFile ?? "", '{', 1), MaxConsecutiveOccurrences(fileContents.ReadTextFile ?? "", '}', 1)) + 1));
                
                var doubleQuotes =
                    string.Join("", Enumerable.Repeat('"', MaxConsecutiveOccurrences(fileContents.ReadTextFile ?? "", '"', 3) + 1));

                var openBraces = string.Join("", Enumerable.Repeat("{", dollarSigns.Length));
                var closeBraces = string.Join("", Enumerable.Repeat("}", dollarSigns.Length));

                relativePath = MakeParametric(copybot, relativePath, "{", "}");
                
                GraphQLOperations.AddText(execute.Id,
                    $$"""
                    
                    GraphQLOperations.AddFile($"{configuration.OutputPath}/{{relativePath.Trim('/')}}",
                        {{dollarSigns}}{{doubleQuotes}}
                        {{CaretRef.New(out var fileContentsCaret, separator: "", indentation: "            " + string.Join("", Enumerable.Repeat(' ', dollarSigns.Length)))}}
                        {{doubleQuotes}});
                    
                    """);

                var fileContentsText = fileContents.ReadTextFile ?? "";

                fileContentsText = MakeParametric(copybot, fileContentsText, openBraces, closeBraces);

                GraphQLOperations.AddText(fileContentsCaret.Id, fileContentsText);
            }
        }

        foreach (var field in fields)
        {
            GraphQLOperations.AddTextByTags([
                    new CaretTagInput() { Name = "outputPath", Value = configuration.OutputPath },
                    new CaretTagInput() { Name = "location", Value = "configurationSchema.graphql/Configuration" },
                ],
                $"""
                     {field}: String!
                 
                 """);

            GraphQLOperations.AddTextByTags([
                    new CaretTagInput() { Name = "outputPath", Value = configuration.OutputPath },
                    new CaretTagInput() { Name = "location", Value = "operations.graphql/GetConfiguration/configuration" },
                ],
                $"""
                         {field}
                 
                 """);
        }
    }

    private static string MakeParametric(GetConfigurationCopybot copybot, string fileContentsText, string openBraces,
        string closeBraces)
    {
        foreach (var fieldDefinition in copybot.FieldDefinitions ?? [])
        {
            fileContentsText = fileContentsText.Replace(fieldDefinition.Needle, $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}{closeBraces}");
            var variants = fieldDefinition.Variants ?? [];
            foreach (var variant in variants)
            {
                switch (variant)
                {
                    case DotnetCopybotStringVariant.CamelCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.Camelize(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.Camelize(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.SnakeCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.Underscore(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.Underscore(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.UpperSnakeCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.Underscore().ToUpper(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.Underscore().ToUpper(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.LowerSnakeCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.Underscore().ToLower(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.Underscore().ToLower(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.KebabCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.Kebaberize(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.Kebaberize(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.UpperKebabCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.Kebaberize().ToUpper(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.Kebaberize().ToUpper(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.LowerKebabCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.Kebaberize().ToLower(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.Kebaberize().ToLower(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.LowerCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.ToLower(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.ToLower(){closeBraces}");
                        break;
                    case DotnetCopybotStringVariant.UpperCase:
                        fileContentsText = fileContentsText.Replace(fieldDefinition.Needle.ToUpper(), $"{openBraces}configuration.{fieldDefinition.FieldName.Pascalize()}.ToUpper(){closeBraces}");
                        break;
                    default:
                        Imports.Log(new LogEvent()
                        {
                            Level = LogEventLevel.Error,
                            Message = "Unknown variant type {Variant}",
                            Args = [variant.ToString()],
                        });
                        break;
                }
            }
        }

        return fileContentsText;
    }

    private int MaxConsecutiveOccurrences(string input, char searchChar, int min)
    {
        int maxCount = 0;
        int count = 0;

        foreach (char c in input)
        {
            if (c == searchChar)
            {
                count++;
                if (count > maxCount)
                {
                    maxCount = count;
                }
            }
            else
            {
                count = 0;
            }
        }

        return Math.Max(min, maxCount);
    }
}