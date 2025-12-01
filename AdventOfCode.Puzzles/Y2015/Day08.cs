namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 8, "Matchsticks ")]
public class Day08 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        int sum = 0;
        foreach (var line in span.EnumerateLines())
        {
            var isEscaped = false;
            int hex = 0;
            sum += 2;
            for (int i = 1; i < line.Length - 1; i++)
            {
                var c = line[i];
                if (isEscaped)
                {
                    sum++;
                    if (hex == 0)
                    {
                        if (c == 'x')
                        {
                            hex++;
                        }
                        else
                        {
                            isEscaped = false;
                        }
                    }
                    else
                        if (++hex == 3)
                    {
                        isEscaped = false;
                        hex = 0;
                    }
                }
                else if (c == '\\')
                {
                    isEscaped = true;
                }
            }
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        int sum = 0;
        foreach (var line in span.EnumerateLines())
        {
            var isEscaped = false;
            int hex = 0;
            sum += 4;
            for (int i = 1; i < line.Length - 1; i++)
            {
                var c = line[i];
                if (isEscaped)
                {
                    if (hex == 0)
                    {
                        if (c == 'x')
                        {
                            hex++;
                        }
                        else
                        {
                            sum++;
                            isEscaped = false;
                        }
                    }
                    else if (++hex == 3)
                    {
                        isEscaped = false;
                        hex = 0;
                    }
                }
                else if (c == '\\')
                {
                    sum++;
                    isEscaped = true;
                }
            }
        }
        return sum;
    }
}
