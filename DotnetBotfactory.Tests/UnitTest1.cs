using System.Text.Json;
using System.Text.Json.Nodes;

namespace DotnetBotfactory.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var response = """
                   {
                     "data": {
                       "graphQL": {
                         "objectTypes": [
                           {
                             "interfaces": [],
                             "name": "Configuration",
                             "fields": [
                               {
                                 "name": "id",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "language",
                                 "parameters": [],
                                 "type": {
                                   "text": "DotnetLanguage!"
                                 }
                               },
                               {
                                 "name": "outputPath",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "projectName",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "minimalWorkingExample",
                                 "parameters": [],
                                 "type": {
                                   "text": "Boolean!"
                                 }
                               },
                               {
                                 "name": "provideApi",
                                 "parameters": [],
                                 "type": {
                                   "text": "Boolean!"
                                 }
                               },
                               {
                                 "name": "dotnetVersion",
                                 "parameters": [],
                                 "type": {
                                   "text": "DotnetVersion!"
                                 }
                               },
                               {
                                 "name": "copybots",
                                 "parameters": [],
                                 "type": {
                                   "text": "[DotnetCopybot!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "DotnetCopybot",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "inputDirectory",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "whitelist",
                                 "parameters": [],
                                 "type": {
                                   "text": "[String!]!"
                                 }
                               },
                               {
                                 "name": "fieldDefinitions",
                                 "parameters": [],
                                 "type": {
                                   "text": "[DotnetCopybotFieldDefinition!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "DotnetCopybotFieldDefinition",
                             "fields": [
                               {
                                 "name": "needle",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "fieldName",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "variants",
                                 "parameters": [],
                                 "type": {
                                   "text": "[DotnetCopybotStringVariant!]"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "BotSpec",
                             "fields": [
                               {
                                 "name": "unparseJson",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "readMePath",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "configurationSchemaPath",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "stitchSchemaPath",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "consumedSchemaPath",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "excludeConfigurationFromConsumedSchema",
                                 "parameters": [],
                                 "type": {
                                   "text": "Boolean"
                                 }
                               },
                               {
                                 "name": "dependencies",
                                 "parameters": [],
                                 "type": {
                                   "text": "[KeyValuePairOfStringAndString!]!"
                                 }
                               },
                               {
                                 "name": "providedSchemaPath",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "tags",
                                 "parameters": [],
                                 "type": {
                                   "text": "[KeyValuePairOfStringAndString!]"
                                 }
                               },
                               {
                                 "name": "private",
                                 "parameters": [],
                                 "type": {
                                   "text": "Boolean"
                                 }
                               },
                               {
                                 "name": "exec",
                                 "parameters": [],
                                 "type": {
                                   "text": "[KeyValuePairOfStringAndString!]"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "Caret",
                             "fields": [
                               {
                                 "name": "separator",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "string",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "tags",
                                 "parameters": [],
                                 "type": {
                                   "text": "[CaretTag!]!"
                                 }
                               },
                               {
                                 "name": "tagValue",
                                 "parameters": [
                                   {
                                     "name": "tagName",
                                     "type": {
                                       "text": "String!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "matches",
                                 "parameters": [
                                   {
                                     "name": "tags",
                                     "type": {
                                       "text": "[CaretTagInput!]!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Boolean!"
                                 }
                               },
                               {
                                 "name": "id",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "CaretTag",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "Configuration",
                             "fields": [
                               {
                                 "name": "outputPath",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "DenestedOfSelectionItem",
                             "fields": [
                               {
                                 "name": "depth",
                                 "parameters": [],
                                 "type": {
                                   "text": "Int!"
                                 }
                               },
                               {
                                 "name": "item",
                                 "parameters": [],
                                 "type": {
                                   "text": "SelectionItem!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "File",
                             "fields": [
                               {
                                 "name": "path",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "kind",
                                 "parameters": [],
                                 "type": {
                                   "text": "FileKind!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLArgument",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLValue!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLBooleanValue",
                             "fields": [
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "Boolean!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLDirective",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "arguments",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLArgument!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLDirectiveType",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "parameters",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLParameter!]!"
                                 }
                               },
                               {
                                 "name": "isRepeatable",
                                 "parameters": [],
                                 "type": {
                                   "text": "Boolean!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLEnumValue",
                             "fields": [
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLEnumeration",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "values",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLEnumerationValue!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLEnumerationValue",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLField",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "type",
                                 "parameters": [],
                                 "type": {
                                   "text": "TypeRef!"
                                 }
                               },
                               {
                                 "name": "parameters",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLParameter!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "description",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLSelection"
                             ],
                             "name": "GraphQLFieldSelection",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "alias",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "selections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[SelectionItem!]!"
                                 }
                               },
                               {
                                 "name": "denestedSelections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[DenestedOfSelectionItem!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "arguments",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLArgument!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLFloatValue",
                             "fields": [
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLFragment",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "typeCondition",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "variables",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLVariable!]!"
                                 }
                               },
                               {
                                 "name": "selections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[SelectionItem!]!"
                                 }
                               },
                               {
                                 "name": "denestedSelections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[DenestedOfSelectionItem!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLSelection"
                             ],
                             "name": "GraphQLFragmentSpreadSelection",
                             "fields": [
                               {
                                 "name": "fragmentName",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLSelection"
                             ],
                             "name": "GraphQLInlineFragmentSelection",
                             "fields": [
                               {
                                 "name": "typeName",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "selections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[SelectionItem!]!"
                                 }
                               },
                               {
                                 "name": "denestedSelections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[DenestedOfSelectionItem!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLInputField",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "type",
                                 "parameters": [],
                                 "type": {
                                   "text": "TypeRef!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLValue"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLInputObjectType",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "fields",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLInputField!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLIntValue",
                             "fields": [
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLObjectOrInterfaceType"
                             ],
                             "name": "GraphQLInterfaceType",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "fields",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLField!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLListValue",
                             "fields": [
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLValue!]!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLMetadata",
                             "fields": [
                               {
                                 "name": "operations",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLOperation!]!"
                                 }
                               },
                               {
                                 "name": "objectTypes",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLObjectType!]!"
                                 }
                               },
                               {
                                 "name": "inputObjectTypes",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLInputObjectType!]!"
                                 }
                               },
                               {
                                 "name": "interfaceTypes",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLInterfaceType!]!"
                                 }
                               },
                               {
                                 "name": "scalarTypes",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLScalarType!]!"
                                 }
                               },
                               {
                                 "name": "fragments",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLFragment!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirectiveType!]!"
                                 }
                               },
                               {
                                 "name": "enumerations",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLEnumeration!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLNullValue",
                             "fields": [
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLObjectOrInterfaceType"
                             ],
                             "name": "GraphQLObjectType",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "fields",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLField!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "interfaces",
                                 "parameters": [],
                                 "type": {
                                   "text": "[String!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLObjectValue",
                             "fields": [
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "[KeyValuePairOfStringAndGraphQLValue!]!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLOperation",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "operationType",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLOperationType!"
                                 }
                               },
                               {
                                 "name": "selections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[SelectionItem!]!"
                                 }
                               },
                               {
                                 "name": "denestedSelections",
                                 "parameters": [],
                                 "type": {
                                   "text": "[DenestedOfSelectionItem!]!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "variables",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLVariable!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLParameter",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "type",
                                 "parameters": [],
                                 "type": {
                                   "text": "TypeRef!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "defaultValue",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLValue"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLScalarType",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLStringValue",
                             "fields": [
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "GraphQLVariable",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "type",
                                 "parameters": [],
                                 "type": {
                                   "text": "TypeRef!"
                                 }
                               },
                               {
                                 "name": "directives",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLDirective!]!"
                                 }
                               },
                               {
                                 "name": "defaultValue",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLValue"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [
                               "GraphQLValue"
                             ],
                             "name": "GraphQLVariableValue",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "KeyValuePairOfStringAndGraphQLValue",
                             "fields": [
                               {
                                 "name": "key",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLValue!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "KeyValuePairOfStringAndString",
                             "fields": [
                               {
                                 "name": "key",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "value",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "Mutation",
                             "fields": [
                               {
                                 "name": "noop",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "getOrAddCaret",
                                 "parameters": [
                                   {
                                     "name": "caretId",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "tags",
                                     "type": {
                                       "text": "[CaretTagInput!]!"
                                     }
                                   },
                                   {
                                     "name": "indentation",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "separator",
                                     "type": {
                                       "text": "String!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Caret!"
                                 }
                               },
                               {
                                 "name": "addFile",
                                 "parameters": [
                                   {
                                     "name": "filePath",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "textAndCarets",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "caretDelimiterLength",
                                     "type": {
                                       "text": "Int!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Caret!"
                                 }
                               },
                               {
                                 "name": "addKeyedText",
                                 "parameters": [
                                   {
                                     "name": "caretId",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "key",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "textAndCarets",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "caretDelimiterLength",
                                     "type": {
                                       "text": "Int!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Caret!"
                                 }
                               },
                               {
                                 "name": "addText",
                                 "parameters": [
                                   {
                                     "name": "caretId",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "textAndCarets",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "caretDelimiterLength",
                                     "type": {
                                       "text": "Int!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Caret!"
                                 }
                               },
                               {
                                 "name": "addKeyedTextByTags",
                                 "parameters": [
                                   {
                                     "name": "tags",
                                     "type": {
                                       "text": "[CaretTagInput!]!"
                                     }
                                   },
                                   {
                                     "name": "key",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "textAndCarets",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "caretDelimiterLength",
                                     "type": {
                                       "text": "Int!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "[Caret!]!"
                                 }
                               },
                               {
                                 "name": "addTextByTags",
                                 "parameters": [
                                   {
                                     "name": "tags",
                                     "type": {
                                       "text": "[CaretTagInput!]"
                                     }
                                   },
                                   {
                                     "name": "textAndCarets",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "caretDelimiterLength",
                                     "type": {
                                       "text": "Int!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "[Caret!]!"
                                 }
                               },
                               {
                                 "name": "log",
                                 "parameters": [
                                   {
                                     "name": "severity",
                                     "type": {
                                       "text": "LogSeverity!"
                                     }
                                   },
                                   {
                                     "name": "message",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "arguments",
                                     "type": {
                                       "text": "[String!]"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "Query",
                             "fields": [
                               {
                                 "name": "noop",
                                 "parameters": [],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "botSpec",
                                 "parameters": [
                                   {
                                     "name": "botFilePath",
                                     "type": {
                                       "text": "String!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "BotSpec"
                                 }
                               },
                               {
                                 "name": "botSchema",
                                 "parameters": [
                                   {
                                     "name": "botFilePath",
                                     "type": {
                                       "text": "String!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "hypotheticalBotSchema",
                                 "parameters": [
                                   {
                                     "name": "configurationSchema",
                                     "type": {
                                       "text": "String"
                                     }
                                   },
                                   {
                                     "name": "dependencies",
                                     "type": {
                                       "text": "[BotDependencyInput!]!"
                                     }
                                   },
                                   {
                                     "name": "deduplicateConfigurationSchema",
                                     "type": {
                                       "text": "Boolean!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "randomUuid",
                                 "parameters": [
                                   {
                                     "name": "seed",
                                     "type": {
                                       "text": "Int"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "randomDouble",
                                 "parameters": [
                                   {
                                     "name": "seed",
                                     "type": {
                                       "text": "Int"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Float!"
                                 }
                               },
                               {
                                 "name": "randomSingle",
                                 "parameters": [
                                   {
                                     "name": "seed",
                                     "type": {
                                       "text": "Int"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Float!"
                                 }
                               },
                               {
                                 "name": "randomBytes",
                                 "parameters": [
                                   {
                                     "name": "seed",
                                     "type": {
                                       "text": "Int"
                                     }
                                   },
                                   {
                                     "name": "byteCount",
                                     "type": {
                                       "text": "Int!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "caret",
                                 "parameters": [
                                   {
                                     "name": "caretId",
                                     "type": {
                                       "text": "String!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "Caret"
                                 }
                               },
                               {
                                 "name": "carets",
                                 "parameters": [
                                   {
                                     "name": "tags",
                                     "type": {
                                       "text": "[CaretTagInput!]!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "[Caret!]!"
                                 }
                               },
                               {
                                 "name": "readTextFile",
                                 "parameters": [
                                   {
                                     "name": "textFilePath",
                                     "type": {
                                       "text": "String!"
                                     }
                                   },
                                   {
                                     "name": "fileVersion",
                                     "type": {
                                       "text": "FileVersion"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "String"
                                 }
                               },
                               {
                                 "name": "files",
                                 "parameters": [
                                   {
                                     "name": "whitelist",
                                     "type": {
                                       "text": "[String!]"
                                     }
                                   },
                                   {
                                     "name": "blacklist",
                                     "type": {
                                       "text": "[String!]"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "[File!]!"
                                 }
                               },
                               {
                                 "name": "logNoop",
                                 "parameters": [],
                                 "type": {
                                   "text": "Boolean!"
                                 }
                               },
                               {
                                 "name": "graphQL",
                                 "parameters": [
                                   {
                                     "name": "fileWhitelist",
                                     "type": {
                                       "text": "[String!]"
                                     }
                                   },
                                   {
                                     "name": "additionalFiles",
                                     "type": {
                                       "text": "[AdditionalFileInput!]!"
                                     }
                                   }
                                 ],
                                 "type": {
                                   "text": "GraphQLMetadata!"
                                 }
                               },
                               {
                                 "name": "configuration",
                                 "parameters": [],
                                 "type": {
                                   "text": "Configuration!"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "SelectionItem",
                             "fields": [
                               {
                                 "name": "selection",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLSelection!"
                                 }
                               },
                               {
                                 "name": "fieldSelection",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLFieldSelection"
                                 }
                               },
                               {
                                 "name": "fragmentSpreadSelection",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLFragmentSpreadSelection"
                                 }
                               },
                               {
                                 "name": "inlineFragmentSelection",
                                 "parameters": [],
                                 "type": {
                                   "text": "GraphQLInlineFragmentSelection"
                                 }
                               }
                             ]
                           },
                           {
                             "interfaces": [],
                             "name": "TypeRef",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "genericArguments",
                                 "parameters": [],
                                 "type": {
                                   "text": "[TypeRef!]!"
                                 }
                               },
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           }
                         ],
                         "inputObjectTypes": [
                           {
                             "name": "AdditionalFileInput",
                             "fields": [
                               {
                                 "name": "filePath",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "content",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "name": "BotDependencyInput",
                             "fields": [
                               {
                                 "name": "botId",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "botVersion",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "name": "CaretTagInput",
                             "fields": [
                               {
                                 "name": "name",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "value",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           }
                         ],
                         "interfaceTypes": [
                           {
                             "name": "GraphQLObjectOrInterfaceType",
                             "fields": [
                               {
                                 "name": "name",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "fields",
                                 "parameters": [],
                                 "type": {
                                   "text": "[GraphQLField!]!"
                                 }
                               }
                             ]
                           },
                           {
                             "name": "GraphQLSelection",
                             "fields": [
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           },
                           {
                             "name": "GraphQLValue",
                             "fields": [
                               {
                                 "name": "text",
                                 "parameters": [],
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ]
                           }
                         ],
                         "enumerations": [
                           {
                             "name": "DotnetLanguage",
                             "values": [
                               {
                                 "name": "CSHARP"
                               },
                               {
                                 "name": "RUST"
                               },
                               {
                                 "name": "TYPESCRIPT"
                               },
                               {
                                 "name": "GO"
                               },
                               {
                                 "name": "KOTLIN"
                               }
                             ]
                           },
                           {
                             "name": "DotnetVersion",
                             "values": [
                               {
                                 "name": "DotNet8"
                               }
                             ]
                           },
                           {
                             "name": "DotnetCopybotStringVariant",
                             "values": [
                               {
                                 "name": "CamelCase"
                               },
                               {
                                 "name": "SnakeCase"
                               },
                               {
                                 "name": "UpperSnakeCase"
                               },
                               {
                                 "name": "LowerSnakeCase"
                               },
                               {
                                 "name": "KebabCase"
                               },
                               {
                                 "name": "UpperKebabCase"
                               },
                               {
                                 "name": "LowerKebabCase"
                               },
                               {
                                 "name": "LowerCase"
                               },
                               {
                                 "name": "UpperCase"
                               }
                             ]
                           },
                           {
                             "name": "FileKind",
                             "values": [
                               {
                                 "name": "BINARY"
                               },
                               {
                                 "name": "TEXT"
                               }
                             ]
                           },
                           {
                             "name": "FileVersion",
                             "values": [
                               {
                                 "name": "GENERATED"
                               },
                               {
                                 "name": "HEAD"
                               }
                             ]
                           },
                           {
                             "name": "GraphQLOperationType",
                             "values": [
                               {
                                 "name": "QUERY"
                               },
                               {
                                 "name": "MUTATION"
                               },
                               {
                                 "name": "SUBSCRIPTION"
                               }
                             ]
                           },
                           {
                             "name": "LogSeverity",
                             "values": [
                               {
                                 "name": "TRACE"
                               },
                               {
                                 "name": "DEBUG"
                               },
                               {
                                 "name": "INFORMATION"
                               },
                               {
                                 "name": "WARNING"
                               },
                               {
                                 "name": "ERROR"
                               },
                               {
                                 "name": "CRITICAL"
                               }
                             ]
                           }
                         ],
                         "fragments": [
                           {
                             "name": "DenestedSelections",
                             "text": "fragment DenestedSelections on DenestedOfSelectionItem {\n  depth\n  item {\n    selection {\n      text\n      __typename\n      ... on GraphQLFieldSelection {\n        name\n        alias\n      }\n      ... on GraphQLFragmentSpreadSelection {\n        fragmentName\n      }\n      ... on GraphQLInlineFragmentSelection {\n        typeName\n      }\n    }\n  }\n}",
                             "typeCondition": "DenestedOfSelectionItem",
                             "variables": [],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "depth",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "depth",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "item {\n  selection {\n    text\n    __typename\n    ... on GraphQLFieldSelection {\n      name\n      alias\n    }\n    ... on GraphQLFragmentSpreadSelection {\n      fragmentName\n    }\n    ... on GraphQLInlineFragmentSelection {\n      typeName\n    }\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "item",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "selection {\n  text\n  __typename\n  ... on GraphQLFieldSelection {\n    name\n    alias\n  }\n  ... on GraphQLFragmentSpreadSelection {\n    fragmentName\n  }\n  ... on GraphQLInlineFragmentSelection {\n    typeName\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "selection",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "__typename",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "__typename",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "... on GraphQLFieldSelection {\n  name\n  alias\n}",
                                     "__typename": "GraphQLInlineFragmentSelection",
                                     "typeName": "GraphQLFieldSelection"
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "alias",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "alias",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "... on GraphQLFragmentSpreadSelection {\n  fragmentName\n}",
                                     "__typename": "GraphQLInlineFragmentSelection",
                                     "typeName": "GraphQLFragmentSpreadSelection"
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "fragmentName",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "fragmentName",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "... on GraphQLInlineFragmentSelection {\n  typeName\n}",
                                     "__typename": "GraphQLInlineFragmentSelection",
                                     "typeName": "GraphQLInlineFragmentSelection"
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "typeName",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "typeName",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           }
                         ],
                         "operations": [
                           {
                             "name": "GetFiles",
                             "operationType": "QUERY",
                             "text": "query GetFiles($whitelist: [String!]!, $blacklist: [String!]!) {\n  files(whitelist: $whitelist, blacklist: $blacklist) {\n    path\n    kind\n  }\n}",
                             "variables": [
                               {
                                 "name": "whitelist",
                                 "type": {
                                   "text": "[String!]!"
                                 }
                               },
                               {
                                 "name": "blacklist",
                                 "type": {
                                   "text": "[String!]!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "files(whitelist: $whitelist, blacklist: $blacklist) {\n  path\n  kind\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "files",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "path",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "path",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "kind",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "kind",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "ReadTextFile",
                             "operationType": "QUERY",
                             "text": "query ReadTextFile($textFilePath: String!) {\n  readTextFile(textFilePath: $textFilePath)\n}",
                             "variables": [
                               {
                                 "name": "textFilePath",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "readTextFile(textFilePath: $textFilePath)",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "readTextFile",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "AddFile",
                             "operationType": "MUTATION",
                             "text": "mutation AddFile($filePath: String!, $textAndCarets: String!) {\n  addFile(filePath: $filePath, textAndCarets: $textAndCarets) {\n    id\n  }\n}",
                             "variables": [
                               {
                                 "name": "filePath",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "textAndCarets",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "addFile(filePath: $filePath, textAndCarets: $textAndCarets) {\n  id\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "addFile",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "id",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "id",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "AddText",
                             "operationType": "MUTATION",
                             "text": "mutation AddText($caretId: String!, $textAndCarets: String!) {\n  addText(caretId: $caretId, textAndCarets: $textAndCarets) {\n    id\n  }\n}",
                             "variables": [
                               {
                                 "name": "caretId",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "textAndCarets",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "addText(caretId: $caretId, textAndCarets: $textAndCarets) {\n  id\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "addText",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "id",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "id",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "AddKeyedText",
                             "operationType": "MUTATION",
                             "text": "mutation AddKeyedText($key: String!, $caretId: String!, $textAndCarets: String!) {\n  addKeyedText(key: $key, caretId: $caretId, textAndCarets: $textAndCarets) {\n    id\n  }\n}",
                             "variables": [
                               {
                                 "name": "key",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "caretId",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "textAndCarets",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "addKeyedText(key: $key, caretId: $caretId, textAndCarets: $textAndCarets) {\n  id\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "addKeyedText",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "id",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "id",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "AddTextByTags",
                             "operationType": "MUTATION",
                             "text": "mutation AddTextByTags($tags: [CaretTagInput!]!, $textAndCarets: String!) {\n  addTextByTags(tags: $tags, textAndCarets: $textAndCarets) {\n    id\n  }\n}",
                             "variables": [
                               {
                                 "name": "tags",
                                 "type": {
                                   "text": "[CaretTagInput!]!"
                                 }
                               },
                               {
                                 "name": "textAndCarets",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "addTextByTags(tags: $tags, textAndCarets: $textAndCarets) {\n  id\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "addTextByTags",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "id",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "id",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "AddKeyedTextByTags",
                             "operationType": "MUTATION",
                             "text": "mutation AddKeyedTextByTags($key: String!, $tags: [CaretTagInput!]!, $textAndCarets: String!) {\n  addKeyedTextByTags(key: $key, tags: $tags, textAndCarets: $textAndCarets) {\n    id\n  }\n}",
                             "variables": [
                               {
                                 "name": "key",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "tags",
                                 "type": {
                                   "text": "[CaretTagInput!]!"
                                 }
                               },
                               {
                                 "name": "textAndCarets",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "addKeyedTextByTags(key: $key, tags: $tags, textAndCarets: $textAndCarets) {\n  id\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "addKeyedTextByTags",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "id",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "id",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "Log",
                             "operationType": "MUTATION",
                             "text": "mutation Log($severity: LogSeverity!, $message: String!, $arguments: [String!]) {\n  log(severity: $severity, message: $message, arguments: $arguments)\n}",
                             "variables": [
                               {
                                 "name": "severity",
                                 "type": {
                                   "text": "LogSeverity!"
                                 }
                               },
                               {
                                 "name": "message",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "arguments",
                                 "type": {
                                   "text": "[String!]"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "log(severity: $severity, message: $message, arguments: $arguments)",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "log",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "GetConfiguration",
                             "operationType": "QUERY",
                             "text": "query GetConfiguration {\n  configuration {\n    id\n    projectName\n    outputPath\n    minimalWorkingExample\n    dotnetVersion\n    provideApi\n    language\n    copybots {\n      name\n      inputDirectory\n      whitelist\n      fieldDefinitions {\n        needle\n        fieldName\n        variants\n      }\n    }\n  }\n}",
                             "variables": [],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "configuration {\n  id\n  projectName\n  outputPath\n  minimalWorkingExample\n  dotnetVersion\n  provideApi\n  language\n  copybots {\n    name\n    inputDirectory\n    whitelist\n    fieldDefinitions {\n      needle\n      fieldName\n      variants\n    }\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "configuration",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "id",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "id",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "projectName",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "projectName",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "outputPath",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "outputPath",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "minimalWorkingExample",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "minimalWorkingExample",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "dotnetVersion",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "dotnetVersion",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "provideApi",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "provideApi",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "language",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "language",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "copybots {\n  name\n  inputDirectory\n  whitelist\n  fieldDefinitions {\n    needle\n    fieldName\n    variants\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "copybots",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "inputDirectory",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "inputDirectory",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "whitelist",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "whitelist",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "fieldDefinitions {\n  needle\n  fieldName\n  variants\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "fieldDefinitions",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "needle",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "needle",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "fieldName",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "fieldName",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "variants",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "variants",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "ReadTextFileWithVersion",
                             "operationType": "QUERY",
                             "text": "query ReadTextFileWithVersion($textFilePath: String!, $fileVersion: FileVersion) {\n  readTextFile(textFilePath: $textFilePath, fileVersion: $fileVersion)\n}",
                             "variables": [
                               {
                                 "name": "textFilePath",
                                 "type": {
                                   "text": "String!"
                                 }
                               },
                               {
                                 "name": "fileVersion",
                                 "type": {
                                   "text": "FileVersion"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "readTextFile(textFilePath: $textFilePath, fileVersion: $fileVersion)",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "readTextFile",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "GetSchema",
                             "operationType": "QUERY",
                             "text": "query GetSchema($botFilePath: String!) {\n  botSchema(botFilePath: $botFilePath)\n  botSpec(botFilePath: $botFilePath) {\n    consumedSchemaPath\n    excludeConfigurationFromConsumedSchema\n    providedSchemaPath\n  }\n}",
                             "variables": [
                               {
                                 "name": "botFilePath",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "botSchema(botFilePath: $botFilePath)",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "botSchema",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "botSpec(botFilePath: $botFilePath) {\n  consumedSchemaPath\n  excludeConfigurationFromConsumedSchema\n  providedSchemaPath\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "botSpec",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "consumedSchemaPath",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "consumedSchemaPath",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "excludeConfigurationFromConsumedSchema",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "excludeConfigurationFromConsumedSchema",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "providedSchemaPath",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "providedSchemaPath",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "ParseGraphQLSchemaAndOperations",
                             "operationType": "QUERY",
                             "text": "query ParseGraphQLSchemaAndOperations($graphql: [AdditionalFileInput!]!) {\n  graphQL(additionalFiles: $graphql) {\n    objectTypes {\n      interfaces\n      name\n      fields {\n        name\n        parameters {\n          name\n          type {\n            text\n          }\n        }\n        type {\n          text\n        }\n      }\n    }\n    inputObjectTypes {\n      name\n      fields {\n        name\n        type {\n          text\n        }\n      }\n    }\n    interfaceTypes {\n      name\n      fields {\n        name\n        parameters {\n          name\n          type {\n            text\n          }\n        }\n        type {\n          text\n        }\n      }\n    }\n    enumerations {\n      name\n      values {\n        name\n      }\n    }\n    fragments {\n      name\n      text\n      typeCondition\n      variables {\n        name\n        type {\n          text\n        }\n      }\n      denestedSelections {\n        ... DenestedSelections\n      }\n    }\n    operations {\n      name\n      operationType\n      text\n      variables {\n        name\n        type {\n          text\n        }\n      }\n      denestedSelections {\n        ... DenestedSelections\n      }\n    }\n  }\n}",
                             "variables": [
                               {
                                 "name": "graphql",
                                 "type": {
                                   "text": "[AdditionalFileInput!]!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                                     "text": "graphQL(additionalFiles: $graphql) {\n  objectTypes {\n    interfaces\n    name\n    fields {\n      name\n      parameters {\n        name\n        type {\n          text\n        }\n      }\n      type {\n        text\n      }\n    }\n  }\n  inputObjectTypes {\n    name\n    fields {\n      name\n      type {\n        text\n      }\n    }\n  }\n  interfaceTypes {\n    name\n    fields {\n      name\n      parameters {\n        name\n        type {\n          text\n        }\n      }\n      type {\n        text\n      }\n    }\n  }\n  enumerations {\n    name\n    values {\n      name\n    }\n  }\n  fragments {\n    name\n    text\n    typeCondition\n    variables {\n      name\n      type {\n        text\n      }\n    }\n    denestedSelections {\n      ... DenestedSelections\n    }\n  }\n  operations {\n    name\n    operationType\n    text\n    variables {\n      name\n      type {\n        text\n      }\n    }\n    denestedSelections {\n      ... DenestedSelections\n    }\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "graphQL",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "objectTypes {\n  interfaces\n  name\n  fields {\n    name\n    parameters {\n      name\n      type {\n        text\n      }\n    }\n    type {\n      text\n    }\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "objectTypes",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "interfaces",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "interfaces",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "fields {\n  name\n  parameters {\n    name\n    type {\n      text\n    }\n  }\n  type {\n    text\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "fields",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "parameters {\n  name\n  type {\n    text\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "parameters",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "type {\n  text\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "type",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 5,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "type {\n  text\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "type",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "inputObjectTypes {\n  name\n  fields {\n    name\n    type {\n      text\n    }\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "inputObjectTypes",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "fields {\n  name\n  type {\n    text\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "fields",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "type {\n  text\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "type",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "interfaceTypes {\n  name\n  fields {\n    name\n    parameters {\n      name\n      type {\n        text\n      }\n    }\n    type {\n      text\n    }\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "interfaceTypes",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "fields {\n  name\n  parameters {\n    name\n    type {\n      text\n    }\n  }\n  type {\n    text\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "fields",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "parameters {\n  name\n  type {\n    text\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "parameters",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "type {\n  text\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "type",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 5,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "type {\n  text\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "type",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "enumerations {\n  name\n  values {\n    name\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "enumerations",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "values {\n  name\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "values",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "fragments {\n  name\n  text\n  typeCondition\n  variables {\n    name\n    type {\n      text\n    }\n  }\n  denestedSelections {\n    ... DenestedSelections\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "fragments",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "typeCondition",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "typeCondition",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "variables {\n  name\n  type {\n    text\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "variables",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "type {\n  text\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "type",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "denestedSelections {\n  ... DenestedSelections\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "denestedSelections",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "... DenestedSelections",
                                     "__typename": "GraphQLFragmentSpreadSelection",
                                     "fragmentName": "DenestedSelections"
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                                     "text": "operations {\n  name\n  operationType\n  text\n  variables {\n    name\n    type {\n      text\n    }\n  }\n  denestedSelections {\n    ... DenestedSelections\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "operations",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "operationType",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "operationType",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "variables {\n  name\n  type {\n    text\n  }\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "variables",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "name",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "name",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                                     "text": "type {\n  text\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "type",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 4,
                                 "item": {
                                   "selection": {
                                     "text": "text",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "text",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                                     "text": "denestedSelections {\n  ... DenestedSelections\n}",
                                     "__typename": "GraphQLFieldSelection",
                                     "name": "denestedSelections",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFragmentSpreadSelection",
                                     "text": "... DenestedSelections",
                                     "fragmentName": "DenestedSelections"
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "Test",
                             "operationType": "QUERY",
                             "text": "query Test($graphql: [AdditionalFileInput!]!) {\n  graphQL(additionalFiles: $graphql) {\n    fragments {\n      denestedSelections {\n        ... DenestedSelections\n      }\n    }\n    operations {\n      denestedSelections {\n        ... DenestedSelections\n      }\n    }\n  }\n}",
                             "variables": [
                               {
                                 "name": "graphql",
                                 "type": {
                                   "text": "[AdditionalFileInput!]!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFieldSelection",
                                     "text": "graphQL(additionalFiles: $graphql) {\n  fragments {\n    denestedSelections {\n      ... DenestedSelections\n    }\n  }\n  operations {\n    denestedSelections {\n      ... DenestedSelections\n    }\n  }\n}",
                                     "name": "graphQL",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFieldSelection",
                                     "text": "fragments {\n  denestedSelections {\n    ... DenestedSelections\n  }\n}",
                                     "name": "fragments",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFieldSelection",
                                     "text": "denestedSelections {\n  ... DenestedSelections\n}",
                                     "name": "denestedSelections",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFragmentSpreadSelection",
                                     "text": "... DenestedSelections",
                                     "fragmentName": "DenestedSelections"
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFieldSelection",
                                     "text": "operations {\n  denestedSelections {\n    ... DenestedSelections\n  }\n}",
                                     "name": "operations",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 2,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFieldSelection",
                                     "text": "denestedSelections {\n  ... DenestedSelections\n}",
                                     "name": "denestedSelections",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 3,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFragmentSpreadSelection",
                                     "text": "... DenestedSelections",
                                     "fragmentName": "DenestedSelections"
                                   }
                                 }
                               }
                             ]
                           },
                           {
                             "name": "GetCaret",
                             "operationType": "QUERY",
                             "text": "query GetCaret($caretId: String!) {\n  caret(caretId: $caretId) {\n    string\n  }\n}",
                             "variables": [
                               {
                                 "name": "caretId",
                                 "type": {
                                   "text": "String!"
                                 }
                               }
                             ],
                             "denestedSelections": [
                               {
                                 "depth": 0,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFieldSelection",
                                     "text": "caret(caretId: $caretId) {\n  string\n}",
                                     "name": "caret",
                                     "alias": null
                                   }
                                 }
                               },
                               {
                                 "depth": 1,
                                 "item": {
                                   "selection": {
                   "__typename": "GraphQLFieldSelection",
                                     "text": "string",
                                     "name": "string",
                                     "alias": null
                                   }
                                 }
                               }
                             ]
                           }
                         ]
                       }
                     }
                   }
                   """;

        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        
        var result = JsonSerializer.Deserialize<
          GraphQLResponse<ParseGraphQLSchemaAndOperationsData>
        >(
          response,
          GraphQLClientJsonSerializerContext
            .Default
            .GraphQLResponseParseGraphQLSchemaAndOperationsData
        );
        
        Console.WriteLine("asd");
    }
}