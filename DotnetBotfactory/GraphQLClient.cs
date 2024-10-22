using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodegenBot;

namespace DotnetBotfactory;

public partial class GraphQLResponse<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("errors")]
    public List<GraphQLError>? Errors { get; set; }
}

public partial class GraphQLError
{
    [JsonPropertyName("message")]
    public required string Message { get; set; }
}

[JsonSerializable(typeof(GraphQLError))]
[JsonSerializable(typeof(DotnetCopybotStringVariant))]
[JsonSerializable(typeof(DotnetLanguage))]
[JsonSerializable(typeof(DotnetVersion))]
[JsonSerializable(typeof(FileKind))]
[JsonSerializable(typeof(FileVersion))]
[JsonSerializable(typeof(GraphQLOperationType))]
[JsonSerializable(typeof(LogSeverity))]
[JsonSerializable(typeof(AdditionalFileInput))]
[JsonSerializable(typeof(BotDependencyInput))]
[JsonSerializable(typeof(CaretTagInput))]
[JsonSerializable(typeof(AddFileVariables))]
[JsonSerializable(typeof(AddFileData))]
[JsonSerializable(typeof(GraphQLResponse<AddFileData>))]
[JsonSerializable(typeof(GraphQLRequest<AddFileVariables>))]
[JsonSerializable(typeof(AddFileAddFile))]
[JsonSerializable(typeof(AddKeyedTextVariables))]
[JsonSerializable(typeof(AddKeyedTextData))]
[JsonSerializable(typeof(GraphQLResponse<AddKeyedTextData>))]
[JsonSerializable(typeof(GraphQLRequest<AddKeyedTextVariables>))]
[JsonSerializable(typeof(AddKeyedTextAddKeyedText))]
[JsonSerializable(typeof(AddKeyedTextByTagsVariables))]
[JsonSerializable(typeof(AddKeyedTextByTagsData))]
[JsonSerializable(typeof(GraphQLResponse<AddKeyedTextByTagsData>))]
[JsonSerializable(typeof(GraphQLRequest<AddKeyedTextByTagsVariables>))]
[JsonSerializable(typeof(AddKeyedTextByTagsAddKeyedTextByTags))]
[JsonSerializable(typeof(AddTextVariables))]
[JsonSerializable(typeof(AddTextData))]
[JsonSerializable(typeof(GraphQLResponse<AddTextData>))]
[JsonSerializable(typeof(GraphQLRequest<AddTextVariables>))]
[JsonSerializable(typeof(AddTextAddText))]
[JsonSerializable(typeof(AddTextByTagsVariables))]
[JsonSerializable(typeof(AddTextByTagsData))]
[JsonSerializable(typeof(GraphQLResponse<AddTextByTagsData>))]
[JsonSerializable(typeof(GraphQLRequest<AddTextByTagsVariables>))]
[JsonSerializable(typeof(AddTextByTagsAddTextByTags))]
[JsonSerializable(typeof(GetCaretVariables))]
[JsonSerializable(typeof(GetCaretData))]
[JsonSerializable(typeof(GraphQLResponse<GetCaretData>))]
[JsonSerializable(typeof(GraphQLRequest<GetCaretVariables>))]
[JsonSerializable(typeof(GetCaretCaret))]
[JsonSerializable(typeof(GetConfigurationVariables))]
[JsonSerializable(typeof(GetConfigurationData))]
[JsonSerializable(typeof(GraphQLResponse<GetConfigurationData>))]
[JsonSerializable(typeof(GraphQLRequest<GetConfigurationVariables>))]
[JsonSerializable(typeof(GetConfigurationConfiguration))]
[JsonSerializable(typeof(GetConfigurationConfigurationCopybots))]
[JsonSerializable(typeof(GetConfigurationConfigurationCopybotsFieldDefinitions))]
[JsonSerializable(typeof(GetFilesVariables))]
[JsonSerializable(typeof(GetFilesData))]
[JsonSerializable(typeof(GraphQLResponse<GetFilesData>))]
[JsonSerializable(typeof(GraphQLRequest<GetFilesVariables>))]
[JsonSerializable(typeof(GetFilesFiles))]
[JsonSerializable(typeof(GetSchemaVariables))]
[JsonSerializable(typeof(GetSchemaData))]
[JsonSerializable(typeof(GraphQLResponse<GetSchemaData>))]
[JsonSerializable(typeof(GraphQLRequest<GetSchemaVariables>))]
[JsonSerializable(typeof(GetSchemaBotSpec))]
[JsonSerializable(typeof(LogVariables))]
[JsonSerializable(typeof(LogData))]
[JsonSerializable(typeof(GraphQLResponse<LogData>))]
[JsonSerializable(typeof(GraphQLRequest<LogVariables>))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsData))]
[JsonSerializable(typeof(GraphQLResponse<ParseGraphQLSchemaAndOperationsData>))]
[JsonSerializable(typeof(GraphQLRequest<ParseGraphQLSchemaAndOperationsVariables>))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQL))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsParameters))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsParametersType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsParameters))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsParametersType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLEnumerations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLEnumerationsValues))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLFragments))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLFragmentsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLFragmentsVariablesType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLFragmentsDenestedSelections))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLFragmentsDenestedSelectionsItem))]
[JsonSerializable(typeof(IGraphQLSelection))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLOperations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLOperationsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLOperationsVariablesType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLOperationsDenestedSelections))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsGraphQLOperationsDenestedSelectionsItem))]
[JsonSerializable(typeof(IGraphQLSelection))]
[JsonSerializable(typeof(ReadTextFileVariables))]
[JsonSerializable(typeof(ReadTextFileData))]
[JsonSerializable(typeof(GraphQLResponse<ReadTextFileData>))]
[JsonSerializable(typeof(GraphQLRequest<ReadTextFileVariables>))]
[JsonSerializable(typeof(ReadTextFileWithVersionVariables))]
[JsonSerializable(typeof(ReadTextFileWithVersionData))]
[JsonSerializable(typeof(GraphQLResponse<ReadTextFileWithVersionData>))]
[JsonSerializable(typeof(GraphQLRequest<ReadTextFileWithVersionVariables>))]
[JsonSerializable(typeof(TestVariables))]
[JsonSerializable(typeof(TestData))]
[JsonSerializable(typeof(GraphQLResponse<TestData>))]
[JsonSerializable(typeof(GraphQLRequest<TestVariables>))]
[JsonSerializable(typeof(TestGraphQL))]
[JsonSerializable(typeof(TestGraphQLFragments))]
[JsonSerializable(typeof(TestGraphQLFragmentsDenestedSelections))]
[JsonSerializable(typeof(TestGraphQLFragmentsDenestedSelectionsItem))]
[JsonSerializable(typeof(IGraphQLSelection))]
[JsonSerializable(typeof(TestGraphQLOperations))]
[JsonSerializable(typeof(TestGraphQLOperationsDenestedSelections))]
[JsonSerializable(typeof(TestGraphQLOperationsDenestedSelectionsItem))]
[JsonSerializable(typeof(IGraphQLSelection))]
public partial class GraphQLClientJsonSerializerContext : JsonSerializerContext { }

