namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 6, "Wait For It")]
public class Day06 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var enumerator = span.EnumerateLines(1);
        var line1 = enumerator.Current.Slice(9).EnumerateSlices(" ");
        enumerator.MoveNext();
        var line2 = enumerator.Current.Slice(9).EnumerateSlices(" ");

        int result = 1;
        while (line1.MoveNext() && line2.MoveNext())
        {
            var time = int.Parse(line1.Current);
            var distance = int.Parse(line2.Current);
            result *= NumberOfWins(time, distance);
        }
        return result;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var enumerator = span.EnumerateLines(1);
        long time = ExtractNumber(enumerator.Current);
        enumerator.MoveNext();
        long distance = ExtractNumber(enumerator.Current);

        return NumberOfWins(time, distance);
    }

    static long ExtractNumber(ReadOnlySpan<char> span)
    {
        long result = 0;
        foreach (char c in span)
        {
            if (char.IsNumber(c))
                result = result * 10 + c - '0';
        }
        return result;
    }

    static int NumberOfWins(long time, long distance)
    {
        int canBeatNumber = 0;
        for (var i = 1; i < time; i++)
        {
            var item = (time - i) * i;
            if (item > distance)
            {
                canBeatNumber++;
            }
        }
        return canBeatNumber;
    }
}
