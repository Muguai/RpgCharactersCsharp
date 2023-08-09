namespace Utils;
using Spectre.Console;

public static class ConsoleUtils
{
    public static void PressEnterToContinue()
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>("'\u00A0'" + PadCenterSpecify("Press Enter To Continue..", 6))
            .AllowEmpty());
    }

    public static void EmptyPressEnterToContinue()
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>("'\u00A0'" + PadCenterSpecify(".", 6))
            .AllowEmpty());
    }

    public static string PadCenterText(string text)
    {
        var consoleWidth = AnsiConsole.Profile.Width;
        var padding = (consoleWidth - text.Length) / 2;
        return new string(' ', Math.Max(0, padding)) + text;
    }
    public static string PadCenterSpecify(string text, int specify)
    {
        var consoleWidth = AnsiConsole.Profile.Width;
        var padding = (consoleWidth - (text.Length + specify)) / 2;
        return new string(' ', Math.Max(0, padding)) + text;
    }

    public static void AddEmptyStringPadding(int width)
    {
        var consoleWidth = AnsiConsole.Profile.Width;
        int padding = (int)(consoleWidth - width)! / 2;

        for (int i = 0; i < padding; i++)
        {
            AnsiConsole.Write(" ");
        }
    }

    public static async Task DisplayTextSlowly(string dialogText)
    {
        var delayBetweenChars = 50;
        int startIndex = dialogText.IndexOf('*');
        int endIndex = dialogText.LastIndexOf('*');
        AnsiConsole.Write(PadCenterSpecify(" ", dialogText.Length));

        if (startIndex >= 0 && endIndex >= 0 && startIndex < endIndex)
        {
            AnsiConsole.Write(dialogText.Substring(0, startIndex));

            AnsiConsole.Write(dialogText.Substring(startIndex + 1, endIndex - startIndex - 1));

            for (int i = endIndex + 1; i < dialogText.Length; i++)
            {
                AnsiConsole.Write(dialogText[i]);
                await Task.Delay(delayBetweenChars);
            }
        }
        else
        {
            foreach (char c in dialogText)
            {
                AnsiConsole.Write(c);
                await Task.Delay(delayBetweenChars);
            }
        }

        AnsiConsole.WriteLine();

        EmptyPressEnterToContinue();
    }

    public static string StringAsk(string question)
    {
        var answer = AnsiConsole.Ask<string>("'\u00A0'" + question);
        return answer;
    }

    public static void DisplayGenericFiglet(string title)
    {
        AnsiConsole.Write(
           new FigletText(title)
               .Justify(Justify.Center)
               .Color(Color.Aqua));
    }
}