using AdventOfCode.Puzzles.Numerics;
using System.Globalization;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 18, "Lavaduct Lagoon")]
public class Day18 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span) => CountOfCubicMeters(span, line =>
    {
        Span<Range> split = stackalloc Range[3];
        line.Split(split, ' ');
        var direction = line[0];
        var distance = int.Parse(line[split[1]]);
        return (distance, direction);
    });

    ReadOnlySpan<char> NumToChar => ['R', 'D', 'L', 'U'];

    public long Part2(ReadOnlySpan<char> span) => CountOfCubicMeters(span, line =>
    {
        line = line.Slice(line.IndexOf('#') + 1);
        var direction = NumToChar[line[^2] - '0'];
        var distance = int.Parse(line.Slice(0, 5), NumberStyles.HexNumber);
        return (distance, direction);
    });

    static long CountOfCubicMeters(ReadOnlySpan<char> span, Func<ReadOnlySpan<char>, (long Distance, char Direction)> lineParser)
    {
        var points = new List<Vector2<long>>();
        long l = 0;
        Vector2<long> point = default;
        foreach (var line in span.EnumerateLines())
        {
            var data = lineParser(line);
            switch (data.Direction)
            {
                case 'R':
                    point.X += data.Distance;
                    break;
                case 'D':
                    point.Y += data.Distance;
                    break;
                case 'L':
                    point.X -= data.Distance;
                    break;
                case 'U':
                    point.Y -= data.Distance;
                    break;
                default:
                    throw new FormatException();
            }
            l += data.Distance;
            points.Add(point);
        }
        var s = ShoelaceFormula(points.AsSpan());

        //https://en.wikipedia.org/wiki/Pick%27s_theorem
        return s - l / 2 + l + 1;
    }

    // Camster (enigmaticcam)   https://discord.com/channels/143867839282020352/782914654284021800/1186204931967950888
    //https://en.wikipedia.org/wiki/Shoelace_formula#Example
    static long ShoelaceFormula(ReadOnlySpan<Vector2<long>> points)
    {
        var sum = 0L;
        var last = points[0];
        foreach (var point in points.Slice(1))
        {
            sum += Calculate(last, point);
            last = point;
        }
        sum += Calculate(last, points[0]);
        return sum / 2;

        static long Calculate(Vector2<long> last, Vector2<long> point)
        {
            return last.X * point.Y - last.Y * point.X;
        }
    }
}
