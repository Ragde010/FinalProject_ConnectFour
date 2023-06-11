namespace CnnectFourGame;
public class Cell
{
    public char Symbol { get; set; }

    public Cell(char symbol)
    {
        Symbol = symbol;
    }
}

public abstract class BoardGame
{
    protected const int Rows = 6;
    public const int Columns = 7;
    protected Cell[,] Cells;

    public abstract void InitializeBoard();
    public abstract void PrintBoard();
    public abstract bool IsValidMove(int column);
    public abstract void DropSymbol(int column, char symbol);
    public abstract bool CheckWin(char symbol);
    public abstract bool IsBoardFull();
}

public class ConnectFour : BoardGame
{
    public ConnectFour()
    {
        Cells = new Cell[Rows, Columns];
    }

    public override void InitializeBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                cells[row, col] = new Cell(' ');
            }
        }
    }

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

    private char GetCellSymbol(int row, int col)
    {
        return Cells[row, col].Symbol;
    }

    private void PrintColumnNumbers()
    {
        for (int col = 0; col < Columns; col++)
        {
            Console.Write($"  {col + 1} ");
        }
        
    }

    public override bool IsValidMove(int column)
    {
        
    }

    public override void DropSymbol(int column, char symbol)
    {
        
    }

    public override bool CheckWin(char symbol)
    {
        // Check rows for a win
        
       
    }

    public override bool IsBoardFull()
    {
        
    }
}

public class GameEngine
{
    private BoardGame Game;

    public void StartGame()
    {

    }

}

    class Program
    {
        static void Main(string[] args)
        {

        }
    }

