using YYHEggEgg.Logger;

namespace YYHEggEgg.Shell;

/// <summary>
/// Implement a simple history manager, outputing execution time
/// and actual command content to a file.
/// </summary>
/// <param name="historyFilePath"></param>
public class CommandHistoryManager(string historyFilePath)
{
    /// <summary>
    /// The maximum size of history commands to be loaded into memory.
    /// </summary>
    public const int HISTSIZE = 1000;

    private StreamWriter? historyWriter;

    /// <summary>
    /// Read saved history.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> ReadSavedHistory()
    {
        if (File.Exists(historyFilePath))
        {
            List<string> res = [];
            var maximumChars = ConsoleWrapper.HistoryMaximumChars;
            foreach (var line in File.ReadLines(historyFilePath)
                .Where(x => x.Length <= maximumChars).TakeLast(HISTSIZE))
            {
                var separatorIdx = line.IndexOf(';');
                if (separatorIdx >= 0) res.Add(line[(separatorIdx + 1)..]);
                else res.Add(line);
            }
            return res;
        }
        else return [];
    }

    /// <summary>
    /// Create one record in the file. It'll open the target file if not yet.
    /// </summary>
    /// <param name="command"></param>
    public void PushExecuted(string command)
    {
        if (historyWriter == null)
        {
            historyWriter = new(historyFilePath, true);
            historyWriter.AutoFlush = true;
        }

        historyWriter.WriteLine($"{DateTime.Now};{command}");
    }
}

