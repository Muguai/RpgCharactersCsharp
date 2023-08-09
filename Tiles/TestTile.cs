namespace Tiles;
using Spectre.Console;
using Utils;
using Hero;
public class TestTile : Tile
{
    private List<string> options = new List<string>(){
        ConsoleUtils.PadCenterSpecify("Do Nothing", 4)
    };



    public TestTile()
    {

    }
    public async override Task Enter(HeroClass hero)
    {
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