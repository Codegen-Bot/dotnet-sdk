using System.Threading;
using System.Threading.Tasks;
using CodegenBot;

namespace DotnetBotfactory;

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

        GraphQLClient.AddFile(
            $"{configuration.Configuration.OutputPath}",
            $$"""
            This file was generated by a C# bot.
            
            """
        );
    }
}
