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

        Weapon weapon = new Weapon(WeaponType.Fists, 1, 0, "Your Fists", 1);
        hero.Equip(weapon);
        Misc misc = new Misc("Food", MiscType.Rations, 10);
        hero.AddToInventory(misc);

        Misc misc2 = new Misc("faf", MiscType.Rations, 10);
        hero.AddToInventory(misc2);

        Misc misc3 = new Misc("Baba", MiscType.Rations, 10);
        hero.AddToInventory(misc3);

        Misc misc5 = new Misc("fefde", MiscType.Rations, 10);
        hero.AddToInventory(misc5);

        Weapon misc4 = new Weapon(WeaponType.Dagger, 1, 1, "Your dagger", 1);
        hero.AddToInventory(misc4);


        Armor misc6 = new Armor(ArmorType.Cloth, Slot.Body, new HeroStats(1, 1, 1), "Ugly Armor", 1);
        hero.AddToInventory(misc6);

    /*
        Combat combat = new();
        Item[] drops = new Item[] { misc6, misc2 };
        Enemy enemy = new("Goblin", 1, 1, 10, drops);

        bool what = combat.StartCombat(hero, enemy);
        */
        AnsiConsole.WriteLine(ConsoleUtils.PadCenterText(dialog));
        ConsoleUtils.PressEnterToContinue();







        AnsiConsole.Clear();



        IslandGenerator islandGenerator = new IslandGenerator();
        return (islandGenerator.InitIsland(10, 10), hero);

    }




}
