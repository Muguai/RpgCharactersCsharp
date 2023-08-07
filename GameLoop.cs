namespace GameStructure;
using Spectre.Console;
using Hero;
using Utils;
using Equipment;
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
        options.Add("Equip");

        Misc food = (Misc)HeroUtils.FindItemInInventory(currentHero.Inventory, "Food");

        if(currentHero.Health < currentHero.MaxHealth && food.Amount > 0)
             options.Add("Eat Food");


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
            case "Equip":
                Equip();
                break;
            case "Eat Food":
                Eat();
                break;
        }

        AnsiConsole.Clear();
        Loop();
    }

    private void Display()
    {
        currentHero.Display();
    }

    private void Equip(){
        currentHero.EquipFromInventory(ConsoleUtils.StringAsk("Name Item You Wanna Equip"));
    }

    private void Travel()
    {
        Misc food = (Misc)HeroUtils.FindItemInInventory(currentHero.Inventory, "Food");
        food.Amount -= 1;
        if(food.Amount <= 0){
            AnsiConsole.Write(
            new FigletText("No Food Left")
            .LeftJustified()
            .Color(Color.Orange3));
            GameOver();
        }
        currentIsland.DisplayIsland();
        string direction = currentIsland.DisplayAvailableDirections();
        currentIsland.MovePlayer(direction);
        //if(currentHero.Inventory.Contains)
    }

    private void GameOver(){
        AnsiConsole.Write(
            new FigletText("Game Over")
            .LeftJustified()
            .Color(Color.Red));
        ConsoleUtils.PressEnterToContinue();
        Environment.Exit(1);
    }

    private void Eat(){
        Misc food = (Misc)HeroUtils.FindItemInInventory(currentHero.Inventory, "Food");
        food.Amount -= 1;
        int healAmount = 5;
        currentHero.Health += healAmount;
        if(currentHero.Health > currentHero.MaxHealth){
            healAmount = healAmount - (currentHero.Health - currentHero.MaxHealth);
            currentHero.Health = currentHero.MaxHealth;
        }

         AnsiConsole.Write(
            new FigletText("You healed " + healAmount + " Health")
            .LeftJustified()
            .Color(Color.Green));
        ConsoleUtils.PressEnterToContinue();
    }

    private void PlayerOptions()
    {

    }
}