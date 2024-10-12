using System.Collections.Generic;
using CodegenBot;

namespace CodegenBot;

public interface INestedSelection<TFieldSelection, TFragmentSpreadSelection>
{
    TFieldSelection? FieldSelection { get; set; }
    TFragmentSpreadSelection? FragmentSpreadSelection { get; set; }
    int Depth { get; set; }
}

public class Selection<TFieldSelection, TFragmentSpreadSelection>
{
    public TFieldSelection? FieldSelection { get; set; }
    public TFragmentSpreadSelection? FragmentSpreadSelection
    {
        get;
        set;
    }

    public List<Selection<TFieldSelection, TFragmentSpreadSelection>> Children { get; set; } = new();
}

public static class SelectionExtensions
{
    public static IReadOnlyList<Selection<TFieldSelection, TFragmentSpreadSelection>> ToSelections<TFieldSelection, TFragmentSpreadSelection>(
        this IEnumerable<INestedSelection<TFieldSelection, TFragmentSpreadSelection>> selections)
    {
        var result = new List<Selection<TFieldSelection, TFragmentSpreadSelection>>();
        var stack = new Stack<Selection<TFieldSelection, TFragmentSpreadSelection>>();
        Selection<TFieldSelection, TFragmentSpreadSelection>? previousSelection = null;
        var previousDepth = 0;
        
        foreach (var selection in selections)
        {
            var convertedItem = new Selection<TFieldSelection, TFragmentSpreadSelection>()
            {
                FieldSelection = selection.FieldSelection,
                FragmentSpreadSelection = selection.FragmentSpreadSelection,
                Children = new List<Selection<TFieldSelection, TFragmentSpreadSelection>>(),
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