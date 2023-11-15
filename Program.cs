// See https://aka.ms/new-console-template for more information

internal class Program
{
    static int[,] map =
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    private static int coinCounter;
    static int mapLength = map.GetLength(0);
    static int mapHeight = map.Length / mapLength;

    private static bool isCoinSpawned;
    private static (int x, int y) playerCoords;
    
    public static void Main()
    {
        StartGame();
    }

    public static void StartGame()
    {
        GameStartSetup();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Console CoinHunter!");
            Console.WriteLine("<><><><><><><><><><>");
            Console.WriteLine($"Coins collected: {coinCounter}");
            Console.WriteLine("<><><><><><><><><><>");
            SpawnCoin();
            RenderGame();
            PlayerMovementInput();
        }
    }

    public static void RenderGame()
    {
        for (int i = 0; i < mapHeight; i++)
        {
            Console.Write("< ");
            for (int j = 0; j < mapLength; j++)
            {
                DrawPosition(map[i, j]);
            }
            Console.WriteLine(">");
        }
    }

    public static void DrawPosition(int positionValue)
    {
        if (positionValue == 0) // Floor 
        {
            Console.Write("- ");
        }

        if (positionValue == 1) // Player
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("P "); 
        }

        if (positionValue == 2) // Coin
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("C "); 
        }
        
        Console.ResetColor();
    }

    public static void GameStartSetup()
    {
        Console.CursorVisible = false;
        playerCoords = (4, 4);
        map[playerCoords.y, playerCoords.x] = 1; // Spawns player
    }

    public static void SpawnCoin()
    {
        if (!isCoinSpawned)
        {
            int randomHeight;
            int randomLength;
            do
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                randomHeight = rand.Next(0, mapHeight);
                randomLength = rand.Next(0, mapLength);
            } while (map[randomHeight, randomLength] != 0);

            map[randomHeight, randomLength] = 2;
            isCoinSpawned = true;
        }
    }

    public static void PlayerMovementInput()
    {
        ConsoleKey input = Console.ReadKey(true).Key;

        // clean current spot
        map[playerCoords.y, playerCoords.x] = 0;
        
        // Left
        if (input == ConsoleKey.A || input == ConsoleKey.LeftArrow) 
            if (playerCoords.x != 0) playerCoords.x--;
        
        // Right
        if (input == ConsoleKey.D || input == ConsoleKey.RightArrow) 
            if (playerCoords.x != mapLength-1) playerCoords.x++;
        
        // Up
        if (input == ConsoleKey.W || input == ConsoleKey.UpArrow) 
            if (playerCoords.y != 0) playerCoords.y--;
        
        // Down
        if (input == ConsoleKey.S || input == ConsoleKey.DownArrow) 
            if (playerCoords.y != mapHeight-1) playerCoords.y++;
        
        // this is before map location value is changed since we need to know if a coin is here
        CheckIfCoinWasCollected();
        // new potential spot if moved
        map[playerCoords.y, playerCoords.x] = 1;
    }

    public static void CheckIfCoinWasCollected()
    {
        // Player standing on coin == collected
        if (map[playerCoords.y, playerCoords.x] == 2)
        {
            coinCounter++;
            isCoinSpawned = false;
        }
    }
}
