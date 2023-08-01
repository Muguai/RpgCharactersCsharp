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

        AnsiConsole.Clear();


        IslandGenerator islandGenerator = new IslandGenerator();
        Island island = islandGenerator.InitIsland(10, 10);


        island.DisplayIsland();

        string direction = island.DisplayAvailableDirections();

        Console.WriteLine(direction);
        

        AnsiConsole.Clear();

    }

    

    
}
