using AdventOfCode.Puzzles.Y2016;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2016_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(236, 182);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, string>("78985", "57DD8");
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, int>(917, 1649);
    [TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(278221, 267);
    [TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, string>("f97c354d", "863dde27");
    [TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, string>("agmwzecr", "owlaxqvq");
    [TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, int>(110, 242);
    [TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, string>("121", "RURUCEOEIL");
    [TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, long>(183269, 11317278863);
    [TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, int>(73, 3965);
    //[TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, int>(default, default);
    [TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(317993, 9227647);
    [TestMethod] public async Task Day13() => await Assert.TestAsync<Day13, int>(92, 124);
    [TestMethod] public async Task Day14() => await Assert.TestAsync<Day14, int>(35186, 22429);
    [TestMethod] public async Task Day15() => await Assert.TestAsync<Day15, int>(203660, 2408135);
    [TestMethod] public async Task Day16() => await Assert.TestAsync<Day16, string>("10100011010101011", "01010001101011001");
    //[TestMethod] public async Task Day17() => await Assert.TestAsync<Day17, int>(default, default);
    [TestMethod] public async Task Day18() => await Assert.TestAsync<Day18, int>(1974, 19991126);
    [TestMethod] public async Task Day19() => await Assert.TestAsync<Day19, int>(1816277, 1410967);
    [TestMethod] public async Task Day20() => await Assert.TestAsync<Day20, uint>(14975795, 101);
    [TestMethod] public async Task Day21() => await Assert.TestAsync<Day21, string>("gbhafcde", default);
    [TestMethod] public async Task Day22() => await Assert.TestAsync<Day22, int>(1003, default);
    [TestMethod] public async Task Day23() => await Assert.TestAsync<Day23, int>(12480, 479009040);
    //[TestMethod] public async Task Day24() => await Assert.TestAsync<Day24, int>(default, default);
    //[TestMethod] public async Task Day25() => await Assert.TestAsync<Day25, int>(default, default);
}
