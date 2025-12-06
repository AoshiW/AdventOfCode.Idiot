namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 5, "Cafeteria ")]
public class Day05 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var enumerator = span.EnumerateLines();
        var ranges = new List<(long From, long To)>();
        Span<Range> splits = stackalloc Range[2];
        while (enumerator.MoveNext() && !enumerator.Current.IsEmpty)
        {
            enumerator.Current.Split(splits, '-');
            ranges.Add((
                long.Parse(enumerator.Current[splits[0]]),
                long.Parse(enumerator.Current[splits[1]])));
        }

        long count = 0;
        foreach (var id in enumerator)
        {
            var num = long.Parse(id);
            if (IsInAnyRange(ranges, num))
                count++;
        }
        return count;
    }

    static bool IsInAnyRange(List<(long, long)> ranges, long id)
    {
        foreach (var item in ranges)
        {
            if (id >= item.Item1 && id <= item.Item2)
                return true;
        }
        return false;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var enumerator = span.EnumerateLines();
        var ranges = new List<(long From, long To)>();
        Span<Range> splits = stackalloc Range[2];
        while (enumerator.MoveNext() && !enumerator.Current.IsEmpty)
        {
            enumerator.Current.Split(splits, '-');
            var newRange = (
                long.Parse(enumerator.Current[splits[0]]),
                long.Parse(enumerator.Current[splits[1]]));
            ranges.Add(newRange);
        }

        ranges.Sort((l, r) => l.From.CompareTo(r.From));
        for (var i = 1; i < ranges.Count; i++)
        {
            var prev = ranges[i - 1];
            var curr = ranges[i];
            if (prev.To + 1 >= curr.From)
            {
                ranges[i - 1] = (prev.From, long.Max(curr.To, prev.To));
                ranges.Remove(curr);
                i--;
            }
        }
        return ranges.Sum(x => x.To - x.From + 1);
    }
}
