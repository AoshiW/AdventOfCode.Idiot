using AdventOfCode.Puzzles.Numerics;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Puzzles;

public readonly ref struct Input2D
{
    private readonly ReadOnlySpan<char> _span;
    private readonly int _trueRowLength;

    public int Rows { get; }
    public int Columns { get; }

    public static Input2D Parse(ReadOnlySpan<char> span) 
        => new(span);

    public Input2D(ReadOnlySpan<char> span)
    {
        _span = span;
        _trueRowLength = _span.IndexOf('\n');
        Columns = _trueRowLength;
        if (_span[Columns - 1] == '\r')
        {
            Columns--;
        }
        Rows = _span.Length / _trueRowLength;
        _trueRowLength++;
    }

    public ref readonly char this[int x, int y]
    {
        get
        {
            return ref _span[y * _trueRowLength + x];
        }
    }

    public ref readonly char this[Vector2<int> point]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref this[point.X, point.Y];
        }
    }

    public bool IsPointValid(Vector2<int> point)
    {
        return
            point.X >= 0 && point.X < Columns &&
            point.Y >= 0 && point.Y < Rows;
    }

    public bool TryGet(Vector2<int> point, out char c)
    {
        if (IsPointValid(point))
        {
            c = this[point];
            return true;
        }
        c = default;
        return false;
    }

    public ReadOnlySpan<char> GetRow(int y) => _span.Slice(y * _trueRowLength, Columns);

    public Vector2<int> GetPointFromRawIndex(int rawIndex)
    {
        var result = int.DivRem(rawIndex, _trueRowLength);
        return new Vector2<int>(result.Remainder, result.Quotient);
    }
}