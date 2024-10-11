using System.Reflection.Metadata;
using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class GraphQLServerBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLOperations.GetConfiguration().Configuration;

        if (!configuration.ProvideApi)
        {
            return;
        }
        
        var rootNamespace = configuration.ProjectName.Replace("-", " ").Pascalize();

        var botSpec = GraphQLOperations.GetSchema($"{configuration.OutputPath}/bot.json").BotSpec;

        var schemaFilePath =  botSpec?.ProvidedSchemaPath;
        var schema = schemaFilePath is null ? null : GraphQLOperations.ReadTextFile(schemaFilePath)?.ReadTextFile;

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

        var parsedSchema = GraphQLOperations.ParseGraphQLSchema(schema).GraphQL;
        
        GraphQLOperations.AddFile($"{configuration.OutputPath}/GraphQLServer.cs",
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
         
         foreach (var objectType in parsedSchema.ObjectTypes ?? [])
         {
             if (objectType is null)
             {
                 continue;
             }
             
             GraphQLOperations.AddText(typeDefinitions.Id,
                 $$"""
                 
                 public partial class {{objectType.Name}}
                 
                 """);
         }
    }
}