using System.Diagnostics;
using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell.AutoCompletion;

/// <summary>
/// Complete the file path when user is in a pair of <c>""</c>.
/// </summary>
public class FilePathAutoCompleteHandler : IAutoCompleteHandler
{
    /// <summary>
    /// The working path of program.
    /// </summary>
    public string CurrentPath { get; set; } = Environment.CurrentDirectory;

    private static char[] GetPathSeparators()
    {
        if (OperatingSystem.IsWindows()) return ['\\', '/'];
        else return ['/'];
    }

    /// <inheritdoc/>
    public SuggestionResult GetSuggestions(string text, int index)
    {
        var left = text[..index];
        var right = text[index..];
        string endlimit;
        if (right.Contains('"')) endlimit = right[..right.IndexOf('"')];
        else endlimit = right;

        // unclosed " trigger the file-name completions
        var leftCount = left.Count(c => c == '"');
        // Log.Info($"left: '{left}', right: '{right}', endlimit: '{endlimit}', leftCount: '{leftCount}'");
        if (leftCount % 2 != 1) return new();
        if (endlimit.IndexOfAny(GetPathSeparators()) >= 0) return new();

        var startIndex = left.LastIndexOf('"') + 1;
        Debug.Assert(startIndex > 0);
        var endIndex = text.IndexOf('"', index);
        // Log.Info($"index: [{startIndex}, {endIndex})");

        var requestedPath = left[startIndex..];
        var separatorIdx = requestedPath.LastIndexOfAny(GetPathSeparators());
        // Log.Info($"requestedPath: '{requestedPath}', separatorIdx: {separatorIdx}");
        var inputDir = requestedPath[..(separatorIdx + 1)];
        if (inputDir == string.Empty) inputDir = $".{Path.DirectorySeparatorChar}";

        var parentDir = Path.GetFullPath(inputDir, CurrentPath);
        var startlimit = requestedPath[(separatorIdx + 1)..];
        // Log.Info($"parentDir: '{parentDir}'', startlimit: {startlimit}");

        try
        {
            var enumeratedNames = Directory.EnumerateDirectories(parentDir, "*", SearchOption.TopDirectoryOnly)
                .Concat(Directory.EnumerateFiles(parentDir, "*.*", SearchOption.TopDirectoryOnly))
                .Select(x => Path.GetFileName(x));
            var names = from name in enumeratedNames
                        where name.StartsWith(startlimit, StringComparison.OrdinalIgnoreCase) && name.EndsWith(endlimit)
                        select $"{inputDir[..^1]}{Path.DirectorySeparatorChar}{name}";
            // Log.Info($"names: {string.Join(',', names)}");
            return new()
            {
                Suggestions = names.ToList(),
                StartIndex = startIndex,
                EndIndex = endIndex,
            };
        }
        catch (Exception ex)
        {
            if (ex is IOException ioex2) Log.Info(ioex2.HResult.ToString());
            if (ex is DirectoryNotFoundException)
            {
                Log.Warn($"The specified directory does not exist. ('{parentDir}')", nameof(FilePathAutoCompleteHandler));
                return new();
            }
            else if (ex is IOException ioex && ((uint)ioex.HResult == 0x80070057 || (uint)ioex.HResult == 0x80070003))
            {
                Log.Warn($"You're trying to query subentries of a file. Consider remove the last '{inputDir[^1]}'. ('{inputDir}')", nameof(FilePathAutoCompleteHandler));
                return new();
            }
            else throw;
        }
    }
}
