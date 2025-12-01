namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 22, "Sand Slabs")]
public class Day22 : IDay<int>
{
    Dictionary<int, (List<int> Upper, List<int> Lower)> ParseInput(ReadOnlySpan<char> span)
    {
        var bricks = new List<(Brick Point, int Id)>();
        int id = 0;
        Span<Range> splits = stackalloc Range[6];
        foreach (var item in span.EnumerateLines())
        {
            item.SplitAny(splits, ",~", StringSplitOptions.RemoveEmptyEntries);
            var x = new Range<int>(int.Parse(item[splits[0]]), int.Parse(item[splits[3]]));
            var y = new Range<int>(int.Parse(item[splits[1]]), int.Parse(item[splits[4]]));
            var z = new Range<int>(int.Parse(item[splits[2]]), int.Parse(item[splits[5]]));
            bricks.Add((new() { X = x, Y = y, Z = z }, id++));
        }
        bricks.Sort((l, r) => l.Point.Z.From.CompareTo(r.Point.Z.From));

        var brickSpan = bricks.AsSpan();
        var word = new HashSet<(Range<int>, Range<int>, int)>(bricks.Count, EC.Default);
        var wordItem = new List<(Range<int>, Range<int>, int)>();
        foreach (ref var item in brickSpan)
        {
            var tempItem = item.Point;
            Set(tempItem, wordItem);
            while (!word.Overlaps(wordItem))
            //while (!FastOverlaps(word, wordItem))
                {
                if (item.Point.Z.From == 1)
                    break;
                item.Point = tempItem;
                tempItem = item.Point with
                {
                    Z = new(item.Point.Z.From - 1, item.Point.Z.To - 1)
                };
                Set(tempItem, wordItem);
            }
            Set(item.Point, wordItem);
            word.UnionWith(wordItem);
        }
        static bool FastOverlaps<T>( HashSet<T> hashset, List<T> t)
        {
            foreach(var item in t)
            {
            }
            return false;
        }
        static void Set(Brick brick, List<(Range<int> X, Range<int> Y, int Z)> cache)
        {
            cache.Clear();
            for (int z = brick.Z.From; z <= brick.Z.To; z++)
            {
                cache.Add((brick.X, brick.Y, z));
            }
        }

        bricks.Sort((l, r) => l.Point.Z.From.CompareTo(r.Point.Z.From));
        var map = bricks.ToDictionary(x => x.Id, x => (new List<int>(), new List<int>()));

        var upperBricksCache = new List<(Brick Point, int Id)>();
        foreach (var brick in bricks)
        {
            var brickMap = map[brick.Id];
            upperBricksCache.Clear();
            IsUnder(brick, bricks.AsSpan(), upperBricksCache);
            foreach (var upper in upperBricksCache.AsSpan())
            {
                brickMap.Item1.Add(upper.Id);
                map[upper.Id].Item2.Add(brick.Id);
            }
        }
        return map;
    }

    public int Part1(ReadOnlySpan<char> span)
    {
        var bricks = ParseInput(span);
        var removeCount = 0;
        foreach (var brick in bricks)
        {
            if (CanRemove(brick.Key, bricks))
                removeCount++;
        }
        return removeCount;

        static bool CanRemove(int id, Dictionary<int, (List<int> Upper, List<int> Lower)> bricks)
        {
            var item = bricks[id];
            foreach (var upper in item.Upper.AsSpan())
            {
                if (!Any())
                    return false;

                bool Any()
                {
                    foreach (var lower in bricks[upper].Lower.AsSpan())
                    {
                        if (lower != id)
                            return true;
                    }
                    return false;
                }
            }
            return true;

        }
    }

    static bool IsUnder((Brick Point, int Id) brick, ReadOnlySpan<(Brick Point, int Id)> bricks, List<(Brick Point, int Id)> upperBricks)
    {
        var z = brick.Point.Z.To + 1;
        foreach (var item in bricks)
        {
            if (item.Point.Z.From == z)
            {
                if (item.Point.X.Overlaps(brick.Point.X))
                {
                    if (item.Point.Y.Overlaps(brick.Point.Y))
                    {
                        upperBricks.Add(item);
                    }
                }
            }
        }
        return upperBricks.Count > 0;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var bricks = ParseInput(span);
        var sum = 0;
        var hs = new HashSet<int>();
        var inQ = new Queue<int>();
        var outQ = new Queue<int>();
        foreach (var brick in bricks)
        {
            sum += hs.Count;
            hs.Clear();
            var mapItem = bricks[brick.Key];
            foreach (var u in mapItem.Upper)
            {
                outQ.Enqueue(u);
            }

            hs.Add(brick.Key);
            while (outQ.Count > 0)
            {
                (outQ, inQ) = (inQ, outQ);
                while (inQ.TryDequeue(out var item))
                {
                    if (bricks[item].Lower.All(hs.Contains))
                    {
                        if (hs.Add(item))
                        {
                            foreach (var u in bricks[item].Upper)
                            {
                                outQ.Enqueue(u);
                            }
                        }
                    }
                }
            }
            hs.Remove(brick.Key);
        }
        return sum;
    }

    class EC : IEqualityComparer<(Range<int> X, Range<int> Y, int Z)>
    {
        static EC? _default;
        public static EC Default => _default ??= new();
        public bool Equals((Range<int> X, Range<int> Y, int Z) x, (Range<int> X, Range<int> Y, int Z) y)
        {
            return x.Z == y.Z &&
                x.X.Overlaps(y.X) &&
                x.Y.Overlaps(y.Y);
        }

        public int GetHashCode((Range<int> X, Range<int> Y, int Z) obj)
        {
            return obj.Z;
        }
    }

    struct Brick
    {
        public Range<int> X, Y, Z;
    }

}
