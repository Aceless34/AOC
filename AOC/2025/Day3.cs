using AOC.Grid;
using AOC.Visualizer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2025
{
    public class Day3 : ISolver
    {
        public uint Day => 3;

        public uint Year => 2025;

        public string Title => "Lobby";

        public object SolvePart1(string input)
        {
            List<List<int>> banks = input.Split("\n").Select(line => line.Trim().Select(c => int.Parse(c.ToString())).ToList()).ToList();

            long sum = 0;
            foreach(List<int> bank in banks)
            {
                (int idx, int val) max1 = (bank.IndexOf(bank.Max()), bank.Max());
                
                if(max1.idx == bank.Count - 1)
                {
                    int oldValue = max1.val;
                    int oldIdx = max1.idx;
                    bank[max1.idx] = -1;
                    max1 = (bank.IndexOf(bank.Max()), bank.Max());
                    bank[oldIdx] = oldValue;
                }

                int max2 = bank.Skip(max1.idx+1).Max();

                string num = max1.val.ToString() + max2.ToString();
                sum += long.Parse(num);

            }
            return sum;
        }

        public object SolvePart2(string input)
        {
            List<List<int>> banks = input.Split("\n").Select(line => line.Trim().Select(c => int.Parse(c.ToString())).ToList()).ToList();

            long sum = 0;
            foreach (List<int> bank in banks)
            {
                string jolt = "";
                List<int> bankCopy = new List<int>(bank);

                while (jolt.Length < 12)
                {
                    int highest = bankCopy.Take(bankCopy.Count - 12 + jolt.Length + 1).Max();
                    jolt += highest.ToString();
                    bankCopy = bankCopy.Skip(bankCopy.IndexOf(highest)+1).ToList();
                }

                sum += long.Parse(jolt);
            }

            return sum;
        }
    }
}
