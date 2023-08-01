public class IslandGenerator
{
    bool debug = false;
    public Island InitIsland(int rows, int columns)
    {
        int iterations = 8; 
        int minLandMass = 30;


        int[,] island = GenerateIsland(rows, columns);

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

        int landCells = island.Cast<int>().Count(cell => cell == 1);
        Random random = new Random();
        int startPositionIndex = random.Next(landCells);

        if (debug)
        {
            Console.WriteLine("--- ISLAND AFTER ---");
            PrintIsland(island);    
            Console.WriteLine("TOTAL LANDMASS --> " + landCells + " VS TOTAL CELLS " + totalCells);
        }

        Coordinate playerPos = new Coordinate(0,0);
        int count = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (island[i, j] == 1)
                {
                    if (count == startPositionIndex)
                    {
                        island[i, j] = 2;
                        playerPos = new Coordinate(i,j);
                    }
                    count++;
                }
            }
        }
        return new Island(island, playerPos, rows, columns);
    }

    static int[,] GenerateIsland(int rows, int columns)
    {
        int[,] island = new int[rows, columns];
        Random random = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                island[i, j] = 0;
            }
        }

        int startRow = random.Next(rows / 4, rows / 2);
        int startCol = random.Next(columns / 4, columns / 2);

        island[startRow, startCol] = 1;

        return island;
    }

    private int[,] CellularAutomaton(int[,] island, int iterations, double chance, int minLandMass)
    {
        int rows = island.GetLength(0);
        int columns = island.GetLength(1);

        Random random = new Random();

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            int[,] newIsland = new int[rows, columns];

            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < columns - 1; j++)
                {
                    int neighborsCount = CountNeighbors(island, i, j);
                    if (island[i, j] == 1 || (island[i, j] == 0 && neighborsCount > 0 && random.NextDouble() < chance))
                    {
                        newIsland[i, j] = 1;
                    }
                    if(debug)
                        Console.WriteLine($"Cell [{i}, {j}] Neighbors: {neighborsCount}");
                }
            }

            island = newIsland;

            if(debug){
                Console.WriteLine($"Island after iteration {iteration + 1}:");
                PrintIsland(island);
            }
            int currentLandMass = (island.Cast<int>().Count(cell => cell == 1));
            if(iteration + 1 == iterations && currentLandMass < minLandMass){
                iteration--;
            }
        }

        return island;
    }

    private int CountNeighbors(int[,] island, int row, int col)
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
                if (island[newRow, newCol] == 1)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void PrintIsland(int[,] island)
    {
        int rows = island.GetLength(0);
        int columns = island.GetLength(1);

        Console.WriteLine("Island:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (island[i, j] == 0)
                {
                    Console.Write(".");
                }
                else if (island[i, j] == 1)
                {
                    Console.Write("X");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}