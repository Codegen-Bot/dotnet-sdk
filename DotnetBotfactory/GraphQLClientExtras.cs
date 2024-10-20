using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Mime;

namespace DotnetBotfactory;

// public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItem : ISelection
// {
//     public IFieldSelection? FieldSelection1 => FieldSelection;
//     public IFragmentSpreadSelection? FragmentSpreadSelection1 => FragmentSpreadSelection;
//     public IInlineFragmentSelection? InlineFragmentSelection1 => InlineFragmentSelection;
// }
//
// public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItemFieldSelection : IFieldSelection
// {
// }
//
// public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItemFragmentSpreadSelection : IFragmentSpreadSelection
// {
// }
//
// public partial class ParseGraphQLSchemaAndOperationsOperationDenestedSelectionItemInlineFragmentSelection : IInlineFragmentSelection
// {
// }
//
// public partial class ParseGraphQLSchemaAndOperationsFragmentDenestedSelection : ISelection
// {
//     public IFieldSelection? FieldSelection1 => Item.FieldSelection;
//     public IFragmentSpreadSelection? FragmentSpreadSelection1 => Item.FragmentSpreadSelection;
//     public IInlineFragmentSelection? InlineFragmentSelection1 => Item.InlineFragmentSelection;
// }
//
// public partial class ParseGraphQLSchemaAndOperationsFragmentDenestedSelectionItemFieldSelection : IFieldSelection
// {
//     
// }
//
// public partial class
//     ParseGraphQLSchemaAndOperationsFragmentDenestedSelectionItemFragmentSpreadSelection : IFragmentSpreadSelection
// {
// }
//
// public partial class ParseGraphQLSchemaAndOperationsFragmentDenestedSelectionItemInlineFragmentSelection : IInlineFragmentSelection { }
//
//
//
// public interface ISelection
// {
//     public IFieldSelection? FieldSelection1 { get; }
//     public IFragmentSpreadSelection? FragmentSpreadSelection1 { get; }
//     public IInlineFragmentSelection? InlineFragmentSelection1 { get; }
// }
//
// public interface IFieldSelection
// {
//     string Name { get; }
//     string? Alias { get; }
// }
//
// public interface IFragmentSpreadSelection
// {
//     string FragmentName { get; }
// }
//
// public interface IInlineFragmentSelection
// {
//     string TypeName { get; }
// }
//
//
//
//
public interface IObjectOrInterface
{
    string Name1 { get; }
    IReadOnlyList<IObjectOrInterfaceField> Fields1 { get; }
}

public interface IObjectOrInterfaceField
{
    string Name1 { get; }
    IReadOnlyList<IParameter> Parameters1 { get; }
    IType Type1 { get; }

}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeField : IObjectOrInterfaceField
{
    public string Name1 => Name;
    public IReadOnlyList<IParameter> Parameters1 => Parameters;
    public IType Type1 => Type;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceType : IObjectOrInterface
{
    public string Name1 => Name;
    public IReadOnlyList<IObjectOrInterfaceField> Fields1 => Fields;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeField : IObjectOrInterfaceField
{
    public string Name1 => Name;
    public IReadOnlyList<IParameter> Parameters1 => Parameters;
    public IType Type1 => Type;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeFieldType : IType
{
    public string Text1 => Text;
}

public interface IParameter
{
    string Name1 { get; }
    IType Type1 { get; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeFieldParameter : IParameter
{
    public string Name1 => Name;
    public IType Type1 => Type;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeFieldParameter : IParameter
{
    public string Name1 => Name;
    public IType Type1 => Type;
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeFieldParameterType : IType
{
    public string Text1 => Text;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeFieldParameterType : IType
{
    public string Text1 => Text;
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeFieldType : IType
{
    public string Text1 => Text;
}

public interface IType
{
    string Text1 { get; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectType : IObjectOrInterface
{
    public string Name1 => Name;
    public IReadOnlyList<IObjectOrInterfaceField> Fields1 => Fields;
}
