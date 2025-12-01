using System.Numerics;

namespace AdventOfCode.Puzzles.Numerics;

public struct Vector2<T> : ISpanFormattable, IEquatable<Vector2<T>>,
    IAdditionOperators<Vector2<T>, Vector2<T>, Vector2<T>>, ISubtractionOperators<Vector2<T>, Vector2<T>, Vector2<T>>, IMultiplyOperators<Vector2<T>, T, Vector2<T>>, IDivisionOperators<Vector2<T>, T, Vector2<T>>,
    IEqualityOperators<Vector2<T>, Vector2<T>, bool>
    where T : INumber<T>
{
    public T X { get; set; }
    public T Y { get; set; }

    public Vector2(T x, T y)
    {
        X = x;
        Y = y;
    }

    public static readonly Vector2<T> Zero = new(T.Zero, T.Zero);

    public static readonly Vector2<T> Up = new(T.Zero, -T.One);
    public static readonly Vector2<T> Down = new(T.Zero, T.One);

    public static readonly Vector2<T> Left = new(-T.One, T.Zero);
    public static readonly Vector2<T> Right = new(T.One, T.Zero);

    /// <inheritdoc/>
    public static Vector2<T> operator +(Vector2<T> left, Vector2<T> right)
    {
        return new Vector2<T>(left.X + right.X, left.Y + right.Y);
    }

    /// <inheritdoc/>
    public static Vector2<T> operator -(Vector2<T> left, Vector2<T> right)
    {
        return new Vector2<T>(left.X - right.X, left.Y - right.Y);
    }

    /// <inheritdoc/>
    public static Vector2<T> operator *(Vector2<T> left, T right)
    {
        return new Vector2<T>(left.X * right, left.Y * right);
    }

    /// <inheritdoc/>
    public static Vector2<T> operator /(Vector2<T> left, T right)
    {
        return new Vector2<T>(left.X / right, left.Y / right);
    }

    /// <inheritdoc/>
    public static bool operator ==(Vector2<T> left, Vector2<T> right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc/>
    public static bool operator !=(Vector2<T> left, Vector2<T> right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        return ToString(null, null);
    }

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"{X},{Y}";
    }

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return destination.TryWrite(provider, $"{X},{Y}", out charsWritten);
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj)
    {
        return obj is Vector2<T> vector && Equals(vector);
    }

    /// <inheritdoc/>
    public readonly bool Equals(Vector2<T> other)
    {
        return EqualityComparer<T>.Default.Equals(X, other.X) &&
               EqualityComparer<T>.Default.Equals(Y, other.Y);
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
