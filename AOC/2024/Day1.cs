using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day1 : Day
    {
        public string Task1()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day1.txt");

            //System.Text.RegularExpressions.Regex.Split( stringvalue, @"\s{2,}")

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            foreach (var item in lines.Select(line => new int[2] { int.Parse(System.Text.RegularExpressions.Regex.Split(line, @"\s+")[0]), int.Parse(System.Text.RegularExpressions.Regex.Split(line, @"\s+")[1]) }))
            {
                left.Add(item[0]);
                right.Add(item[1]);
            }

            left.Sort();
            right.Sort();
            
            long sum = 0;
            for (int i = 0; i < left.Count; i++) {
                sum += Math.Abs(left[i] - right[i]);
            }

            return sum.ToString();
        }

        public string Task2()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day1.txt");

            List<int> left = new List<int>();
            List<int> right = new List<int>();
            foreach (var item in lines.Select(line => new int[2] { int.Parse(System.Text.RegularExpressions.Regex.Split(line, @"\s+")[0]), int.Parse(System.Text.RegularExpressions.Regex.Split(line, @"\s+")[1]) }))
            {
                left.Add(item[0]);
                right.Add(item[1]);
            }

            long sum = 0;
            foreach (var item in left)
            {
                sum += item * right.FindAll(x => x == item).Count;
            }

            return sum.ToString();

        }

    }
}
