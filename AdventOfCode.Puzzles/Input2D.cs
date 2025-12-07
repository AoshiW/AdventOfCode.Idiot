using AdventOfCode.Puzzles.Numerics;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Puzzles;

public class Grid<T>
{
    private T[] _array;

    public Grid(int r, int c)
    {
        _array = new T[r*c];
        Rows = r;
        Columns = c;
    }

    public int Rows { get; }
    public int Columns { get; }

    public bool IsPointValid(Vector2<int> point)
    {
        return
            point.X >= 0 && point.X < Columns &&
            point.Y >= 0 && point.Y < Rows;
    }

    public bool TryGet(Vector2<int> point, out T c)
    {
        if (IsPointValid(point))
        {
            c = this[point];
            return true;
        }
        c = default;
        return false;
    }
    public ref T this[int x, int y]
    {
        get
        {
            return ref _array[y * Columns + x];
        }
    }
    public ref T this[Vector2<int> point]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return ref this[point.X, point.Y];
        }
    }
    public Span<T> GetRow(int y) => _array.AsSpan().Slice(y * Columns, Columns);
}
public readonly ref struct Input2D
{
    private readonly ReadOnlySpan<char> _span;
    private readonly int _trueRowLength;

    public int Rows { get; }
    public int Columns { get; }

    public Grid<char> ToGrid()
    {
        var grid = new Grid<char>(Rows, Columns);
        for (int i = 0; i < Rows; i++)
        {
            var from = GetRow(i);
            var to = grid.GetRow(i);
            from.CopyTo(to);
        }
        return grid;
    }

    public static Input2D Parse(ReadOnlySpan<char> span) 
        => new(span);

    public Input2D(ReadOnlySpan<char> span)
    {
        _span = span;
        _trueRowLength = _span.IndexOf('\n');
        Columns = _trueRowLength;
        var p = 1;
        if (_span[Columns - 1] == '\r')
        {
            p++;
            Columns--;
        }
        _trueRowLength++;
        Rows = (_span.Length + p) / _trueRowLength;
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