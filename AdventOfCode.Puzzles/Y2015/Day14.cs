using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 14, "Reindeer Olympics")]
public class Day14 : IDay<int>
{
    const int TimeLimit = 2503;

    public int Part1(ReadOnlySpan<char> span)
    {
        int max = 0;
        foreach (var line in span.EnumerateLines())
        {
            var reindeer = Reindeer.Parse(line);
            var periodTime = reindeer.FlyTime + reindeer.RestTime;
            var countPeriod = TimeLimit / periodTime;
            var modulo = TimeLimit % periodTime;
            if (modulo > reindeer.FlyTime)
                modulo = reindeer.FlyTime;
            reindeer.Points = reindeer.Speed * (countPeriod * reindeer.FlyTime + modulo);
            if (reindeer.Points > max)
                max = reindeer.Points;
        }
        return max;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var data = new List<Reindeer>();
        foreach (var line in span.EnumerateLines())
        {
            data.Add(Reindeer.Parse(line));
        }
        var dataSpan = CollectionsMarshal.AsSpan(data);
        int maxDistance = 0;
        for (int i = 0; i < TimeLimit; i++)
        {
            foreach (ref var item in dataSpan)
            {
                if (i % (item.FlyTime + item.RestTime) < item.FlyTime)
                {
                    item.DistanceTraveled += item.Speed;
                    if (item.DistanceTraveled > maxDistance)
                    {
                        maxDistance = item.DistanceTraveled;
                    }
                }
            }
            foreach (ref var item in dataSpan)
            {
                if (item.DistanceTraveled == maxDistance)
                {
                    item.Points++;
                }
            }
        }
        var maxPoints = 0;
        foreach (var item in dataSpan)
        {
            if (item.Points > maxPoints)
            {
                maxPoints = item.Points;
            }
        }
        return maxPoints;
    }
}

file struct Reindeer
{
    public static Reindeer Parse (ReadOnlySpan<char> span)
    {
        var reindeer = new Reindeer();
        span = span.Slice(span.IndexOf(" fly ") + 5);
        reindeer.Speed = int.Parse(span.Slice(0, span.IndexOf(' ')));
        span = span.Slice(span.IndexOf(" for ") + 5);
        reindeer.FlyTime = int.Parse(span.Slice(0, span.IndexOf(' ')));
        int i = span.LastIndexOf(" for ") + 5;
        reindeer.RestTime = int.Parse(span.Slice(i, span.LastIndexOf(' ') - i));
        return reindeer;
    }

    public int Speed, FlyTime, RestTime,
        Points,
        DistanceTraveled;
}
