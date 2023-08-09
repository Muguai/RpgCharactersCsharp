using Spectre.Console;
using GameStructure;
using Hero;
using Equipment;
using Utils;

public static class Program
{

    public static async Task Main()
    {
        (Island, HeroClass?) initTuple = Setup();
        GameLoop gameLoop = new GameLoop();
        await gameLoop.InitiateLoop(initTuple.Item1, initTuple.Item2!);

        /*
        island.DisplayIsland();

        while(true){

            string direction = island.DisplayAvailableDirections();

            island.MovePlayer(direction);

        }
        */
    }

    private static (Island, HeroClass?) Setup()
    {

        AnsiConsole.Write(
            new FigletText("Dumb Adventure")
                .Justify(Justify.Center)
                .Color(Color.Red));

        var markup = new Markup("[yellow]Whats your name?[/]").Centered();
        AnsiConsole.Write(markup);
        var name = ConsoleUtils.StringAsk(ConsoleUtils.PadCenterSpecify(">", 20));
        var markup2 = new Markup($"Your name is [yellow]{name}[/]").Centered();
        AnsiConsole.Write(markup2);

        var title = ConsoleUtils.PadCenterText("Choose your Class");
        var heroClass = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
            .Title(title)
            .PageSize(5)
            .AddChoices(new[] {
                    ConsoleUtils.PadCenterSpecify("Barbarian", 4),
                    ConsoleUtils.PadCenterSpecify("Wizard", 4),
                    ConsoleUtils.PadCenterSpecify("Archer", 4),
                    ConsoleUtils.PadCenterSpecify("SwashBuckler", 4),
        }));
        HeroClass hero = null!;
        string dialog = "";
        switch (heroClass.Trim())
        {
            case "Barbarian":
                dialog = "Barbarian sucks... but oh well its your choice i guess";
                hero = new Barbarian(name);
                break;
            case "Wizard":
                dialog = "Ok Wizard nice. You should name yourself Rincewind. Trust me its a good name";
                hero = new Wizard(name);
                break;
            case "Archer":
                hero = new Archer(name);
                dialog = "You made the correct choice well done, this is the coolest class";
                break;
            case "SwashBuckler":
                hero = new SwashBuckler(name);
                dialog = "Sorry the rogue is on a lunchbreak so this is all you get";
                break;
        }

        Weapon weapon = new(WeaponType.Fists, 1, -2, "Your Fists", 1);
        Weapon weapon2 = new(WeaponType.Fists, 1, -2, "Your Fist2s", 1);

        Weapon weapon3 = new(WeaponType.Fists, 1, -2, "Your Fis24ts", 1);

        Weapon weapon4 = new(WeaponType.Fists, 1, -2, "Your Fist3s", 1);
                Weapon weapon5 = new(WeaponType.Fists, 1, -2, "Your Fist43s", 1);

        Weapon weapon6 = new(WeaponType.Fists, 1, -2, "Your Fist333s", 1);


        hero.Equip(weapon);
        Misc misc = new("Food", MiscType.Rations, 10);
        hero.AddToInventory(misc);
        hero.AddToInventory(misc);

        Misc misc1 = new("Food1", MiscType.Rations, 10);
        hero.AddToInventory(weapon2);
        Misc misc2 = new("Food2", MiscType.Rations, 10);
        hero.AddToInventory(weapon3);
        Misc misc3 = new("Food3", MiscType.Rations, 10);
        hero.AddToInventory(weapon4);
        Misc misc4 = new("Food4", MiscType.Rations, 10);
        hero.AddToInventory(weapon5);
                hero.AddToInventory(weapon6);

    
        AnsiConsole.WriteLine(ConsoleUtils.PadCenterText(dialog));
        AnsiConsole.WriteLine(ConsoleUtils.PadCenterText("Also imma give you some food. here you go dummy now get out of"));
        
        ConsoleUtils.PressEnterToContinue();

        AnsiConsole.Clear();

        IslandGenerator islandGenerator = new();
        return (islandGenerator.InitIsland(10, 10), hero);

    }




}
