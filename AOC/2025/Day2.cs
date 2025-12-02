using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC._2025
{
    public class Day2 : ISolver
    {
        public uint Day => 2;
        public uint Year => 2025;

        public string Title => "Gift Shop";

        public object SolvePart1(string input)
        {
            IEnumerable<(string start, string end)> ranges= input.Split(",").Select(range => (range.Split("-")[0], range.Split("-")[1]));

            long sum = 0;
            foreach((string start, string end) range in ranges)
            {
                for(long i = long.Parse(range.start); i <= long.Parse(range.end); i++)
                {
                    string numString = i.ToString();
                    if(numString.Length % 2 == 0)
                    {
                        string left = numString.Substring(0, numString.Length / 2);
                        string right = numString.Substring(numString.Length / 2);
                        if (left == right)
                        {
                            sum += i;
                        }
                    }
                }
            }

            return sum;
        }

        public object SolvePart2(string input)
        {
            IEnumerable<(string start, string end)> ranges = input.Split(",").Select(range => (range.Split("-")[0], range.Split("-")[1]));

            long sum = 0;
            foreach ((string start, string end) range in ranges)
            {
                for (long i = long.Parse(range.start); i <= long.Parse(range.end); i++)
                {
                    string numString = i.ToString();
                    if(Regex.Match(numString, @"^(.+?)\1+$").Success)
                    {
                        sum += i;
                    }
                }
            }

            return sum;
        }
    }
}
