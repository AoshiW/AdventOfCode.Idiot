using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles;

public static class CollectionsExtensions
{
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this List<T> list) => CollectionsMarshal.AsSpan(list);

    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> list) => CollectionsMarshal.AsSpan(list);

    public static LinkedListNode<T> NextCircleNode<T>(this LinkedListNode<T> item) => item.Next ?? item!.List!.First!;
    public static LinkedListNode<T> PreviousCircleNode<T>(this LinkedListNode<T> item) => item.Previous ?? item!.List!.Last!;
    public static IEnumerable<T> AsEnumerable<T>(this T[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                yield return array[i, j];
            }
        }
    }

    public static IEnumerable<IEnumerable<T>> GetSection<T>(this T[,] array, int rs, int re, int cs, int ce)
    {
        for (; rs < re; rs++)
        {
            yield return Row(rs);
        }
        IEnumerable<T> Row(int r)
        {
            for (int c = cs; c < ce; c++)
            {
                yield return array[r, c];
            }
        }
    }
}
