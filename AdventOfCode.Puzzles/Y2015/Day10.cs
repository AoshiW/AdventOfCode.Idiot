using System.Text;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 10, "Elves Look, Elves Say")]
public class Day10 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
        => Compute(span, 40);

    public int Part2(ReadOnlySpan<char> span)
        => Compute(span, 50);

    static int Compute(ReadOnlySpan<char> span, int count)
    {
        var input = new StringBuilder().Append(span);
        var output = new StringBuilder();
        for (int countCounter = 0; countCounter < count; countCounter++)
        {
            char number = input[0];
            int numberCount = 1;
            output.Length = 0;
            for (var i = 1; i < input.Length; i++)
            {
                var c = input[i];
                if (number != c)
                {
                    output.Append(numberCount).Append(number);
                    number = c;
                    numberCount = 1;
                }
                else
                    numberCount++;
            }
            output.Append(numberCount).Append(number);
            (output, input) = (input, output);
        }
        return input.Length;
    }
}
