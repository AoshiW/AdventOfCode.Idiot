namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 6, "Trash Compactor")]
public class Day06 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var list = new List<List<long>>();
        var enume = span.EnumerateLines();
        while (enume.MoveNext())
        {
            if (enume.Current[0] is '+' or '*')
                break;
            var l = new List<long>(list.Count > 0 ? list[0].Count : 0);

            foreach(var item in enume.Current.Split(' '))
            {
                var num = enume.Current[item];
                if (!num.IsEmpty)
                    l.Add(long.Parse(num));
            }
            list.Add(l);
        }
        var ops = new List<char>(list[0].Count);
        foreach(var c in enume.Current)
        {
            if (c is not ' ')
                ops.Add(c);
        }
        long sum = 0;
        for (var i = 0; i < ops.Count; i++)
        {
            Func<long, long, long> fnc = ops[i] is '+'
                ? (x, y) => x + y
                : (x, y) => x * y;
            long n = ops[i] is '+' ? 0 : 1;
            for (var j = 0; j < list.Count; j++)
            {
                n = fnc(n, list[j][i]);
            }
            sum += n;
        }
        return sum;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var input = new Input2D(span);
        var index = input.Columns;
        long sum = 0;
        var arrr = new List<long>();
        while (--index >= 0)
        {
            arrr.Insert(0, 0);
            for (var j = 0; j < input.Rows-1; j++)
            {
                var c = input[index, j];
                if (c is ' ')
                    continue;
                arrr[0] = arrr[0] * 10 + c - '0';
            }

            if (input[index, input.Rows-1] is not ' ')
            {
                var op = input[index, input.Rows - 1];
                Func<long, long, long> fnc = op is '+'
                    ? (x, y) => x + y
                    : (x, y) => x * y;
                long n = op is '+' ? 0 : 1; 
                sum += arrr.Aggregate(n, fnc);
                index--;
                arrr.Clear();
            }
        }
        return sum;
    }
}
