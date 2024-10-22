using System.Collections.Generic;

namespace DotnetBotfactory;

// TODO - eventually the client generator will auto-generate these interfaces. For now we can make them manually because every type the client generates is partial.

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
