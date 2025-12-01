namespace AdventOfCode.Puzzles.Y2022;

[AocPuzzle(2022, 1, "Calorie Counting")]
public class Day01 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span) => TopTotalCalories(span, 1);

    public int Part2(ReadOnlySpan<char> span) => TopTotalCalories(span, 3);

    static int TopTotalCalories(ReadOnlySpan<char> span, int count)
    {
        Span<int> maxCalories = stackalloc int[count];
        var calories = 0;
        foreach (var item in span.EnumerateLines())
        {
            if (item.IsEmpty)
            {
                if (calories > maxCalories[0])
                {
                    maxCalories[0] = calories;
                    maxCalories.Sort();
                }
                calories = 0;
            }
            else
            {
                calories += int.Parse(item);
            }
        }
        return maxCalories.Sum();
    }
}
