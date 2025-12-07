using AdventOfCode.Client;
using AdventOfCode.Client.Caching;
using AdventOfCode.Puzzles;
using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace ConsoleApp;

[MemoryDiagnoser]
[ShortRunJob]
public class DayBenchmark<TDay, TResult> where TDay : IDay<TResult>, new()
{
    readonly TDay _day = new();
    readonly string _input;

    public DayBenchmark()
    {
        var options = new AdventOfCodeClientOptions
        {
            Session = Environment.GetEnvironmentVariable("AOC_SESSION")!,
            ContactInformation = new("(User: Aoshi.W@gmail.com)"),
        };
        var Client = new AdventOfCodeClient(options, new FileSystemCache(Environment.GetEnvironmentVariable("AOC_CACHE")!));
        var aocPuzzle = typeof(TDay).GetCustomAttribute<AocPuzzleAttribute>();
        _input = Client.GetPuzzleInputAsStringAsync(aocPuzzle.Year, aocPuzzle.Day).GetAwaiter().GetResult();
        _input = _input.TrimEnd("\r\n").ToString();
    }

    [Benchmark]
    public TResult Part1()
        => _day.Part1(_input);

    [Benchmark]
    public TResult Part2()
        => _day.Part2(_input);
}
