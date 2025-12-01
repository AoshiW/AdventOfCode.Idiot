namespace AdventOfCode.Puzzles;

[AttributeUsage(AttributeTargets.All, Inherited = false)]
public sealed class AocPuzzleAttribute : Attribute
{
    public int Year { get; }
    public int Day { get; }
    public string Title { get; }

    public AocPuzzleAttribute(int year, int day, string title)
    {
        Year = year;
        Day = day;
        Title = title;
    }
}

public interface IDay<T> : IDay
{
    T Part1(ReadOnlySpan<char> span);
    T Part2(ReadOnlySpan<char> span);

    object? IDay.Part1(ReadOnlySpan<char> span) => Part1(span);
    object? IDay.Part2(ReadOnlySpan<char> span) => Part2(span);
}
public interface IDay
{
    object? Part1(ReadOnlySpan<char> span);
    object? Part2(ReadOnlySpan<char> span);
}
