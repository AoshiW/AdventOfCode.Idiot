using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 7, "Laboratories ")]
public class Day07 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var grid = new Input2D(span).ToGrid();
        long splitCount = 0;
        var mem = new List<Vector2<int>>()
        {
            new(grid.GetRow(0).IndexOf('S'), 0)
        };
        while (mem.Count > 0)
        {
            var p = mem[^1];
            mem.RemoveAt(mem.Count - 1);
            while(grid.TryGet(p+Vector2<int>.Down, out var c))
            {
                if (c is '|')
                    break;
                if(c is '^')
                {
                    splitCount++;
                    mem.Add(p + Vector2<int>.Left);
                    mem.Add(p + Vector2<int>.Right);
                    break;
                }
                if(c is '.')
                {
                    p += Vector2<int>.Down;
                    grid[p] = '|';
                    continue;
                }
            }
        }
        return splitCount;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        return default;
        span = """
            .......S.......
            ...............
            .......^.......
            ...............
            ......^.^......
            ...............
            .....^.^.^.....
            ...............
            ....^.^...^....
            ...............
            ...^.^...^.^...
            ...............
            ..^...^.....^..
            ...............
            .^.^.^.^.^...^.
            ...............
            """;
        var grid = new Input2D(span).ToGrid();
        long splitCount = 0;
        var mem = new List<Vector2<int>>()
        {
            new(grid.GetRow(0).IndexOf('S'), 0)
        };
        while (mem.Count > 0)
        {
            var p = mem[^1];
            mem.RemoveAt(mem.Count - 1);
            while (grid.TryGet(p + Vector2<int>.Down, out var c))
            {
                if (c is '|')
                {
                    p += Vector2<int>.Down;
                    continue;
                }
                if (c is '^')
                {
                    splitCount++;
                    mem.Add(p + Vector2<int>.Left);
                    mem.Add(p + Vector2<int>.Right);
                    break;
                }
                if (c is '.')
                {
                    p += Vector2<int>.Down;
                    grid[p] = '|';
                    continue;
                }
            }
        }
        return splitCount + 1;
    }
}