using AdventOfCode.Puzzles.Y2022;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2022_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(69836, 207968);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, int>(15337, 11696);
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, int>(8401, 2641);
    [TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(573, 867);
    [TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, string>("MQTPGLLDN", "LVZPSTTCZ");
    [TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, int>(1920, 2334);
    [TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, int>(1667443, 8998590);
    [TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, int>(1560, 252000);
    [TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, int>(6090, 2566);
    [TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, string>("13760", "RFKZCPEF");
    [TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, int>(100345, default);
  //[TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(default, default);
    [TestMethod] public async Task Day13() => await Assert.TestAsync<Day13, int>(5588, 23958);
  //[TestMethod] public async Task Day14() => await Assert.TestAsync<Day14, int>(1016, 25402);
    //[TestMethod] public async Task Day15() => await Assert.TestAsync<Day15, int>(default, default);
    //[TestMethod] public async Task Day16() => await Assert.TestAsync<Day16, int>(default, default);
    //[TestMethod] public async Task Day17() => await Assert.TestAsync<Day17, int>(default, default);
  //[TestMethod] public async Task Day18() => await Assert.TestAsync<Day18, int>(4348, default);
    //[TestMethod] public async Task Day19() => await Assert.TestAsync<Day19, int>(default, default);
    [TestMethod] public async Task Day20() => await Assert.TestAsync<Day20, long>(7713, 1664569352803);
  //[TestMethod] public async Task Day21() => await Assert.TestAsync<Day21, long>(43699799094202, default);
    //[TestMethod] public async Task Day22() => await Assert.TestAsync<Day22, int>(default, default);
    //[TestMethod] public async Task Day23() => await Assert.TestAsync<Day23, int>(default, default);
    //[TestMethod] public async Task Day24() => await Assert.TestAsync<Day24, int>(default, default);
    [TestMethod] public async Task Day25() => await Assert.TestAsync<Day25, string>("20-1-0=-2=-2220=0011", default!);
}
