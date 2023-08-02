using Spectre.Console;

public class Island
{
    public int[,] islandGrid { get; set; } = null!;
    public int rows { get; set; }
    public int columns { get; set; }
    public Coordinate playerPos;

    private bool debug = false;
    public Island(int[,] islandGrid, Coordinate playerPos, int rows, int columns)
    {
        this.islandGrid = islandGrid;
        this.playerPos = playerPos;
        this.rows = rows;
        this.columns = columns;
    }

    public void MovePlayer()
    {

    }

    public string DisplayAvailableDirections()
    {
        List<string> directions = new List<string>();

        if (islandGrid[playerPos.X - 1, playerPos.Y] == 1)
            directions.Add("Up");
        if (islandGrid[playerPos.X + 1, playerPos.Y] == 1)
            directions.Add("Down");
        if (islandGrid[playerPos.X, playerPos.Y - 1] == 1)
            directions.Add("Left");
        if (islandGrid[playerPos.X, playerPos.Y + 1] == 1)
            directions.Add("Right");

        if (debug)
        {
            Console.WriteLine(
            "X+1 " + islandGrid[playerPos.X + 1, playerPos.Y]
            + " X-1 " + islandGrid[playerPos.X - 1, playerPos.Y]
            + " Y+1 " + islandGrid[playerPos.X, playerPos.Y + 1]
            + " Y-1 " + islandGrid[playerPos.X, playerPos.Y - 1]
            );
        }

        var direction = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
          .Title("Where do you wanna go")
          .PageSize(10)
          .AddChoices(directions));

        return direction;
    }


    public void DisplayIsland()
    {
        var canvas = new Canvas(columns, rows);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int value = islandGrid[i, j];
                if (value == 0)
                {
                    canvas.SetPixel(j, i, Color.Blue);
                }
                else if (value == 1)
                {
                    canvas.SetPixel(j, i, Color.Green);
                }
                else if (value == 2)
                {
                    canvas.SetPixel(j, i, Color.Yellow);
                }
            }
        }

        AnsiConsole.Write(canvas);
    }

}
