using AdventOfCode.Puzzles.Y2015;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2015_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(232, 1783);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, int>(1598415, 3812909);
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, int>(2572, 2631);
    [TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(117946, 3938038);
    [TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, int>(258, 53);
    [TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, int>(543903, 14687245);
    [TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, int>(16076, 2797);
    [TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, int>(1371, 2117);
    //[TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, int>(default, default);
    [TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, int>(329356, 4666278);
    [TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, string>("hepxxyzz", "heqaabcc");
    [TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(191164, 87842);
    [TestMethod] public async Task Day13() => await Assert.TestAsync<Day13, int>(709, 668);
    [TestMethod] public async Task Day14() => await Assert.TestAsync<Day14, int>(2655, 1059);
    //[TestMethod] public async Task Day15() => await Assert.TestAsync<Day15, int>(default, default);
    [TestMethod] public async Task Day16() => await Assert.TestAsync<Day16, int>(40, 241);
    [TestMethod] public async Task Day17() => await Assert.TestAsync<Day17, int>(654, 57);
    [TestMethod] public async Task Day18() => await Assert.TestAsync<Day18, int>(768, 781);
    [TestMethod] public async Task Day19() => await Assert.TestAsync<Day19, int>(576, default);
    [TestMethod] public async Task Day20() => await Assert.TestAsync<Day20, int>(776160, 786240);
    //[TestMethod] public async Task Day21() => await Assert.TestAsync<Day21, int>(default, default);
    //[TestMethod] public async Task Day22() => await Assert.TestAsync<Day22, int>(default, default);
    [TestMethod] public async Task Day23() => await Assert.TestAsync<Day23, int>(307, 160);
    [TestMethod] public async Task Day24() => await Assert.TestAsync<Day24, long>(10439961859, default);
    [TestMethod] public async Task Day25() => await Assert.TestAsync<Day25, long>(2650453, default);
}
