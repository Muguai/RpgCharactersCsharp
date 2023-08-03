namespace GameStructure;
using Spectre.Console;
using Hero;
public class GameLoop
{
    private Island currentIsland = null!;
    private HeroClass currentHero = null!;
    public void InitiateLoop(Island island, HeroClass hero)
    {
        currentHero = hero;
        currentIsland = island;
        Loop();
    }

    private void Loop()
    {

        currentIsland.DisplayIsland();

        List<string> options = new List<string>();
        options.Add("Travel");
        options.Add("CharacterInfo");


        var chosenOption = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Whatchu Wanna do dummy?")
        .PageSize(5)
          .AddChoices(options));


        AnsiConsole.Clear();


        switch (chosenOption)
        {
            case "Travel":
                Travel();
                break;
            case "CharacterInfo":
                Display();
                break;
        }

        AnsiConsole.Clear();
        Loop();
    }

    private void Display()
    {
        currentHero.Display();
    }

    private void Travel()
    {
        currentIsland.DisplayIsland();
        string direction = currentIsland.DisplayAvailableDirections();
        currentIsland.MovePlayer(direction);
    }

    private void PlayerOptions()
    {

    }
}