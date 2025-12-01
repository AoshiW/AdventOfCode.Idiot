using System.Runtime.CompilerServices;

namespace AdventOfCode.Puzzles.Y2022;

[AocPuzzle(2022, 4, "Camp Cleanup")]
public class Day04 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span) => Core(span, true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int Core(ReadOnlySpan<char> span, bool isPart1)
    {
        var result = 0;
        foreach (var item in span.EnumerateLines())
        {
            var index = item.IndexOf(',');
            ParseSections(item.Slice(0, index), out var from1, out var to1);
            ParseSections(item.Slice(index + 1), out var from2, out var to2);
            if (isPart1
                ? from1.IsInRange(from2, to2 + 1) && to1.IsInRange(from2, to2 + 1) || from2.IsInRange(from1, to1 + 1) && to2.IsInRange(from1, to1 + 1)
                : from1.IsInRange(from2, to2 + 1) || to1.IsInRange(from2, to2 + 1) || from2.IsInRange(from1, to1 + 1) || to2.IsInRange(from1, to1 + 1))
            {
                result++;
            }
        }
        return result;
    }

    static void ParseSections(ReadOnlySpan<char> span, out int from, out int to)
    {
        var index = span.IndexOf('-');
        from = int.Parse(span.Slice(0, index));
        to = int.Parse(span.Slice(index + 1));
    }

    public int Part2(ReadOnlySpan<char> span) => Core(span, false);
}
