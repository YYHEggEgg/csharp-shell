namespace YYHEggEgg.Shell.Model;

/// <summary>
///
/// </summary>
public class CmdLineRawString : IEquatable<CmdLineRawString?>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rawString"></param>
    /// <param name="startIndex"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CmdLineRawString(string rawString, int startIndex)
    {
        RawString = rawString ?? throw new ArgumentNullException(nameof(rawString));
        StartIndex = startIndex;
    }

    /// <summary>
    /// The raw string parsed from the cmd user provided.
    /// </summary>
    public string RawString { get; set; }
    /// <summary>
    /// The inclusive index of this raw string.
    /// </summary>
    public int StartIndex { get; set; }
    /// <summary>
    /// The exclusive index of this raw string.
    /// </summary>
    public int EndIndex => StartIndex + RawString.Length;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as CmdLineRawString);
    }

    /// <inheritdoc cref="Equals(object?)"/>
    public bool Equals(CmdLineRawString? other)
    {
        return other is not null &&
               RawString == other.RawString &&
               StartIndex == other.StartIndex;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(RawString, StartIndex);
    }

    /// <inheritdoc cref="Equals(CmdLineRawString?)"/>
    public static bool operator ==(CmdLineRawString? left, CmdLineRawString? right)
    {
        return EqualityComparer<CmdLineRawString>.Default.Equals(left, right);
    }

    /// <inheritdoc cref="Equals(CmdLineRawString?)"/>
    public static bool operator !=(CmdLineRawString? left, CmdLineRawString? right)
    {
        return !(left == right);
    }
}
