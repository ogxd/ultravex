namespace Ultravex;

public unsafe struct Tile
{
    internal fixed int Val[4];

    public static Tile CreateRandom()
    {
        var tile = new Tile();
        for (int i = 0; i < 4; i++)
            tile.Val[i] = Random.Shared.Next(0, 10);
        return tile;
    }

    public int this[int index]
    {
        get => Val[index];
        //set => Val[index] = value;
    }
}