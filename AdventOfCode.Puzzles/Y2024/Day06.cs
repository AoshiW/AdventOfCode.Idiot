using AdventOfCode.Puzzles.Numerics;
using System.Diagnostics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 6, "Guard Gallivant")]
public class Day06 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var pos = Vector2<int>.Zero;
        for (pos.X = -1; pos.X == -1; pos.Y++)
        {
            pos.X = map.GetRow(pos.Y).IndexOf('^');
        }
        var dir = 0;
        pos.Y--;
        var hs = new HashSet<Vector2<int>>() { pos };
        while (true)
        {
            var move = dir switch
            {
                0 => Vector2<int>.Up,
                1 => Vector2<int>.Right,
                2 => Vector2<int>.Down,
                3 => Vector2<int>.Left,
                _ => throw new UnreachableException()
            };
            if (!map.IsPointValid(pos + move))
                return hs.Count;

            if (map[pos + move] is '#')
            {
                dir = ++dir % 4;
            }
            else
            {
                pos += move;
                hs.Add(pos);
            }
        }
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
    }
}
