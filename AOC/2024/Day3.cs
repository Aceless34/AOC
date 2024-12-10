using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day3 : Day
    {
        public string Task1()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day3.txt");
            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            Regex regex = new Regex(pattern);

            long sum = 0;
            int matches = 0;

            foreach (string line in lines)
            {
                //Console.WriteLine(line);
                MatchCollection matchCollection = regex.Matches(line);
                foreach (Match match in matchCollection) {
                    int num1 = int.Parse(match.Groups[1].Value);
                    int num2 = int.Parse(match.Groups[2].Value);
                    //Console.WriteLine($"{num1} * {num2}");
                    sum += num1 * num2;
                    matches++;
                }
                //Console.WriteLine($"Line sum: {sum}");
            }
            return sum.ToString();

        }

        public string Task2()
        {
            string text = File.ReadAllText("2024/Files/Day3.txt");
            string pattern = @"(mul\((\d+),(\d+)\))|(do(n't)?\(\))";
            Regex regex = new Regex(pattern);

            long sum = 0;
            bool enabled = true;

            //Console.WriteLine(line);
            MatchCollection matchCollection = regex.Matches(text);
            foreach (Match match in matchCollection)
            {
                if (match.Groups[0].Value == "don't()") enabled = false;
                else if (match.Groups[0].Value == "do()")
                {
                    enabled = true;
                }
                else
                {
                    if (enabled)
                    {
                        int num1 = int.Parse(match.Groups[2].Value);
                        int num2 = int.Parse(match.Groups[3].Value);
                        //Console.WriteLine($"{num1} * {num2}");
                        sum += num1 * num2;
                    }
                }

            }
            return sum.ToString();
        }
    }
}
