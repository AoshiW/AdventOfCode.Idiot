namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 2, "Cube Conundrum")]
public class Day02 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        List<Bag> sets = new();
        var sum = 0;
        foreach (var line in span.EnumerateLines())
        {
            ParseLine(line, out var id, sets);
            if (IsGamePossible(sets))
                sum += id;
        }
        return sum;

        static bool IsGamePossible(List<Bag> sets)
        {
            foreach (var set in sets.AsSpan())
            {
                if (!(set.Red <= 12 && set.Green <= 13 && set.Blue <= 14))
                    return false;
            }
            return true;
        }
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var sum = 0;
        List<Bag> sets = new();
        foreach (var line in span.EnumerateLines())
        {
            ParseLine(line, out _, sets);
            var maxBag = sets[0];
            foreach (var bag in sets.AsSpan().Slice(1))
            {
                maxBag.Blue = int.Max(bag.Blue, maxBag.Blue);
                maxBag.Red = int.Max(bag.Red, maxBag.Red);
                maxBag.Green = int.Max(bag.Green, maxBag.Green);
            }
            sum += maxBag.GetPower();
        }
        return sum;
    }

    static void ParseLine(ReadOnlySpan<char> span, out int id, List<Bag> sets)
    {
        sets.Clear();
        span = span.Slice(5);
        var index = span.IndexOf(':');
        id = int.Parse(span.Slice(0, index));
        span = span.Slice(index + 2);
        foreach (var set in span.EnumerateSlices(";"))
        {
            var bag = new Bag();
            foreach (var cub in set.Trim().EnumerateSlices(","))
            {
                var s = cub.Trim();
                index = s.IndexOf(' ');
                var count = int.Parse(s.Slice(0, index));
                switch (s.Slice(index + 1))
                {
                    case "red":
                        bag.Red = count;
                        break;
                    case "blue":
                        bag.Blue = count;
                        break;
                    case "green":
                        bag.Green = count;
                        break;
                }
            }
            sets.Add(bag);
        }
    }

    struct Bag
    {
        public int Red, Green, Blue;

        public readonly int GetPower() => Red * Green * Blue;

        public readonly override string ToString()
        {
            return $"R:{Red} G:{Green} B:{Blue}";
        }
    }
}
