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


    public static string StringAsk(string question)
    {
        var name = AnsiConsole.Ask<string>(question);
        return name;
    }
}