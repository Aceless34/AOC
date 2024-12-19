using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day19 : Day
    {
        Dictionary<string, bool> mem = new Dictionary<string, bool>();
        Dictionary<string, long> mem2 = new Dictionary<string, long>();

        public string Task1()
        {
            string input = InputUtils.ReadString("2024/Files/Day19.txt");
            string[] towels = input.Split("\r\n\r\n")[0].Trim().Split(", ");

            List<string> designs = input.Split("\r\n\r\n")[1].Trim().Split("\r\n").ToList();

            int valid = 0;
            designs.ForEach(design =>
            {
                IEnumerable<string> usableTowels = towels.Where(t => design.Contains(t));
                if (ValidDesign(usableTowels.ToList(), design))
                {
                    valid++;
                }
            });

            return valid.ToString();
        }

        private bool ValidDesign(List<string> usableTowels, string design)
        {
            if (design == "")
                return true;

            if (mem.ContainsKey(design))
            {
                return mem[design];
            }

            foreach (var pattern in usableTowels)
            {
                if (design.StartsWith(pattern))
                {
                    string remaining = design.Substring(pattern.Length);
                    if(ValidDesign(usableTowels, remaining))
                    {
                        mem[design] = true;
                        return true;
                    }
                }
            }

            mem[design] = false;
            return false;

        }

        private long PossiblePatternCount(List<string> usableTowels, string design)
        {
            if (design == "")
                return 1;

            if (mem2.ContainsKey(design))
            {
                return mem2[design];
            }
            long count = 0;

            foreach (var pattern in usableTowels)
            {
                if (design.StartsWith(pattern))
                {
                    string remaining = design.Substring(pattern.Length);
                    count += PossiblePatternCount(usableTowels, remaining);
                }
            }

            mem2[design] = count;
            return count;

        }

        public string Task2()
        {
            string input = InputUtils.ReadString("2024/Files/Day19.txt");
            string[] towels = input.Split("\r\n\r\n")[0].Trim().Split(", ");

            List<string> designs = input.Split("\r\n\r\n")[1].Trim().Split("\r\n").ToList();

            long count = 0;
            designs.ForEach(design =>
            {
                IEnumerable<string> usableTowels = towels.Where(t => design.Contains(t));
                count += PossiblePatternCount(usableTowels.ToList(), design);
            });

            return count.ToString();
        }
    }
}
