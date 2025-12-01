namespace AdventOfCode.Puzzles.Y2022;

[AocPuzzle(2022, 25, "Full of Hot Air")]
public class Day25 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        long sum = default;
        foreach (var line in span.EnumerateLines())
        {
            sum += FromSnafu(line);
        }
        return ToSnafu(sum);
    }

    static long FromSnafu(ReadOnlySpan<char> span)
    {
        long snafu = 0;
        for (int i = 0; i < span.Length; i++)
        {
            snafu = snafu * 5 + span[i] switch
            {
                '2' => 2,
                '1' => 1,
                '0' => 0,
                '-' => -1,
                '=' => -2,
            };
        }
        return snafu;
    }

    static string ToSnafu(long value)
    {
        var list = new List<char>();
        while (value != 0)
        {
            (value, var rem) = long.DivRem(value, 5);
            var c = rem switch
            {
                0 => '0',
                1 => '1',
                2 => '2',
                3 => '=',
                4 => '-'
            };
            if (rem is 3 or 4)
                value++;
            list.Add(c);
        }
        list.AsSpan().Reverse();
        return list.AsSpan().ToString();
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        return default!;
    }
}
