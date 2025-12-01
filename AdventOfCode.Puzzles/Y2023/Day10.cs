using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 10, "Pipe Maze")]
public class Day10 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var hs = GetLoop(map);
        return hs.Count / 2;
    }

    static Directions MapCharTo(char c) => c switch
    {
        '|' => Directions.Up | Directions.Down,
        '-' => Directions.Left | Directions.Right,
        'S' => Directions.All,
        'F' => Directions.Down | Directions.Left,
        'J' => Directions.Up | Directions.Right,
        '7' => Directions.Down | Directions.Right,
        'L' => Directions.Up | Directions.Left,
        '.' => Directions.None,
    };

    static HashSet<Vector2<int>> GetLoop(Input2D map)
    {
        Vector2<int> point = default;
        var direction = Directions.All;
        for (var row = 0; point.Y < map.Rows; row++)
        {
            var col = map.GetRow(row).IndexOf('S');
            if (col != -1)
            {
                point = new(col, row);
                if (col == 0)
                    direction &= ~Directions.Left;
                if (row == 0)
                    direction &= ~Directions.Up;
                if (col == map.Columns - 1)
                    direction &= ~Directions.Right;
                if (row == map.Rows - 1)
                    direction &= ~Directions.Down;
                break;
            }
        }

        var hs = new HashSet<Vector2<int>>();
        while (hs.Add(point))
        {
            Directions newDirection;
            if (direction.HasFlag(Directions.Up) && (newDirection = MapCharTo(map[point - Vector2<int>.Down])).HasFlag(Directions.Down))
            {
                direction = newDirection & ~Directions.Down;
                point -= Vector2<int>.Down;
            }
            else if (direction.HasFlag(Directions.Down) && (newDirection = MapCharTo(map[point -Vector2<int>.Up])).HasFlag(Directions.Up))
            {
                direction = newDirection & ~Directions.Up;
                point -= Vector2<int>.Up;
            }
            else if (direction.HasFlag(Directions.Left) && (newDirection = MapCharTo(map[point + Vector2<int>.Right])).HasFlag(Directions.Right))
            {
                direction = newDirection & ~Directions.Right;
                point += Vector2<int>.Right;
            }
            else if (direction.HasFlag(Directions.Right) && (newDirection = MapCharTo(map[point + Vector2<int>.Left])).HasFlag(Directions.Left))
            {
                direction = newDirection & ~Directions.Left;
                point += Vector2<int>.Left;
            }
        }
        return hs;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var hs = GetLoop(map);

        int inCount = 0;
        for (var point = Vector2<int>.Zero; point.Y < map.Rows; point.Y++)
        {
            var row = map.GetRow(point.Y);
            var isIn = false;
            for (point.X = 0; point.X < map.Columns; point.X++)
            {
                var isLoopEdge = hs.Contains(point);
                if (row[point.X] is 'F' or '7' or '|' && isLoopEdge)
                {
                    isIn = !isIn;
                }
                else if (isIn && !isLoopEdge)
                {
                    inCount++;
                }
            }
        }
        return inCount;
    }
}
