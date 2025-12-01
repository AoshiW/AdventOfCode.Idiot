using AdventOfCode.Puzzles.Numerics;
using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 16, "The Floor Will Be Lava")]
public class Day16 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var q = new Queue<(Vector2<int>, Directions)>();
        q.Enqueue((new(-1, 0), Directions.Right));

        return FindAll(map, q, new());
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        int max = 0;
        var cache = new Dictionary<Vector2<int>, Directions>();
        var q = new Queue<(Vector2<int>, Directions)>();
        for (int i = 0; i < map.Rows; i++)
        {
            q.Enqueue((new(-1, i), Directions.Right));
            var newMax = FindAll(map, q, cache);
            max = int.Max(max, newMax);
            cache.Clear();

            q.Enqueue((new(map.Columns, i), Directions.Left));
            newMax = FindAll(map, q, cache);
            max = int.Max(max, newMax);
            cache.Clear();
        }
        for (int i = 0; i < map.Columns; i++)
        {
            q.Enqueue((new(i, -1), Directions.Down));
            var newMax = FindAll(map, q, cache);
            max = int.Max(max, newMax);
            cache.Clear();

            q.Enqueue((new(i, map.Rows), Directions.Up));
            newMax = FindAll(map, q, cache);
            max = int.Max(max, newMax);
        }
        return max;
    }

    static int FindAll(Input2D map, Queue<(Vector2<int> Point, Directions Direction)> queue, Dictionary<Vector2<int>, Directions> visitedCache)
    {
        while (queue.TryDequeue(out var item))
        {
            switch (item.Direction)
            {
                case Directions.Left:
                    item.Point.X--;
                    break;
                case Directions.Right:
                    item.Point.X++;
                    break;
                case Directions.Up:
                    item.Point.Y--;
                    break;
                case Directions.Down:
                    item.Point.Y++;
                    break;
            }
            if (!map.IsPointValid(item.Point))
            {
                continue;
            }
            switch (map[item.Point])
            {
                case '.':
                case '-' when item.Direction is Directions.Left or Directions.Right:
                case '|' when item.Direction is Directions.Up or Directions.Down:
                    Add(item, visitedCache, queue);
                    break;
                case '-':
                    Add((item.Point, Directions.Left), visitedCache, queue);
                    Add((item.Point, Directions.Right), visitedCache, queue);
                    break;
                case '|':
                    Add((item.Point, Directions.Up), visitedCache, queue);
                    Add((item.Point, Directions.Down), visitedCache, queue);
                    break;
                case '\\':
                    var mewDirection1 = item.Direction switch
                    {
                        Directions.Up => Directions.Left,
                        Directions.Down => Directions.Right,
                        Directions.Left => Directions.Up,
                        Directions.Right => Directions.Down,
                    };
                    Add((item.Point, mewDirection1), visitedCache, queue);
                    break;
                case '/':
                    var mewDirection = item.Direction switch
                    {
                        Directions.Up => Directions.Right,
                        Directions.Down => Directions.Left,
                        Directions.Left => Directions.Down,
                        Directions.Right => Directions.Up,
                    };
                    Add((item.Point, mewDirection), visitedCache, queue);
                    break;
            }
        }
        return visitedCache.Count;

        static void Add((Vector2<int> Point, Directions Direction) item, Dictionary<Vector2<int>, Directions> visitedCache, Queue<(Vector2<int> Point, Directions Direction)> queue)
        {
            ref var directions = ref CollectionsMarshal.GetValueRefOrAddDefault(visitedCache, item.Point, out _);
            if (!directions.HasFlag(item.Direction))
            {
                directions |= item.Direction;
                queue.Enqueue(item);
            }
        }
    }
}
