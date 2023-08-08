using Tiles;
public class IslandGenerator
{
    bool debug = true;
    Coordinate playerPos = new(0, 0);

    public Island InitIsland(int rows, int columns)
    {
        int iterations = 6;
        int minLandMass = 30;


        Tile[,] island = GenerateIsland(rows, columns);

        double desiredLandRatio = 0.3;
        int totalCells = rows * columns;
        int desiredLandCells = (int)(totalCells * desiredLandRatio);

        double initialChance = (desiredLandCells - 1) / (double)totalCells;


        if (debug)
        {
            Console.WriteLine("--- ISLAND BEFORE ---");
            PrintIsland(island);
        }

        island = CellularAutomaton(island, iterations, initialChance, minLandMass);

        int landCells = (island.Cast<Tile>().Count(cell => !(cell is WaterTile)));
        Random random = new Random();
        int startPositionIndex = random.Next(landCells);

        if (debug)
        {
            Console.WriteLine("--- ISLAND AFTER ---");
            PrintIsland(island);
            Console.WriteLine("TOTAL LANDMASS --> " + landCells + " VS TOTAL CELLS " + totalCells);
        }
        Coordinate playerPos = new(0, 0);
        int count = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (!(island[i, j] is WaterTile))
                {

                    if (count == startPositionIndex)
                    {
                        playerPos = new Coordinate(i, j);
                        Console.WriteLine("playerpos i " + i + " J " + j);
                    }
                    count++;
                }
            }
        }
        return new Island(island, playerPos, rows, columns);
    }

    static Tile[,] GenerateIsland(int rows, int columns)
    {
        Tile[,] island = new Tile[rows, columns];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                island[i, j] = new WaterTile();
            }
        }

        int startRow = random.Next(rows / 4, rows / 2);
        int startCol = random.Next(columns / 4, columns / 2);

        island[startRow, startCol] = new TestTile();
        return island;
    }

    private Tile[,] CellularAutomaton(Tile[,] island, int iterations, double chance, int minLandMass)
    {
        int rows = island.GetLength(0);
        int columns = island.GetLength(1);

        Random random = new Random();

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            Console.WriteLine("IslandStart");
            PrintIsland(island);


            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < columns - 1; j++)
                {
                    int neighborsCount = CountNeighbors(island, i, j);
                    if (!(island[i, j] is WaterTile) || ((island[i, j] is WaterTile) && neighborsCount > 0 && random.NextDouble() < chance))
                    {
                        Console.WriteLine("Add");
                        island[i, j] = new TestTile();
                    }
                    if (debug)
                        Console.WriteLine($"Cell [{i}, {j}] Neighbors: {neighborsCount}");
                }
            }


            if (debug)
            {
                Console.WriteLine($"Island after iteration {iteration + 1}:");
                PrintIsland(island);
            }
            int currentLandMass = (island.Cast<Tile>().Count(cell => !(cell is WaterTile)));
            if (iteration + 1 == iterations && currentLandMass < minLandMass)
            {
                iteration--;
            }
        }

        return island;
    }

    private int CountNeighbors(Tile[,] island, int row, int col)
    {
        int count = 0;

        int rows = island.GetLength(0);
        int columns = island.GetLength(1);

        int[] rowOffsets = { -1, 1, 0, 0 };
        int[] colOffsets = { 0, 0, -1, 1 };

        for (int direction = 0; direction < 4; direction++)
        {
            int newRow = row + rowOffsets[direction];
            int newCol = col + colOffsets[direction];

            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < columns)
            {
                if (!(island[newRow, newCol] is WaterTile))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void PrintIsland(Tile[,] island)
    {
        int rows = island.GetLength(0);
        int columns = island.GetLength(1);

        Console.WriteLine("Island:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (island[i, j] is WaterTile)
                {
                    Console.Write(".");
                }
                else if (!(island[i, j] is WaterTile))
                {
                    Console.Write("X");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}