using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodegenBot;

namespace DotnetBotfactory;

public class GraphQLResponse<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("errors")]
    public List<GraphQLError>? Errors { get; set; }
}

public class GraphQLError
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
[JsonSerializable(typeof(AddFile))]
[JsonSerializable(typeof(AddKeyedTextVariables))]
[JsonSerializable(typeof(AddKeyedTextData))]
[JsonSerializable(typeof(GraphQLResponse<AddKeyedTextData>))]
[JsonSerializable(typeof(GraphQLRequest<AddKeyedTextVariables>))]
[JsonSerializable(typeof(AddKeyedText))]
[JsonSerializable(typeof(AddKeyedTextByTagsVariables))]
[JsonSerializable(typeof(AddKeyedTextByTagsData))]
[JsonSerializable(typeof(GraphQLResponse<AddKeyedTextByTagsData>))]
[JsonSerializable(typeof(GraphQLRequest<AddKeyedTextByTagsVariables>))]
[JsonSerializable(typeof(AddKeyedTextByTags))]
[JsonSerializable(typeof(AddTextVariables))]
[JsonSerializable(typeof(AddTextData))]
[JsonSerializable(typeof(GraphQLResponse<AddTextData>))]
[JsonSerializable(typeof(GraphQLRequest<AddTextVariables>))]
[JsonSerializable(typeof(AddText))]
[JsonSerializable(typeof(AddTextByTagsVariables))]
[JsonSerializable(typeof(AddTextByTagsData))]
[JsonSerializable(typeof(GraphQLResponse<AddTextByTagsData>))]
[JsonSerializable(typeof(GraphQLRequest<AddTextByTagsVariables>))]
[JsonSerializable(typeof(AddTextByTags))]
[JsonSerializable(typeof(GetConfigurationVariables))]
[JsonSerializable(typeof(GetConfigurationData))]
[JsonSerializable(typeof(GraphQLResponse<GetConfigurationData>))]
[JsonSerializable(typeof(GraphQLRequest<GetConfigurationVariables>))]
[JsonSerializable(typeof(GetConfiguration))]
[JsonSerializable(typeof(GetConfigurationCopybots))]
[JsonSerializable(typeof(GetConfigurationCopybotsFieldDefinitions))]
[JsonSerializable(typeof(GetFilesVariables))]
[JsonSerializable(typeof(GetFilesData))]
[JsonSerializable(typeof(GraphQLResponse<GetFilesData>))]
[JsonSerializable(typeof(GraphQLRequest<GetFilesVariables>))]
[JsonSerializable(typeof(GetFiles))]
[JsonSerializable(typeof(GetSchemaVariables))]
[JsonSerializable(typeof(GetSchemaData))]
[JsonSerializable(typeof(GraphQLResponse<GetSchemaData>))]
[JsonSerializable(typeof(GraphQLRequest<GetSchemaVariables>))]
[JsonSerializable(typeof(GetSchema))]
[JsonSerializable(typeof(LogVariables))]
[JsonSerializable(typeof(LogData))]
[JsonSerializable(typeof(GraphQLResponse<LogData>))]
[JsonSerializable(typeof(GraphQLRequest<LogVariables>))]
[JsonSerializable(typeof(ParseGraphQLSchemaVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaData))]
[JsonSerializable(typeof(GraphQLResponse<ParseGraphQLSchemaData>))]
[JsonSerializable(typeof(GraphQLRequest<ParseGraphQLSchemaVariables>))]
[JsonSerializable(typeof(ParseGraphQLSchema))]
[JsonSerializable(typeof(ParseGraphQLSchemaObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaObjectTypesFieldsArguments))]
[JsonSerializable(typeof(ParseGraphQLSchemaObjectTypesFieldsArgumentsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaInputObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaInputObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaInputObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaEnumerations))]
[JsonSerializable(typeof(ParseGraphQLSchemaEnumerationsValues))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsData))]
[JsonSerializable(typeof(GraphQLResponse<ParseGraphQLSchemaAndOperationsData>))]
[JsonSerializable(typeof(GraphQLRequest<ParseGraphQLSchemaAndOperationsVariables>))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFieldsArguments))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFieldsArgumentsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsInputObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsInputObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsInputObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsEnumerations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsEnumerationsValues))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperationsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperationsVariablesType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperationsNestedSelection))]
[JsonSerializable(typeof(ReadTextFileVariables))]
[JsonSerializable(typeof(ReadTextFileData))]
[JsonSerializable(typeof(GraphQLResponse<ReadTextFileData>))]
[JsonSerializable(typeof(GraphQLRequest<ReadTextFileVariables>))]
[JsonSerializable(typeof(ReadTextFileWithVersionVariables))]
[JsonSerializable(typeof(ReadTextFileWithVersionData))]
[JsonSerializable(typeof(GraphQLResponse<ReadTextFileWithVersionData>))]
[JsonSerializable(typeof(GraphQLRequest<ReadTextFileWithVersionVariables>))]
public partial class GraphQLOperationsJsonSerializerContext : JsonSerializerContext { }

