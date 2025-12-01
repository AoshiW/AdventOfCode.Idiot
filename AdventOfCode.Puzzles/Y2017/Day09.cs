namespace AdventOfCode.Puzzles.Y2017;

[AocPuzzle(2017, 9, "Stream Processing")]
public class Day09 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var score = 0;
        bool isInGarbage = false;
        var level = 0;

        for (var i = 0; i < span.Length; i++)
        {
            switch (span[i])
            {
                case '!':
                    i++;
                    break;
                case '<':
                    isInGarbage = true;
                    break;
                case '>':
                    isInGarbage = false;
                    break;
                case '{' when !isInGarbage:
                    level++;
                    break;
                case '}' when !isInGarbage:
                    score += level;
                    level--;
                    break;
            }
        }
        return score;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var score = 0;
        bool isInGarbage = false;

        for (var i = 0; i < span.Length; i++)
        {
            switch (span[i])
            {
                case '!':
                    i++;
                    break;
                case '<':
                    if (isInGarbage)
                        score++;
                    isInGarbage = true;
                    break;
                case '>':
                    isInGarbage = false;
                    break;
                default:
                    if (isInGarbage)
                        score++;
                    break;
            }
        }
        return score;
    }
}
