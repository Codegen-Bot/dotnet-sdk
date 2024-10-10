using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodegenBot;

internal class BotSpec
{
    [JsonPropertyName("providedSchema")]
    public string? ProvidedSchema { get; set; }

}

[JsonSerializable(typeof(BotSpec))]
internal partial class BotSpecJsonSerializerContext : JsonSerializerContext
{
    
}

public static class ProvidedSchemaUtility
{
    /// <summary>
    /// Finds the bot.json file by looking in the specified directory and then its parent directory, grandparent, and so on;
    /// and once the bot.json file is found, reads the providedSchema field from it. That providedSchema field is a path
    /// to where we should write the GraphQL schema that this bot implements.
    /// </summary>
    /// <param name="directory">The directory to start searching in for bot.json. If null, defaults to the current working directory.</param>
    /// <returns>The absolute path to where we should write the providedSchema file, if bot.json is found and the providedSchema field is in it.</returns>
    public static string? CalculateProvidedSchemaPath(string? directory = null)
    {
        directory ??= Environment.CurrentDirectory;

        while (!File.Exists(Path.Combine(directory, "bot.json")))
        {
            directory = Path.GetDirectoryName(directory);
            if (directory is null)
            {
                Console.WriteLine("Can't find bot.json");
                return null;
            }
        }
        
        var botSpec = JsonSerializer.Deserialize(File.ReadAllText(Path.Combine(directory, "bot.json")), BotSpecJsonSerializerContext.Default.BotSpec);

        if (botSpec is null)
        {
            Console.WriteLine("Can't read bot.json");
            return null;
        }

        if (botSpec.ProvidedSchema is null)
        {
            Console.WriteLine("providedSchema is not specified in bot.json");
            return null;
        }
        
        var providedSchemaFilePath = Path.Combine(directory, botSpec.ProvidedSchema);
        return providedSchemaFilePath;
    }
}
