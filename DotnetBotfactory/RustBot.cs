using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class RustBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLClient.GetConfiguration().Configuration;

        if (configuration.Language != DotnetLanguage.RUST)
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
            *.wasm
            
            """);
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/Cargo.toml",
            """
            [package]
            name = "botfactory"
            version = "0.1.0"
            edition = "2021"
            
            # See more keys and their definitions at https://doc.rust-lang.org/cargo/reference/manifest.html
            [lib]
            crate-type = ["cdylib"]
            
            [dependencies]
            extism-pdk = "1.1.0"
            serde = { version = "1.0.197", features = ["derive"] }
            serde_json = "1.0.128"
            
            """);

        GraphQLClient.AddFile($"{configuration.OutputPath}/src/imports.rs",
            """
            
            #[derive(Serialize, Deserialize, ToBytes, FromBytes)]
            #[encoding(Json)]
            struct GraphQLRequest {
                pub count: i32,
            }
            
            #[derive(Serialize, Deserialize, ToBytes, FromBytes)]
            #[encoding(Json)]
            struct GraphQLResponse {
                pub count: i32,
            }
            
            #[host_fn("extism:env/user")]
            extern "ExtismHost" {
              fn make_graphql_request(graphql_request: GraphQLRequest) -> GraphQLResponse;
              fn make_graphql_request(graphql_request: GraphQLRequest) -> GraphQLResponse;
            }
            
            """);
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/src/lib.rs",
            $$"""
              use extism_pdk::*;
              use serde::{Deserialize, Serialize};
              
              #[plugin_fn]
              pub fn handle_request(request: GraphQLRequest) -> FnResult<String> {
                  let name = "whatevs";
                  Ok(format!("Hello, {}!", name))
              }
              
              #[plugin_fn]
              pub fn entry_point() -> FnResult<i32> {
                  Ok(0)
              }
              
              """);

        GraphQLClient.AddFile($"{configuration.OutputPath}/src/lib.rs",
            $$"""
              use extism_pdk::*;
              use serde::{Deserialize, Serialize};

              #[derive(FromBytes, Serialize, Deserialize, Debug, PartialEq)]
              #[encoding(Json)]
              struct GraphQLRequest {
                  #[serde(rename = "query")]
                  query: String,
                  #[serde(rename = "operationName")]
                  operation_name: Option<String>,
                  #[serde(rename = "variables")]
                  variables: Option<std::collections::HashMap<String, Option<serde_json::Value>>>,
              }

              impl GraphQLRequest {
                  pub fn from_json_string(request_body: &str) -> serde_json::Result<Self> {
                      serde_json::from_str(request_body)
                  }
              
                  pub fn to_json_string(&self) -> serde_json::Result<String> {
                      serde_json::to_string_pretty(self)
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

        GraphQLClient.AddFile($"{configuration.OutputPath}/eventually_put_in_crate.rs",
            $$""""
              
              impl MiniBot for ExampleMiniBot {
                  fn execute(&self) {
                      let configuration = GraphQLClient::get_configuration();
              
                      GraphQLClient::add_file(
                          &format!("{}", configuration.output_path),
                          "This file was generated by a Rust bot.",
                      );
                  }
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

        CaretRef graphql, staticConstructor;
        
        if (configuration.MinimalWorkingExample)
        {
                    GraphQLClient.AddFile($"{configuration.OutputPath}/Exports.cs",
          $$""""
            using System;
            using System.Collections.Generic;
            using System.Runtime.InteropServices;
            using System.Threading;
            using CodegenBot;
            using Extism;
            
            namespace {{rootNamespace}};
            
            /// <summary>
            /// This class contains all the static methods that codegen.bot calls. See also the Imports class,
            /// which contains static methods that we can call from within a bot that are implemented by codegen.bot.
            /// </summary>
            public class Exports
            {
                public static void Main()
                {
                    // Note: a `Main` method is required for the app to compile
                }
                
                {{CaretRef.New(out staticConstructor)}}
                
                [UnmanagedCallersOnly(EntryPoint = "entry_point")]
                public static int Run()
                {
                    try
                    {
                        // Here is where we make API requests to codegen.bot asking for details on the codebase
                        // or our configuration.
                        var configuration = GraphQLClient.GetConfiguration().Configuration;
                        GraphQLClient.AddFile(configuration.OutputPath, "This file was created by a dotnet bot");
        
                        return 0;
                    }
                    catch (Exception e)
                    {
                        Imports.Log(new LogEvent()
                        {
                            // Only a critical error will cause codegen.bot to realize that the generated code should not be used 
                            Level = LogEventLevel.Critical,
                            Message = "Failed to execute bot: {ExceptionType} {Message}, {StackTrace}",
                            Args = [e.GetType().Name, e.Message, e.StackTrace ?? ""],
                        });
                        Pdk.SetError($"{e.GetType()}: {e.Message}");
                        return 0;
                    }
                }
                
                {{CaretRef.New(out graphql)}}
            }
            
            """");
        }
        else
        {
            GraphQLClient.AddFile($"{configuration.OutputPath}/Exports.cs",
          $$""""
            using System;
            using System.Collections.Generic;
            using System.Runtime.InteropServices;
            using System.Threading;
            using CodegenBot;
            using Extism;
            
            namespace {{rootNamespace}};
            
            /// <summary>
            /// This class contains all the static methods that codegen.bot calls. See also the Imports class,
            /// which contains static methods that we can call from within a bot that are implemented by codegen.bot.
            /// </summary>
            public class Exports
            {
                public static void Main()
                {
                    // Note: a `Main` method is required for the app to compile
                }
                                
                {{CaretRef.New(out staticConstructor)}}

                [UnmanagedCallersOnly(EntryPoint = "entry_point")]
                public static int Run()
                {
                    try
                    {
                        // Create all our minibots here
                        IMiniBot[] miniBots = [
                            // TODO - remove the ExampleMiniBot entry from this list because it creates a hello world file
                            // that won't be useful in real life, and could even be harmful if you're writing to configuration.OutputPath elsewhere,
                            // or if you're assuming configuration.OutputPath is a directory and you're writing to files under it.
                            new ExampleMiniBot(),
                            {{CaretRef.New(new CaretTag("outputPath", configuration.OutputPath), new CaretTag("location", "Exports.cs/miniBots"))}}
                        ];

                        // Run each minibot in order
                        foreach (var miniBot in miniBots)
                        {
                            try
                            {
                                miniBot.Execute();
                            }
                            catch (Exception e)
                            {
                                Imports.Log(new LogEvent()
                                {
                                    // Only a critical error will cause codegen.bot to realize that the generated code should not be used 
                                    Level = LogEventLevel.Critical,
                                    Message = "Failed to run minibot {MiniBot}: {ExceptionType} {Message}, {StackTrace}",
                                    Args = [miniBot.GetType().Name, e.GetType().Name, e.Message, e.StackTrace ?? ""],
                                });
                            }
                        }
            
                        return 0;
                    }
                    catch (Exception e)
                    {
                        Imports.Log(new LogEvent()
                        {
                            // Only a critical error will cause codegen.bot to realize that the generated code should not be used 
                            Level = LogEventLevel.Critical,
                            Message = "Failed to initialize bot: {ExceptionType} {Message}, {StackTrace}",
                            Args = [e.GetType().Name, e.Message, e.StackTrace ?? ""],
                        });
                        Pdk.SetError($"{e.GetType()}: {e.Message}");
                        return 0;
                    }
                }
                
                {{CaretRef.New(out graphql)}}
            }
            
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
            GraphQLClient.AddText(staticConstructor.Id,
                $$"""

                  private static GraphQLServer _graphqlServer;
                  
                  static Exports()
                  {
                      var services = new ServiceCollection();
                  
                      services.AddLogging(x => x.AddProvider(new CustomLoggerProvider()));
                      
                      // Register services here that will be injected into
                      // GraphQL servers, if needed
                      
                      services.AddGraphQL()
                          .AddQueryType<Query>(x => x.Name("Query"));
                      
                      var serviceProvider = services.BuildServiceProvider();
                      var requestExecutorResolver = serviceProvider.GetRequiredService<IRequestExecutorResolver>();
                      
                      _graphqlServer = new GraphQLServer(serviceProvider, requestExecutorResolver);
                  }

                  """);

            GraphQLClient.AddFile($"{configuration.OutputPath}/CustomLogger.cs",
                $$"""
                using System;
                using CodegenBot;
                using Microsoft.Extensions.Logging;
                
                namespace {{rootNamespace}};
                
                internal class CustomLoggerProvider : ILoggerProvider
                {
                    public ILogger CreateLogger(string categoryName)
                    {
                        return new CustomLogger();
                    }
                    
                    public void Dispose()
                    {
                    }
                }
                
                internal class CustomLogger() : ILogger
                {
                    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
                    {
                        return null;
                    }
                
                    public bool IsEnabled(LogLevel logLevel)
                    {
                        return true;
                    }
                
                    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
                    {
                        if (logLevel > LogLevel.Warning)
                        {
                            Imports.Log(new LogEvent()
                            {
                                Level = logLevel switch
                                {
                                    LogLevel.Trace => LogEventLevel.Trace,
                                    LogLevel.Debug => LogEventLevel.Debug,
                                    LogLevel.Information => LogEventLevel.Information,
                                    LogLevel.Warning => LogEventLevel.Warning,
                                    LogLevel.Error => LogEventLevel.Error,
                                    LogLevel.Critical => LogEventLevel.Critical,
                                    LogLevel.None => LogEventLevel.Information,
                                    _ => LogEventLevel.Information
                                },
                                Message = formatter(state, exception),
                                Args = Array.Empty<string>(),
                            });
                        }
                    }
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

        GraphQLClient.AddTextByTags(
            [
                new CaretTagInput() { Name = "location", Value = ".sln/Project" }
            ], $$"""
                 Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "{{configuration.ProjectName}}", "{{configuration.OutputPath}}\{{configuration.ProjectName}}.csproj", "{1AA5F22B-62F8-414F-AE50-635E99EB3F76}"
                 EndProject
                 
                 """);
        GraphQLClient.AddTextByTags(
            [
                new CaretTagInput() { Name = "location", Value = ".sln/ProjectConfigurationPlatforms" }
            ], $$"""
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|Any CPU.Build.0 = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|ARM64.ActiveCfg = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|ARM64.Build.0 = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|x64.ActiveCfg = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|x64.Build.0 = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|x86.ActiveCfg = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Debug|x86.Build.0 = Debug|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|Any CPU.ActiveCfg = Release|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|Any CPU.Build.0 = Release|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|ARM64.ActiveCfg = Release|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|ARM64.Build.0 = Release|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|x64.ActiveCfg = Release|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|x64.Build.0 = Release|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|x86.ActiveCfg = Release|Any CPU
                 {1AA5F22B-62F8-414F-AE50-635E99EB3F76}.Release|x86.Build.0 = Release|Any CPU
                 
                 """);

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
