using System.Runtime.CompilerServices;

namespace AdventOfCode.Puzzles.Y2016;

[AocPuzzle(2016, 2, "Bathroom Security")]
public class Day02 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        int x = 1, y = 2;
        var sb = new DefaultInterpolatedStringHandler();
        foreach (var line in span.EnumerateLines())
        {
            foreach (var item in line)
            {
                if (item == 'U' && x > 0)
                    x--;
                else if (item == 'D' && x < 2)
                    x++;
                else if (item == 'L' && y > 1)
                    y--;
                else if (item == 'R' && y < 3)
                    y++;
            }
            sb.AppendFormatted(x * 3 + y);
        }
        return sb.ToStringAndClear();
    }

    static readonly char?[,] Map = new char?[,]
    {
        { null, null, '1', null, null },
        { null, '2','3','4',null },
        { '5','6','7','8','9' },
        { null, 'A','B','C',null  },
        { null, null, 'D', null, null },
    };

    public string Part2(ReadOnlySpan<char> span)
    {
        int x = 1, y = 2;
        var sb = new DefaultInterpolatedStringHandler();
        foreach (var line in span.EnumerateLines())
        {
            foreach (var item in line)
            {
                if (item == 'U' && x > 0 && Map[x - 1, y] is not null)
                    x--;
                else if (item == 'D' && x < 4 && Map[x + 1, y] is not null)
                    x++;
                else if (item == 'L' && y > 0 && Map[x, y - 1] is not null)
                    y--;
                else if (item == 'R' && y < 4 && Map[x, y + 1] is not null)
                    y++;
            }
            sb.AppendFormatted(Map[x, y]);
        }
        return sb.ToStringAndClear();
    }
}
