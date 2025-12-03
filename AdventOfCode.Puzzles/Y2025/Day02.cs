namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 2, "Gift Shop")]
public class Day02 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
        => PartCore(span, IsInvalid);

    public long Part2(ReadOnlySpan<char> span)
        => PartCore(span, IsInvalid2);

    static long PartCore(ReadOnlySpan<char> span, Func<long, bool> isInvalid)
    {
        long sum = 0;
        Span<Range> splits = stackalloc Range[2];
        foreach (var item in span.Split(','))
        {
            var range = span[item];
            range.Split(splits, '-');
            var s = long.Parse(range[splits[0]]);
            var e = long.Parse(range[splits[1]]);
            while (s <= e)
            {
                if (isInvalid(s))
                    sum += s;
                s++;
            }
        }
        return sum;
    }

    static bool IsInvalid(long id)
    {
        Span<char> str = stackalloc char[MagicNumbers.MaxStringLengthFor<long>()];
        id.TryFormat(str, out var len);
        str = str.Slice(0, len);

        if ((len & 2) == 1)
            return false;

        str = str.Slice(0, len);
        return str.Slice(0, str.Length / 2).SequenceEqual(str.Slice(str.Length / 2));
    }

    static bool IsInvalid2(long id)
    {
        Span<char> str = stackalloc char[MagicNumbers.MaxStringLengthFor<long>()];
        id.TryFormat(str, out var len);
        str = str.Slice(0, len);

        for (int i = 1; i <= str.Length / 2; i++)
        {
            if (str.Count(str.Slice(0, i)) * i == str.Length)
                return true;
        }
        return false;
    }
}
