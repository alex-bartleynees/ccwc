using System.Text.RegularExpressions;

namespace Application;
public partial class ProcessArgs
{
    private const string HelpArg = "--h";
    private const string CountArg = "--c";
    private const string LinesArg = "--l";

    private const string WordsArg = "--w";

    private const string CharsArg = "--m";
    public static string Process(string[] args)
    {
        var command = args.Length > 0 ? args[0] : null;
        var filePath = args.Length == 0 ? null : args.Length > 1 ? args[1] : args[0];

        if (!IsValidFilePath(filePath))
        {
            return ProcessStream(command);
        }

        return ProcessCommand(command, filePath);

    }

    private static bool IsValidFilePath(string? filePath)
    {
        return File.Exists(filePath);
    }

    private static string ProcessStream(string? command)
    {
        try
        {
            using StreamReader reader = new(Console.OpenStandardInput());
            string text = reader.ReadToEnd();
            var filePath = "temp.txt";
            File.WriteAllText(filePath, text);
            return ProcessCommand(command, filePath);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private static string ProcessCommand(string? command, string? filePath)
    {
        return command switch
        {
            HelpArg => "Usage: FileInfo <path>",
            CountArg => ProcessCountCommand(filePath),
            LinesArg => ProcessLinesCommand(filePath),
            WordsArg => ProcessWordsCommand(filePath),
            CharsArg => ProcessCharsCommand(filePath),
            _ => ProcessAllCommands(filePath),
        };
    }

    private static string ProcessCountCommand(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return "No file path provided";
        }

        try
        {
            var fileInfo = new FileInfo(filePath);
            return $"{fileInfo.Length} {fileInfo.Name}";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private static string ProcessLinesCommand(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return "No file path provided";
        }

        try
        {
            var lines = File.ReadAllLines(filePath);
            return $"{lines.Length} {Path.GetFileName(filePath)}";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private static string ProcessWordsCommand(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return "No file path provided";
        }

        try
        {
            var text = File.ReadAllText(filePath);
            string[] words = GetWords(text);
            return $"{words.Length} {Path.GetFileName(filePath)}";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private static string ProcessCharsCommand(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return "No file path provided";
        }

        try
        {
            var text = File.ReadAllText(filePath);
            return $"{text.Length} {Path.GetFileName(filePath)}";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private static string ProcessAllCommands(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return "No file path provided";
        }

        try
        {
            var fileInfo = new FileInfo(filePath);
            var lines = File.ReadAllLines(filePath);
            var text = File.ReadAllText(filePath);
            string[] words = GetWords(text);
            return $"{lines.Length} {words.Length} {fileInfo.Length} {fileInfo.Name}";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private static string[] GetWords(string text)
    {
        string[] words = MyRegex().Split(text);
        words = words.Where(word => !string.IsNullOrEmpty(word)).ToArray();
        return words;
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex MyRegex();
}
