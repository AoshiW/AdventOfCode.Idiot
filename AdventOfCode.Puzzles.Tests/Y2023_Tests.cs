using AdventOfCode.Puzzles.Y2023;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2023_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(53921, 54676);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, int>(2716, 72227);
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, int>(560670, 91622824);
    [TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(25183, 5667240);
    [TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, int>(389056265, 137516820);
    [TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, int>(2269432, 35865985);
    [TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, int>(251058093, 249781879);
    [TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, long>(17621, 20685524831999);
    [TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, int>(1584748274, 1026);
    [TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, int>(6942, 297);
    [TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, long>(9545480, 406725732046);
    [TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(7221, default);
    [TestMethod] public async Task Day13() => await Assert.TestAsync<Day13, int>(31956, 37617);
    [TestMethod] public async Task Day14() => await Assert.TestAsync<Day14, int>(108759, 89089);
    [TestMethod] public async Task Day15() => await Assert.TestAsync<Day15, int>(504449, 262044);
    [TestMethod] public async Task Day16() => await Assert.TestAsync<Day16, int>(7060, 7493);
    //[TestMethod] public async Task Day17() => await Assert.TestAsync<Day17, int>(default, default);
    [TestMethod] public async Task Day18() => await Assert.TestAsync<Day18, long>(39194, 78242031808225);
    [TestMethod] public async Task Day19() => await Assert.TestAsync<Day19, long>(402185, 130291480568730);
    [TestMethod] public async Task Day20() => await Assert.TestAsync<Day20, int>(763500168, default);
    [TestMethod] public async Task Day21() => await Assert.TestAsync<Day21, int>(3768, default);
    [TestMethod] public async Task Day22() => await Assert.TestAsync<Day22, int>(495, 76158);
    //[TestMethod] public async Task Day23() => await Assert.TestAsync<Day23, int>(default, default);
    //[TestMethod] public async Task Day24() => await Assert.TestAsync<Day24, int>(default, default);
    //[TestMethod] public async Task Day25() => await Assert.TestAsync<Day25, int>(default, default);
}
