using AdventOfCode.Puzzles.Y2020;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2020_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(1019571, 100655544);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, int>(483, 482);
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, long>(247, 2983070376);
    [TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(256, 198);
    [TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, int>(996, 671);
    [TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, int>(6585, 3276);
    [TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, int>(302, 4165);
    [TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, int>(1915, 944);
    [TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, long>(14144619, 1766397);
    [TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, long>(2400, 338510590509056);
    [TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, int>(2238, 2013);
    [TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(319, 50157);
    [TestMethod] public async Task Day13() => await Assert.TestAsync<Day13, long>(2305, default);
    [TestMethod] public async Task Day14() => await Assert.TestAsync<Day14, ulong>(11501064782628, 5142195937660);
    [TestMethod] public async Task Day15() => await Assert.TestAsync<Day15, int>(232, 18929178);
    [TestMethod] public async Task Day16() => await Assert.TestAsync<Day16, long>(20013, default);
    //[TestMethod] public async Task Day17() => await Assert.TestAsync<Day17, int>(default, default);
    [TestMethod] public async Task Day18() => await Assert.TestAsync<Day18, long>(11076907812171, 283729053022731);
    [TestMethod] public async Task Day19() => await Assert.TestAsync<Day19, int>(205, default);
    //[TestMethod] public async Task Day20() => await Assert.TestAsync<Day20, int>(default, default);
    ////[TestMethod] public async Task Day21() => await Assert.TestAsync<Day21, int>(default, default);
    [TestMethod] public async Task Day22() => await Assert.TestAsync<Day22, int>(32856, 33805);
    [TestMethod] public async Task Day23() => await Assert.TestAsync<Day23, long>(28946753, 519044017360);
    [TestMethod] public async Task Day24() => await Assert.TestAsync<Day24, int>(356, 3887);
    [TestMethod] public async Task Day25() => await Assert.TestAsync<Day25, long>(18608573, default);
}
