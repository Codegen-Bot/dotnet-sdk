using System.Collections.Generic;
using CodegenBot;

namespace DotnetBotfactory;


public class Selection
{
    public ParseGraphQLSchemaAndOperationsOperationNestedSelectionFieldSelection? FieldSelection { get; set; }
    public ParseGraphQLSchemaAndOperationsOperationNestedSelectionFragmentSpreadSelection? FragmentSpreadSelection
    {
        get;
        set;
    }

    public List<Selection> Children { get; set; } = new();
}

public static class SelectionExtensions
{
    public static IReadOnlyList<Selection> ToSelections(
        this IEnumerable<ParseGraphQLSchemaAndOperationsOperationNestedSelection> selections)
    {
        var result = new List<Selection>();
        var stack = new Stack<Selection>();
        Selection? previousSelection = null;
        var previousDepth = 0;
        
        foreach (var selection in selections)
        {
            var convertedItem = new Selection()
            {
                FieldSelection = selection.FieldSelection,
                FragmentSpreadSelection = selection.FragmentSpreadSelection,
                Children = new List<Selection>(),
            };
            
            if (selection.Depth == previousDepth + 1)
            {
                stack.Push(previousSelection!);
            }
            
            while (selection.Depth < previousDepth)
            {
                stack.Pop();
                previousDepth--;
            }

            if (stack.Count == 0)
            {
                result.Add(convertedItem);
            }
            else
            {
                stack.Peek().Children.Add(convertedItem);
            }

            previousSelection = convertedItem;
            previousDepth = selection.Depth;
        }

        // foreach (var item in result)
        // {
        //     recursivelyLog(item);
        // }
        // 
        // void recursivelyLog(Selection selection, string depth = "")
        // {
        //     var name = selection.FieldSelection?.Name ?? selection.FragmentSpreadSelection!.Name;
        //     Imports.Log(new LogEvent()
        //     {
        //         Level = LogEventLevel.Information,
        //         Message = $"{depth} {name}",
        //         Args = [],
        //     });
        //
        //     foreach (var x in selection.Children)
        //     {
        //         recursivelyLog(x, depth + "  ");
        //     }
        // }
        
        return result;
    }
}