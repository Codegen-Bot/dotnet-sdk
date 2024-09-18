using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodegenBot;

/// <summary>
/// Represents a reference to a caret. A caret is a position in a text file; text can be inserted at that position.
/// </summary>
public class CaretRef
{
    public string Id { get; init; }
    public string Separator { get; init; }
    public string Indentation { get; init; }
    public IReadOnlyList<CaretTag> Tags { get; init; }

    public static CaretRef New(string id, string separator = "", string indentation = "")
    {
        return new CaretRef()
        {
            Id = id,
            Separator = separator,
            Indentation = indentation,
        };
    }

    public static CaretRef New(out CaretRef caretRef, string separator, params CaretTag[] tags)
    {
        caretRef = new CaretRef()
        {
            Id = Guid.NewGuid().ToString(),
            Separator = separator,
            Tags = tags,
        };
        return caretRef;
    }

    public static CaretRef New(out CaretRef caretRef, string separator, string indentation, params CaretTag[] tags)
    {
        caretRef = new CaretRef()
        {
            Id = Guid.NewGuid().ToString(),
            Separator = separator,
            Indentation = indentation,
            Tags = tags,
        };
        return caretRef;
    }

    public static CaretRef New(out CaretRef caretRef, params CaretTag[] tags)
    {
        caretRef = new CaretRef()
        {
            Id = Guid.NewGuid().ToString(),
            Separator = "",
            Tags = tags,
        };
        return caretRef;
    }
    
    public static CaretRef New(string separator, params CaretTag[] tags)
    {
        var caretRef = new CaretRef()
        {
            Id = Guid.NewGuid().ToString(),
            Separator = separator,
            Tags = tags,
        };
        return caretRef;
    }

    public static CaretRef New(params CaretTag[] tags)
    {
        var caretRef = new CaretRef()
        {
            Id = Guid.NewGuid().ToString(),
            Separator = "",
            Tags = tags,
        };
        return caretRef;
    }

    public static CaretRef? TryParse(string str)
    {
        return JsonSerializer.Deserialize(str, CaretRefJsonSerializer.Default.CaretRef);
    }
    
    public override string ToString()
    {
        var json = JsonSerializer.Serialize(this, CaretRefJsonSerializer.Default.CaretRef);
        return $"<<<<<<<<<{json}>>>>>>>>>";
    }
}

[JsonSerializable(typeof(CaretRef))]
internal partial class CaretRefJsonSerializer : JsonSerializerContext {

}
