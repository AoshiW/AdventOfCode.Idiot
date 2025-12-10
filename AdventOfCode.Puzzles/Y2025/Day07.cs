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
            while (grid.TryGet(p + Vector2<int>.Down, out var c))
            {
                if (c is '|')
                    break;
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
        return splitCount;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var input = new Input2D(span);
        Span<long> active1 = new long[input.Columns];
        Span<long> active2 = new long[input.Columns];

        active1[input.GetRow(0).IndexOf('S')] = 1;

        for (int r = 0; r < input.Rows; r++)
        {
            active2.Fill(default);
            for (int c = 0; c < input.Columns; c++)
            {
                if (active1[c] is not 0)
                {
                    if (input[c, r] == '^')
                    {
                        active2[c + 1] += active1[c];
                        active2[c - 1] += active1[c];
                    }
                    else
                    {
                        active2[c] += active1[c];
                    }
                }
            }
            active2.CopyTo(active1);
        }

        return active1.Sum();
    }
}