public static partial class GraphQLClient
{
    public static AddFileData AddFile(string filePath, string textAndCarets)
    {
        var request = new GraphQLRequest<AddFileVariables>
        {
            Query = """
                mutation AddFile($filePath: String!, $textAndCarets: String!) {
                  addFile(filePath: $filePath, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddFile",
            Variables = new AddFileVariables()
            {
                FilePath = filePath,
                TextAndCarets = textAndCarets,
            },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddFileVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddFileData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddFileData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddFile.");
    }

    public static AddKeyedTextData AddKeyedText(string key, string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddKeyedTextVariables>
        {
            Query = """
                mutation AddKeyedText($key: String!, $caretId: String!, $textAndCarets: String!) {
                  addKeyedText(key: $key, caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddKeyedText",
            Variables = new AddKeyedTextVariables()
            {
                Key = key,
                CaretId = caretId,
                TextAndCarets = textAndCarets,
            },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddKeyedTextVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddKeyedTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddKeyedText.");
    }

    public static AddKeyedTextByTagsData AddKeyedTextByTags(
        string key,
        List<CaretTagInput> tags,
        string textAndCarets
    )
    {
        var request = new GraphQLRequest<AddKeyedTextByTagsVariables>
        {
            Query = """
                mutation AddKeyedTextByTags($key: String!, $tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addKeyedTextByTags(key: $key, tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddKeyedTextByTags",
            Variables = new AddKeyedTextByTagsVariables()
            {
                Key = key,
                Tags = tags,
                TextAndCarets = textAndCarets,
            },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddKeyedTextByTagsVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddKeyedTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request AddKeyedTextByTags."
            );
    }

    public static AddTextData AddText(string caretId, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextVariables>
        {
            Query = """
                mutation AddText($caretId: String!, $textAndCarets: String!) {
                  addText(caretId: $caretId, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddText",
            Variables = new AddTextVariables() { CaretId = caretId, TextAndCarets = textAndCarets },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddTextVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddText.");
    }

    public static AddTextByTagsData AddTextByTags(List<CaretTagInput> tags, string textAndCarets)
    {
        var request = new GraphQLRequest<AddTextByTagsVariables>
        {
            Query = """
                mutation AddTextByTags($tags: [CaretTagInput!]!, $textAndCarets: String!) {
                  addTextByTags(tags: $tags, textAndCarets: $textAndCarets) {
                    id
                  }
                }
                """,
            OperationName = "AddTextByTags",
            Variables = new AddTextByTagsVariables() { Tags = tags, TextAndCarets = textAndCarets },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestAddTextByTagsVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddTextByTags.");
    }

    public static GetCaretData GetCaret(string caretId)
    {
        var request = new GraphQLRequest<GetCaretVariables>
        {
            Query = """
                query GetCaret($caretId: String!) {
                  caret(caretId: $caretId) {
                    string
                  }
                }
                """,
            OperationName = "GetCaret",
            Variables = new GetCaretVariables() { CaretId = caretId },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetCaretVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetCaretData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetCaretData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request GetCaret.");
    }

    public static GetConfigurationData GetConfiguration()
    {
        var request = new GraphQLRequest<GetConfigurationVariables>
        {
            Query = """
                query GetConfiguration {
                  configuration {
                    id
                    projectName
                    outputPath
                    minimalWorkingExample
                    dotnetVersion
                    provideApi
                    language
                    copybots {
                      name
                      inputDirectory
                      whitelist
                      fieldDefinitions {
                        needle
                        fieldName
                        variants
                      }
                    }
                  }
                }
                """,
            OperationName = "GetConfiguration",
            Variables = new GetConfigurationVariables() { },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetConfigurationVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetConfigurationData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetConfigurationData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request GetConfiguration."
            );
    }

    public static GetFilesData GetFiles(List<string> whitelist, List<string> blacklist)
    {
        var request = new GraphQLRequest<GetFilesVariables>
        {
            Query = """
                query GetFiles($whitelist: [String!]!, $blacklist: [String!]!) {
                  files(whitelist: $whitelist, blacklist: $blacklist) {
                    path
                    kind
                  }
                }
                """,
            OperationName = "GetFiles",
            Variables = new GetFilesVariables() { Whitelist = whitelist, Blacklist = blacklist },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetFilesVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetFilesData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetFilesData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request GetFiles.");
    }

    public static GetSchemaData GetSchema(string botFilePath)
    {
        var request = new GraphQLRequest<GetSchemaVariables>
        {
            Query = """
                query GetSchema($botFilePath: String!) {
                  botSchema(botFilePath: $botFilePath)
                  botSpec(botFilePath: $botFilePath) {
                    consumedSchemaPath
                    excludeConfigurationFromConsumedSchema
                    providedSchemaPath
                  }
                }
                """,
            OperationName = "GetSchema",
            Variables = new GetSchemaVariables() { BotFilePath = botFilePath },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetSchemaVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetSchemaData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseGetSchemaData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request GetSchema.");
    }

    public static LogData Log(LogSeverity severity, string message, List<string>? arguments)
    {
        var request = new GraphQLRequest<LogVariables>
        {
            Query = """
                mutation Log($severity: LogSeverity!, $message: String!, $arguments: [String!]) {
                  log(severity: $severity, message: $message, arguments: $arguments)
                }
                """,
            OperationName = "Log",
            Variables = new LogVariables()
            {
                Severity = severity,
                Message = message,
                Arguments = arguments,
            },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestLogVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<LogData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseLogData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request Log.");
    }

    public static ParseGraphQLSchemaAndOperationsData ParseGraphQLSchemaAndOperations(
        List<AdditionalFileInput> graphql
    )
    {
        var request = new GraphQLRequest<ParseGraphQLSchemaAndOperationsVariables>
        {
            Query = """
                query ParseGraphQLSchemaAndOperations($graphql: [AdditionalFileInput!]!) {
                  graphQL(additionalFiles: $graphql) {
                    objectTypes {
                      interfaces
                      name
                      fields {
                        name
                        parameters {
                          name
                          type {
                            text
                          }
                        }
                        type {
                          text
                        }
                      }
                    }
                    inputObjectTypes {
                      name
                      fields {
                        name
                        type {
                          text
                        }
                      }
                    }
                    interfaceTypes {
                      name
                      fields {
                        name
                        parameters {
                          name
                          type {
                            text
                          }
                        }
                        type {
                          text
                        }
                      }
                    }
                    enumerations {
                      name
                      values {
                        name
                      }
                    }
                    fragments {
                      name
                      text
                      typeCondition
                      variables {
                        name
                        type {
                          text
                        }
                      }
                      denestedSelections {
                        ... DenestedSelections
                      }
                    }
                    operations {
                      name
                      operationType
                      text
                      variables {
                        name
                        type {
                          text
                        }
                      }
                      denestedSelections {
                        ... DenestedSelections
                      }
                    }
                  }
                }fragment DenestedSelections on DenestedOfSelectionItem {
                  depth
                  item {
                    selection {
                      text
                      __typename
                      ... on GraphQLFieldSelection {
                        name
                        alias
                      }
                      ... on GraphQLFragmentSpreadSelection {
                        fragmentName
                      }
                      ... on GraphQLInlineFragmentSelection {
                        typeName
                      }
                    }
                  }
                }
                """,
            OperationName = "ParseGraphQLSchemaAndOperations",
            Variables = new ParseGraphQLSchemaAndOperationsVariables() { Graphql = graphql },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext
                .Default
                .GraphQLRequestParseGraphQLSchemaAndOperationsVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<
            GraphQLResponse<ParseGraphQLSchemaAndOperationsData>
        >(
            response,
            GraphQLClientJsonSerializerContext
                .Default
                .GraphQLResponseParseGraphQLSchemaAndOperationsData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request ParseGraphQLSchemaAndOperations."
            );
    }

    public static ReadTextFileData ReadTextFile(string textFilePath)
    {
        var request = new GraphQLRequest<ReadTextFileVariables>
        {
            Query = """
                query ReadTextFile($textFilePath: String!) {
                  readTextFile(textFilePath: $textFilePath)
                }
                """,
            OperationName = "ReadTextFile",
            Variables = new ReadTextFileVariables() { TextFilePath = textFilePath },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestReadTextFileVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<ReadTextFileData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseReadTextFileData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request ReadTextFile.");
    }

    public static ReadTextFileWithVersionData ReadTextFileWithVersion(
        string textFilePath,
        FileVersion? fileVersion
    )
    {
        var request = new GraphQLRequest<ReadTextFileWithVersionVariables>
        {
            Query = """
                query ReadTextFileWithVersion($textFilePath: String!, $fileVersion: FileVersion) {
                  readTextFile(textFilePath: $textFilePath, fileVersion: $fileVersion)
                }
                """,
            OperationName = "ReadTextFileWithVersion",
            Variables = new ReadTextFileWithVersionVariables()
            {
                TextFilePath = textFilePath,
                FileVersion = fileVersion,
            },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext
                .Default
                .GraphQLRequestReadTextFileWithVersionVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<ReadTextFileWithVersionData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseReadTextFileWithVersionData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request ReadTextFileWithVersion."
            );
    }

    public static TestData Test(List<AdditionalFileInput> graphql)
    {
        var request = new GraphQLRequest<TestVariables>
        {
            Query = """
                query Test($graphql: [AdditionalFileInput!]!) {
                  graphQL(additionalFiles: $graphql) {
                    fragments {
                      denestedSelections {
                        ... DenestedSelections
                      }
                    }
                    operations {
                      denestedSelections {
                        ... DenestedSelections
                      }
                    }
                  }
                }fragment DenestedSelections on DenestedOfSelectionItem {
                  depth
                  item {
                    selection {
                      text
                      __typename
                      ... on GraphQLFieldSelection {
                        name
                        alias
                      }
                      ... on GraphQLFragmentSpreadSelection {
                        fragmentName
                      }
                      ... on GraphQLInlineFragmentSelection {
                        typeName
                      }
                    }
                  }
                }
                """,
            OperationName = "Test",
            Variables = new TestVariables() { Graphql = graphql },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestTestVariables
        );
        response = JsonUtility.EnsureTypeDiscriminatorPropertiesComeFirst(response);
        var result = JsonSerializer.Deserialize<GraphQLResponse<TestData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseTestData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request Test.");
    }
}

[JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
public enum DotnetCopybotStringVariant
{
    [EnumMember(Value = "CamelCase")]
    CamelCase,

    [EnumMember(Value = "SnakeCase")]
    SnakeCase,

    [EnumMember(Value = "UpperSnakeCase")]
    UpperSnakeCase,

    [EnumMember(Value = "LowerSnakeCase")]
    LowerSnakeCase,

    [EnumMember(Value = "KebabCase")]
    KebabCase,

    [EnumMember(Value = "UpperKebabCase")]
    UpperKebabCase,

    [EnumMember(Value = "LowerKebabCase")]
    LowerKebabCase,

    [EnumMember(Value = "LowerCase")]
    LowerCase,

    [EnumMember(Value = "UpperCase")]
    UpperCase,
}

[JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
public enum DotnetLanguage
{
    [EnumMember(Value = "CSHARP")]
    CSHARP,

    [EnumMember(Value = "RUST")]
    RUST,

    [EnumMember(Value = "TYPESCRIPT")]
    TYPESCRIPT,

    [EnumMember(Value = "GO")]
    GO,

    [EnumMember(Value = "KOTLIN")]
    KOTLIN,
}

[JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
public enum DotnetVersion
{
    [EnumMember(Value = "DotNet8")]
    DotNet8,
}

[JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
public enum FileKind
{
    [EnumMember(Value = "BINARY")]
    BINARY,

    [EnumMember(Value = "TEXT")]
    TEXT,
}

[JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
public enum FileVersion
{
    [EnumMember(Value = "GENERATED")]
    GENERATED,

    [EnumMember(Value = "HEAD")]
    HEAD,
}

[JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
public enum GraphQLOperationType
{
    [EnumMember(Value = "QUERY")]
    QUERY,

    [EnumMember(Value = "MUTATION")]
    MUTATION,

    [EnumMember(Value = "SUBSCRIPTION")]
    SUBSCRIPTION,
}

[JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
public enum LogSeverity
{
    [EnumMember(Value = "TRACE")]
    TRACE,

    [EnumMember(Value = "DEBUG")]
    DEBUG,

    [EnumMember(Value = "INFORMATION")]
    INFORMATION,

    [EnumMember(Value = "WARNING")]
    WARNING,

    [EnumMember(Value = "ERROR")]
    ERROR,

    [EnumMember(Value = "CRITICAL")]
    CRITICAL,
}

public partial class AdditionalFileInput
{
    [JsonPropertyName("filePath")]
    public required string FilePath { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }
}

public partial class BotDependencyInput
{
    [JsonPropertyName("botId")]
    public required string BotId { get; set; }

    [JsonPropertyName("botVersion")]
    public required string BotVersion { get; set; }
}

public partial class CaretTagInput
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}

public partial class AddFileData
{
    [JsonPropertyName("addFile")]
    public required AddFileAddFile AddFile { get; set; }
}

public partial class AddFileVariables
{
    [JsonPropertyName("filePath")]
    public required string FilePath { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddFileAddFile
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddKeyedTextData
{
    [JsonPropertyName("addKeyedText")]
    public required AddKeyedTextAddKeyedText AddKeyedText { get; set; }
}

public partial class AddKeyedTextVariables
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddKeyedTextAddKeyedText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddKeyedTextByTagsData
{
    [JsonPropertyName("addKeyedTextByTags")]
    public required List<AddKeyedTextByTagsAddKeyedTextByTags> AddKeyedTextByTags { get; set; }
}

public partial class AddKeyedTextByTagsVariables
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("tags")]
    public required List<CaretTagInput> Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddKeyedTextByTagsAddKeyedTextByTags
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddTextData
{
    [JsonPropertyName("addText")]
    public required AddTextAddText AddText { get; set; }
}

public partial class AddTextVariables
{
    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddTextAddText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddTextByTagsData
{
    [JsonPropertyName("addTextByTags")]
    public required List<AddTextByTagsAddTextByTags> AddTextByTags { get; set; }
}

public partial class AddTextByTagsVariables
{
    [JsonPropertyName("tags")]
    public required List<CaretTagInput> Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddTextByTagsAddTextByTags
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class GetCaretData
{
    [JsonPropertyName("caret")]
    public GetCaretCaret? Caret { get; set; }
}

public partial class GetCaretVariables
{
    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }
}

public partial class GetCaretCaret
{
    [JsonPropertyName("string")]
    public required string String { get; set; }
}

public partial class GetConfigurationData
{
    [JsonPropertyName("configuration")]
    public required GetConfigurationConfiguration Configuration { get; set; }
}

public partial class GetConfigurationVariables { }

public partial class GetConfigurationConfiguration
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("projectName")]
    public required string ProjectName { get; set; }

    [JsonPropertyName("outputPath")]
    public required string OutputPath { get; set; }

    [JsonPropertyName("minimalWorkingExample")]
    public required bool MinimalWorkingExample { get; set; }

    [JsonPropertyName("dotnetVersion")]
    public required DotnetVersion DotnetVersion { get; set; }

    [JsonPropertyName("provideApi")]
    public required bool ProvideApi { get; set; }

    [JsonPropertyName("language")]
    public required DotnetLanguage Language { get; set; }

    [JsonPropertyName("copybots")]
    public required List<GetConfigurationConfigurationCopybots> Copybots { get; set; }
}

public partial class GetConfigurationConfigurationCopybots
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("inputDirectory")]
    public required string InputDirectory { get; set; }

    [JsonPropertyName("whitelist")]
    public required List<string> Whitelist { get; set; }

    [JsonPropertyName("fieldDefinitions")]
    public required List<GetConfigurationConfigurationCopybotsFieldDefinitions> FieldDefinitions { get; set; }
}

public partial class GetConfigurationConfigurationCopybotsFieldDefinitions
{
    [JsonPropertyName("needle")]
    public required string Needle { get; set; }

    [JsonPropertyName("fieldName")]
    public required string FieldName { get; set; }

    [JsonPropertyName("variants")]
    public List<DotnetCopybotStringVariant>? Variants { get; set; }
}

public partial class GetFilesData
{
    [JsonPropertyName("files")]
    public required List<GetFilesFiles> Files { get; set; }
}

public partial class GetFilesVariables
{
    [JsonPropertyName("whitelist")]
    public required List<string> Whitelist { get; set; }

    [JsonPropertyName("blacklist")]
    public required List<string> Blacklist { get; set; }
}

public partial class GetFilesFiles
{
    [JsonPropertyName("path")]
    public required string Path { get; set; }

    [JsonPropertyName("kind")]
    public required FileKind Kind { get; set; }
}

public partial class GetSchemaData
{
    [JsonPropertyName("botSchema")]
    public string? BotSchema { get; set; }

    [JsonPropertyName("botSpec")]
    public GetSchemaBotSpec? BotSpec { get; set; }
}

public partial class GetSchemaVariables
{
    [JsonPropertyName("botFilePath")]
    public required string BotFilePath { get; set; }
}

public partial class GetSchemaBotSpec
{
    [JsonPropertyName("consumedSchemaPath")]
    public string? ConsumedSchemaPath { get; set; }

    [JsonPropertyName("excludeConfigurationFromConsumedSchema")]
    public bool? ExcludeConfigurationFromConsumedSchema { get; set; }

    [JsonPropertyName("providedSchemaPath")]
    public string? ProvidedSchemaPath { get; set; }
}

public partial class LogData
{
    [JsonPropertyName("log")]
    public required string Log { get; set; }
}

public partial class LogVariables
{
    [JsonPropertyName("severity")]
    public required LogSeverity Severity { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("arguments")]
    public List<string>? Arguments { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsData
{
    [JsonPropertyName("graphQL")]
    public required ParseGraphQLSchemaAndOperationsGraphQL GraphQL { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsVariables
{
    [JsonPropertyName("graphql")]
    public required List<AdditionalFileInput> Graphql { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQL
{
    [JsonPropertyName("objectTypes")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLObjectTypes> ObjectTypes { get; set; }

    [JsonPropertyName("inputObjectTypes")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypes> InputObjectTypes { get; set; }

    [JsonPropertyName("interfaceTypes")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypes> InterfaceTypes { get; set; }

    [JsonPropertyName("enumerations")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLEnumerations> Enumerations { get; set; }

    [JsonPropertyName("fragments")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLFragments> Fragments { get; set; }

    [JsonPropertyName("operations")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLOperations> Operations { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLObjectTypes
{
    [JsonPropertyName("interfaces")]
    public required List<string> Interfaces { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFields> Fields { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("parameters")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsParameters> Parameters { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsParameters
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsParametersType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsParametersType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypesFields> Fields { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypesFieldsType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInputObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFields> Fields { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("parameters")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsParameters> Parameters { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsParameters
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsParametersType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsParametersType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLInterfaceTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLEnumerations
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("values")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLEnumerationsValues> Values { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLEnumerationsValues
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLFragments
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("typeCondition")]
    public required string TypeCondition { get; set; }

    [JsonPropertyName("variables")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLFragmentsVariables> Variables { get; set; }

    [JsonPropertyName("denestedSelections")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLFragmentsDenestedSelections> DenestedSelections { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLFragmentsVariables
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsGraphQLFragmentsVariablesType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLFragmentsVariablesType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLFragmentsDenestedSelections
    : IDenestedSelections
{
    [JsonPropertyName("depth")]
    public required int Depth { get; set; }

    [JsonPropertyName("item")]
    public required ParseGraphQLSchemaAndOperationsGraphQLFragmentsDenestedSelectionsItem Item { get; set; }
}

public partial interface IDenestedSelections { }

public partial class ParseGraphQLSchemaAndOperationsGraphQLFragmentsDenestedSelectionsItem
    : IDenestedSelections
{
    [JsonPropertyName("selection")]
    public required IGraphQLSelection Selection { get; set; }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "__typename")]
[JsonDerivedType(typeof(GraphQLSelectionGraphQLFieldSelection), "GraphQLFieldSelection")]
[JsonDerivedType(
    typeof(GraphQLSelectionGraphQLFragmentSpreadSelection),
    "GraphQLFragmentSpreadSelection"
)]
[JsonDerivedType(
    typeof(GraphQLSelectionGraphQLInlineFragmentSelection),
    "GraphQLInlineFragmentSelection"
)]
public partial interface IGraphQLSelection
{
    [JsonPropertyName("text")]
    string Text { get; set; }

    [JsonPropertyName("__typename")]
    string _Typename { get; set; }
}

public partial class GraphQLSelectionGraphQLFieldSelection : IGraphQLSelection
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("__typename")]
    public required string _Typename { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }
}

public partial class GraphQLSelectionGraphQLFragmentSpreadSelection : IGraphQLSelection
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("__typename")]
    public required string _Typename { get; set; }

    [JsonPropertyName("fragmentName")]
    public required string FragmentName { get; set; }
}

public partial class GraphQLSelectionGraphQLInlineFragmentSelection : IGraphQLSelection
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("__typename")]
    public required string _Typename { get; set; }

    [JsonPropertyName("typeName")]
    public required string TypeName { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLOperations
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("operationType")]
    public required GraphQLOperationType OperationType { get; set; }

    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("variables")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLOperationsVariables> Variables { get; set; }

    [JsonPropertyName("denestedSelections")]
    public required List<ParseGraphQLSchemaAndOperationsGraphQLOperationsDenestedSelections> DenestedSelections { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLOperationsVariables
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsGraphQLOperationsVariablesType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLOperationsVariablesType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLOperationsDenestedSelections
    : IDenestedSelections
{
    [JsonPropertyName("depth")]
    public required int Depth { get; set; }

    [JsonPropertyName("item")]
    public required ParseGraphQLSchemaAndOperationsGraphQLOperationsDenestedSelectionsItem Item { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsGraphQLOperationsDenestedSelectionsItem
    : IDenestedSelections
{
    [JsonPropertyName("selection")]
    public required IGraphQLSelection Selection { get; set; }
}

public partial class ReadTextFileData
{
    [JsonPropertyName("readTextFile")]
    public string? ReadTextFile { get; set; }
}

public partial class ReadTextFileVariables
{
    [JsonPropertyName("textFilePath")]
    public required string TextFilePath { get; set; }
}

public partial class ReadTextFileWithVersionData
{
    [JsonPropertyName("readTextFile")]
    public string? ReadTextFile { get; set; }
}

public partial class ReadTextFileWithVersionVariables
{
    [JsonPropertyName("textFilePath")]
    public required string TextFilePath { get; set; }

    [JsonPropertyName("fileVersion")]
    public FileVersion? FileVersion { get; set; }
}

public partial class TestData
{
    [JsonPropertyName("graphQL")]
    public required TestGraphQL GraphQL { get; set; }
}

public partial class TestVariables
{
    [JsonPropertyName("graphql")]
    public required List<AdditionalFileInput> Graphql { get; set; }
}

public partial class TestGraphQL
{
    [JsonPropertyName("fragments")]
    public required List<TestGraphQLFragments> Fragments { get; set; }

    [JsonPropertyName("operations")]
    public required List<TestGraphQLOperations> Operations { get; set; }
}

public partial class TestGraphQLFragments
{
    [JsonPropertyName("denestedSelections")]
    public required List<TestGraphQLFragmentsDenestedSelections> DenestedSelections { get; set; }
}

public partial class TestGraphQLFragmentsDenestedSelections : IDenestedSelections
{
    [JsonPropertyName("depth")]
    public required int Depth { get; set; }

    [JsonPropertyName("item")]
    public required TestGraphQLFragmentsDenestedSelectionsItem Item { get; set; }
}

public partial class TestGraphQLFragmentsDenestedSelectionsItem : IDenestedSelections
{
    [JsonPropertyName("selection")]
    public required IGraphQLSelection Selection { get; set; }
}

public partial class TestGraphQLOperations
{
    [JsonPropertyName("denestedSelections")]
    public required List<TestGraphQLOperationsDenestedSelections> DenestedSelections { get; set; }
}

public partial class TestGraphQLOperationsDenestedSelections : IDenestedSelections
{
    [JsonPropertyName("depth")]
    public required int Depth { get; set; }

    [JsonPropertyName("item")]
    public required TestGraphQLOperationsDenestedSelectionsItem Item { get; set; }
}

public partial class TestGraphQLOperationsDenestedSelectionsItem : IDenestedSelections
{
    [JsonPropertyName("selection")]
    public required IGraphQLSelection Selection { get; set; }
}
