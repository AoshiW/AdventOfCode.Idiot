using AdventOfCode.Client;
using AdventOfCode.Puzzles;
using BenchmarkDotNet.Running;
using ConsoleApp;
using System.Diagnostics;
using System.Reflection;

partial class Program
{
    public static AdventOfCodeClient Client { get; private set; } = default!;

    [Conditional("DEBUG")]
    static void RunPuzzle<T>(CancellationToken cancellationToken = default) where T : IDay, new()
    {
        var aocPuzzleAtt = typeof(T).GetCustomAttribute<AocPuzzleAttribute>()!;
        var day = new T();
        var rawInput = Client.GetPuzzleInputAsStringAsync(aocPuzzleAtt.Year, aocPuzzleAtt.Day, cancellationToken).GetAwaiter().GetResult();
        var input = rawInput.AsSpan().TrimEnd();
        {
            // without this code RunPart<T> sometimes allocate 888 bytes ¯\_(ツ)_/¯ (Y2025 D01)
            var enumeator = input.EnumerateLines();
            enumeator.MoveNext();
        }

        Console.WriteLine($"Puzzzle: Y{aocPuzzleAtt.Year} D{aocPuzzleAtt.Day:00} - {aocPuzzleAtt.Title}");

        if (day is IDay<int> dayInd)
        {
            RunPart(dayInd.Part1, input, 1);
            RunPart(dayInd.Part2, input, 2);
        }
        else if (day is IDay<long> dayLong)
        {
            RunPart(dayLong.Part1, input, 1);
            RunPart(dayLong.Part2, input, 2);
        }
        else if (day is IDay<ulong> dayULong)
        {
            RunPart(dayULong.Part1, input, 1);
            RunPart(dayULong.Part2, input, 2);
        }
        else if (day is IDay<string> dayString)
        {
            RunPart(dayString.Part1, input, 1);
            RunPart(dayString.Part2, input, 2);
        }
        else
        {
            Console.WriteLine("Unsupported IDay<> return type, callback to IDay");
            RunPart(day.Part1, input, 1);
            RunPart(day.Part2, input, 2);
        }
    }

    static void RunPart<T>(Func<ReadOnlySpan<char>, T> func, ReadOnlySpan<char> input, int part)
    {
        var alloc = GC.GetAllocatedBytesForCurrentThread();
        var startTime = Stopwatch.GetTimestamp();

        var result = func(input);
        var elapsed = Stopwatch.GetElapsedTime(startTime);
        alloc = GC.GetAllocatedBytesForCurrentThread() - alloc;

        Console.WriteLine($"Part{part}: {result}\t t={elapsed.TotalMilliseconds}ms\t{alloc} bytes");
    }

    [Conditional("RELEASE")]
    static void RunBechmark<T>() where T : IDay, new()
    {
        var iDay = typeof(T).GetInterface(typeof(IDay<>).Name);
        var iDayT = iDay.GenericTypeArguments[0];
        BenchmarkRunner.Run(typeof(DayBenchmark<,>).MakeGenericType(typeof(T), iDayT));
    }
}
