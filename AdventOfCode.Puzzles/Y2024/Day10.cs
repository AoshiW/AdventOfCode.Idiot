using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 10, "Hoof It")]
public class Day10 : IDay<int>
{
    static readonly Vector2<int>[] Directions = [Vector2<int>.Right, Vector2<int>.Up, Vector2<int>.Down, Vector2<int>.Left];

    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var list = new Queue<Vector2<int>>();
        var rawIndex = 0;
        var index = 0;
        var cache9 = new HashSet<Vector2<int>>();
        var count = 0;
        while((index=span.Slice(rawIndex).IndexOf('0')) != -1)
        {
            rawIndex += index;
            var point = map.GetPointFromRawIndex(rawIndex);
            rawIndex++;
            cache9.Clear();
            Find(map, point, '1', cache9);
            count += cache9.Count;
        }
        return count;
    }

    static void Find(Input2D map, Vector2<int> point, char next, HashSet<Vector2<int>> cache9)
    {
        var sum = 0;
        foreach (var direction in Directions)
        {
            var newPoint = point + direction;
            char nextC;
            if (map.IsPointValid(newPoint) && (nextC=map[newPoint]) == next)
            {
                if (nextC == '9')
                    cache9.Add(newPoint);
                else
                Find(map, newPoint, (char)(next+1), cache9);
            }
        }
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var list = new Queue<Vector2<int>>();
        var rawIndex = 0;
        var index = 0;
        var cache9 = new HashSet<Vector2<int>>();
        var count = 0;
        while ((index = span.Slice(rawIndex).IndexOf('0')) != -1)
        {
            rawIndex += index;
            var point = map.GetPointFromRawIndex(rawIndex);
            rawIndex++;
            count += Find(map, point, '1');
        }
        return count;
    }

    static int Find(Input2D map, Vector2<int> point, char next)
    {
        var sum = 0;
        foreach (var direction in Directions)
        {
            var newPoint = point + direction;
            char nextC;
            if (map.IsPointValid(newPoint) && (nextC = map[newPoint]) == next)
            {
                if (nextC == '9')
                    sum++;
                else
                    sum +=Find(map, newPoint, (char)(next + 1));
            }
        }
        return sum;
    }
}
