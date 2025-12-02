using FluentAssertions;

namespace AOC.Test
{
    public class Day2Tests : SolverTestBase<AOC._2025.Day2>
    {
        protected override string SampleInput => @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
  
        [Fact]
        public void Part1_SolveExample()
        {
            var result = Solver.SolvePart1(SampleInput);
            result.Should().Be(1227775554);
        }

        [Fact]
        public void Part2_SolveExample()
        {
            var result = Solver.SolvePart2(SampleInput);
            result.Should().Be(4174379265);
        }

    }
}
