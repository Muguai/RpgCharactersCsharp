﻿using Spectre.Console;
using GameStructure;
using Hero;
using Equipment;

public static class Program
{

    public static void Main()
    {
        (Island, HeroClass?) initTuple = Setup();
        GameLoop gameLoop = new GameLoop();
        gameLoop.InitiateLoop(initTuple.Item1, initTuple.Item2!);

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
                .LeftJustified()
                .Color(Color.Red));

        var name = AnsiConsole.Ask<string>("What's your heros [green]name[/]?");

        AnsiConsole.MarkupLine("You dumb name is: [yellow]{0}[/]", name);


        var heroClass = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
          .Title("Choose your [green]Class[/]?")
          .PageSize(5)
          .AddChoices(new[] {
                "Barbarian", "Wizard", "Archer",
                "SwashBuckler",
      }));
        HeroClass hero = null!;
        string dialog = "";
        switch (heroClass)
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

        Weapon weapon = new Weapon(WeaponType.Fists,1,"Your Fists",1);
        hero.Equip(weapon);
        Misc misc = new Misc("Food", MiscType.Rations, 10);
        hero.AddToInventory(misc);
        
        Misc misc2 = new Misc("faf", MiscType.Rations, 10);
        hero.AddToInventory(misc2);
        
        Misc misc3 = new Misc("Baba", MiscType.Rations, 10);
        hero.AddToInventory(misc3);

        Misc misc5 = new Misc("fefde", MiscType.Rations, 10);
        hero.AddToInventory(misc5);
        AnsiConsole.WriteLine(dialog);
        
        Weapon misc4 = new Weapon(WeaponType.Dagger,1,"Your dagger",1);
        hero.AddToInventory(misc4);
        AnsiConsole.WriteLine(dialog);

        Armor misc6 = new Armor(ArmorType.Cloth, Slot.Body, new HeroStats(1,1,1), "Ugly Armor", 1);
        hero.AddToInventory(misc6);
        AnsiConsole.WriteLine(dialog);
        
        
        

        


        AnsiConsole.Clear();
        


        IslandGenerator islandGenerator = new IslandGenerator();
        return (islandGenerator.InitIsland(10, 10), hero);

    }




}
