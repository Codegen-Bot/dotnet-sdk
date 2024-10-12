using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class CSharpBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLClient.GetConfiguration().Configuration;

        if (configuration.Language != DotnetLanguage.CSHARP)
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
            **/bin
            **/obj
            **/.idea
            *.wasm
            *.sln.DotSettings.user
            
            """);
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/global.json",
            """
            {
              "sdk": {
                "version": "8.0.300",
                "rollForward": "latestFeature"
              }
            }
            """);

        GraphQLClient.AddFile($"{configuration.OutputPath}/{configuration.ProjectName}.csproj",
            $$"""
              <Project Sdk="Microsoft.NET.Sdk">
                <PropertyGroup>
                  <TargetFramework>net8.0</TargetFramework>
                  <!-- This is commented out because otherwise your IDE will be full of red squiggly lines. -->
                  <!-- The build process using the codegenbot docker container should still work -->
                  <!-- because it explicitly specifies the runtime when it builds the bot. -->
                  <!--<RuntimeIdentifier>wasi-wasm</RuntimeIdentifier>-->
                  <OutputType>Exe</OutputType>
                  <PublishTrimmed>true</PublishTrimmed>
                  <!-- WASM bots can be difficult to debug, so it's better ot just have nullable enabled -->
                  <!-- and treat warnings as errors from the beginning. It makes your life easier in the future. -->
                  <Nullable>enable</Nullable>
                  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
                  {{CaretRef.New(out var rootNamespaceCaret)}}
                </PropertyGroup>
                <ItemGroup>
                  <PackageReference Include="Extism.Pdk" Version="1.0.3" />
                  <PackageReference Include="CodegenBot" Version="1.1.0-alpha.163" />
                  <!-- This is used by the GraphQL client to properly serialize enums -->
                  <PackageReference Include="Macross.Json.Extensions" Version="3.0.0" />
                  <PackageReference Include="Humanizer" Version="2.14.1" />
                  {{CaretRef.New(out var packageRefs)}}
                </ItemGroup>
              </Project>
              """);

        // The root namespace defaults to the project name, so if they're the same then don't specify it explicity.
        if (rootNamespace != configuration.ProjectName)
        {
            GraphQLClient.AddText(rootNamespaceCaret.Id,
                $$"""
                <RootNamespace>{{rootNamespace}}</RootNamespace>
                """);
        }

        GraphQLClient.AddFile($"{configuration.OutputPath}/Properties/AssemblyInfo.cs",
            $$"""
              [assembly:System.Runtime.Versioning.SupportedOSPlatform("wasi")]

              """);
        
        GraphQLClient.AddFile($"{configuration.OutputPath}/.graphqlrc.json",
            $$"""
              {
                "schema": ["consumedSchema.graphql", "configurationSchema.graphql"],
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
                {{CaretRef.New(out var botJsonFields)}}
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
                  {{CaretRef.New(out var botJsonExecs)}}
                }
              }
              """);

        if (!configuration.MinimalWorkingExample)
        {
            GraphQLClient.AddFile($"{configuration.OutputPath}/ExampleMiniBot.cs",
                $$""""
                  using System.Threading;
                  using System.Threading.Tasks;
                  using CodegenBot;

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
            
//             GraphQLClient.AddFile($"{configuration.OutputPath}/IMiniBot.cs",
//                 $$""""
//                   using System.Threading;
//                   using System.Threading.Tasks;
//
//                   namespace {{rootNamespace}};
//
//                   /// <summary>
//                   /// New bots include the concept of a mini bot. You can put all your bot code in one or more mini bots.
//                   /// Mini bots can be moved from one bot's code to another, making it easy to refactor bots.
//                   /// </summary>
//                   public interface IMiniBot
//                   {
//                       void Execute();
//                   }
//
//                   """");
        }
        
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

        CaretRef graphql, staticConstructor, exportsUsings, mainBody;
        
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
            {{CaretRef.New(out exportsUsings)}}
            
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
                    {{CaretRef.New(out mainBody)}}
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
            {{CaretRef.New(out exportsUsings)}}
            
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
                    {{CaretRef.New(out mainBody)}}
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
            GraphQLClient.AddText(botJsonFields.Id,
                """
                "providedSchema": "providedSchema.graphql",
                """);

            GraphQLClient.AddText(botJsonExecs.Id,
                """
                , "schema": "dotnet run"
                """);
            
            GraphQLClient.AddText(packageRefs.Id,
                """
                <PackageReference Include="Microsoft.Extensions.Logging" Version="8.0.0" />
                
                """);
            
            GraphQLClient.AddText(mainBody.Id,
                """
                // However this is also used to export the GraphQL schema
                
                var schema = _graphqlServer.GetGraphQLSchema();
                
                var providedSchemaFilePath = ProvidedSchemaUtility.CalculateProvidedSchemaPath();
                
                if (providedSchemaFilePath is null)
                {
                    return;
                }
                
                Console.WriteLine($"Writing provided schema to {providedSchemaFilePath}");
                File.WriteAllText(providedSchemaFilePath, schema);
                """);
            
            GraphQLClient.AddText(exportsUsings.Id,
                """
                using Microsoft.Extensions.DependencyInjection;
                using Microsoft.Extensions.Logging;
                using HotChocolate.Execution;
                using System.IO;
                
                """);

            GraphQLClient.AddText(graphql.Id,
                $$"""
                
                [UnmanagedCallersOnly(EntryPoint = "handle_request")]
                public static int HandleRequest()
                {
                    try
                    {
                        var request = Pdk.GetInputString();
                    
                        var result = _graphqlServer.Execute(request, _serviceProvider);
                    
                        Pdk.SetOutput(result);
                    
                        return 0;
                    }
                    catch (Exception e)
                    {
                        Imports.Log(
                            new LogEvent()
                            {
                                // Only a critical error will cause codegen.bot to realize that the generated code should not be used
                                Level = LogEventLevel.Critical,
                                Message = "Failed to handle GraphQL request: {ExceptionType} {Message}, {StackTrace}",
                                Args = [e.GetType().Name, e.Message, e.StackTrace ?? ""],
                            }
                        );
                        Pdk.SetError($"{e.GetType()}: {e.Message}");
                        return 0;
                    }
                }
                
                """);
            
            GraphQLClient.AddText(staticConstructor.Id,
                $$"""

                  private static GraphQLServer _graphqlServer;
                  private static IServiceProvider _serviceProvider;
                  
                  static Exports()
                  {
                      var services = new ServiceCollection();
                  
                      services.AddLogging(x => x.AddProvider(new CustomLoggerProvider()));
                      
                      // Register services here that will be injected into
                      // GraphQL servers, if needed
                      
                      services.AddGraphQL()
                          .AddQueryType<Query>(x => x.Name("Query"));
                      
                      _serviceProvider = services.BuildServiceProvider();
                      var requestExecutorResolver =
                          _serviceProvider.GetRequiredService<IRequestExecutorResolver>();
                      
                      _graphqlServer = new GraphQLServer(_serviceProvider, requestExecutorResolver);
                  }

                  """);
        }

        //var projectGuid = 
        
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
              
              This bot can be built by doing the following:
              
              1. Download  [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)
              2. Download and unzip [WASI SDK]( )
              3. Set the `WASI_SDK_PATH` environment variable to point to the unzipped WASI SDK
              4. Run these commands in the bot directory (the directory that contains `bot.json`):
              
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
