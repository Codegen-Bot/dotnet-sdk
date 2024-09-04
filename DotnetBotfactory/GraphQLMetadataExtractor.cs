using System;
using System.Collections.Immutable;
using System.Linq;
using HotChocolate.Language;

namespace DotnetBotfactory;

public class GraphQLMetadataExtractionService
{
    public void Extract(string graphql, GraphQLMetadata metadata)
    {
        if (string.IsNullOrWhiteSpace(graphql))
        {
            return;
        }

        var parsedSchema = Utf8GraphQLParser.Parse(graphql);

        foreach (var def in parsedSchema.Definitions)
        {
            if (def is ObjectTypeDefinitionNode objectDefinition)
            {
                var newObjectType = new GraphQLObjectType()
                {
                    Name = objectDefinition.Name.Value,
                };

                foreach (var field in objectDefinition.Fields)
                {
                    var newField = new GraphQLField()
                    {
                        Name = field.Name.Value,
                        Type = field.Type.ToTypeRef(),
                    };

                    foreach (var argument in field.Arguments)
                    {
                        newField.Arguments.Add(argument.ToArgument());
                    }
                    
                    foreach (var directive in field.Directives)
                    {
                        newField.Directives.Add(directive.ToDirective());
                    }

                    newObjectType.Fields.Add(newField);
                }

                foreach (var directive in objectDefinition.Directives)
                {
                    newObjectType.Directives.Add(directive.ToDirective());
                }
            
                metadata.ObjectTypes.Add(newObjectType);
            }
            else if (def is InputObjectTypeDefinitionNode inputObjectDefinition)
            {
                var newObjectType = new GraphQLInputObjectType()
                {
                    Name = inputObjectDefinition.Name.Value,
                };

                foreach (var field in inputObjectDefinition.Fields)
                {
                    var newField = new GraphQLInputField()
                    {
                        Name = field.Name.Value,
                        Type = field.Type.ToTypeRef(),
                    };

                    foreach (var directive in field.Directives)
                    {
                        newField.Directives.Add(directive.ToDirective());
                    }

                    if (field.DefaultValue is not null)
                    {
                        newField.Value = field.DefaultValue.ToString();
                    }
                    
                    newObjectType.Fields.Add(newField);
                }

                foreach (var directive in inputObjectDefinition.Directives)
                {
                    newObjectType.Directives.Add(directive.ToDirective());
                }
            
                metadata.InputObjectTypes.Add(newObjectType);
            }
            else if (def is ScalarTypeDefinitionNode scalarType)
            {
                var newScalarType = new GraphQLScalarType()
                {
                    Name = scalarType.Name.Value,
                };

                foreach (var directive in scalarType.Directives)
                {
                    newScalarType.Directives.Add(directive.ToDirective());
                }
                
                metadata.ScalarTypes.Add(newScalarType);
            }
            else if (def is ScalarTypeExtensionNode) { }
            else if (def is DirectiveDefinitionNode directiveDef)
            {
                var newDirective = new GraphQLDirectiveType()
                {
                    Name = directiveDef.Name.Value,
                    IsRepeatable = directiveDef.IsRepeatable,
                };

                foreach (var argument in directiveDef.Arguments)
                {
                    newDirective.Arguments.Add(argument.ToArgument());
                }
                
                metadata.Directives.Add(newDirective);
            }
            else if (def is EnumTypeDefinitionNode enumDef)
            {
                var enumDefinition = new GraphQLEnumeration()
                {
                    Name = enumDef.Name.Value,
                };

                foreach (var directive in enumDef.Directives)
                {
                    enumDefinition.Directives.Add(directive.ToDirective());
                }

                foreach (var value in enumDef.Values)
                {
                    var newValue = new GraphQLEnumerationValue()
                    {
                        Name = value.Name.Value,
                    };

                    foreach (var directive in value.Directives)
                    {
                        newValue.Directives.Add(directive.ToDirective());
                    }
                    
                    enumDefinition.Values.Add(newValue);
                }
                
                metadata.Enumerations.Add(enumDefinition);
            }
            else if (def is EnumTypeExtensionNode)
            {
                
            }
            else if (def is FragmentDefinitionNode fragment)
            {
                var newFragment = new GraphQLFragment()
                {
                    Name = fragment.Name?.Value,
                    TypeCondition = fragment.TypeCondition.Name(),
                };

                foreach (var variable in fragment.VariableDefinitions)
                {
                    newFragment.Variables.Add(variable.ToVariable());
                }

                foreach (var directive in fragment.Directives)
                {
                    newFragment.Directives.Add(directive.ToDirective());
                }

                foreach (var selection in fragment.SelectionSet.Selections)
                {
                    newFragment.Selections.Add(selection.ToSelection());
                }

                metadata.Fragments.Add(newFragment);
            }
            else if (def is InputObjectTypeExtensionNode) { }
            else if (def is InterfaceTypeDefinitionNode interfaceType)
            {
                var newInterfaceType = new GraphQLInterfaceType()
                {
                    Name = interfaceType.Name.Value,
                };

                foreach (var field in interfaceType.Fields)
                {
                    var newField = new GraphQLField()
                    {
                        Name = field.Name.Value,
                        Type = field.Type.ToTypeRef(),
                    };

                    foreach (var directive in field.Directives)
                    {
                        newField.Directives.Add(directive.ToDirective());
                    }

                    newInterfaceType.Fields.Add(newField);
                }

                foreach (var directive in interfaceType.Directives)
                {
                    newInterfaceType.Directives.Add(directive.ToDirective());
                }
            
                metadata.InterfaceTypes.Add(newInterfaceType);
            }
            else if (def is InterfaceTypeExtensionNode) { }
            else if (def is ObjectTypeExtensionNode) { }
            else if (def is OperationDefinitionNode operation)
            {
                var newOperation = new GraphQLOperation()
                {
                    Name = operation.Name?.Value,
                    Text = operation.ToString(true),
                    OperationType = Enum.Parse<GraphQLOperationType>(operation.Operation.ToString()),
                };

                foreach (var variable in operation.VariableDefinitions)
                {
                    newOperation.Variables.Add(variable.ToVariable());
                }

                foreach (var directive in operation.Directives)
                {
                    newOperation.Directives.Add(directive.ToDirective());
                }

                foreach (var selection in (operation.SelectionSet?.Selections ?? Array.Empty<ISelectionNode>()))
                {
                    newOperation.Selections.Add(selection.ToSelection());
                }

                metadata.Operations.Add(newOperation);
            }
            else if (def is SchemaDefinitionNode) { }
            else if (def is SchemaExtensionNode) { }
            else if (def is UnionTypeDefinitionNode) { }
            else if (def is UnionTypeExtensionNode) { }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}

public static class Extensions
{
    public static IGraphQLSelection ToSelection(this ISelectionNode selection)
    {
        if (selection is FieldNode field)
        {
            var result = new GraphQLFieldSelection()
            {
                Name = field.Name.Value,
                Alias = field.Alias?.Value,
            };

            foreach (var directive in field.Directives)
            {
                result.Directives.Add(directive.ToDirective());
            }
        
            foreach (var subselection in (field.SelectionSet?.Selections ?? Array.Empty<ISelectionNode>()))
            {
                result.Selections.Add(subselection.ToSelection());
            }

            result.FragmentSpreadSelections =
                result.Selections.OfType<GraphQLFragmentSpreadSelection>().ToList();
            result.FieldSelections =
                result.Selections.OfType<GraphQLFieldSelection>().ToList();

            return result;
        }
        else if (selection is FragmentSpreadNode fragmentSpread)
        {
            return new GraphQLFragmentSpreadSelection() { Name = fragmentSpread.Name.Value };
        }
        else
        {
            throw new NotImplementedException();
        }
    }
    
    public static GraphQLArgument ToArgument(this InputValueDefinitionNode argument)
    {
        var result = new GraphQLArgument()
        {
            Name = argument.Name.Value,
            Type = argument.Type.ToTypeRef(),
        };
        
        foreach (var directive in argument.Directives)
        {
            result.Directives.Add(directive.ToDirective());
        }

        if (argument.DefaultValue is not null)
        {
            result.Value = argument.DefaultValue.ToString();
        }
        
        return result;
    }

    public static GraphQLVariable ToVariable(this VariableDefinitionNode variable)
    {
        var result = new GraphQLVariable()
        {
            Name = variable.Variable.Name.Value,
            Type = variable.Type.ToTypeRef(),
        };

        foreach (var directive in variable.Directives)
        {
            result.Directives.Add(directive.ToDirective());
        }

        if (variable.DefaultValue is not null)
        {
            result.Value = variable.DefaultValue.ToString();
        }
        
        return result;
    }

    public static GraphQLDirective ToDirective(this DirectiveNode directive)
    {
        return new GraphQLDirective()
        {
            Name = directive.Name.Value,
            Arguments = directive.Arguments.Select(arg => new GraphQLDirectiveArgument() { Name = arg.Name.Value, Value = arg.Value.ToString() }).ToList(),
        };
    }
    
    public static TypeRef ToTypeRef(this ITypeNode typeNode)
    {
        if (typeNode.IsListType())
        {
            return new TypeRef("List", new [] {typeNode.ElementType().ToTypeRef()}.ToImmutableList(),
                typeNode.ToString());
        }

        if (typeNode.IsNonNullType())
        {
            return new TypeRef("NotNull", new [] {typeNode.InnerType().ToTypeRef()}.ToImmutableList(),
                typeNode.ToString());
        }

        return new TypeRef(typeNode.Name(), ImmutableList<TypeRef>.Empty, typeNode.ToString());
    }
        
    public static TypeRef SkipByName(this TypeRef type, params string[] namesToSkip)
    {
        while (type.GenericArguments.Count == 1 && namesToSkip.Any(n => n == type.Name))
        {
            type = type.GenericArguments[0];
        }

        return type;
    }
}