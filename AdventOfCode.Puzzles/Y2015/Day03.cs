using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 3, "Perfectly Spherical Houses in a Vacuum")]
public class Day03 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var houses = new HashSet<Vector2<int>>();
        var santa = Vector2<int>.Zero;
        foreach (var item in span)
        {
            santa += item switch
            {
                '>' => Vector2<int>.Right,
                '<' => Vector2<int>.Left,
                '^' => Vector2<int>.Up,
                'v' => Vector2<int>.Down,
                _ => throw new ArgumentException(null, nameof(span))
            };
            houses.Add(santa);
        }
        return houses.Count;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var houses = new HashSet<Vector2<int>>();
        var santa = Vector2<int>.Zero;
        var roboSanta = Vector2<int>.Zero;
        bool isRoboSanta = false;
        foreach (var item in span)
        {
            ref var currentSanta = ref isRoboSanta ? ref roboSanta : ref santa;
            currentSanta += item switch
            {
                '>' => Vector2<int>.Right,
                '<' => Vector2<int>.Left,
                '^' => Vector2<int>.Up,
                'v' => Vector2<int>.Down,
                _ => throw new ArgumentException(null, nameof(span))
            };
            houses.Add(currentSanta);
            isRoboSanta = !isRoboSanta;
        }
        return houses.Count;
    }
}
