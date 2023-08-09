namespace Tiles;
using Spectre.Console;
using Utils;
using System.Text;
using Hero;
public class TestTile : Tile
{
    private List<string> options = new List<string>(){
        ConsoleUtils.PadCenterSpecify("Do Nothing", 4)
    };

    bool enteredBefore = false;


    public TestTile()
    {

    }
    public async override Task Enter(HeroClass hero)
    {
        enteredBefore = true;
        ConsoleUtils.DisplayGenericFiglet(nameof(TestTile));
        await EnterFromJson(hero);
    }

    public override List<string> Options()
    {
        return options;
    }


    public override void ChooseOptions(string chosenOption)
    {
        if (chosenOption == options[0])
        {
            DoNothing();
        }
    }

    private void DoNothing()
    {
    }

}