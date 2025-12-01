using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 4, "The Ideal Stocking Stuffer")]
public class Day04 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
        => AdventCoin(span, static bytes => bytes[0] == 0 && bytes[1] == 0 && (bytes[2] & 0xF0) == 0);

    public int Part2(ReadOnlySpan<char> span)
        => AdventCoin(span, static bytes => bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0);

    static int AdventCoin(ReadOnlySpan<char> span, Func<ReadOnlySpan<byte>, bool> predicate)
    {
        Span<byte> bytesIn = stackalloc byte[span.Length + MagicNumbers.MaxStringLengthFor<int>()];
        Encoding.ASCII.GetBytes(span, bytesIn);
        var bytesInNumber = bytesIn.Slice(span.Length);
        Span<byte> bytesOut = stackalloc byte[MD5.HashSizeInBytes];
        for (int i = 1; ; i++)
        {
            i.TryFormat(bytesInNumber, out var written);
            MD5.HashData(bytesIn.Slice(0, span.Length + written), bytesOut);
            if (predicate(bytesOut))
                return i;
        }
    }
}
