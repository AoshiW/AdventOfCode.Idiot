using AdventOfCode.Puzzles.Numerics;
using System.Drawing;

namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 9, "Movie Theater")]
public class Day09 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var m = new List<Vector2<int>>();
        Span<Range> splits = stackalloc Range[2];
        foreach (var line in span.EnumerateLines())
        {
            line.Split(splits, ',');
            m.Add(new(
                int.Parse(line[splits[0]]),
                int.Parse(line[splits[1]])));
        }
        long max = 0;
        for (var i = 0; i < m.Count; i++)
        {
            var l = m[i];
            for (var j = i + 1; j < m.Count; j++)
            {
                var r = m[j];
                var temp = CalculateArea(l, r);
                max = long.Max(max, temp);
            }
        }
        return max;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        span = """
            7,1
            11,1
            11,7
            9,7
            9,5
            2,5
            2,3
            7,3
            """;
        var m = new List<Vector2<int>>();
        Span<Range> splits = stackalloc Range[2];
        foreach (var line in span.EnumerateLines())
        {
            line.Split(splits, ',');
            m.Add(new(
                int.Parse(line[splits[0]]),
                int.Parse(line[splits[1]])));
        }

        minX = m[0].X;
        maxX = m[0].X;
        minY = m[0].Y;
        maxY = m[0].Y;
        for (int i = 1; i < m.Count; i++)
        {
            var q = m[i];
            minX = Math.Min(q.X, minX);
            maxX = Math.Max(q.X, maxX);
            minY = Math.Min(q.Y, minY);
            maxY = Math.Max(q.Y, maxY);
        }

        long max = 0;
        for (var i = 0; i < m.Count; i++)
        {
            var l = m[i];
            for (var j = i + 1; j < m.Count; j++)
            {
                var r = m[j];
                var temp = CalculateArea(l, r);

                if (temp > max && IsInPolygon(l, r, m))
                {
                    Console.WriteLine(temp);
                    max = temp;
                }
            }
        }
        return max;
    }

    static long CalculateArea(Vector2<int> l, Vector2<int> r)
        => (long.Abs(l.X - r.X) + 1) * (long.Abs(l.Y - r.Y) + 1);

    static bool IsInPolygon(Vector2<int> start, Vector2<int> end, List<Vector2<int>> p)
    {
        var min = new Vector2<int>(int.Min(start.X, end.X), int.Min(start.Y, end.Y));
        var max = new Vector2<int>(int.Max(start.X, end.X), int.Max(start.Y, end.Y));
        if (!IsPointInPoligon(min, p) ||
            !IsPointInPoligon(max, p) ||
            !IsPointInPoligon(new(min.X, max.Y), p) ||
            !IsPointInPoligon(new(max.X, min.Y), p) 
            )
            return false;
        var diffY = int.Abs(start.Y - end.Y);
        for (var v = min; v.X <= max.X; v.X++)
        {
            if (!IsPointInPoligon(v, p) || !IsPointInPoligon(v + new Vector2<int>(0, diffY), p))
                return false;
        }
        var diffX = int.Abs(start.X - end.X);
        for (var v = min; v.Y <= max.Y; v.Y++)
        {
            if (!IsPointInPoligon(v, p) || !IsPointInPoligon(v + new Vector2<int>(diffX, 0), p))
                return false;
        }
        return true;
    }
    static int minX, minY, maxX, maxY;
    static bool IsPointInPoligon(Vector2<int> v, List<Vector2<int>> polygon)
    {

        //if (v.X < minX || v.X > maxX || v.Y < minY || v.Y > maxY)
        //{
        //    return false;
        //}
        // https://wrf.ecse.rpi.edu/Research/Short_Notes/pnpoly.html
        bool inside = false;
        for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        {
            if ((polygon[i].Y > v.Y) != (polygon[j].Y > v.Y) &&
                 v.X < (polygon[j].X - polygon[i].X) * (v.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
            {
                inside = !inside;
            }
        }

        return inside;
    }
}
