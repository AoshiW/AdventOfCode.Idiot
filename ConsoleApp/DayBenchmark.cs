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
        var aocPuzzle = typeof(TDay).GetCustomAttribute<AocPuzzleAttribute>();
        _input = Program.Client.GetPuzzleInputAsStringAsync(aocPuzzle.Year, aocPuzzle.Day).GetAwaiter().GetResult();
        _input = _input.TrimEnd();
    }

    [Benchmark]
    public TResult Part1()
        => _day.Part1(_input);

    [Benchmark]
    public TResult Part2()
        => _day.Part2(_input);
}
