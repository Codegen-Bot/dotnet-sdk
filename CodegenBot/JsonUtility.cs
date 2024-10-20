using System.Text;
using System.Text.Json;

namespace CodegenBot;

public class JsonUtility
{
    /// <summary>
    /// Recursively ensures the type discriminator property comes first in any objects in the JSON tree
    /// </summary>
    /// <param name="json"></param>
    /// <param name="typeDiscriminatorProperty"></param>
    /// <returns></returns>
    public static string EnsureTypeDiscriminatorPropertiesComeFirst(string json, string typeDiscriminatorProperty = "__typename")
    {
        using (var document = JsonDocument.Parse(json))
        {
            var rootElement = document.RootElement;
            var options = new JsonWriterOptions { Indented = true };

            using (var stream = new MemoryStream())
            using (var writer = new Utf8JsonWriter(stream, options))
            {
                ProcessElement(rootElement, writer, typeDiscriminatorProperty);
                writer.Flush();

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }

    private static void ProcessElement(JsonElement element, Utf8JsonWriter writer, string typeDiscriminatorProperty)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                writer.WriteStartObject();

                // Find and write the type discriminator property first if it exists
                if (element.TryGetProperty(typeDiscriminatorProperty, out JsonElement discriminatorProp))
                {
                    writer.WritePropertyName(typeDiscriminatorProperty);
                    discriminatorProp.WriteTo(writer);
                }

                // Write the rest of the properties, except the type discriminator
                foreach (var property in element.EnumerateObject())
                {
                    if (property.Name != typeDiscriminatorProperty)
                    {
                        property.WriteTo(writer);
                    }
                }

                writer.WriteEndObject();
                break;

            case JsonValueKind.Array:
                writer.WriteStartArray();

                foreach (var item in element.EnumerateArray())
                {
                    ProcessElement(item, writer, typeDiscriminatorProperty);
                }

                writer.WriteEndArray();
                break;

            default:
                element.WriteTo(writer);
                break;
        }
    }
}