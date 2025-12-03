using System.Drawing;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Puzzles;

public static class MagicNumbers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MaxStringLengthFor<T>()
    {
        if (typeof(T) == typeof(int))
            return 11;
        if (typeof(T) == typeof(long))
            return 20;
        throw new ArgumentException(null, nameof(T));
    }

    public static ReadOnlySpan<Size> Offset8 => Offset8Data;
    private static readonly Size[] Offset8Data = new Size[]
    {
            new(-1,-1), new(-1,0), new(-1,1),
            new(0,-1), new(0,1),
            new(1,-1), new(1,0), new(1,1)
    };
    public static readonly Size[] Offset4 = new Size[]
    {
            new(-1,0),
            new(0,-1), new(0,1), new(1,0),   };
}
