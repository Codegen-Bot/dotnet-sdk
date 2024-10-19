namespace CodegenBot;

public static class GraphQLTypeRefParser
{
    public static GraphQLTypeRef ToTypeRef(this string str)
    {
        var typeRef = str.Trim();
        
        if (typeRef.EndsWith("!"))
        {
            return new GraphQLTypeRef()
            {
                Name = "NotNull",
                Text = typeRef,
                GenericArguments =
                [
                    typeRef.Substring(0, typeRef.Length - 1).Trim().ToTypeRef(),
                ]
            };
        }

        if (typeRef.StartsWith("[") && typeRef.EndsWith("]"))
        {
            return new GraphQLTypeRef()
            {
                Name = "List",
                Text = typeRef,
                GenericArguments =
                [
                    typeRef.Substring(1, typeRef.Length - 2).Trim().ToTypeRef(),
                ]
            };
        }

        return new GraphQLTypeRef()
        {
            Name = typeRef,
            Text = typeRef,
            GenericArguments = [],
        };
    }
}

public class GraphQLTypeRef
{
    public GraphQLTypeRef Skip(Func<GraphQLTypeRef, bool> predicate)
    {
        if (GenericArguments.Count > 0 && predicate(this))
        {
            return GenericArguments[0].Skip(predicate);
        }

        return this;
    }
    
    public required string Name { get; set; }
    public required string Text { get; set; }
    public required List<GraphQLTypeRef> GenericArguments { get; set; }
}