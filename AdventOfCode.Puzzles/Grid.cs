using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles;

static class Grid
{
    public static Vector2<int>[] Offset8 { get; } = [
        Vector2<int>.Left + Vector2<int>.Up,   Vector2<int>.Up,   Vector2<int>.Right+ Vector2<int>.Up,
        Vector2<int>.Left, /*                                  */ Vector2<int>.Right,
        Vector2<int>.Left + Vector2<int>.Down, Vector2<int>.Down, Vector2<int>.Right + Vector2<int>.Down
        ];

    public static Vector2<int>[] Offset4 { get; } = [Vector2<int>.Up, Vector2<int>.Right, Vector2<int>.Down, Vector2<int>.Left];
}