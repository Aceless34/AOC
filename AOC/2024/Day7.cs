using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    internal class Day7 : Day
    {
        public string Task1()
        {
            List<string> list = InputUtils.ReadLines("2024/Files/Day7.txt");

                long sum = 0;
            foreach (string line in list) {
                long result = long.Parse(line.Split(":")[0]);
                string equation = line.Split(":")[1].Trim();
                List<long> numbers = equation.Split(" ").Select(x => long.Parse(x)).ToList();

                if (IsValid(result, numbers.ToArray(), 0))
                {
                    Console.WriteLine($"{result}: {equation}");
                    sum += result;
                }

            }

            return sum.ToString();
        }

        bool IsValid(long target, long[] nums, long acc)
        {
            if(nums.Length == 0)
                return acc == target;

            return IsValid(target, nums[1..], acc + nums[0])
                || IsValid(target, nums[1..], acc * nums[0]);
        }

        public string Task2()
        {
            List<string> list = InputUtils.ReadLines("2024/Files/Day7.txt");

            long sum = 0;
            foreach (string line in list)
            {
                long result = long.Parse(line.Split(":")[0]);
                string equation = line.Split(":")[1].Trim();
                List<long> numbers = equation.Split(" ").Select(x => long.Parse(x)).ToList();

                if (IsValid2(result, numbers.ToArray(), 0))
                {
                    Console.WriteLine($"{result}: {equation}");
                    sum += result;
                }

            }

            return sum.ToString();
        }

        bool IsValid2(long target, long[] nums, long acc)
        {
            if (acc > target)
                return false;
            if (nums.Length == 0)
                return acc == target;

            return IsValid2(target, nums[1..], long.Parse($"{acc}{nums[0]}"))
                || IsValid2(target, nums[1..], acc + nums[0])
                || IsValid2(target, nums[1..], acc * nums[0]);
        }
    }
}
