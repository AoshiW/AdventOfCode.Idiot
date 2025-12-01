using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 18, "")]
public class Day18 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        var cache = new HashSet<Vector2<int>>();
        foreach (var line in span.EnumerateLines())
        {
            var index = line.IndexOf(',');
            var x = int.Parse(line.Slice(0, index));
            var y = int.Parse(line.Slice(index + 1));
            cache.Add(new(x, y));
            if (cache.Count == 1024)
                break;
        }
        var next = new Queue<(int,Vector2<int>)>();
        next.Enqueue((0,Vector2<int>.Zero));
        const int edge = 70;
        while (next.TryDequeue(out var point))
        {
            foreach (var dir in new Vector2<int>[] { Vector2<int>.Down, Vector2<int>.Left, Vector2<int>.Right, Vector2<int>.Up })
            {
                var temp = point.Item2 + dir;
                if (temp.X is >= 0 && temp.X < edge+1 && temp.Y is >= 0 && temp.Y < edge+1)
                {
                    if (temp.X == edge && temp.Y == edge)
                        return (point.Item1+1).ToString();
                    if (cache.Add(temp))
                        next.Enqueue((point.Item1 + 1, temp));
                }
            }
        }
        return "";
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
        var wals = new HashSet<Vector2<int>>();
        var line = span;
        int index, x, y;
        var enumerator = span.EnumerateLines();
        while(wals.Count != 1024)
        {
            enumerator.MoveNext();
            line = enumerator.Current;
            index = line.IndexOf(',');
            x = int.Parse(line.Slice(0, index));
            y = int.Parse(line.Slice(index + 1));
            wals.Add(new(x, y));
            if (wals.Count == 1024)
                break;
        }

        enumerator.MoveNext();
        line = enumerator.Current;
        index = line.IndexOf(',');
        x = int.Parse(line.Slice(0, index));
        y = int.Parse(line.Slice(index + 1));
        wals.Add(new(x, y));

        var next = new Queue<(int, Vector2<int>)>();
        var visited = new HashSet<Vector2<int>>();
    start:
        next.Clear();    
        visited.Clear();
        next.Enqueue((0, Vector2<int>.Zero));
        const int edge = 70;
        while (next.TryDequeue(out var point))
        {
            foreach (var dir in new Vector2<int>[] { Vector2<int>.Down, Vector2<int>.Left, Vector2<int>.Right, Vector2<int>.Up })
            {
                var temp = point.Item2 + dir;
                if (temp.X is >= 0 && temp.X < edge + 1 && temp.Y is >= 0 && temp.Y < edge + 1)
                {
                    if (temp.X == edge && temp.Y == edge){

                        goto start;
                    }
                    if (!wals.Contains(temp) && visited.Add(temp))
                        next.Enqueue((point.Item1 + 1, temp));
                }
            }
        }
        return $"{x},{y}";
    }
}
