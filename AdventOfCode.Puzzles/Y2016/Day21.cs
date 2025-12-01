namespace AdventOfCode.Puzzles.Y2016;

[AocPuzzle(2016, 21, "Scrambled Letters and Hash")]
public class Day21 : IDay<string>
{
    const string tr = "abcdefgh";
    const string tr2 = "fbgdceah";

    public string Part1(ReadOnlySpan<char> span)
    {
        Span<char> ou = stackalloc char[tr.Length];
        Part1(span, tr, ou);
        return ou.ToString();
        Span<char> text = stackalloc char[tr.Length];
        Span<char> textCache = stackalloc char[tr.Length];
        tr.CopyTo(text);
        foreach (var item in span.EnumerateLines())
        {
            var enumerator = item.EnumerateSlices(" ");
            enumerator.MoveNext();
            if (enumerator.Current.Equals("swap", StringComparison.OrdinalIgnoreCase))
            {
                enumerator.MoveNext();
                if (enumerator.Current[0] == 'p')
                {
                    enumerator.MoveNext();
                    var p1 = int.Parse(enumerator.Current);
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    var p2 = int.Parse(enumerator.Current);
                    var temp = text[p1];
                    text[p1] = text[p2];
                    text[p2] = temp;
                }
                else
                {
                    enumerator.MoveNext();
                    var c1 = enumerator.Current[0];
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    var c2 = enumerator.Current[0];
                    foreach (ref var c in text)
                    {
                        if (c == c1)
                            c = c2;
                        else if (c == c2)
                            c = c1;
                    }
                }
            }
            else if (enumerator.Current.Equals("move", StringComparison.OrdinalIgnoreCase))
            {
                enumerator.MoveNext();
                enumerator.MoveNext();
                var from = int.Parse(enumerator.Current);
                enumerator.MoveNext();
                enumerator.MoveNext();
                enumerator.MoveNext();
                var to = int.Parse(enumerator.Current);
                var temp = text[from];
                if (from < to)
                {
                    while (from != to)
                    {
                        text[from] = text[from + 1];
                        from++;
                    }
                }
                else
                {
                    while (from != to)
                    {
                        text[from] = text[from - 1];
                        from--;
                    }
                }
                text[from] = temp;
            }
            else if (enumerator.Current.Equals("rotate", StringComparison.OrdinalIgnoreCase))
            {
                enumerator.MoveNext();
                int num;
                if (enumerator.Current[0] == 'l')
                {
                    enumerator.MoveNext();
                    var l = int.Parse(enumerator.Current);
                    num = tr.Length - l;
                }
                else if (enumerator.Current[0] == 'r')
                {
                    enumerator.MoveNext();
                    num = int.Parse(enumerator.Current);
                }
                else
                {
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    num = 1 + text.IndexOf(enumerator.Current[0]);
                    if (num > 4)
                        num++;
                    num %= text.Length;
                }
                text.Slice(text.Length - num).CopyTo(textCache);
                for (int i = text.Length - 1; i >= num; i--)
                {
                    text[i] = text[i - num];
                }
                textCache.Slice(0, num).CopyTo(text);
            }
            else
            {
                enumerator.MoveNext();
                enumerator.MoveNext();
                var from = int.Parse(enumerator.Current);
                enumerator.MoveNext();
                enumerator.MoveNext();
                var to = int.Parse(enumerator.Current);
                text.Slice(from, to - from + 1).Reverse();
            }
        }
        return text.ToString();
    }
    public void Part1(ReadOnlySpan<char> span, ReadOnlySpan<char> input, Span<char> textCache)
    {
        Span<char> text = stackalloc char[tr.Length];
        input.CopyTo(text);
        foreach (var item in span.EnumerateLines())
        {
            var enumerator = item.EnumerateSlices(" ");
            enumerator.MoveNext();
            if (enumerator.Current.Equals("swap", StringComparison.OrdinalIgnoreCase))
            {
                enumerator.MoveNext();
                if (enumerator.Current[0] == 'p')
                {
                    enumerator.MoveNext();
                    var p1 = int.Parse(enumerator.Current);
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    var p2 = int.Parse(enumerator.Current);
                    var temp = text[p1];
                    text[p1] = text[p2];
                    text[p2] = temp;
                }
                else
                {
                    enumerator.MoveNext();
                    var c1 = enumerator.Current[0];
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    var c2 = enumerator.Current[0];
                    foreach (ref var c in text)
                    {
                        if (c == c1)
                            c = c2;
                        else if (c == c2)
                            c = c1;
                    }
                }
            }
            else if (enumerator.Current.Equals("move", StringComparison.OrdinalIgnoreCase))
            {
                enumerator.MoveNext();
                enumerator.MoveNext();
                var from = int.Parse(enumerator.Current);
                enumerator.MoveNext();
                enumerator.MoveNext();
                enumerator.MoveNext();
                var to = int.Parse(enumerator.Current);
                var temp = text[from];
                if (from < to)
                {
                    while (from != to)
                    {
                        text[from] = text[from + 1];
                        from++;
                    }
                }
                else
                {
                    while (from != to)
                    {
                        text[from] = text[from - 1];
                        from--;
                    }
                }
                text[from] = temp;
            }
            else if (enumerator.Current.Equals("rotate", StringComparison.OrdinalIgnoreCase))
            {
                enumerator.MoveNext();
                int num;
                if (enumerator.Current[0] == 'l')
                {
                    enumerator.MoveNext();
                    var l = int.Parse(enumerator.Current);
                    num = tr.Length - l;
                }
                else if (enumerator.Current[0] == 'r')
                {
                    enumerator.MoveNext();
                    num = int.Parse(enumerator.Current);
                }
                else
                {
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    enumerator.MoveNext();
                    num = 1 + text.IndexOf(enumerator.Current[0]);
                    if (num > 4)
                        num++;
                    num %= text.Length;
                }
                text.Slice(text.Length - num).CopyTo(textCache);
                for (int i = text.Length - 1; i >= num; i--)
                {
                    text[i] = text[i - num];
                }
                textCache.Slice(0, num).CopyTo(text);
            }
            else
            {
                enumerator.MoveNext();
                enumerator.MoveNext();
                var from = int.Parse(enumerator.Current);
                enumerator.MoveNext();
                enumerator.MoveNext();
                var to = int.Parse(enumerator.Current);
                text.Slice(from, to - from + 1).Reverse();
            }
        }
        text.CopyTo(textCache);
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
        Span<char> input = stackalloc char[tr2.Length];
        Span<char> outt = stackalloc char[tr2.Length];
        input.Fill('a');
        while (true)
        {
            Part1(span, input, outt);
            if (outt.SequenceEqual(tr2))
                return input.ToString();
            UpdateString(input);
        }
    }
    static void UpdateString(Span<char> str)
    {
        for (int i = str.Length - 1; i >= 0; i--)
        {
            if (str[i] != 'z')
            {
                str[i]++;
                return;
            }
            str[i] = 'a';
        }
    }
}
