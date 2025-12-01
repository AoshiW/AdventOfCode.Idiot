
using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 11, "Cosmic Expansion")]
public class Day11 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
        => CoreFnc(span, 2);

    public long Part2(ReadOnlySpan<char> span)
        => CoreFnc(span, 1_000_000);

    static (List<Vector2<int>> Points, List<int> Rows, List<int> Cols) ParseInput(ReadOnlySpan<char> span)
    {
        var points = new List<Vector2<int>>();
        var emptyRows = new List<int>();
        var emptyCols = new List<int>();
        var map = new Input2D(span);
        for (var row = 0; row < map.Rows; row++)
        {
            var line = map.GetRow(row); ;
            if (line.Contains('#'))
            {
                for (int c = 0; c < line.Length; c++)
                {
                    if (line[c] == '#')
                        points.Add(new(c, row));
                }
            }
            else
            {
                emptyRows.Add(row);
            }
        }

        for (var point = Vector2<int>.Zero; point.X < map.Columns; point.X++)
        {
            var addCol = true;
            for (point.Y = 0; point.Y < map.Rows; point.Y++)
            {
                if (map[point] == '#')
                {
                    addCol = false;
                    break;
                }
            }
            if (addCol)
            {
                emptyCols.Add(point.X);
            }
        }
        return (points, emptyRows, emptyCols);
    }

    static long CoreFnc(ReadOnlySpan<char> span, int expansionCount)
    {
        var (points, rows, cols) = ParseInput(span);
        long sum = 0;
        while (points.Count > 1)
        {
            var last = points[^1];
            points.RemoveAt(points.Count - 1);
            foreach (var point in points.AsSpan())
            {
                var min = Min(point, last);
                var max = Max(point, last);

                int expansionRow = EmptyCount(rows, min.Y, max.Y) * (expansionCount - 1);
                int expansionCol = EmptyCount(cols, min.X, max.X) * (expansionCount - 1);

                sum += int.ManhattanDistance(
                    max - Vector2<int>.Up * expansionCol + Vector2<int>.Right * expansionRow,
                    min
                    );
            }
        }
        return sum;
    }
    public static Vector2<int> Max(Vector2<int> left, Vector2<int> right)
    {
        var maxX = int.Max(left.X, right.X);
        var maxY = int.Max(left.Y, right.Y);
        return new(maxX, maxY);
    }

    public static Vector2<int> Min(Vector2<int> left, Vector2<int> right)
    {
        var minX = int.Min(left.X, right.X);
        var minY = int.Min(left.Y, right.Y);
        return new(minX, minY);
    }

    static int EmptyCount(List<int> source, int from, int to)
    {
        var count = 0;
        foreach (var item in source.AsSpan())
        {
            if (item > from && item < to)
                count++;
        }
        return count;
    }
}
