using CodegenBot;
using Humanizer;

namespace DotnetBotfactory;

public class CSharpBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLOperations.GetConfiguration().Configuration;

        if (!configuration.Id.StartsWith("bot://hub/"))
        {
            GraphQLOperations.Log(LogSeverity.WARNING, "Bot IDs must begin with {BotIdBeginning}",
                ["bot://hub/"]);
        }

        var rootNamespace = configuration.ProjectName.Replace("-", " ").Pascalize();

        GraphQLOperations.AddKeyedTextByTags("", [new CaretTagInput() { Name = "location", Value = ".gitignore" }],
            """
            **/bin/**/*
            **/obj/**/*
            **/.idea/**/*
            *.wasm
            
            """);
        
        GraphQLOperations.AddFile($"{configuration.OutputPath}/global.json",
            """
            {
              "sdk": {
                "version": "8.0.300",
                "rollForward": "latestFeature"
              }
            }
            """);

        GraphQLOperations.AddFile($"{configuration.OutputPath}/{configuration.ProjectName}.csproj",
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
                  <PackageReference Include="CodegenBot" Version="1.1.0-alpha.89" />
                  {{CaretRef.New(out var packageRefs)}}
                </ItemGroup>
              </Project>
              """);

        // The root namespace defaults to the project name, so if they're the same then don't specify it explicity.
        if (rootNamespace != configuration.ProjectName)
        {
            GraphQLOperations.AddText(rootNamespaceCaret.Id,
                $$"""
                <RootNamespace>{{rootNamespace}}</RootNamespace>
                """);
        }

        GraphQLOperations.AddFile($"{configuration.OutputPath}/Properties/AssemblyInfo.cs",
            $$"""
              [assembly:System.Runtime.Versioning.SupportedOSPlatform("wasi")]

              """);
        
        GraphQLOperations.AddFile($"{configuration.OutputPath}/.graphqlrc.json",
            $$"""
              {
                "schema": ["schema.graphql", "configurationSchema.graphql"],
                "documents": "**/*.graphql"
              }
              
              """);

         GraphQLOperations.AddFile($"{configuration.OutputPath}/configurationSchema.graphql",
           $$""""
             # This is where we put all configuration settings that are needed by this bot.
             # This file can contain any number of types, but it is best to keep configuration simple
             # and prefer convention over configuration. That helps keep bots easy to use, focused,
             # and easy to refactor.
             
             # This is a special type. A non-nullable field of this type called "configuration" will be
             # inserted in the query root type, so that this bot can access its configuration values.
             type Configuration {
                 """
                 It's best to add documentation strings for your fields, because they are displayed
                 when codegen.bot prompts the bot user for each value.
                 """
                 outputPath: String!
             }

             """");

        GraphQLOperations.AddFile($"{configuration.OutputPath}/bot.json",
            $$"""
              {
                "type": "wasm",
                "id": "{{configuration.Id}}",
                "readme": "Bot.md",
                "configurationSchema": "configurationSchema.graphql",
                "dependenciesSchema": "schema.graphql",
                "wasm": "{{configuration.ProjectName}}.wasm",
                "deduplicateConfigurationSchema": true,
                "dependencies": {
                  "bot://core/output": "1.0.0",
                  "bot://core/filesystem": "1.0.0",
                  "bot://core/log": "1.0.0"
                }
              }
              """);

        if (!configuration.MinimalWorkingExample)
        {
            GraphQLOperations.AddFile($"{configuration.OutputPath}/ExampleMiniBot.cs",
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
                          var configuration = GraphQLOperations.GetConfiguration();
                  
                          GraphQLOperations.AddFile($"{configuration.Configuration.OutputPath}",
                              $$"""
                                This file was generated by a C# bot.
                          
                                """);
                      }
                  }

                  """");
            GraphQLOperations.AddFile($"{configuration.OutputPath}/IMiniBot.cs",
                $$""""
                  using System.Threading;
                  using System.Threading.Tasks;

                  namespace {{rootNamespace}};

                  /// <summary>
                  /// New bots include the concept of a mini bot. You can put all your bot code in one or more mini bots.
                  /// Mini bots can be moved from one bot's code to another, making it easy to refactor bots.
                  /// </summary>
                  public interface IMiniBot
                  {
                      void Execute();
                  }

                  """");
        }
        
        GraphQLOperations.AddFile($"{configuration.OutputPath}/operations.graphql",
            $$""""
              query GetConfiguration() {
                  configuration {
                      outputPath
                  }
              }

              """");

        GraphQLOperations.AddFile($"{configuration.OutputPath}/defaultOperations.graphql",
            $$""""
              query GetFiles($whitelist: [String!]! $blacklist: [String!]!) {
                  files(whitelist: $whitelist blacklist: $blacklist) {
                      path
                      kind
                  }
              }
              
              query GetFileContents($textFilePath: String!) {
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

        GraphQLOperations.AddFile($"{configuration.OutputPath}/Imports.cs",
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

        if (configuration.MinimalWorkingExample)
        {
                    GraphQLOperations.AddFile($"{configuration.OutputPath}/Exports.cs",
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
                
                [UnmanagedCallersOnly(EntryPoint = "entry_point")]
                public static int Run()
                {
                    try
                    {
                        // Here is where we make API requests to codegen.bot asking for details on the codebase
                        // or our configuration.
                        var configuration = GraphQLOperations.GetConfiguration().Configuration;
                        GraphQLOperations.AddFile(configuration.OutputPath, "This file was created by a dotnet bot");
        
                        return 0;
                    }
                    catch (Exception e)
                    {
                        Imports.Log(new LogEvent()
                        {
                            // Only a critical error will cause codegen.bot to realize that the generated code should not be used 
                            Level = LogEventLevel.Critical,
                            Message = "Failed to execute bot: {ExceptionType} {Message}, {StackTrace}",
                            Args = [e.GetType().Name, e.Message, e.StackTrace],
                        });
                        Pdk.SetError($"{e.GetType()}: {e.Message}");
                        return 0;
                    }
                }
            }
            
            """");
        }
        else
        {
            GraphQLOperations.AddFile($"{configuration.OutputPath}/Exports.cs",
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
                
                [UnmanagedCallersOnly(EntryPoint = "entry_point")]
                public static int Run()
                {
                    try
                    {
                        // Create all our minibots here
                        IMiniBot[] miniBots = [
                            // TODO - remove the ExampleMiniBot entry from this list because it creates a hello world file
                            // that won't be useful in real life.
                            new ExampleMiniBot(),
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
                                    Args = [miniBot.GetType().Name, e.GetType().Name, e.Message, e.StackTrace],
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
                            Args = [e.GetType().Name, e.Message, e.StackTrace],
                        });
                        Pdk.SetError($"{e.GetType()}: {e.Message}");
                        return 0;
                    }
                }
            }
            
            """");
        }

        GraphQLOperations.AddTextByTags(
            [
                new CaretTagInput() { Name = "location", Value = ".sln/Project" }
            ], $$"""
                 Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "{{configuration.ProjectName}}", "{{configuration.OutputPath}}\{{configuration.ProjectName}}.csproj", "{1AA5F22B-62F8-414F-AE50-635E99EB3F76}"
                 EndProject
                 
                 """);
        GraphQLOperations.AddTextByTags(
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

        GraphQLOperations.AddFile($"{configuration.OutputPath}/README.md",
            $$"""
              # {{configuration.ProjectName}}
              
              This is the source code for the bot `{{configuration.Id}}`.
              
              ## How to build
              
              This bot can be built by installing the .NET SDK, then running these commands in the bot directory (the directory that contains `bot.json`):
              
              ```shell
              dotnet workload install wasi-experimental
              dotnet build -c Release
              codegen.bot push
              ```
              
              Alternatively, you can use [a docker image specifically designed for building .NET bots](https://hub.docker.com/r/codegenbot/dotnet-bot-builder) like this:
              
              ```shell
              docker run -v .:/src codegenbot/dotnet-bot-builder:net8.0
              ```
              
              If the above docker container doesn't work, take a look at [the Dockerfile that builds that container](https://github.com/Codegen-Bot/dotnet-sdk/blob/master/CodegenBot.Builder/Dockerfile) for ideas.
              
              """);

        GraphQLOperations.AddFile($"{configuration.OutputPath}/Bot.md",
            $$"""
              Add overview here.
              
              """);
    }
}
