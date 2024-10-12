namespace CodegenBot;

public static class GraphQLTypeRefParser
{
    public static TypeRef ToTypeRef(this string str)
    {
        var typeRef = str.Trim();
        
        if (typeRef.EndsWith("!"))
        {
            return new TypeRef()
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
            return new TypeRef()
            {
                Name = "List",
                Text = typeRef,
                GenericArguments =
                [
                    typeRef.Substring(1, typeRef.Length - 2).Trim().ToTypeRef(),
                ]
            };
        }

        return new TypeRef()
        {
            Name = typeRef,
            Text = typeRef,
            GenericArguments = [],
        };
    }
}

public class TypeRef
{
    public required string Name { get; set; }
    public required string Text { get; set; }
    public required List<TypeRef> GenericArguments { get; set; }
}