using System.Runtime.CompilerServices;

namespace Ultravex;

public static class BitUtils
{
    /// <summary>
    /// 0 <-> 2
    /// 1 <-> 3
    /// </summary>
    /// <param name="side"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetOpposedSide(this int side) => (side + 2) & 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RotateClockwise(this int side) => (side + 1) & 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RotateCounterClockwise(this int side) => (side - 1) & 3;
}
