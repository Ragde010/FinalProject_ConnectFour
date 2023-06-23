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
        //Check rows for a win
        for(int row = 0; row <Rows; row++)
        {
            if(CheckSequence(Cells,row, 0, 0, 1, symbol,4))
            {
                return true;
            }
        }
        //Check columns for a win
        for(int col = 0; col < Columns; col++)
        {
            if(CheckSequence(Cells, 0, col, 1, 0, symbol,4))
            {
                return true;
            }
        }

        //Check diagonals (top-left to bottom-right) for a win
        for(int row = 0; row < Rows - 3; row++)
        {
            for(int col = 0; col <Columns - 3; col++)
            {
                if(CheckSequence(Cells,row,col,1,1,symbol,4))
                {
                    return true;
                }
            }
        }

        //Check diagonals (top-right to bottom-left) for a win
        for (int row = 0; row < Rows - 3; row++)
        {
            for (int col = 3; col < Columns; col++)
            {
                if (CheckSequence(cells, row, col, 1, -1, symbol, 4))
                {
                    return true;
                }
            }
        }
        return false; // No win condition found
       
    }
    // This method checks for a sequence of symbols in the Connect4 game grid
    // It takes the current game grid, starting row and column, row and column increments,
    // the symbol to check, and the required count of symbols for a win condition
    private bool CheckSequence(Cell[,] cells, int startRow, int startCol, int rowIncrement, int colIncrement, char symbol, int count)
    {   
        // Calculate the end row and column based on the increments and count
        int endRow = startRow + (rowIncrement * (count-1));
        int endCol = startCol +(colIncrement * (count-1));
        
        
        // Check if the end row and column are within the game grid boundaries
        if(endRow >= 0 && endRow <Rows && endCol >= 0 && endCol < Columns)
        {
             // Iterate through the sequence of cells to check for the symbol
            for (int i = 0; i<count; i++)
            {
                int row = startRow + (rowIncrement * i);
                int col = startCol + (colIncrement * i);

                // If the symbol at the current cell does not match the desired symbol, return false
                if(cells[row,col].Symbol !=symbol)
                {
                    return false;
                }
            }
            // If all symbols in the sequence match the desired symbol, return true
            return true;
        }
        // If the sequence extends beyond the game grid boundaries, return false
        return false; 
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

