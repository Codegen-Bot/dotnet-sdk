using System.Collections.Immutable;

namespace DotnetBotfactory;

public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItem : ISelection
{
    public IFieldSelection? FieldSelection1 => FieldSelection;
    public IFragmentSpreadSelection? FragmentSpreadSelection1 => FragmentSpreadSelection;
    public IInlineFragmentSelection? InlineFragmentSelection1 => InlineFragmentSelection;
}

public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItemFieldSelection : IFieldSelection
{
}

public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItemFragmentSpreadSelection : IFragmentSpreadSelection
{
}

public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItemInlineFragmentSelection : IInlineFragmentSelection
{
}

public partial class ParseGraphQLSchemaAndOperationsFragmentDenestedSelection : ISelection
{
    public IFieldSelection? FieldSelection1 => Item.FieldSelection;
    public IFragmentSpreadSelection? FragmentSpreadSelection1 => Item.FragmentSpreadSelection;
    public IInlineFragmentSelection? InlineFragmentSelection1 => Item.InlineFragmentSelection;
}

public partial class ParseGraphQLSchemaAndOperationsFragmentDenestedSelectionItemFieldSelection : IFieldSelection
{
    
}

public partial class
    ParseGraphQLSchemaAndOperationsFragmentDenestedSelectionItemFragmentSpreadSelection : IFragmentSpreadSelection
{
}

public partial class ParseGraphQLSchemaAndOperationsFragmentDenestedSelectionItemInlineFragmentSelection : IInlineFragmentSelection { }



public interface ISelection
{
    public IFieldSelection? FieldSelection1 { get; }
    public IFragmentSpreadSelection? FragmentSpreadSelection1 { get; }
    public IInlineFragmentSelection? InlineFragmentSelection1 { get; }
}

public interface IFieldSelection
{
    string Name { get; }
    string? Alias { get; }
}

public interface IFragmentSpreadSelection
{
    string FragmentName { get; }
}

public interface IInlineFragmentSelection
{
    string TypeName { get; }
}
