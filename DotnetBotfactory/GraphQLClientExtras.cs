using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Mime;
using System.Threading;

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
public partial interface IObjectOrInterface
{
    string Name { get; }
    IReadOnlyList<IObjectOrInterfaceField> Fields { get; }
}

public interface IObjectOrInterfaceField
{
    string Name { get; }
    IReadOnlyList<IParameter> Parameters { get; }
    IType Type { get; }

}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeField : IObjectOrInterfaceField
{
    IReadOnlyList<IParameter> IObjectOrInterfaceField.Parameters => Parameters;
    IType IObjectOrInterfaceField.Type => Type;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceType : IObjectOrInterface
{
    IReadOnlyList<IObjectOrInterfaceField> IObjectOrInterface.Fields => Fields;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeField : IObjectOrInterfaceField
{
    IReadOnlyList<IParameter> IObjectOrInterfaceField.Parameters => Parameters;
    IType IObjectOrInterfaceField.Type => Type;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeFieldType : IType
{
    
}

public interface IParameter
{
    string Name { get; }
    IType Type { get; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeFieldParameter : IParameter
{
    IType IParameter.Type => Type;
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeFieldParameter : IParameter
{
    IType IParameter.Type => Type;
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeFieldParameterType : IType
{
    
}

public partial class ParseGraphQLSchemaAndOperationsInterfaceTypeFieldParameterType : IType
{
    
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypeFieldType : IType
{
    public string Text1 => Text;
}

public interface IType
{
    string Text { get; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectType : IObjectOrInterface
{
    IReadOnlyList<IObjectOrInterfaceField> IObjectOrInterface.Fields => Fields;
}
