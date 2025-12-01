namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 9, "Disk Fragmenter")]
public class Day09 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        bool isEmpty = false;
        var l = 0;
        foreach (char c in span)
        {
            l += c - '0';
        }
        Span<int>buff= new int[l];
        var buffSlice = buff;
        var id = 0;
        foreach(char c in span)
        {
            var num = c - '0';
            if (isEmpty)
            {
                buffSlice.Slice(0, num).Fill(-1);
            }
            else
            {
                buffSlice.Slice(0, num).Fill(id++);
            }
            buffSlice = buffSlice.Slice(num);
            isEmpty = !isEmpty;

        }
        var ii = -1;
        foreach(ref int c in buff)
        {
            ii++;
            if (c is -1)
            {
                var i = buff.LastIndexOfAnyExcept(-1);
                if (i is not -1 && ii<=i)
                {
                    c = buff[i];
                    buff[i] = -1;

                }
            }
        }
        long sum = 0;
        for (int i = 0; i < buff.Length; i++)
        {
            if (buff[i] is not -1)
                sum += buff[i] * i;
        }
        return sum;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var l = 0;
        foreach (char c in span)
        {
            l += c - '0';
        }
        Span<int> buff = new int[l];
        var buffSlice = buff;
        var id = 0;
        bool isEmpty = false;
        foreach (char c in span)
        {
            var num = c - '0';
            if (isEmpty)
            {
                buffSlice.Slice(0, num).Fill(-1);
            }
            else
            {
                buffSlice.Slice(0, num).Fill(id++);
            }
            buffSlice = buffSlice.Slice(num);
            isEmpty = !isEmpty;

        }
        var res = buff;
        for (int i = buff.Length - 1; i >= 0; i--)
        {
            buff = buff.Slice(0, i+1);
            if (buff[i] is -1)
                continue;
            if (buff[i] is 2)
                ;
            var start = buff.Slice(0,i).LastIndexOfAnyExcept([buff[i]]);
            var sp = buff.Slice(1+start, i - start);
            Span<int> need = stackalloc int[sp.Length];
            need.Fill(-1);
            var nI = buff.IndexOf(need);
            if(nI is -1)
            {
                var newStart = buff.Slice(0, i).LastIndexOfAnyExcept(buff[i]);
                i = newStart+1;
            }
            else
            {
                sp.CopyTo(buff.Slice(nI));
                sp.Fill(-1);
            }

        }
        long sum = 0;
        for (int i = 0; i < res.Length; i++)
        {
            if (res[i] is not -1)
                sum += res[i] * i;
        }
        return sum;
    }
}
