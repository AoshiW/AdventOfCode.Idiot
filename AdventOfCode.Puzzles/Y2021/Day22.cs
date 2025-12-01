using System.Numerics;

namespace AdventOfCode.Puzzles.Y2021;

[AocPuzzle(2021, 22, "Reactor Reboot")]
public class Day22 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var cube = new HashSet<Vector3>();
        Span<int> num = stackalloc int[6];
        Func<Vector3, bool> fnc;
        foreach (var item in span.EnumerateLines())
        {
            ParseLine(item, num, out var isOn);
            if (num.Any(static x => !x.IsInRange(-50, 51)))
                continue;
            fnc = isOn ? cube.Add : cube.Remove;
            for (int x = num[0]; x <= num[1]; x++)
            {
                for (int y = num[2]; y <= num[3]; y++)
                {
                    for (int z = num[4]; z <= num[5]; z++)
                    {
                        fnc(new(x, y, z));
                    }
                }
            }
        }
        return cube.Count;
    }

    static void ParseLine(ReadOnlySpan<char> span, Span<int> num, out bool isOn)
    {
        isOn = span[1] == 'n';
        var i = 0;
        foreach (var item in span.EnumerateSlices("onf xyz=,."))
        {
            num[i++] = int.Parse(item);
        }
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
    }
}
