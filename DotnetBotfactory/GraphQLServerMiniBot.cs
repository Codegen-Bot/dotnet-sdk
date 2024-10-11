using System.Reflection.Metadata;
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
        var schema = schemaFilePath is null ? null : GraphQLClient.ReadTextFile(schemaFilePath)?.ReadTextFile;

        if (schema is null)
        {
            Imports.Log(new LogEvent()
            {
                Level = LogEventLevel.Critical,
                Message = "Cannot find providedSchema",
                Args = []
            });
            return;
        }

        var parsedSchema = GraphQLClient.ParseGraphQLSchema(schema).GraphQL;
        
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
             GraphQLClient.AddText(typeDefinitions.Id,
                 $$"""
                 
                 public partial class {{objectType.Name}}
                 {
                     {{CaretRef.New(out var body)}}
                 }
                 
                 """);

             foreach (var field in objectType.Fields)
             {
                 
             }
         }
    }
}