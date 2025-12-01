using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 12, "Garden Groups")]
public class Day12 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var visited = new HashSet<Vector2<int>>(map.Rows * map.Columns);
        var cache = new Stack<Vector2<int>>();
        var sum = 0;
        for (Vector2<int> v = default; v.Y < map.Rows; v.Y++)
        {
            for (v.X = 0; v.X < map.Columns; v.X++)
            {
                if (visited.Add(v))
                {
                    sum += Calculate(map, v, visited, cache);
                }
            }
        }
        return sum;
    }
    static int Calculate(Input2D map, Vector2<int> v, HashSet<Vector2<int>> visited, Stack<Vector2<int>> cache)
    {
        var area = 0;
        var perimeter = 0;
        cache.Push(v);
        var c = map[v];
        while(cache.TryPop(out v))
        {
            area++;
            perimeter += 4;
            foreach(var move in new[] { Vector2<int>.Left, Vector2<int>.Right, Vector2<int>.Down, Vector2<int>.Up })
            {
                var v2 = v + move;
                if (map.IsPointValid(v2) && map[v2] == c)
                {
                    perimeter--;
                    if (visited.Add(v2))
                        cache.Push(v2);
                }
            }
        }
        return area * perimeter;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        span = """
            AAAAAA
            AAABBA
            AAABBA
            ABBAAA
            ABBAAA
            AAAAAA
            """;
        var map = new Input2D(span);
        var visited = new HashSet<Vector2<int>>(map.Rows * map.Columns);
        var sum = 0;
        for (Vector2<int> v = default; v.Y < map.Rows; v.Y++)
        {
            for (v.X = 0; v.X < map.Columns; v.X++)
            {
                if (visited.Add(v))
                {
                    sum += Calculate2(map, v, visited);
                }
            }
        }
        return sum;
    }
    static int Calculate2(Input2D map, Vector2<int> v, HashSet<Vector2<int>> visited)
    {
        throw new NotImplementedException();
        var area = 0;
        var cache = new Stack<Vector2<int>>();
        cache.Push(v);
        var c = map[v];
        var areas = new List<Vector2<int>>();
        while (cache.TryPop(out v))
        {
            areas.Add(v);
            area++;
            var fenceCounter = 0;
            foreach (var move in new[] { Vector2<int>.Left, Vector2<int>.Right, Vector2<int>.Down, Vector2<int>.Up })
            {
                var v2 = v + move;

                if (map.IsPointValid(v2) && map[v2] == c)
                {
                    fenceCounter++;
                    if (visited.Add(v2))
                        cache.Push(v2);
                }
                if (fenceCounter != 4)
                    areas.Add(v);
            }
        }
        var sides = 0;
        Console.WriteLine($"[{c}]  area: {area}, sides: {sides}");
        return area * sides;
    }
}
