using FluentAssertions;

namespace AOC.Test
{
    public class Day4Tests : SolverTestBase<AOC._2025.Day4>
    {
        protected override string SampleInput => @"..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.";
  
        [Fact]
        public void Part1_SolveExample()
        {
            var result = Solver.SolvePart1(SampleInput);
            result.Should().Be(13);
        }

        [Fact]
        public void Part2_SolveExample()
        {
            var result = Solver.SolvePart2(SampleInput);
            result.Should().Be(43);
        }

    }
}
