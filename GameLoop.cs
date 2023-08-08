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
        options.Add(ConsoleUtils.PadCenterSpecify("Travel", 4));
        options.Add(ConsoleUtils.PadCenterSpecify("CharacterInfo", 4));
        options.Add(ConsoleUtils.PadCenterSpecify("Equip", 4));
        
        options = options.Concat(currentIsland.CurrentTile().Options()).ToList();
        Misc food = (Misc)HeroUtils.FindItemInInventory(currentHero.Inventory, "Food");

        if(currentHero.Health < currentHero.MaxHealth && food.Amount > 0)
             options.Add("Eat Food");


        var chosenOption = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title(ConsoleUtils.PadCenterText("Whatchu Wanna do dummy?"))
        .PageSize(5)
          .AddChoices(options));


        AnsiConsole.Clear();


        switch (chosenOption.Trim())
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
            default:
            currentIsland.CurrentTile().ChooseOptions(chosenOption);
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
         AnsiConsole.Write(
            new FigletText("Equip")
                .Justify(Justify.Center)
                .Color(Color.Red));
        AnsiConsole.Write(new Markup("Whatchu wanna equip").Centered());
        currentHero.EquipFromInventory(ConsoleUtils.StringAsk(ConsoleUtils.PadCenterSpecify(">", "Whatchu wanna equip".Length + 1)));
    }

    private void Travel()
    {
        Misc food = (Misc)HeroUtils.FindItemInInventory(currentHero.Inventory, "Food");
        food.Amount -= 1;
        if(food.Amount <= 0){
            AnsiConsole.Write(
            new FigletText("No Food Left")
            .Centered()
            .Color(Color.Orange3));
            HeroUtils.GameOver();
        }
        currentIsland.DisplayIsland();
        string direction = currentIsland.DisplayAvailableDirections();
        currentIsland.MovePlayer(direction);
        //if(currentHero.Inventory.Contains)
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
            .Centered()
            .Color(Color.Green));
        ConsoleUtils.PressEnterToContinue();
    }

    private void PlayerOptions()
    {

    }
}