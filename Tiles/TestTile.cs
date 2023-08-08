namespace Tiles;
using Spectre.Console;
using Utils;
public class TestTile : Tile
{
    private List<string> options = new List<string>(){
        ConsoleUtils.PadCenterSpecify("Do Nothing", 4)
    };

    bool enteredBefore = false;


    public TestTile()
    {

    }
    public async override Task Enter()
    {
        enteredBefore = true;
        var table = new Table().Centered();

        await AnsiConsole.Live(table)
            .StartAsync(async ctx =>
            {
                table.AddColumn("Donothing");
                ctx.Refresh();
                await Task.Delay(1000);

                table.AddColumn("Now");
                ctx.Refresh();
                await Task.Delay(1000);
            });
        ConsoleUtils.PressEnterToContinue();
        
        //throw new NotImplementedException();
    }

    public override List<string> Options()
    {
        return options;
    }


    public override void ChooseOptions(string chosenOption)
    {
        if(chosenOption == options[0]){
            DoNothing();
        }
    }

    private void DoNothing()
    {
    }

}