using System.Collections.Generic;
using System.Collections.Immutable;
using HotChocolate.Types;

namespace DotnetBotfactory;

public class GraphQLMetadata
{
    public List<GraphQLOperation> Operations { get; set; } = new();
    public List<GraphQLObjectType> ObjectTypes { get; set; } = new();
    public List<GraphQLInputObjectType> InputObjectTypes { get; set; } = new();
    public List<GraphQLInterfaceType> InterfaceTypes { get; set; } = new();
    public List<GraphQLScalarType> ScalarTypes { get; set; } = new();
    public List<GraphQLFragment> Fragments { get; set; } = new();
    public List<GraphQLDirectiveType> Directives { get; set; } = new();
    public List<GraphQLEnumeration> Enumerations { get; set; } = new();
}

public class GraphQLDirectiveType
{
    public required string Name { get; set; }
    public List<GraphQLArgument> Arguments { get; set; } = new();
    public bool IsRepeatable { get; set; }
}

public class GraphQLObjectType : IDirectives
{
    public required string Name { get; set; }
    public List<GraphQLField> Fields { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
}

public class GraphQLInterfaceType : IDirectives
{
    public required string Name { get; set; }
    public List<GraphQLField> Fields { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
}

public class GraphQLInputObjectType : IDirectives
{
    public required string Name { get; set; }
    public List<GraphQLInputField> Fields { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
}

public class GraphQLOperation : IDirectives, ISelections
{
    public required string? Name { get; set; }
    public required string Text { get; set; }
    public GraphQLOperationType OperationType { get; set; }
    public List<IGraphQLSelection> Selections { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
    public List<GraphQLVariable> Variables { get; set; } = new();
}

public class GraphQLVariable : IDirectives, ITyped
{
    public required string Name { get; set; }
    public required TypeRef Type { get; set; }
    public List<GraphQLDirective> Directives { get; set; } = new();
    public string? Value { get; set; }
}

public enum GraphQLOperationType
{
    Query,
    Mutation,
    Subscription,
}

public class GraphQLFragment : IDirectives, ISelections
{
    public required string Name { get; set; }
    public required string TypeCondition { get; set; }
    public List<GraphQLVariable> Variables { get; set; } = new();
    public List<IGraphQLSelection> Selections { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
}

[InterfaceType("GraphQLSelection")]
public interface IGraphQLSelection
{
    string Name { get; set; }
}

public class GraphQLFieldSelection : IGraphQLSelection, IDirectives, ISelections
{
    public required string Name { get; set; }
    public string? Alias { get; set; }
    public List<IGraphQLSelection> Selections { get; set; } = new();
    public List<GraphQLFieldSelection> FieldSelections { get; set; } = new();
    public List<GraphQLFragmentSpreadSelection> FragmentSpreadSelections { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
}

public class GraphQLFragmentSpreadSelection : IGraphQLSelection
{
    public required string Name { get; set; }
}

public class GraphQLDirective
{
    public required string Name { get; set; }
    public List<GraphQLDirectiveArgument> Arguments { get; set; } = new();
}

public class GraphQLDirectiveArgument
{
    public required string Name { get; set; }
    public required string Value { get; set; }
}

public class GraphQLEnumeration : IDirectives
{
    public required string Name { get; set; }
    public List<GraphQLDirective> Directives { get; set; } = new();
    public List<GraphQLEnumerationValue> Values { get; set; } = new();
}

public class GraphQLEnumerationValue : IDirectives
{
    public required string Name { get; set; }
    public List<GraphQLDirective> Directives { get; set; } = new();
}

public record TypeRef(string Name, ImmutableList<TypeRef> GenericArguments, string Text);

public class GraphQLField : IDirectives, ITyped
{
    public required string Name { get; set; }
    public required TypeRef Type { get; set; }
    public List<GraphQLArgument> Arguments { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
    public string? Description { get; set; }
}

public class GraphQLInputField : IDirectives, ITyped
{
    public required string Name { get; set; }
    public required TypeRef Type { get; set; }
    public List<GraphQLArgument> Arguments { get; set; } = new();
    public List<GraphQLDirective> Directives { get; set; } = new();
    public string? Value { get; set; }
}

public class GraphQLArgument : IDirectives, ITyped
{
    public required string Name { get; set; }
    public required TypeRef Type { get; set; }
    public List<GraphQLDirective> Directives { get; set; } = new();
    public string? Value { get; set; }
}

public class GraphQLScalarType : IDirectives
{
    public required string Name { get; set; }
    public List<GraphQLDirective> Directives { get; set; } = new();
}

public interface IDirectives
{
    List<GraphQLDirective> Directives { get; }
}

public interface ISelections
{
    List<IGraphQLSelection> Selections { get; }
}

public interface ITyped
{
    string Name { get; }
    TypeRef Type { get; }
}
