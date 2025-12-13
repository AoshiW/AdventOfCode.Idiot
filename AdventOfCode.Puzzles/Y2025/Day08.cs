using System.Diagnostics;

namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 8, "Playground")]
public class Day08 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var l = new List<(double, double, double)>();
        Span<Range> splits = new Range[3];
        foreach (var line in span.EnumerateLines())
        {
            line.Split(splits, ',');
            l.Add(
                (int.Parse(line[splits[0]]),
                int.Parse(line[splits[1]]),
                int.Parse(line[splits[2]])
                ));
        }
        var m = new Dictionary<((double, double, double), (double, double, double)), double>();
        for (int i = 0; i < l.Count; i++)
        {
            var item1 = l[i];
            foreach (var item2 in l.Skip(i+1))
            {
                m[(item1, item2)] = item1.EuclideanDistance(item2);
            }
        }
        var m2 = m.OrderBy(x => x.Value).ToList();
        var r = new List<HashSet<(double, double, double)>>();
        foreach(var item in m2.Take(1000))
        {
            var fs = r.Where(x => x.Contains(item.Key.Item1) || x.Contains(item.Key.Item2)).ToArray();
            if(fs.Length is 0)
            {
                r.Add(new() { item.Key.Item1, item.Key.Item2 });
            }
            else
            {
                var f = fs[0];
                if (fs.Length == 2)
                {
                    foreach(var it in fs[1])
                    {
                        f.Add(it);
                    }
                    r.Remove(fs[1]);
                }
                f.Add(item.Key.Item1);
                f.Add(item.Key.Item2);
            }
       
        }
        return r.OrderByDescending(x => x.Count).Take(3).Aggregate(1, (p, c) => p * c.Count);
    }
    public int Part2(ReadOnlySpan<char> span)
    {
        var l = new List<(double, double, double)>();
        Span<Range> splits = new Range[3];
        foreach (var line in span.EnumerateLines())
        {
            line.Split(splits, ',');
            l.Add(
                (int.Parse(line[splits[0]]),
                int.Parse(line[splits[1]]),
                int.Parse(line[splits[2]])
                ));
        }
        var m = new Dictionary<((double, double, double), (double, double, double)), double>();
        for (int i = 0; i < l.Count; i++)
        {
            var item1 = l[i];
            foreach (var item2 in l.Skip(i + 1))
            {
                m[(item1, item2)] = item1.EuclideanDistance(item2);
            }
        }
        var m2 = m.OrderBy(x => x.Value).ToList();
        var r = new List<HashSet<(double, double, double)>>();
        foreach (var item in m2)
        {
            var fs = r.Where(x => x.Contains(item.Key.Item1) || x.Contains(item.Key.Item2)).ToArray();
            switch (fs.Length)
            {
                case 0:
                    r.Add(new() { item.Key.Item1, item.Key.Item2 });
                    break;
                case 1:
                    var f = fs[0];
                    f.Add(item.Key.Item1);
                    f.Add(item.Key.Item2);
                    break;
                case 2:
                    var ft = fs[0];
                    foreach (var it in fs[1])
                    {
                        ft.Add(it);
                    }
                    r.Remove(fs[1]);
                    goto case 1;
                default:
                    break;
            }
            l.Remove(item.Key.Item1);
            l.Remove(item.Key.Item2);
            if (l.Count == 0)
                return (int)item.Key.Item1.Item1 * (int)item.Key.Item2.Item1;
        }
        throw new FormatException();
    }
}
