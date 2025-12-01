using System.Buffers;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 11, "Corporate Policy")]
public class Day11 : IDay<string>
{
    static readonly SearchValues<char> ilo = SearchValues.Create(nameof(ilo));
    public string Part1(ReadOnlySpan<char> span)
    {
        Span<char> password = stackalloc char[span.Length];
        span.CopyTo(password);
        while (!ValidatePassword(password))
        {
            UpdatePassword(password);
        }
        return new string(password);
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        Span<char> password = stackalloc char[span.Length];
        span.CopyTo(password);
        while (!ValidatePassword(password))
        {
            UpdatePassword(password);
        }
        do
        {
            UpdatePassword(password);
        }
        while (!ValidatePassword(password));
        return new string(password);
    }

    static void UpdatePassword(Span<char> str)
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

    static bool ValidatePassword(Span<char> str)
    {
        bool rule1 = false, rule3 = false;
        for (int i = 0; i < str.Length - 2; i++)
        {
            if (str[i + 1] == str[i] + 1 && str[i + 2] == str[i] + 2)
            {
                rule1 = true;
                break;
            }
        }
        if(!rule1) 
            return false;

        if (str.ContainsAny(ilo))
            return false;

        char c = 'o';
        for (int i = 0; i < str.Length - 1; i++)
        {
            if (str[i] == str[i + 1])
            {
                if (c == 'o')
                {
                    c = str[i];
                }
                else if (str[i] != c)
                {
                    rule3 = true;
                    break;
                }
            }
        }
        return rule3;
    }
}
