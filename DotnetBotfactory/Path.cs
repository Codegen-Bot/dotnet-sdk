using System.Collections.Immutable;
using System.Text;

namespace DotnetBotfactory;

public record Path(ImmutableList<PathPart> Parts)
{
    public Path(PathPart part) : this(ImmutableList<PathPart>.Empty.Add(part))
    {
    }
    
    public static Path operator +(Path a, PathPart b)
    {
        return new Path(a.Parts.Add(b));
    }
    
    public override string ToString()
    {
        var result = new StringBuilder();
        for (var i = 0; i < Parts.Count; i++)
        {
            var part = Parts[i];
            result.Append(" ");
            result.Append(part.Value);
        }

        return result.ToString();
    }
}