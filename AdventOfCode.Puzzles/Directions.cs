namespace AdventOfCode.Puzzles;

[Flags]
public enum Directions
{
    None,

    /// <summary>
    /// North
    /// </summary>
    Up = 1,

    /// <summary>
    /// South
    /// </summary>
    Down = 2,

    /// <summary>
    /// West
    /// </summary>
    Left = 4,

    /// <summary>
    /// East
    /// </summary>
    Right = 8,

    All = Up | Down | Right | Left
}
