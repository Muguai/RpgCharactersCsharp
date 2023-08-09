using Spectre.Console;
using Utils;
namespace Combat;

public static class Dice
{

    private static Dictionary<int, (int, int)[]> diceValues = new()
    {
        { 1, new[] { (3, 3)}},
        { 2, new[] { (1,1), (5,5)}},
        { 3, new[] { (1,1),(3,3) ,(5,5)}},
        { 4, new[] { (1,1), (1,5), (5,5) ,(5,1)}},
        { 5, new[] { (1,1), (1,5), (5,5) ,(5,1), (3,3)}},
        { 6, new[] { (1,1), (1,5), (5,5) ,(5,1), (1,3), (5,3)}}

    };

    public static int RollDice(int diceAmount)
    {
        Random r = new();
        var diceResults = new List<int>();

        for (int i = 0; i < diceAmount; i++)
        {
            int rolledResult = r.Next(1, 7);
            diceResults.Add(rolledResult);
        }

        int canvasWidth = (diceAmount * 7) + (diceAmount - 1);
        var canvas = new Canvas(canvasWidth, 7);

        for (int i = 0; i < diceAmount; i++)
        {
            int diceNumber = diceResults[i];
            var diceCoords = diceValues[diceNumber];

            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    int x = (i * 8) + col;
                    int y = row;

                    if (col == 7) 
                    {
                        continue;
                    }

                    if (diceCoords.Contains((col, row)))
                    {
                        canvas.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        canvas.SetPixel(x, y, Color.White);
                    }
                }
            }
        }

        var grid = new Grid();
        var align = new Align(canvas, HorizontalAlignment.Center);
        // Add columns 
        grid.AddColumn();

        // Add header row 
        grid.AddRow(canvas);

        AnsiConsole.Write(align);
        
        return diceResults.Sum();
    }
}