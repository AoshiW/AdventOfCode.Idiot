using AdventOfCode.Client;
using AdventOfCode.Client.Caching;
using AdventOfCode.Puzzles;
using AdventOfCode.Puzzles.Y2022;
using System.Diagnostics;
using System.Reflection;

Console.WriteLine("Hello, AdventOfCode!");

var options = new AdventOfCodeClientOptions
{
    Session = "53616c7465645f5fb6dde0806376db5061fdb7e7bba1f6757e23a31abbce6c9955c8c487f900df6d481992c16f686e9f770691a8156c6c624c61514591f8a0b7",
    ContactInformation = new("(User: Aoshi.W@gmail.com)"),
};
var client = new AdventOfCodeClient(options, new FileSystemCache("X:/.temp/spc_cache"));

// Y22/20, Y17/10
RunPuzzle<Day01>(client);

[Conditional("DEBUG")]
static void RunPuzzle<T>(AdventOfCodeClient client) where T : IDay, new()
{
    var att = typeof(T).GetCustomAttribute<AocPuzzleAttribute>()!;
    var day = new T();
    var rawInput = client.GetPuzzleInputAsStringAsync(att.Year, att.Day, default).GetAwaiter().GetResult();
    var input = rawInput.AsSpan().TrimEnd();
    Console.WriteLine($"Puzzzle: Y{att.Year} D{att.Day:00} - {att.Title}");
    foreach(var line in input.EnumerateLines()) { }
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
