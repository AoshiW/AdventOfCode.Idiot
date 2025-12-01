using System.Diagnostics;
using System.Numerics;

namespace AdventOfCode.Puzzles;

public readonly struct Range<T> : IEquatable<Range<T>> where T : struct, INumber<T>
{
    public T From { get; }
    public T To { get; }

    public Range(T from, T to)
    {
        Debug.Assert(from <= to);
        From = from;
        To = to;
    }

    public bool Overlaps(Range<T> other)
    {
        return From <= other.To && other.From <= To;
    }

    public bool Overlaps(Range<T> other, out Range<T> overlap)
    {
        if (Overlaps(other))
        {
            var from = T.Max(From, other.From);
            var to = T.Min(To, other.To);
            overlap = new(from, to);
            return true;
        }
        else
        {
            overlap = default;
            return false;
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Range<T> range && Equals(range);
    }

    /// <inheritdoc/>
    public bool Equals(Range<T> other)
    {
        return EqualityComparer<T>.Default.Equals(From, other.From) &&
               EqualityComparer<T>.Default.Equals(To, other.To);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(From, To);
    }

    /// <inheritdoc/>
    public static bool operator ==(Range<T> left, Range<T> right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc/>
    public static bool operator !=(Range<T> left, Range<T> right)
    {
        return !(left == right);
    }
    public override string ToString()
    {
        return $"{From}-{To}";
    }
}
