namespace AdventOfCode.Puzzles.Y2022;

[AocPuzzle(2022, 20, "Grove Positioning System")]
public class Day20 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var input = ParseInput(span);
        var buffer = new (long, bool)[input.Count];
        Mix(input, buffer);
        var _1000 = input.Find((0, true))!;
        for (var i = 0; i < 1000; i++)
            _1000 = _1000.NextCircleNode();
        var _2000 = _1000;
        for (var i = 0; i < 1000; i++)
            _2000 = _2000.NextCircleNode();
        var _3000 = _2000;
        for (var i = 0; i < 1000; i++)
            _3000 = _3000.NextCircleNode();
        return _1000.Value.Item1 + _2000.Value.Item1 + _3000.Value.Item1;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        span = """
            1
            2
            -3
            3
            -2
            0
            4
            """;
        var input = ParseInput(span);
        var next = input.First;
        do
        {
            next!.ValueRef.Item1 *= 811589153;
        }
        while ((next = next!.Next) is not null);
        var buffer = new (long, bool)[input.Count];
        for (int i = 0; i < 4; i++)
        {
            Mix(input, buffer);
            Mix2(input, buffer);
        }
        var _1000 = input.Find((0, true));
        for (var i = 0; i < 1000; i++)
            _1000 = _1000.NextCircleNode();
        var _2000 = _1000;
        for (var i = 0; i < 1000; i++)
            _2000 = _2000.NextCircleNode();
        var _3000 = _2000;
        for (var i = 0; i < 1000; i++)
            _3000 = _3000.NextCircleNode();
        return _1000.Value.Item1 + _2000.Value.Item1 + _3000.Value.Item1;
    }

    static LinkedList<(long, bool)> ParseInput(ReadOnlySpan<char> span)
    {
        var list = new LinkedList<(long, bool)>();
        foreach (var line in span.EnumerateLines())
        {
            list.AddLast((long.Parse(line), false));
        }
        return list;
    }

    static void Mix(LinkedList<(long Value, bool IsMoved)> data, (long, bool)[] buffer)
    {
        data.CopyTo(buffer, 0);
        foreach (var line in buffer)
        {
            var item = data.Find(line)!;
            item.ValueRef.IsMoved = true;
            var moveCount = long.Abs(item.Value.Value % buffer.Length);
            if (moveCount == 0)
                continue;
            var next = item;
            if (item.Value.Value > 0)
                for (var i = 0; i < moveCount; i++)
                    next = next.NextCircleNode();
            else
            {
                next = next.PreviousCircleNode();
                for (var i = 0; i < moveCount; i++)
                    next = next.PreviousCircleNode();
            }
            data.Remove(item);
            data.AddAfter(next, item);
        }
    }

    static void Mix2(LinkedList<(long Value, bool IsMoved)> data, (long, bool)[] buffer)
    {
        data.CopyTo(buffer, 0);
        foreach (var line in buffer)
        {
            var item = data.Find((line.Item1, true))!;
            item.ValueRef.Item2 = false;
            var moveCount = long.Abs(item.Value.Item1 % data.Count);
            if (moveCount == 0)
                continue;
            var next = item;
            if (item.Value.Item1 > 0)
                for (var i = 0; i < moveCount; i++)
                    next = next.NextCircleNode();
            else
            {
                next = next.PreviousCircleNode();
                for (var i = 0; i < moveCount; i++)
                    next = next.PreviousCircleNode();
            }
            data.Remove(item);
            data.AddAfter(next, item);
        }
    }
    record Data(int idx, long num);

    public object PartOne(string input) =>
         GetGrooveCoordinates(Mix(Parse(input, 1)));

    public object PartTwo(string input)
    {
        var data = Parse(input, 811589153L);
        for (var i = 0; i < 10; i++)
        {
            data = Mix(data);
        }
        return GetGrooveCoordinates(data);
    }

    List<Data> Parse(string input, long m) =>
        input
            .Split("\n")
            .Select((line, idx) => new Data(idx, long.Parse(line) * m))
            .ToList();

    List<Data> Mix(List<Data> numsWithIdx)
    {
        var mod = numsWithIdx.Count - 1;
        for (var idx = 0; idx < numsWithIdx.Count; idx++)
        {
            var srcIdx = numsWithIdx.FindIndex(x => x.idx == idx);
            var num = numsWithIdx[srcIdx];

            var dstIdx = (srcIdx + num.num) % mod;
            if (dstIdx < 0)
            {
                dstIdx += mod;
            }

            numsWithIdx.RemoveAt(srcIdx);
            numsWithIdx.Insert((int)dstIdx, num);
        }
        return numsWithIdx;
    }

    long GetGrooveCoordinates(List<Data> numsWithIdx)
    {
        var idx = numsWithIdx.FindIndex(x => x.num == 0);
        return (
            numsWithIdx[(idx + 1000) % numsWithIdx.Count].num +
            numsWithIdx[(idx + 2000) % numsWithIdx.Count].num +
            numsWithIdx[(idx + 3000) % numsWithIdx.Count].num
        );
    }
}
