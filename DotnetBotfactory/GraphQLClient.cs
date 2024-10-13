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
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsData))]
[JsonSerializable(typeof(GraphQLResponse<ParseGraphQLSchemaAndOperationsData>))]
[JsonSerializable(typeof(GraphQLRequest<ParseGraphQLSchemaAndOperationsVariables>))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFieldsParameters))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFieldsParametersType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsInputObjectTypes))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsInputObjectTypesFields))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsInputObjectTypesFieldsType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsEnumerations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsEnumerationsValues))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsFragments))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsFragmentsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsFragmentsVariablesType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperations))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperationsVariables))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperationsVariablesType))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperationsDenestedSelections))]
[JsonSerializable(typeof(ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItem))]
[JsonSerializable(
    typeof(ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemFieldSelection)
)]
[JsonSerializable(
    typeof(ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemFragmentSpreadSelection)
)]
[JsonSerializable(
    typeof(ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemInlineFragmentSelection)
)]
[JsonSerializable(typeof(ReadTextFileVariables))]
[JsonSerializable(typeof(ReadTextFileData))]
[JsonSerializable(typeof(GraphQLResponse<ReadTextFileData>))]
[JsonSerializable(typeof(GraphQLRequest<ReadTextFileVariables>))]
[JsonSerializable(typeof(ReadTextFileWithVersionVariables))]
[JsonSerializable(typeof(ReadTextFileWithVersionData))]
[JsonSerializable(typeof(GraphQLResponse<ReadTextFileWithVersionData>))]
[JsonSerializable(typeof(GraphQLRequest<ReadTextFileWithVersionVariables>))]
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
        var result = JsonSerializer.Deserialize<GraphQLResponse<AddTextByTagsData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseAddTextByTagsData
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
            GraphQLClientJsonSerializerContext.Default.GraphQLRequestGetConfigurationVariables
        );
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
                    enumerations {
                      name
                      values {
                        name
                      }
                    }
                    fragments {
                      name
                      variables {
                        name
                        type {
                          text
                        }
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
                        depth
                        item {
                          fieldSelection {
                            name
                            alias
                          }
                          fragmentSpreadSelection {
                            fragmentName
                          }
                          inlineFragmentSelection {
                            typeName
                          }
                        }
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
        var result = JsonSerializer.Deserialize<GraphQLResponse<ReadTextFileWithVersionData>>(
            response,
            GraphQLClientJsonSerializerContext.Default.GraphQLResponseReadTextFileWithVersionData
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
    public required AddFile AddFile { get; set; }
}

public partial class AddFileVariables
{
    [JsonPropertyName("filePath")]
    public required string FilePath { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddFile
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddKeyedTextData
{
    [JsonPropertyName("addKeyedText")]
    public required AddKeyedText AddKeyedText { get; set; }
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

public partial class AddKeyedText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddKeyedTextByTagsData
{
    [JsonPropertyName("addKeyedTextByTags")]
    public required List<AddKeyedTextByTags> AddKeyedTextByTags { get; set; }
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

public partial class AddKeyedTextByTags
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddTextData
{
    [JsonPropertyName("addText")]
    public required AddText AddText { get; set; }
}

public partial class AddTextVariables
{
    [JsonPropertyName("caretId")]
    public required string CaretId { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddText
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class AddTextByTagsData
{
    [JsonPropertyName("addTextByTags")]
    public required List<AddTextByTags> AddTextByTags { get; set; }
}

public partial class AddTextByTagsVariables
{
    [JsonPropertyName("tags")]
    public required List<CaretTagInput> Tags { get; set; }

    [JsonPropertyName("textAndCarets")]
    public required string TextAndCarets { get; set; }
}

public partial class AddTextByTags
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

public partial class GetConfigurationData
{
    [JsonPropertyName("configuration")]
    public required GetConfiguration Configuration { get; set; }
}

public partial class GetConfigurationVariables { }

public partial class GetConfiguration
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
    public required List<GetConfigurationCopybots> Copybots { get; set; }
}

public partial class GetConfigurationCopybots
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("inputDirectory")]
    public required string InputDirectory { get; set; }

    [JsonPropertyName("whitelist")]
    public required List<string> Whitelist { get; set; }

    [JsonPropertyName("fieldDefinitions")]
    public required List<GetConfigurationCopybotsFieldDefinitions> FieldDefinitions { get; set; }
}

public partial class GetConfigurationCopybotsFieldDefinitions
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
    public required List<GetFiles> Files { get; set; }
}

public partial class GetFilesVariables
{
    [JsonPropertyName("whitelist")]
    public required List<string> Whitelist { get; set; }

    [JsonPropertyName("blacklist")]
    public required List<string> Blacklist { get; set; }
}

public partial class GetFiles
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
    public GetSchema? BotSpec { get; set; }
}

public partial class GetSchemaVariables
{
    [JsonPropertyName("botFilePath")]
    public required string BotFilePath { get; set; }
}

public partial class GetSchema
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
    public required ParseGraphQLSchemaAndOperations GraphQL { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsVariables
{
    [JsonPropertyName("graphql")]
    public required List<AdditionalFileInput> Graphql { get; set; }
}

public partial class ParseGraphQLSchemaAndOperations
{
    [JsonPropertyName("objectTypes")]
    public required List<ParseGraphQLSchemaAndOperationsObjectTypes> ObjectTypes { get; set; }

    [JsonPropertyName("inputObjectTypes")]
    public required List<ParseGraphQLSchemaAndOperationsInputObjectTypes> InputObjectTypes { get; set; }

    [JsonPropertyName("enumerations")]
    public required List<ParseGraphQLSchemaAndOperationsEnumerations> Enumerations { get; set; }

    [JsonPropertyName("fragments")]
    public required List<ParseGraphQLSchemaAndOperationsFragments> Fragments { get; set; }

    [JsonPropertyName("operations")]
    public required List<ParseGraphQLSchemaAndOperationsOperations> Operations { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public required List<ParseGraphQLSchemaAndOperationsObjectTypesFields> Fields { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("parameters")]
    public required List<ParseGraphQLSchemaAndOperationsObjectTypesFieldsParameters> Parameters { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsObjectTypesFieldsType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypesFieldsParameters
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsObjectTypesFieldsParametersType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypesFieldsParametersType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsInputObjectTypes
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fields")]
    public required List<ParseGraphQLSchemaAndOperationsInputObjectTypesFields> Fields { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsInputObjectTypesFields
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsInputObjectTypesFieldsType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsInputObjectTypesFieldsType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsEnumerations
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("values")]
    public required List<ParseGraphQLSchemaAndOperationsEnumerationsValues> Values { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsEnumerationsValues
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsFragments
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("variables")]
    public required List<ParseGraphQLSchemaAndOperationsFragmentsVariables> Variables { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsFragmentsVariables
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsFragmentsVariablesType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsFragmentsVariablesType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperations
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("operationType")]
    public required GraphQLOperationType OperationType { get; set; }

    [JsonPropertyName("text")]
    public required string Text { get; set; }

    [JsonPropertyName("variables")]
    public required List<ParseGraphQLSchemaAndOperationsOperationsVariables> Variables { get; set; }

    [JsonPropertyName("denestedSelections")]
    public required List<ParseGraphQLSchemaAndOperationsOperationsDenestedSelections> DenestedSelections { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperationsVariables
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required ParseGraphQLSchemaAndOperationsOperationsVariablesType Type { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperationsVariablesType
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperationsDenestedSelections
{
    [JsonPropertyName("depth")]
    public required int Depth { get; set; }

    [JsonPropertyName("item")]
    public required ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItem Item { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItem
{
    [JsonPropertyName("fieldSelection")]
    public ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemFieldSelection? FieldSelection { get; set; }

    [JsonPropertyName("fragmentSpreadSelection")]
    public ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemFragmentSpreadSelection? FragmentSpreadSelection { get; set; }

    [JsonPropertyName("inlineFragmentSelection")]
    public ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemInlineFragmentSelection? InlineFragmentSelection { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemFieldSelection
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemFragmentSpreadSelection
{
    [JsonPropertyName("fragmentName")]
    public required string FragmentName { get; set; }
}

public partial class ParseGraphQLSchemaAndOperationsOperationsDenestedSelectionsItemInlineFragmentSelection
{
    [JsonPropertyName("typeName")]
    public required string TypeName { get; set; }
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
