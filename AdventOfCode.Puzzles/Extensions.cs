using System.Numerics;

namespace AdventOfCode.Puzzles;

public static class ExtensionsD
{
    public static bool IsInRange<T>(this T item, T minInclusive, T maxExclusive) where T : IComparable<T>
        => item.CompareTo(minInclusive) >= 0 && item.CompareTo(maxExclusive) < 0;

    public static void ToHexString(this ReadOnlySpan<byte> souce, Span<char> destination, bool isUpper = false)
    {
        for (int i = 0; i < souce.Length; i++)
        {
            destination[i * 2] = (souce[i] >> 4).ToHex(isUpper);
            destination[i * 2 + 1] = (souce[i] & 0xF).ToHex(isUpper);
        }
    }

    public static void ToHexString(this Span<byte> souce, Span<char> destination, bool isUpper = false)
    {
        ToHexString((ReadOnlySpan<byte>)souce, destination, isUpper);
    }

    public static char ToHex(this int num, bool isUpper = false) => num switch
    {
        0 => '0',
        1 => '1',
        2 => '2',
        3 => '3',
        4 => '4',
        5 => '5',
        6 => '6',
        7 => '7',
        8 => '8',
        9 => '9',
        10 => isUpper ? 'A' : 'a',
        11 => isUpper ? 'B' : 'b',
        12 => isUpper ? 'C' : 'c',
        13 => isUpper ? 'D' : 'd',
        14 => isUpper ? 'E' : 'e',
        15 => isUpper ? 'F' : 'f',
        _ => throw new ArgumentOutOfRangeException(nameof(num))
    };
    public static int FromHex(this char c) => c switch
    {
        '0' => 0,
        '1' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '6' => 6,
        '7' => 7,
        '8' => 8,
        '9' => 9,
        'A' or 'a' => 10,
        'B' or 'b' => 11,
        'C' or 'c' => 12,
        'D' or 'd' => 13,
        'E' or 'e' => 14,
        'F' or 'f' => 15,
        _ => throw new ArgumentOutOfRangeException(nameof(c))
    };

    public static char Ocr(this int letterHash) => letterHash switch
    {
        0x19297A52 => 'A',
        0x392E4A5C => 'B',
        0x1928424C => 'C',
        0x39294A5C => 'D',
        0x3D0E421E => 'E',
        0x3D0E4210 => 'F',
        0x19285A4E => 'G',
        0x252F4A52 => 'H',
        0x1C42108E => 'I',
        0x0C210A4C => 'J',
        0x254C5292 => 'K',
        0x2108421E => 'L',
        0x23BAC631 => 'M',
        0x252D5A52 => 'N',
        0x19294A4C => 'O',
        0x39297210 => 'P',
        0x19295A4D => 'Q',
        0x39297292 => 'R',
        0x1D08305C => 'S',
        0x1C421084 => 'T',
        0x25294A4C => 'U',
        0x2318A944 => 'V',
        0x231AD6AA => 'W',
        0x22A22951 => 'X',
        0x23151084 => 'Y',
        0x3C22221E => 'Z',
        0x00000000 => ' ',
        _ => throw new ArgumentOutOfRangeException(nameof(letterHash), letterHash, null)
    };
}

public static class Wx
{
    public static T ManhattanDistance<T>(this (T X, T Y, T Z) left, (T X, T Y, T Z) right) where T : INumber<T>
    {
        return T.Abs(left.X - right.X) + T.Abs(left.Y - right.Y) + T.Abs(left.Z - right.Z);
    }
    extension<T>(INumber<T>) where T : INumber<T>
    {
        public static T ManhattanDistance(Numerics.Vector2<T> left, Numerics.Vector2<T> right)
        {
            return T.Abs(left.X - right.X) + T.Abs(left.Y - right.Y);
        }

        public static T LeastCommonMultiple(T a, T b)
        {
            return (a / GreatestCommonDivisor(a, b)) * b;
        }
        public static T GreatestCommonDivisor(T a, T b)
        {
            while (b != T.Zero)
            {
                T temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}