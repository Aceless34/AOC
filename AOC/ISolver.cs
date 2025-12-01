using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public interface ISolver
    {
        uint Day { get; }
        uint Year { get; }
        string Title { get; }
        object SolvePart1(string input);
        object SolvePart2(string input);
    }
}
