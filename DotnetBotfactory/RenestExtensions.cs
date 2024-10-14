using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace DotnetBotfactory;

public record Renested<TItem>(TItem Item, ImmutableList<Renested<TItem>> Children);

public static class RenestExtensions
{
    private class RenestedBuilder<TItem, TValue>(TItem item, Func<TItem, ImmutableList<TValue>, TValue> valueFunc)
    {
        public TItem Item => item;
        public List<RenestedBuilder<TItem, TValue>> Children { get; } = new();

        public Renested<TValue> Build()
        {
            var children = Children.Select(x => x.Build()).ToImmutableList();
            
            return new Renested<TValue>(valueFunc(Item, children.Select(x => x.Item).ToImmutableList()), children);
        }
    }

    public static IReadOnlyList<Renested<TItem>> Renest<TItem>(this IEnumerable<TItem> denestedItems,
        Func<TItem, int> depthFunc)
    {
        return denestedItems.Renest<TItem, TItem>(depthFunc, (x, _) => x);
    }
    
    public static IReadOnlyList<Renested<TValue>> Renest<TItem, TValue>(this IEnumerable<TItem> denestedItems, Func<TItem, int> depthFunc, Func<TItem, ImmutableList<TValue>, TValue> valueFunc)
    {
        var result = new List<RenestedBuilder<TItem, TValue>>();
        var stack = new Stack<RenestedBuilder<TItem, TValue>>();
        RenestedBuilder<TItem, TValue>? previousSelection = null;
        var previousDepth = 0;
        
        foreach (var item in denestedItems)
        {
            var convertedItem = new RenestedBuilder<TItem, TValue>(item, valueFunc);
            
            var depth = depthFunc(item);
            
            if (depth == previousDepth + 1)
            {
                stack.Push(previousSelection!);
            }
            
            while (depth < previousDepth)
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
            previousDepth = depth;
        }

        return result.Select(x => x.Build()).ToImmutableList();
    }
}