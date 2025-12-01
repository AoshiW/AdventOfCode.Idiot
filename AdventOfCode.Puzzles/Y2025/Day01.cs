namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 1, "Secret Entrance")]
public class Day01 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var z = 0;
        var dial = 50;
        foreach (var line in span.EnumerateLines())
        {
            var rotation = int.Parse(line.Slice(1));
            rotation *= (line[0] == 'L' ? -1 : 1);

            dial += rotation;
            if (dial < 0)
                dial += 100;
            dial %= 100;
            if (dial is 0)
                z++;
        }
        return z;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var zeros = 0;
        var dial = 50;
        foreach (var line in span.EnumerateLines())
        {
            var rotation = int.Parse(line.Slice(1));
            var dr = int.DivRem(rotation, 100);
            zeros += dr.Quotient;
            rotation = dr.Remainder;
            rotation *= (line[0] == 'L' ? -1 : 1);

            var initDialValue = dial;
            dial += rotation;
            if (dial < 0)
            {
                dial += 100;
                if (initDialValue != 0)
                    zeros++;
            }
            else if (dial > 99)
            {
                dial -= 100;
                zeros++;
            }
            else if (dial is 0)
                zeros++;
        }
        return zeros;
    }
}
