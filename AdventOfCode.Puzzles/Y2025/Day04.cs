
using AdventOfCode.Puzzles.Numerics;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 4, "Printing Department")]
public class Day04 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var sum = 0;
        var map = new Input2D(span);
        for (int r = 0; r < map.Rows; r++)
        {
            for (int c = 0; c < map.Columns; c++)
            {
                var point = new Vector2<int>(c, r);
                if (map[point] is '.')
                    continue;
                var paper = 0;
                foreach (var dir in Grid.Offset8)
                {
                    var nextPoint = point + dir;
                    if (map.TryGet(nextPoint, out var item) && item is '@')
                    {
                        paper++;
                    }
                }
                if (paper < 4)
                    sum++;
            }
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var sum = 0;
        var lastSum = -1;
        var map = new Input2D(span).ToGrid();
        var hs = new HashSet<Vector2<int>>();
        while (sum != lastSum)
        {
            lastSum = sum;
            for (int r = 0; r < map.Rows; r++)
            {
                for (int c = 0; c < map.Columns; c++)
                {
                    var point = new Vector2<int>(c, r);
                    if (map[point] is '.')
                        continue;
                    var paper = 0;
                    foreach (var dir in Grid.Offset8)
                    {
                        var nextPoint = point + dir;
                        if (map.TryGet(nextPoint, out var item) && item is '@')
                        {
                            paper++;
                        }
                    }
                    if (paper < 4)
                    {
                        map[point] = '.';
                        sum++;
                    }
                }
            }
        }
        return sum;
    }
}
