namespace Utils;
using Equipment;
using Spectre.Console;
public static class HeroUtils{
    //This gets fucky if there is multiple items with the same name. but im just gonna ignore that
    public static Item FindItemInInventory(List<Item> inventory, string itemName){
        return inventory.Find(x => x.ItemName.ToLower() == itemName.ToLower())!;
    }

     public static void GameOver(){
        AnsiConsole.Write(
            new FigletText("Game Over")
            .Centered()
            .Color(Color.Red));
        ConsoleUtils.PressEnterToContinue();
        Environment.Exit(1);
    }
}