public static partial class GraphQLOperations
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestAddFileVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddFileData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseAddFileData
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestAddKeyedTextVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseAddKeyedTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddKeyedText.");
    }

    public static AddKeyedTextByTagsData AddKeyedTextByTags(
        string key,
        List<CaretTagInput>? tags,
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestAddKeyedTextByTagsVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddKeyedTextByTagsData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseAddKeyedTextByTagsData
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestAddTextVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseAddTextData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddText.");
    }

    public static AddTextByTagsData AddTextByTags(List<CaretTagInput>? tags, string textAndCarets)
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestAddTextByTagsVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextByTagsData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseAddTextByTagsData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request AddTextByTags.");
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestGetConfigurationVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetConfigurationData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseGetConfigurationData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request GetConfiguration."
            );
    }

    public static GetFilesData GetFiles(List<string>? whitelist, List<string>? blacklist)
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestGetFilesVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetFilesData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseGetFilesData
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestGetSchemaVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<GetSchemaData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseGetSchemaData
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestLogVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<LogData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseLogData
        );
        return result?.Data
            ?? throw new InvalidOperationException("Received null data for request Log.");
    }

    public static ParseGraphQLSchemaData ParseGraphQLSchema(string graphql)
    {
        var request = new GraphQLRequest<ParseGraphQLSchemaVariables>
        {
            Query = """
                query ParseGraphQLSchema($graphql: String!) {
                  graphQL(additionalFiles: [ $graphql ]) {
                    objectTypes {
                      name
                      fields {
                        name
                        arguments {
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
                    enumerations {
                      name
                      values {
                        name
                      }
                    }
                  }
                }
                """,
            OperationName = "ParseGraphQLSchema",
            Variables = new ParseGraphQLSchemaVariables() { Graphql = graphql },
        };

        var response = Imports.GraphQL(
            request,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestParseGraphQLSchemaVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<ParseGraphQLSchemaData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseParseGraphQLSchemaData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request ParseGraphQLSchema."
            );
    }

    public static ParseGraphQLSchemaAndOperationsData ParseGraphQLSchemaAndOperations(
        List<AdditionalFileInput>? graphql
    )
    {
        var request = new GraphQLRequest<ParseGraphQLSchemaAndOperationsVariables>
        {
            Query = """
                query ParseGraphQLSchemaAndOperations($graphql: [AdditionalFileInput!]!) {
                  graphQL(additionalFiles: $graphql) {
                    objectTypes {
                      name
                      fields {
                        name
                        arguments {
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
                    enumerations {
                      name
                      values {
                        name
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
                      nestedSelection {
                        depth
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
            GraphQLOperationsJsonSerializerContext
                .Default
                .GraphQLRequestParseGraphQLSchemaAndOperationsVariables
        );
        var result = JsonSerializer.Deserialize<
            GraphQLResponse<ParseGraphQLSchemaAndOperationsData>
        >(
            response,
            GraphQLOperationsJsonSerializerContext
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
            GraphQLOperationsJsonSerializerContext.Default.GraphQLRequestReadTextFileVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<ReadTextFileData>>(
            response,
            GraphQLOperationsJsonSerializerContext.Default.GraphQLResponseReadTextFileData
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
            GraphQLOperationsJsonSerializerContext
                .Default
                .GraphQLRequestReadTextFileWithVersionVariables
        );
        var result = JsonSerializer.Deserialize<GraphQLResponse<ReadTextFileWithVersionData>>(
            response,
            GraphQLOperationsJsonSerializerContext
                .Default
                .GraphQLResponseReadTextFileWithVersionData
        );
        return result?.Data
            ?? throw new InvalidOperationException(
                "Received null data for request ReadTextFileWithVersion."
            );
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

public class AdditionalFileInput
{
    [JsonPropertyName("filePath")]
    public required string FilePath { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }
}

public class BotDependencyInput
{
    [JsonPropertyName("botId")]
    public required string BotId { get; set; }

    [JsonPropertyName("botVersion")]
    public required string BotVersion { get; set; }
}

public class CaretTagInput
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}

public class AddFileData
{
    [JsonPropertyName("addFile")]
    public required AddFile AddFile { get; set; }
}

public class AddFileVariables
{
    [JsonPropertyName("filePath")]
    public required string FilePath { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public class AddFile
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public class AddKeyedTextData
{
    [JsonPropertyName("addKeyedText")]
    public required AddKeyedText AddKeyedText { get; set; }
}

public class AddKeyedTextVariables
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public class AddKeyedText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public class AddKeyedTextByTagsData
{
    [JsonPropertyName("addKeyedTextByTags")]
    public List<AddKeyedTextByTags>? AddKeyedTextByTags { get; set; }
}

public class AddKeyedTextByTagsVariables
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("tags")]
    public List<CaretTagInput>? Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public class AddKeyedTextByTags
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public class AddTextData
{
    [JsonPropertyName("addText")]
    public required AddText AddText { get; set; }
}

public class AddTextVariables
{
    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public class AddText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public class AddTextByTagsData
{
    [JsonPropertyName("addTextByTags")]
    public List<AddTextByTags>? AddTextByTags { get; set; }
}

public class AddTextByTagsVariables
{
    [JsonPropertyName("tags")]
    public List<CaretTagInput>? Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public class AddTextByTags
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public class GetConfigurationData
{
    [JsonPropertyName("configuration")]
    public required GetConfiguration Configuration { get; set; }
}

public class GetConfigurationVariables { }

public class GetConfiguration
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
    public List<GetConfigurationCopybots>? Copybots { get; set; }
}

public class GetConfigurationCopybots
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("inputDirectory")]
    public required string InputDirectory { get; set; }

    [JsonPropertyName("whitelist")]
    public List<string>? Whitelist { get; set; }

    [JsonPropertyName("fieldDefinitions")]
    public List<GetConfigurationCopybotsFieldDefinitions>? FieldDefinitions { get; set; }
}

public class GetConfigurationCopybotsFieldDefinitions
{
    [JsonPropertyName("needle")]
    public required string Needle { get; set; }

    [JsonPropertyName("fieldName")]
    public required string FieldName { get; set; }

    [JsonPropertyName("variants")]
    public List<DotnetCopybotStringVariant>? Variants { get; set; }
}

public class GetFilesData
{
    [JsonPropertyName("files")]
    public List<GetFiles>? Files { get; set; }
}

public class GetFilesVariables
{
    [JsonPropertyName("whitelist")]
    public List<string>? Whitelist { get; set; }

    [JsonPropertyName("blacklist")]
    public List<string>? Blacklist { get; set; }
}

public class GetFiles
{
    [JsonPropertyName("path")]
    public required string Path { get; set; }

    [JsonPropertyName("kind")]
    public required FileKind Kind { get; set; }
}

public class GetSchemaData
{
    [JsonPropertyName("botSchema")]
    public string? BotSchema { get; set; }

    [JsonPropertyName("botSpec")]
    public GetSchema? BotSpec { get; set; }
}

public class GetSchemaVariables
{
    [JsonPropertyName("botFilePath")]
    public required string BotFilePath { get; set; }
}

public class GetSchema
{
    [JsonPropertyName("consumedSchemaPath")]
    public string? ConsumedSchemaPath { get; set; }

    [JsonPropertyName("excludeConfigurationFromConsumedSchema")]
    public bool? ExcludeConfigurationFromConsumedSchema { get; set; }

    [JsonPropertyName("providedSchemaPath")]
    public string? ProvidedSchemaPath { get; set; }
}

public class LogData
{
    [JsonPropertyName("log")]
    public required string Log { get; set; }
}

public class LogVariables
{
    [JsonPropertyName("severity")]
    public required LogSeverity Severity { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("arguments")]
    public List<string>? Arguments { get; set; }
}

public class ParseGraphQLSchemaData
{
    [JsonPropertyName("graphQL")]
    public required ParseGraphQLSchema GraphQL { get; set; }
}

public class ParseGraphQLSchemaVariables
{
    [JsonPropertyName("graphql")]
    public required string Graphql { get; set; }
}

public class ParseGraphQLSchema
{
    [JsonPropertyName("objectTypes")]
    public List<ParseGraphQLSchemaObjectTypes>? ObjectTypes { get; set; }

    [JsonPropertyName("inputObjectTypes")]
    public List<ParseGraphQLSchemaInputObjectTypes>? InputObjectTypes { get; set; }

    [JsonPropertyName("enumerations")]
    public List<ParseGraphQLSchemaEnumerations>? Enumerations { get; set; }
}

public class ParseGraphQLSchemaObjectTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public List<ParseGraphQLSchemaObjectTypesFields>? Fields { get; set; }
}

public class ParseGraphQLSchemaObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("arguments")]
    public List<ParseGraphQLSchemaObjectTypesFieldsArguments>? Arguments { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaObjectTypesFieldsType Type { get; set; }
}

public class ParseGraphQLSchemaObjectTypesFieldsArguments
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaObjectTypesFieldsArgumentsType Type { get; set; }
}

public class ParseGraphQLSchemaObjectTypesFieldsArgumentsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ParseGraphQLSchemaObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ParseGraphQLSchemaInputObjectTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public List<ParseGraphQLSchemaInputObjectTypesFields>? Fields { get; set; }
}

public class ParseGraphQLSchemaInputObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaInputObjectTypesFieldsType Type { get; set; }
}

public class ParseGraphQLSchemaInputObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ParseGraphQLSchemaEnumerations
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("values")]
    public List<ParseGraphQLSchemaEnumerationsValues>? Values { get; set; }
}

public class ParseGraphQLSchemaEnumerationsValues
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

public class ParseGraphQLSchemaAndOperationsData
{
    [JsonPropertyName("graphQL")]
    public required ParseGraphQLSchemaAndOperations GraphQL { get; set; }
}

public class ParseGraphQLSchemaAndOperationsVariables
{
    [JsonPropertyName("graphql")]
    public List<AdditionalFileInput>? Graphql { get; set; }
}

public class ParseGraphQLSchemaAndOperations
{
    [JsonPropertyName("objectTypes")]
    public List<ParseGraphQLSchemaAndOperationsObjectTypes>? ObjectTypes { get; set; }

    [JsonPropertyName("inputObjectTypes")]
    public List<ParseGraphQLSchemaAndOperationsInputObjectTypes>? InputObjectTypes { get; set; }

    [JsonPropertyName("enumerations")]
    public List<ParseGraphQLSchemaAndOperationsEnumerations>? Enumerations { get; set; }

    [JsonPropertyName("operations")]
    public List<ParseGraphQLSchemaAndOperationsOperations>? Operations { get; set; }
}

public class ParseGraphQLSchemaAndOperationsObjectTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public List<ParseGraphQLSchemaAndOperationsObjectTypesFields>? Fields { get; set; }
}

public class ParseGraphQLSchemaAndOperationsObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("arguments")]
    public List<ParseGraphQLSchemaAndOperationsObjectTypesFieldsArguments>? Arguments { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsObjectTypesFieldsType Type { get; set; }
}

public class ParseGraphQLSchemaAndOperationsObjectTypesFieldsArguments
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsObjectTypesFieldsArgumentsType Type { get; set; }
}

public class ParseGraphQLSchemaAndOperationsObjectTypesFieldsArgumentsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ParseGraphQLSchemaAndOperationsObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ParseGraphQLSchemaAndOperationsInputObjectTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public List<ParseGraphQLSchemaAndOperationsInputObjectTypesFields>? Fields { get; set; }
}

public class ParseGraphQLSchemaAndOperationsInputObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsInputObjectTypesFieldsType Type { get; set; }
}

public class ParseGraphQLSchemaAndOperationsInputObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ParseGraphQLSchemaAndOperationsEnumerations
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("values")]
    public List<ParseGraphQLSchemaAndOperationsEnumerationsValues>? Values { get; set; }
}

public class ParseGraphQLSchemaAndOperationsEnumerationsValues
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

public class ParseGraphQLSchemaAndOperationsOperations
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("operationType")]
    public required GraphQLOperationType OperationType { get; set; }

    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("variables")]
    public List<ParseGraphQLSchemaAndOperationsOperationsVariables>? Variables { get; set; }

    [JsonPropertyName("nestedSelection")]
    public List<ParseGraphQLSchemaAndOperationsOperationsNestedSelection>? NestedSelection { get; set; }
}

public class ParseGraphQLSchemaAndOperationsOperationsVariables
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsOperationsVariablesType Type { get; set; }
}

public class ParseGraphQLSchemaAndOperationsOperationsVariablesType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public class ParseGraphQLSchemaAndOperationsOperationsNestedSelection
{
    [JsonPropertyName("depth")]
    public required int Depth { get; set; }
}

public class ReadTextFileData
{
    [JsonPropertyName("readTextFile")]
    public string? ReadTextFile { get; set; }
}

public class ReadTextFileVariables
{
    [JsonPropertyName("textFilePath")]
    public required string TextFilePath { get; set; }
}

public class ReadTextFileWithVersionData
{
    [JsonPropertyName("readTextFile")]
    public string? ReadTextFile { get; set; }
}

public class ReadTextFileWithVersionVariables
{
    [JsonPropertyName("textFilePath")]
    public required string TextFilePath { get; set; }

    [JsonPropertyName("fileVersion")]
    public FileVersion? FileVersion { get; set; }
}
