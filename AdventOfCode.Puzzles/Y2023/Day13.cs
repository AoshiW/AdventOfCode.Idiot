using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 13, "Point of Incidence")]
public class Day13 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
        => CoreFnc(span, true);

    public int Part2(ReadOnlySpan<char> span)
        => CoreFnc(span, false);

    static int CoreFnc(ReadOnlySpan<char> span, bool hasSmudgeDefault)
    {
        var sum = 0;
        var rows = new List<uint>();
        var cols = new List<uint>();
        foreach (var line in span.EnumerateLines())
        {
            if (line.IsEmpty)
            {
                sum += SummarizePattern(rows, cols, hasSmudgeDefault);
                rows.Clear();
                cols.AsSpan().Clear();
            }
            else
            {
                var row = 0u;
                CollectionsMarshal.SetCount(cols, line.Length);
                var colsSpan = cols.AsSpan();
                for (var i = 0; i < line.Length; i++)
                {
                    ref var col = ref colsSpan[i];
                    col <<= 1;
                    row <<= 1;
                    if (line[i] == '#')
                    {
                        row |= 1;
                        col |= 1;
                    }
                }
                rows.Add(row);
            }
        }
        return sum + SummarizePattern(rows, cols, hasSmudgeDefault);
    }

    static int SummarizePattern(List<uint> rows, List<uint> cols, bool hasSmudge)
    {
        var span = rows.AsSpan();
        for (var i = 1; i < span.Length; i++)
        {
            if (IsReflection(span, i, hasSmudge))
            {
                return i * 100;
            }
        }
        span = cols.AsSpan();
        for (var i = 1; i < span.Length; i++)
        {
            if (IsReflection(span, i, hasSmudge))
            {
                return i;
            }
        }
        throw new Exception();
    }

    static bool IsReflection(ReadOnlySpan<uint> span, int index, bool hasSmudge)
    {
        var low = index - 1;
        var big = index;
        while (low >= 0 && big < span.Length)
        {
            if (span[low] == span[big])
            {
                low--;
                big++;
            }
            else if (!hasSmudge && uint.PopCount(span[low] ^ span[big]) == 1)
            {
                hasSmudge = true;
                low--;
                big++;
            }
            else
                return false;
        }
        return hasSmudge;
    }
}
