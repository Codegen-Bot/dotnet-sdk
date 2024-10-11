using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class TypescriptBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLClient.GetConfiguration().Configuration;

        if (configuration.Language != DotnetLanguage.TYPESCRIPT)
        {
            return;
        }

        if (!configuration.Id.StartsWith("bot://hub/"))
        {
            GraphQLClient.Log(LogSeverity.WARNING, "Bot IDs must begin with {BotIdBeginning}",
                ["bot://hub/"]);
        }

        var rootNamespace = configuration.ProjectName.Replace("-", " ").Pascalize();

        GraphQLClient.AddKeyedTextByTags("", [new CaretTagInput() { Name = "location", Value = ".gitignore" }],
            """
            node_modules
            dist
            *.wasm
            
            """);
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/esbuild.js",
            """
            const esbuild = require('esbuild');
            
            esbuild
              .build({
                entryPoints: ['src/index.ts'],
                outdir: 'dist',
                bundle: true,
                sourcemap: true,
                minify: false, // might want to use true for production build
                format: 'cjs', // needs to be CJS for now
                target: ['es2020'] // don't go over es2020 because quickjs doesn't support it
              })
            """);

        GraphQLClient.AddFile($"{configuration.OutputPath}/package.json",
            $$"""
              {
                "name": "{{configuration.ProjectName}}",
                "version": "1.0.0",
                "description": "",
                "main": "src/index.ts",
                "scripts": {
                  "build": "node esbuild.js && extism-js dist/index.js -i src/index.d.ts -o dist/plugin.wasm"
                },
                "keywords": [],
                "author": "",
                "license": "BSD-3-Clause",
                "devDependencies": {
                  "esbuild": "^0.19.6",
                  "typescript": "^5.3.2"
                }
              }
              
              """);

        GraphQLClient.AddFile($"{configuration.OutputPath}/.graphqlrc.json",
            $$"""
              {
                "schema": ["schema.graphql", "configurationSchema.graphql"],
                "documents": "**/*.graphql"
              }
              
              """);

         GraphQLClient.AddFile($"{configuration.OutputPath}/configurationSchema.graphql",
           $$""""
             """
             This is a special type. A non-nullable field of this type called "configuration" will be
             inserted in the query root type, so that this bot can access its configuration values.

             This is where we put all configuration settings that are needed by this bot.
             This file can contain any number of types, but it is best to keep configuration simple
             and prefer convention over configuration. That helps keep bots easy to use, focused,
             and easy to refactor.
             """
             type Configuration {
                 """
                 It's best to add documentation strings for your fields, because they are displayed
                 when codegen.bot prompts the bot user for each value.
                 """
                 outputPath: String!
             {{CaretRef.New(new CaretTag("outputPath", configuration.OutputPath), new CaretTag("location", "configurationSchema.graphql/Configuration"))}}
             }

             """");

        GraphQLClient.AddFile($"{configuration.OutputPath}/bot.json",
            $$"""
              {
                "type": "wasm",
                "id": "{{configuration.Id}}",
                "readme": "Bot.md",
                "configurationSchema": "configurationSchema.graphql",
                "consumedSchema": "consumedSchema.graphql",
                "wasm": "bin/Release/net8.0/wasi-wasm/AppBundle/{{configuration.ProjectName}}.wasm",
                "deduplicateConfigurationSchema": true,
                "dependencies": {
                  "bot://core/output": "1.0.0",
                  "bot://core/filesystem": "1.0.0",
                  "bot://core/log": "1.0.0",
                  "bot://core/ready": "1.0.0"
                },
                "exec": {
                  "devenv": "dotnet workload install wasi-experimental",
                  "build": "dotnet build -c Release -r wasi-wasm",
                  "build-docker": "docker run -v .:/src codegenbot/dotnet-bot-builder:net8.0"
                }
              }
              """);

        GraphQLClient.AddFile($"{configuration.OutputPath}/ExampleMiniBot.cs",
            $$""""
              using System.Threading;
              using System.Threading.Tasks;

              namespace {{rootNamespace}};

              /// <summary>
              /// This is an example of a mini bot. When building a real bot, the first thing you should do is copy
              /// this example mini bot to create one or more non-example mini bots and put your bot code in those.
              /// </summary>
              public class ExampleMiniBot() : IMiniBot
              {
                  public void Execute()
                  {
                      // Here is where we make API requests to codegen.bot asking for details on the codebase
                      // or our configuration.
                      var configuration = GraphQLClient.GetConfiguration();
              
                      GraphQLClient.AddFile($"{configuration.Configuration.OutputPath}",
                          $$"""
                            This file was generated by a C# bot.
                      
                            """);
                  }
              }

              """");
        GraphQLClient.AddFile($"{configuration.OutputPath}/index.d.ts",
            $$""""
              declare module 'main' {
                  export function greet(): I32;
              }
              
              """");
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/operations.graphql",
            $$""""
              query GetConfiguration() {
                  configuration {
                      outputPath
              {{CaretRef.New(new CaretTag("outputPath", configuration.OutputPath), new CaretTag("location", "operations.graphql/GetConfiguration/configuration"))}}
                  }
              }

              """");

        GraphQLClient.AddFile($"{configuration.OutputPath}/defaultOperations.graphql",
            $$""""
              query GetFiles($whitelist: [String!]! $blacklist: [String!]!) {
                  files(whitelist: $whitelist blacklist: $blacklist) {
                      path
                      kind
                  }
              }
              
              query ReadTextFile($textFilePath: String!) {
                  readTextFile(textFilePath: $textFilePath)
              }
              
              mutation AddFile($filePath: String! $textAndCarets: String!) {
                  addFile(filePath: $filePath textAndCarets: $textAndCarets) {
                      id
                  }
              }
              
              mutation AddText($caretId: String! $textAndCarets: String!) {
                  addText(caretId: $caretId textAndCarets: $textAndCarets) {
                      id
                  }
              }
              
              mutation AddKeyedText($key: String! $caretId: String! $textAndCarets: String!) {
                  addKeyedText(key: $key caretId: $caretId textAndCarets: $textAndCarets) {
                      id
                  }
              }
              
              mutation AddTextByTags($tags: [CaretTagInput!]! $textAndCarets: String!) {
                  addTextByTags(tags: $tags textAndCarets: $textAndCarets) {
                      id
                  }
              }
              
              mutation AddKeyedTextByTags($key: String! $tags: [CaretTagInput!]! $textAndCarets: String!) {
                  addKeyedTextByTags(key: $key tags: $tags textAndCarets: $textAndCarets) {
                      id
                  }
              }
              
              mutation Log($severity: LogSeverity! $message: String! $arguments: [String!]) {
                  log(severity: $severity message: $message arguments: $arguments)
              }
              
              """");

        GraphQLClient.AddFile($"{configuration.OutputPath}/Imports.cs",
          $$""""
            using System;
            using System.Runtime.InteropServices;
            using System.Text.Json;
            using Extism;
            using CodegenBot;
            
            namespace {{rootNamespace}};
            
            /// <summary>
            /// This class contains all the static methods that we can call that codegen.bot implements. See also the Exports class,
            /// which contains static methods that we can implement that are called by codegen.bot.
            /// </summary>
            public class Imports
            {
                [DllImport("extism", EntryPoint = "cgb_graphql")]
                public static extern ulong ExternGraphQL(ulong offset);
            
                public static string GraphQL<T>(GraphQLRequest<T> request)
                {
                    var json = request.ToJsonString();
                    using var block = Pdk.Allocate(json);
                    var ptr = ExternGraphQL(block.Offset);
                    var response = MemoryBlock.Find(ptr).ReadString();
                    return response;
                }
            
                [DllImport("extism", EntryPoint = "cgb_log")]
                public static extern void ExternLog(ulong offset);
            
                public static void Log(LogEvent logEvent)
                {
                    var json = JsonSerializer.Serialize(logEvent, LogEventJsonContext.Default.LogEvent);
                    using var block = Pdk.Allocate(json);
                    ExternLog(block.Offset);
                }
            
                [DllImport("extism", EntryPoint = "cgb_random")]
                public static extern ulong ExternRandom(ulong offset);
                
                public static byte[] GetRandomBytes(int numBytes)
                {
                    var text = numBytes.ToString();
                    using var block = Pdk.Allocate(text);
                    var ptr = ExternRandom(block.Offset);
                    var response = MemoryBlock.Find(ptr).ReadBytes();
                    return response;
                }
            }
            
            """");

        CaretRef graphql;
        
        if (configuration.MinimalWorkingExample)
        {
                    GraphQLClient.AddFile($"{configuration.OutputPath}/Exports.cs",
          $$""""
            export function handle_request() {
              Host.outputString(`Hello, ${Host.inputString()}`)
            }
            
            export function entry_point() {
              
            }
            
            {{CaretRef.New(out graphql)}}
            
            """");
        }
        else
        {
            GraphQLClient.AddFile($"{configuration.OutputPath}/Exports.cs",
          $$""""
            export function handle_request() {
              Host.outputString(`Hello, ${Host.inputString()}`)
            }
            
            export function entry_point() {
              
            }
                
            {{CaretRef.New(out graphql)}}
            
            """");
        }

        if (configuration.ProvideApi)
        {
            GraphQLClient.AddText(graphql.Id,
                $$"""
                
                [UnmanagedCallersOnly(EntryPoint = "handle_request")]
                public static int HandleRequest()
                {
                    var request = Pdk.GetInputJson(SourceGenerationContext.Default.Add);
                    var sum = new Sum(parameters.a + parameters.b);
                    
                    return 0;
                }
                
                """);
            
            GraphQLClient.AddFile($"{configuration.OutputPath}/Query.cs",
                $$"""

                  namespace {{rootNamespace}};

                  public class Query
                  {
                      public string Hello(string name) => $"Hello, {name}!";
                  }

                  """);
        }

        GraphQLClient.AddFile($"{configuration.OutputPath}/README.md",
            $$"""
              # {{configuration.ProjectName}}
              
              This is the source code for the bot `{{configuration.Id}}`.
              
              ## How to build
              
              This bot can be built by installing the .NET SDK, then running these commands in the bot directory (the directory that contains `bot.json`):
              
              ```shell
              dotnet workload install wasi-experimental
              dotnet build -c Release -r wasi-wasm
              codegen.bot push
              ```
              
              Alternatively, you can use [a docker image specifically designed for building .NET bots](https://hub.docker.com/r/codegenbot/dotnet-bot-builder) like this:
              
              ```shell
              docker run -v .:/src codegenbot/dotnet-bot-builder:net8.0
              ```
              
              If the above docker container doesn't work, take a look at [the Dockerfile that builds that container](https://github.com/Codegen-Bot/dotnet-sdk/blob/master/CodegenBot.Builder/Dockerfile) for ideas.
              
              The above command specifically won't work if there are ProjectReferences in the bot's csproj file.
              
              """);

        GraphQLClient.AddFile($"{configuration.OutputPath}/Bot.md",
            $$"""
              Add overview here.
              
              """);
    }
}
