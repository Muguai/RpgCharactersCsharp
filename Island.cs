using Spectre.Console;
using Utils;
using Tiles;
using Hero;
public class Island
{
    public Tile[,] islandGrid { get; set; } = null!;
    public int rows { get; set; }
    public int columns { get; set; }
    public Coordinate playerPos;

    private bool debug = false;
    public Island(Tile[,] islandGrid, Coordinate playerPos, int rows, int columns)
    {
        this.islandGrid = islandGrid;
        this.playerPos = playerPos;
        this.rows = rows;
        this.columns = columns;
    }

    public async Task MovePlayer(string dir, HeroClass hero)
    {
     
        //islandGrid[playerPos.X, playerPos.Y] = new TestTile();
        
        DisplayIsland();
        await islandGrid[playerPos.X,playerPos.Y].Exit();

        playerPos = playerPos.getAdjacent(dir);
        AnsiConsole.Clear();
        DisplayIsland();

        await islandGrid[playerPos.X,playerPos.Y].Enter(hero);

        AnsiConsole.Clear();
        //islandGrid[playerPos.X, playerPos.Y] = 2;
        DisplayIsland();
        
    }

    public string DisplayAvailableDirections()
    {
        List<string> directions = new List<string>();

        if (!(islandGrid[playerPos.X - 1, playerPos.Y] is WaterTile))
            directions.Add(ConsoleUtils.PadCenterSpecify("Up", 4));
        if (!(islandGrid[playerPos.X + 1, playerPos.Y] is WaterTile))
            directions.Add(ConsoleUtils.PadCenterSpecify("Down", 4));
        if (!(islandGrid[playerPos.X, playerPos.Y - 1] is WaterTile))
            directions.Add(ConsoleUtils.PadCenterSpecify("Left", 4));
        if (!(islandGrid[playerPos.X, playerPos.Y + 1] is WaterTile))
            directions.Add(ConsoleUtils.PadCenterSpecify("Right", 4));

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
          .Title(ConsoleUtils.PadCenterText("Where do you wanna go"))
          .PageSize(10)
          .AddChoices(directions));

        return direction.Trim();
    }

    public Tile CurrentTile(){
        return islandGrid[playerPos.X, playerPos.Y];
    }


    public void DisplayIsland()
    {
        var canvas = new Canvas(columns, rows);
        canvas.MaxWidth = 50;


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Tile value = islandGrid[i, j];
                if ((i, j) == (playerPos.X, playerPos.Y))
                {
                    canvas.SetPixel(j, i, Color.Yellow);
                }
                else if (value is WaterTile)
                {
                    canvas.SetPixel(j, i, Color.Blue);
                }
                else if (!(value is WaterTile))
                {
                    canvas.SetPixel(j, i, Color.Green);
                }
            }
        }

        var align = new Align(canvas, HorizontalAlignment.Center);

        AnsiConsole.Write(align);
    }

}
