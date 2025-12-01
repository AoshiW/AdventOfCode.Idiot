using System.Buffers;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 3, "Gear Ratios")]
public class Day03 : IDay<int>
{
    delegate int Fn(ReadOnlySpan<char> s, SearchValues<char> values);

    static readonly SearchValues<char> _searchValuesP1 = SearchValues.Create(".0123456789\r\n");
    static readonly SearchValues<char> _searchValuesP2 = SearchValues.Create("*");

    public int Part1(ReadOnlySpan<char> span) => CoreFnc(span, MemoryExtensions.IndexOfAnyExcept, _searchValuesP1, static x => x.Sum());

    public int Part2(ReadOnlySpan<char> span) => CoreFnc(span, MemoryExtensions.IndexOfAny, _searchValuesP2, static x => x.Count == 2 ? x[0] * x[1] : 0);

    static int CoreFnc(ReadOnlySpan<char> span, Fn indexFn, SearchValues<char> searchValues, Func<List<int>, int> aggregate)
    {
        var map = new Input2D(span);
        var rawIndex = 0;
        var sum = 0;

        List<int> nums = new();
        int index;
        while ((index = indexFn(span.Slice(rawIndex), searchValues)) != -1)
        {
            rawIndex += index;
            var point = map.GetPointFromRawIndex(rawIndex++);
            nums.Clear();
            if (point.Y != 0)
                Line(map.GetRow(point.Y - 1), point.X, nums);
            Line(map.GetRow(point.Y), point.X, nums);
            if (point.Y < map.Rows)
                Line(map.GetRow(point.Y + 1), point.X, nums);
            sum += aggregate(nums);
        }
        return sum;
    }

    static void Line(ReadOnlySpan<char> line, int c, List<int> nums)
    {
        if (char.IsDigit(line[c]))
        {
            int start = c,
                end = c;
            SeachrNumberLeft(line, ref start, c);
            SeachrNumberRight(line, c, ref end);
            nums.Add(int.Parse(line.Slice(start, end - start + 1)));
        }
        else
        {
            if (char.IsDigit(line[c + 1]))
            {
                var end = c + 1;
                SeachrNumberRight(line, c, ref end);
                nums.Add(int.Parse(line.Slice(c + 1, end - c)));
            }
            if (char.IsDigit(line[c - 1]))
            {
                var start = c - 1;
                SeachrNumberLeft(line, ref start, c);
                nums.Add(int.Parse(line.Slice(start, c - start)));
            }
        }
    }

    static void SeachrNumberLeft(ReadOnlySpan<char> line, ref int from, int to)
    {
        while (from > 0 && char.IsDigit(line[from - 1]))
            from--;
    }

    static void SeachrNumberRight(ReadOnlySpan<char> line, int from, ref int to)
    {
        while (to + 1 < line.Length && char.IsDigit(line[to + 1]))
            to++;
    }
}
