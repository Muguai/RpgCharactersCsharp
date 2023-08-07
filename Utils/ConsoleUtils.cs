namespace Utils;
using Spectre.Console;

public static class ConsoleUtils
{
    public static void PressEnterToContinue()
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>("Press Enter To Continue..")
            .AllowEmpty());
    }

    public static void EmptyPressEnterToContinue()
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>("")
            .AllowEmpty());
    }

    public static string PadCenterText(string text){
        var consoleWidth = AnsiConsole.Profile.Width;
        var padding = (consoleWidth - text.Length) / 2;
        return new string(' ', Math.Max(0, padding)) + text;
    }

    public static void AddEmptyStringPadding(int width){
        var consoleWidth = AnsiConsole.Profile.Width;
        int padding = (int)(consoleWidth - width)! / 2;

        for (int i = 0; i < padding; i++)
        {
            AnsiConsole.Write(" ");
        }
    }


    public static string StringAsk(string question)
    {
        var name = AnsiConsole.Ask<string>(question);
        return name;
    }
}