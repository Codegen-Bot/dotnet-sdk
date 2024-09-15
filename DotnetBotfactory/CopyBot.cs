using System;
using System.Linq;
using CodegenBot;

namespace DotnetBotfactory;

public class CopyBot : IMiniBot
{
    public void Execute()
    {
        var configuration = GraphQLOperations.GetConfiguration().Configuration;
        var copybots = configuration.Copybots ?? [];

        foreach (var copybot in copybots)
        {
            GraphQLOperations.AddFile($"{configuration.OutputPath}/{copybot.Name}.cs",
                $$""""
                  namespace {{configuration.ProjectName}};
                  
                  public class {{copybot.Name}} : IMiniBot
                  {
                      public void Execute()
                      {
                          {{CaretRef.New(out var execute, separator: "", indentation: "        ")}}
                      }
                  }
                  """");

            var files = GraphQLOperations.GetFiles(copybot.Whitelist ?? [], []).Files ?? [];
            
            foreach(var file in files)
            {
                var relativePath = file.Path.Replace(copybot.InputDirectory, "");
                
                var fileContents = GraphQLOperations.GetFileContents(file.Path, null);

                var dollarSigns = string.Join("", Enumerable.Repeat('$', Math.Max(MaxConsecutiveOccurrences(fileContents.ReadTextFile ?? "", '{'), MaxConsecutiveOccurrences(fileContents.ReadTextFile ?? "", '}')) + 1));
                
                var doubleQuotes =
                    string.Join("", Enumerable.Repeat('"', MaxConsecutiveOccurrences(fileContents.ReadTextFile ?? "", '"') + 1));
                
                GraphQLOperations.AddText(execute.Id,
                    $$"""
                    
                    GraphQLOperations.AddFile($"{configuration.OutputPath}/{{relativePath}}",
                        {{dollarSigns}}{{doubleQuotes}}
                        {{CaretRef.New(out var fileContentsCaret, separator: "", indentation: "            ")}}
                        {{doubleQuotes}};
                    
                    """);

                GraphQLOperations.AddText(fileContentsCaret.Id, fileContents.ReadTextFile ?? "");
            }
        }
    }
    
    private int MaxConsecutiveOccurrences(string input, char searchChar)
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

        return maxCount;
    }
}