namespace AdventOfCode.Puzzles.Y2017;

public partial class Day19
{
    [Flags]
    enum States
    {
        Up = 1, Down = 2, Left = 4, Right = 8,
        Horizontal = 16, Vertical = 32,
        VerticalUp = Vertical | Up,
        VerticalDown = Vertical | Down,
        HorizontalLeft = Horizontal | Left,
        HorizontalRight = Horizontal | Right,
    }
}
