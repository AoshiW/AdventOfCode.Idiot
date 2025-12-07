namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 6, "Trash Compactor")]
public class Day06 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var ops = map.GetRow(map.Rows - 1);
        var lasi = ops.Length;
        long sum = 0;
        while (!ops.IsEmpty)
        {
            var i = ops.LastIndexOfAnyExcept(' ');
            var l = lasi - i;
            long n = ops[i] is '+' ? 0 : 1;
            for (int r = 0; r < map.Rows-1; r++)
            {
                var number = long.Parse(map.GetRow(r).Slice(i, l).Trim());
                n = ops[i] is '+'
                    ? n + number
                    : n * number;
            }
            lasi = i;
            ops = i is 0 ? default : ops.Slice(0, i - 1);
            sum += n;
        }
        return sum;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var input = new Input2D(span);
        var index = input.Columns;
        long sum = 0;
        var block = new List<long>();
        while (--index >= 0)
        {
            block.Insert(0, 0);
            for (var j = 0; j < input.Rows-1; j++)
            {
                var c = input[index, j];
                if (c is ' ')
                    continue;
                block[0] = block[0] * 10 + c - '0';
            }

            if (input[index, input.Rows-1] is not ' ')
            {
                var op = input[index, input.Rows - 1];
                Func<long, long, long> fnc = op is '+'
                    ? (x, y) => x + y
                    : (x, y) => x * y;
                long n = op is '+' ? 0 : 1; 
                sum += block.Aggregate(n, fnc);
                index--;
                block.Clear();
            }
        }
        return sum;
    }
}
