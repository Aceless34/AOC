using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2025
{
    public class Day1 : ISolver
    {
        public uint Day => 1;
        public uint Year => 2025;

        public string Title => "Secret Entrance";

        public object SolvePart1(string input)
        {
            List<int> nums = input.Split("\n").Select(line =>
            {
                bool neg = line.StartsWith("L");
                return neg ? int.Parse(line.Substring(1)) * -1 : int.Parse(line.Substring(1));
            }).ToList();

            int count = 50;
            int zeros = 0;
            foreach (int num in nums)
            {
                count = (count + num) % 100;
                if (count < 0)
                    count = 100 - count * -1;

                zeros = count == 0 ? zeros + 1 : zeros;
            }

            return zeros;
        }

        public object SolvePart2(string input)
        {
            List<int> nums = input.Split("\n").Select(line =>
            {
                bool neg = line.StartsWith("L");
                return neg ? int.Parse(line.Substring(1)) * -1 : int.Parse(line.Substring(1));
            }).ToList();

            int count = 50;
            int zeros = 0;
            foreach (int num in nums)
            {
                int steps = Math.Abs(num);
                while (steps > 0)
                {
                    count += 1 * Math.Sign(num);
                    count = count % 100;
                    if (count < 0)
                        count = 99;
                    if(count == 0)
                        zeros += 1;
                    steps--;
                }
            }

            return zeros;

        }

    }
}
