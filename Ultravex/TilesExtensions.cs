using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ultravex;

public unsafe static class TilesExtensions
{
    public static void Solve(this Tile[] tiles)
    {
        int boardSize = (int)Math.Sqrt(tiles.Length);
        for (var i = 0; i < tiles.Length; i++)
        {
            var browsedTiles = new HashSet<int>();
            // Try find a valid 2x2
            for (var s = 0; i < 4; i++)
            {
                browsedTiles.Add(i);
            }
        }
    }

    public static IEnumerable<Tile[]> Find2x2(this Span<Tile> tiles)
    {
        int[] solution = new int[4];
        for (int i = 0; i < tiles.Length; i++)
        {
            solution[0] = i:
            int s = 2; // Starts right
            for (int j = 1; j < tiles.Length; j++)
            {
                if (tiles[0][s] == tiles[j][s.GetOpposedSide()])
                {
                    tiles.SwapTiles(1, j);
                    s = s.RotateClockwise();
                    for (int k = 2; k < tiles.Length; k++)
                    {
                        if (tiles[1][s] == tiles[k][s.GetOpposedSide()])
                        {
                            tiles.SwapTiles(2, k);
                            s = s.RotateClockwise();
                            if (tiles[2][s] == tiles[3][s.GetOpposedSide()])
                            {
                                s = s.RotateClockwise();
                                // Check if it connects back to first tile to finish the 2x2
                                if (tiles[3][s] == tiles[0][s.GetOpposedSide()])
                                {
                                    var solution = new Tile[4];
                                    solution[0] = tiles[0];
                                    solution[1] = tiles[1];
                                    solution[2] = tiles[3];
                                    solution[3] = tiles[2];
                                    yield return solution;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // Solving a 2x2 is quite trivial because the number of possibilities is 4! = 4 * 3 * 2 = 24 placement possibilities
    public static IEnumerable<Tile[]> Solve2x2(this Tile[] tiles)
    {
        Debug.Assert(tiles.Length == 4, "Solve2x2 can only operate on 2x2 tile sets");

        for (int i = 0; i < 4; i++)
        {
            tiles.SwapTiles(0, i);
            int s = 2; // Starts right
            for (int j = 1; j < 4; j++)
            {
                if (tiles[0][s] == tiles[j][s.GetOpposedSide()])
                {
                    tiles.SwapTiles(1, j);
                    s = s.RotateClockwise();
                    for (int k = 2; k < 4; k++)
                    {
                        if (tiles[1][s] == tiles[k][s.GetOpposedSide()])
                        {
                            tiles.SwapTiles(2, k);
                            s = s.RotateClockwise();
                            if (tiles[2][s] == tiles[3][s.GetOpposedSide()])
                            {
                                s = s.RotateClockwise();
                                // Check if it connects back to first tile to finish the 2x2
                                if (tiles[3][s] == tiles[0][s.GetOpposedSide()])
                                {
                                    var solution = new Tile[4];
                                    solution[0] = tiles[0];
                                    solution[1] = tiles[1];
                                    solution[2] = tiles[3];
                                    solution[3] = tiles[2];
                                    yield return solution;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public static Tile[] CreateValidSet(int boardSize)
    {
        var tiles = new Tile[boardSize * boardSize];
        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                var tile = Tile.CreateRandom();

                if (x > 0)
                    tile.Val[0] = tiles[y * boardSize + x - 1].Val[2];

                if (y > 0)
                    tile.Val[1] = tiles[(y - 1) * boardSize + x].Val[3];

                tiles[y * boardSize + x] = tile;
            }
        }

        return tiles;
    }

    public static void Shuffle(this Tile[] tiles)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles.SwapTiles(i, Random.Shared.Next(0, tiles.Length));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SwapTiles(this Tile[] tiles, int a, int b)
    {
        var swap = tiles[a];
        tiles[a] = tiles[b];
        tiles[b] = swap;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SwapTiles(this Span<Tile> tiles, int a, int b)
    {
        var swap = tiles[a];
        tiles[a] = tiles[b];
        tiles[b] = swap;
    }

    public static void Save(this Tile[] tiles, string file)
    {
        fixed (Tile* tilesPtr = tiles)
        {
            var span = new Span<byte>(tilesPtr, sizeof(Tile) * tiles.Length);
            using FileStream fs = new FileStream(file, FileMode.CreateNew);
            fs.Write(span);
        }
    }

    public static bool IsCompleted(this Tile[] tiles)
    {
        int boardSize = (int)Math.Sqrt(tiles.Length);
        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                var tile = tiles[y * boardSize + x];

                if (x > 0 && tile.Val[0] != tiles[y * boardSize + x - 1].Val[2])
                    return false;

                if (y > 0 && tile.Val[1] != tiles[(y - 1) * boardSize + x].Val[3])
                    return false;
            }
        }

        return true;
    }

    public static bool Print(this Tile[] tiles)
    {
        int boardSize = (int)Math.Sqrt(tiles.Length);
        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                var tile = tiles[y * boardSize + x];
                Console.Write($"┌{tile.Val[1]}┐ ");
            }
            Console.WriteLine();

            for (int x = 0; x < boardSize; x++)
            {
                var tile = tiles[y * boardSize + x];
                Console.Write($"{tile.Val[0]} {tile.Val[2]} ");
            }
            Console.WriteLine();

            for (int x = 0; x < boardSize; x++)
            {
                var tile = tiles[y * boardSize + x];
                Console.Write($"└{tile.Val[3]}┘ ");
            }

            Console.WriteLine();
        }

        return true;
    }
}
