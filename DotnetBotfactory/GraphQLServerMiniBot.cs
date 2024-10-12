using System.IO;
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
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/GraphQLServer.cs",
            $$""""
              using System;
              using System.Collections.Generic;
              using System.Linq;
              using System.Text.Json;
              using System.Text.Json.Serialization;
              using System.Runtime.Serialization;
              using CodegenBot;

              namespace {{rootNamespace}};

              {{CaretRef.New(out var jsonSerializerContextAttributes)}}
              public partial class GraphQLServerJsonSerializerContext : JsonSerializerContext
              {
              }

              public static partial class GraphQLServer
              {
              {{CaretRef.New(out var serverBody)}}
              }

              {{CaretRef.New(out var typeDefinitions)}}

              """");

         foreach (var objectType in parsedSchema.ObjectTypes)
         {
             GraphQLClient.AddText(jsonSerializerContextAttributes.Id,
                 $$"""
                 [JsonSerializable(typeof({{objectType.Name}}))]

                 """);

             GraphQLClient.AddText(typeDefinitions.Id,
                 $$"""

                 public partial class {{objectType.Name}}
                 {
                     {{CaretRef.New(out var body)}}
                 }

                 """);

             foreach (var field in objectType.Fields)
             {
                 var type = GraphQLCSharpTypes.GetVariableCSharpType(field.Type.Text.ToTypeRef(), out var enumName,
                     metadata);

                 if (!type.EndsWith("?"))
                 {
                     type = $"{type}?";
                 }

                 GraphQLClient.AddText(body.Id,
                     $$"""
                     public {{type}} {{field.Name.Pascalize()}} { get; set; }
                     
                     """);
             }
         }
    }
}
