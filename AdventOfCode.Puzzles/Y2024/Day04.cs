using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 4, "Ceres Search")]
public class Day04 : IDay<int>
{
    static readonly Vector2<int> UpRight = Vector2<int>.Up + Vector2<int>.Right;
    static readonly Vector2<int> UpLeft = Vector2<int>.Up + Vector2<int>.Left;
    static readonly Vector2<int> DownRight = Vector2<int>.Down + Vector2<int>.Right;
    static readonly Vector2<int> DownLeft = Vector2<int>.Down + Vector2<int>.Left;

    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var iRaw = 0;
        var xmasCount = 0;
        ReadOnlySpan<Vector2<int>> directions = [
            Vector2<int>.Up,
            Vector2<int>.Down,
            Vector2<int>.Right,
            Vector2<int>.Left,
            UpRight,
            UpLeft,
            DownRight,
            DownLeft,
            ];

        while (span.Slice(iRaw).IndexOf('X') is int i && i is not -1)
        {
            iRaw += i;
            var point = map.GetPointFromRawIndex(iRaw++);
            foreach (var direction in directions)
            {
                if (IsXmas(map, point, direction))
                    xmasCount++;
            }
        }
        return xmasCount;

        static bool IsXmas(Input2D map, Vector2<int> point, Vector2<int> direction)
        {
            ReadOnlySpan<char> xmas = "MAS";
            foreach (var item in xmas)
            {
                point += direction;
                if (map.IsPointValid(point) && map[point] == item) { }
                else return false;
            }
            return true;

        }
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var iRaw = 0;
        var xmasCount = 0;
        while (span.Slice(iRaw).IndexOf('A') is int i && i is not -1)
        {
            iRaw += i;
            var point = map.GetPointFromRawIndex(iRaw++);
            if (point.X is 0 || point.Y is 0 || point.X == map.Columns - 1 || point.Y == map.Rows - 1)
                continue;
            if ((map[point + UpRight] is 'M' && map[point + DownLeft] is 'S') || (map[point + UpRight] is 'S' && map[point + DownLeft] is 'M'))
            {
                if ((map[point + DownRight] is 'M' && map[point + UpLeft] is 'S') || (map[point + DownRight] is 'S' && map[point + UpLeft] is 'M'))
                {
                    xmasCount++;
                }
            }
        }
        return xmasCount;
    }
}
