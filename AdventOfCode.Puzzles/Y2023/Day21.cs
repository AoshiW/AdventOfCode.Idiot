
using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 21, "Step Counter")]
public class Day21 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var start = map.GetPointFromRawIndex(span.IndexOf('S'));
        var hs = new HashSet<Vector2<int>>();
        var q = new Queue<(Vector2<int> Point, int Steps)>();
        q.Enqueue((start, 0));

        int valid = 0;
        while (q.TryDequeue(out var item))
        {
            if (item.Steps > 64 || !hs.Add(item.Point))
                continue;
            if ((item.Steps & 1) == 0)
                valid++;

            item.Steps++;
            if (map[item.Point + Vector2<int>.Up] == '.')
                q.Enqueue((item.Point + Vector2<int>.Up, item.Steps));

            if (map[item.Point + Vector2<int>.Down] == '.')
                q.Enqueue((item.Point + Vector2<int>.Down, item.Steps));

            if (map[item.Point + Vector2<int>.Right] == '.')
                q.Enqueue((item.Point + Vector2<int>.Right, item.Steps));

            if (map[item.Point + Vector2<int>.Left] == '.')
                q.Enqueue((item.Point + Vector2<int>.Left, item.Steps));
        }
        return valid;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
    }
}
