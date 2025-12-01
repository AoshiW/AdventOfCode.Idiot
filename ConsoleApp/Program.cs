using AdventOfCode.Client;
using AdventOfCode.Client.Caching;
using AdventOfCode.Puzzles;
using AdventOfCode.Puzzles.Y2017;
using System.Diagnostics;
using System.Reflection;

Console.WriteLine("Hello, AdventOfCode!");

var cacheDir = Environment.GetEnvironmentVariable("AOC_CACHE");
ArgumentNullException.ThrowIfNull(cacheDir);
var options = new AdventOfCodeClientOptions
{
    Session = Environment.GetEnvironmentVariable("AOC_SESSION")!,
    ContactInformation = new("(User: Aoshi.W@gmail.com)"),
};
var client = new AdventOfCodeClient(options, new FileSystemCache(cacheDir!));

RunPuzzle<Day10>(client);

[Conditional("DEBUG")]
static void RunPuzzle<T>(AdventOfCodeClient client) where T : IDay, new()
{
    var att = typeof(T).GetCustomAttribute<AocPuzzleAttribute>()!;
    var day = new T();
    var rawInput = client.GetPuzzleInputAsStringAsync(att.Year, att.Day, default).GetAwaiter().GetResult();
    var input = rawInput.AsSpan().TrimEnd();

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
