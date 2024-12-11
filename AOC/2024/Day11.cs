using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AOC._2024
{
    public class Day11 : Day
    {
        public string Task1()
        {
            string line = InputUtils.ReadLines("2024/Files/Day11.txt")[0];
            List<long> stones = line.Split(' ').Select(x => long.Parse(x)).ToList();
            int blinks = 25;

            for (int i = 0; i < blinks; i++) {
                List<long> newStones = new List<long>();
                foreach (long stone in stones)
                {
                    if (stone == 0) newStones.Add(1);
                    else if (stone.ToString().ToCharArray().Length % 2 == 0)
                    {
                        int length = stone.ToString().Length;
                        newStones.Add(long.Parse(stone.ToString().Substring(0, length / 2)));
                        newStones.Add(long.Parse(stone.ToString().Substring(length / 2, length / 2)));
                    }
                    else
                    {
                        newStones.Add(stone * 2024);
                    }
                }
                //Console.WriteLine(string.Join(" ", newStones));

                stones = newStones;
            }

            return stones.Count.ToString();
        }

        public string Task2()
        {
            string line = InputUtils.ReadLines("2024/Files/Day11.txt")[0];
            List<long> stones = line.Split(' ').Select(x => long.Parse(x)).ToList();
            int blinks = 75;

            long sum = 0;
            foreach (long stone in stones)
            {
                sum += Solve2(stone, blinks);
            }
            //Console.WriteLine(string.Join(" ", newStones));

            return sum.ToString();
        }

        Dictionary<(long n, int blinks), long> mem = new();

        public long Solve2(long n, int blinks)
        {
            if (blinks == 0) return 1;
            else if(mem.ContainsKey((n, blinks)))
            {
                return mem[(n, blinks)];
            }
            else
            {
                long a = n;
                if(n == 0)
                {
                    a = Solve2(1, blinks - 1);
                }
                else if (n.ToString().ToCharArray().Length % 2 == 0)
                    a = Solve2(long.Parse(n.ToString().Substring(0, n.ToString().Length / 2)), blinks - 1)
                         + Solve2(long.Parse(n.ToString().Substring(n.ToString().Length / 2, n.ToString().Length / 2)), blinks - 1);
                else
                    a = Solve2(n * 2024, blinks - 1);
                mem[(n, blinks)] = a;
                return a;
            }
        }
    }
}
