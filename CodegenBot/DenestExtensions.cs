namespace CodegenBot;

public class Denested<TItem>
{
    public int Depth { get; init; }
    public required TItem Item { get; init; }
}

public static class DenestExtensions
{
    public static IEnumerable<Denested<TItem>> Denest<TItem>(IEnumerable<TItem> items, Func<TItem, IEnumerable<TItem>> children)
    {
        return DenestInternal(items, children, 0);
    }

    private static IEnumerable<Denested<TItem>> DenestInternal<TItem>(this IEnumerable<TItem> items, Func<TItem, IEnumerable<TItem>> children, int depth)
    {
        foreach (var item in items)
        {
            yield return new Denested<TItem> { Depth = depth, Item = item };
            var childItems = children(item);
            if (childItems != null)
            {
                foreach (var denestedChild in DenestInternal(childItems, children, depth + 1))
                {
                    yield return denestedChild;
                }
            }
        }
    }
}