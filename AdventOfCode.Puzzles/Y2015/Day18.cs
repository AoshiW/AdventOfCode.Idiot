using AdventOfCode.Puzzles.Extensions;
using System.Drawing;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 18, "Like a GIF For Your Yard")]
public class Day18 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var light = ParseInput(span);
        var pointOffset = MagicNumbers.Offset8;
        for (int i = 0; i < 100; i++)
        {
            foreach (var p in GetPointEnumerator(light[0]))
            {
                int sum = 0;
                foreach (var item in pointOffset)
                {
                    var np = p + item;
                    if (np.X.IsInRange(0, 100) && np.Y.IsInRange(0, 100))
                    {
                        if (light[i & 1][np.X, np.Y])
                        {
                            sum++;
                        }
                    }
                }
                light[i + 1 & 1][p.X, p.Y] = light[i % 2][p.X, p.Y] ? sum is 2 or 3 : sum == 3;
            }
        }
        return light[0].AsEnumerable().Count(static l => l);
    }

    static bool[][,] ParseInput(ReadOnlySpan<char> span)
    {
        var light = new bool[2][,] { new bool[100, 100], new bool[100, 100] };
        var enumerator = span.EnumerateLines();
        for (int i = 0; enumerator.MoveNext(); i++)
        {
            var line = enumerator.Current;
            for (int i2 = 0; i2 < line.Length; i2++)
            {
                light[0][i, i2] = line[i2] == '#';
            }
        }
        return light;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var light = ParseInput(span);
        var pointOffset = MagicNumbers.Offset8;
        for (int i = 0; i < 100; i++)
        {
            TrueOnCorner(light[i & 1]);
            foreach (var p in GetPointEnumerator(light[0]))
            {
                int sum = 0;
                foreach (var item in pointOffset)
                {
                    var np = p + item;
                    if (np.X.IsInRange(0, 100) && np.Y.IsInRange(0, 100) && light[i & 1][np.X, np.Y])
                    {
                        sum++;
                    }
                }
                light[i + 1 & 1][p.X, p.Y] = light[i & 1][p.X, p.Y] ? sum is 2 or 3 : sum == 3;
            }
        }
        TrueOnCorner(light[0]);
        return light[0].AsEnumerable().Count(static l => l);
    }

    static void TrueOnCorner(bool[,] map)
    {
        map[0, 0] = map[0, 99] = map[99, 0] = map[99, 99] = true;
    }

    static IEnumerable<Point> GetPointEnumerator(bool[,] map)
    {
        var p = new Point();
        for (; p.Y < map.GetLength(0); p.Y++)
        {
            for (p.X = 0; p.X < map.GetLength(1); p.X++)
            {
                yield return p;
            }
        }
    }
}
