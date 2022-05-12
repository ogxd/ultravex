class Program
{
    static void Main(string[] args)
    {
        var board = CreateValidSet(10);
        Console.WriteLine(CheckIfCompleted(board));
        board = ShuffleTiles(board);
        Console.WriteLine(CheckIfCompleted(board));
        Save(board, "10x10.vex2");
    }
}