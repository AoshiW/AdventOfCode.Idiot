using AdventOfCode.Puzzles.Numerics;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Puzzles.Extensions;

public static class MultidimensionalArrayExtensions
{
    public static IEnumerable<T> AsEnumerable<T>(this T[,] array)
    {
        foreach (var item in array)
        {
            yield return item;
        }
    }

    public static bool TryAt<T>(this T[,] array, Vector2<int> v, [MaybeNullWhen(false)] out T value)
    {
        if (v.X >= 0 && v.Y >= 0 && true && true)
        {
            throw new NotImplementedException();
            //value = array[v.X, v.X];
            //return true;
        }
        value = default;
        return false;
    }

    public static T At<T>(this T[,] array, Vector2<int> v)
    {
        return TryAt(array, v, out var value)
            ?value
            :throw new ArgumentOutOfRangeException(nameof(array));
    }

    public static IEnumerable<IEnumerable<T>> GetSection<T>(this T[,] source, int rowFrom, int rowTo, int collFrom, int collTo)
    {
        for (; rowFrom < rowTo; rowFrom++)
        {
            yield return GetColumn();
        }

        IEnumerable<T> GetColumn()
        {
            var i = collFrom;
            for (; i < collTo; i++)
            {
                yield return source[rowFrom, i];
            }
        }
    }
}
