using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day5 : Day
    {
        public string Task1()
        {

            List<string> lines = InputUtils.ReadLines("2024/Files/Day5.txt");

            Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
            List<int[]> updates = new List<int[]>();

            bool rulesIn = true;
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    rulesIn = false;
                }
                else
                {
                    if (rulesIn)
                    {
                        string[] numbers = line.Split("|");
                        if (!rules.ContainsKey(int.Parse(numbers[1])))
                        {
                            rules[int.Parse(numbers[1])] = new List<int>();
                        }
                        rules[int.Parse(numbers[1])].Add(int.Parse(numbers[0]));
                    }
                    else
                    {
                        updates.Add(line.Split(",").Select(x => int.Parse(x)).ToArray());
                    }
                }

            }

            long sum = 0;
            foreach (var update in updates)
            {
                bool correct = true;
                for (int i = 0; i < update.Length; i++)
                {
                    int rule = update[i];
                    if (rules.ContainsKey(rule))
                    {
                        List<int> before = rules[rule];
                        for (int j = 0; j < before.Count; j++)
                        {
                            if (update.Skip(i + 1).Contains(before[j]))
                            {
                                correct = false;
                                break;
                            }
                        }
                    }

                }

                if (correct)
                {
                    int middleIdx = (update.Length - 1) / 2;
                    sum += update[middleIdx];
                }
            }


            return sum.ToString();
        }

        public bool CheckUpdate(int[] update, Dictionary<int, List<int>> rules, out int rule, out int beforeNum)
        {
            bool correct = true;
            rule = -1;
            beforeNum = -1;
            for (int i = 0; i < update.Length; i++)
            {
                rule = update[i];
                if (rules.ContainsKey(rule))
                {
                    List<int> before = rules[rule];
                    for (int j = 0; j < before.Count; j++)
                    {
                        beforeNum = before[j];
                        if (update.Skip(i + 1).Contains(before[j]))
                        {
                            correct = false;
                            break;
                        }
                    }
                }
                if (!correct) break;
            }
            return correct;
        }

        public string Task2()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day5.txt");

            Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
            List<int[]> updates = new List<int[]>();

            bool rulesIn = true;
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    rulesIn = false;
                }
                else
                {
                    if (rulesIn)
                    {
                        string[] numbers = line.Split("|");
                        if (!rules.ContainsKey(int.Parse(numbers[1])))
                        {
                            rules[int.Parse(numbers[1])] = new List<int>();
                        }
                        rules[int.Parse(numbers[1])].Add(int.Parse(numbers[0]));
                    }
                    else
                    {
                        updates.Add(line.Split(",").Select(x => int.Parse(x)).ToArray());
                    }
                }

            }

            long sum = 0;
            foreach (var update in updates.Where(u => !CheckUpdate(u, rules, out int rule, out int before)))
            {
                List<int> newUpdate = update.ToList();
                while (!CheckUpdate(newUpdate.ToArray(), rules, out int rule, out int before))
                {
                    int beforeIdx = newUpdate.IndexOf(before);
                    int ruleIdx = newUpdate.IndexOf(rule);
                    
                    Swap<int>(newUpdate, ruleIdx, beforeIdx);
                }
                int middleIdx = (newUpdate.Count - 1) / 2;
                sum += newUpdate[middleIdx];
            }


            return sum.ToString();
        }

        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}
