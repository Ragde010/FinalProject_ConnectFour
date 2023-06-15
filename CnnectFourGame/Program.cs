namespace CnnectFourGame;
public class Cell
{
    public char Symbol { get; set; }
    //Constructor
    public Cell(char symbol)
    {
        Symbol = symbol;
    }
}
// Base Class
public abstract class BoardGame
{
    protected const int Rows = 6; //It represents the number of rows in the game board.
    public const int Columns = 7; //It represents the number of column in the game board.
    protected Cell[,] Cells; // It represents the game board and hold the cells of the board as a 2D array of Cell object.

    public abstract void InitializeBoard(); //It is resposible for initializing the game board with empty cells or any specific intial state
    public abstract void PrintBoard(); //It is responsible for printing the current state of the game board to the console
    public abstract bool IsValidMove(int column); //It is responsible for checking if the move is valid for the given column on the board game.
    public abstract void DropSymbol(int column, char symbol);//It is responsible for dropping a symbol into a specified column on the board game.
    public abstract bool CheckWin(char symbol); //It is responsible for checking if a player with specified symbol has won the game.
    public abstract bool IsBoardFull(); //It is responsible for checking if the game board is full (no empty cells remaining)
}

public class ConnectFour : BoardGame
{
    public ConnectFour()
    {
        Cells = new Cell[Rows, Columns];
    }
    //This function overrides the InitializeBoard method inherited from the BoardGame class.
    //It initializes the game board by iterating over each cell and setting it to a new Cell object with a symbol of empty space (' ').
    public override void InitializeBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                Cells[row, col] = new Cell(' ');
            }
        }
    }
    //This function overrides the PrintBoard method inherited from the BoardGame class. 
    //It prints the current state of the game board to the console. 
    //It iterates over each cell and prints its symbol, along with surrounding formatting characters.
    public override void PrintBoard()
    {
        Console.Clear();

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                Console.Write($"| {GetCellSymbol(row, col)} ");
            }
            Console.WriteLine("|");
        }

        PrintColumnNumbers();
        Console.WriteLine();
    }
    //This private helper function returns the symbol of the cell at the specified row and column of the board game.
    private char GetCellSymbol(int row, int col) 
    {
        return Cells[row, col].Symbol;
    }
    // This private helper function prints the column numbers above the board game.
    //It iterates over the columns and prints the column numbers with proper spacing.
    private void PrintColumnNumbers()
    {
        for (int col = 0; col < Columns; col++)
        {
            Console.Write($"  {col + 1} ");
        }
        
    }

    public override bool IsValidMove(int column)
    {
        if(column < 0 || column >= Columns)
        {
            return false;
        }
        return Cells[0,column].Symbol == '';
    }

    public override void DropSymbol(int column, char symbol)
    {
        for (int row = Rows - 1; row >= 0; row--)
        {
            if (Cells[row, column].Symbol == ' ')
            {
                Cells[row, column].Symbol = symbol;
                break;
            }
        }
    }

    public override bool CheckWin(char symbol)
    {
        // Check rows/column for a win
        
       
    }

    public override bool IsBoardFull()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (Cells[row, col].Symbol == ' ')
                {
                    return false;
                }
            }
        }

        return true;
    }
}

public class GameEngine
{
    private BoardGame Game;

    public void StartGame()
    {
        Game = new ConnectFour();
        Game.InitializeBoard();

        bool gameOver = false;
        char currentPlayer = 'X';

        while (!gameOver)
        {
            Game.PrintBoard();
            Console.WriteLine($"Player {currentPlayer}'s turn. Enter a column number(1-{BoardGame.Columns}) to drop your symbol:");

            if(int.TryParse(Console.ReadLine(), out int col))
            {
                col--; //Adjust to 0-based index
                if (Game.IsValidMove(col))
                {
                    Game.DropSymbol(col, currentPlayer);
                    if (Game.CheckWin(currentPlayer))
                    {
                        Game.PrintBoard();
                        Console.WriteLine($"Player{currentPlayer} wins!");
                        gameOver = true;
                    }
                    else if (Game.IsBoardFull())
                    {
                        Game.PrintBoard();
                        Console.WriteLine("It's a draw!");
                        gameOver = true;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}

    class Program
    {
        static void Main(string[] args)
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.StartGame();
        }
    }

