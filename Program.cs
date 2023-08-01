using Spectre.Console;
using System;

public static class Program
{

    public static void Main()
    {
        Setup();
    }

    private static void Setup()
    {

        AnsiConsole.Write(
            new FigletText("Dumb Adventure")
                .LeftJustified()
                .Color(Color.Red));

        var heroClass = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
          .Title("Choose your [green]Class[/]?")
          .PageSize(5)
          .AddChoices(new[] {
                "Barbarian", "Wizard", "Archer",
                "SwashBuckler",
      }));
        string dialog = "";
        switch (heroClass)
        {
            case "Barbarian":
                dialog = "Barbarian sucks... but oh well its your choice i guess";
                break;
            case "Wizard":
                dialog = "Ok Wizard nice. You should name yourself Rincewind. Trust me its a good name";
                break;
            case "Archer":
                dialog = "You made the correct choice well done, this is the coolest class";
                break;
            case "SwashBuckler":
                dialog = "Sorry the rogue is on a lunchbreak so this is all you get";
                break;
        }

        AnsiConsole.WriteLine(dialog);

        var name = AnsiConsole.Ask<string>("What's your heros [green]name[/]?");

        AnsiConsole.MarkupLine("You dumb name is: [yellow]{0}[/]", name);

        Console.Clear();

        AnsiConsole.Clear();

        IslandGenerator islandGenerator = new IslandGenerator();
        int rows = 10;
        int columns = 10;
        int[,] island = islandGenerator.InitIsland(rows, columns);


        var canvas = new Canvas(columns, rows);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int value = island[i, j];
                if (value == 0)
                {
                    canvas.SetPixel(j, i, Color.Blue);
                }
                else if (value == 1)
                {
                    canvas.SetPixel(j, i, Color.Green);
                }else if (value == 2)
                {
                    canvas.SetPixel(j, i, Color.Yellow);
                }
            }
        }

        // Render the canvas
        AnsiConsole.Write(canvas);

        var direction = AnsiConsole.Ask<string>($"Where you wanna go [green]{name}[/]?");
        AnsiConsole.Clear();

    }

    

    
}
