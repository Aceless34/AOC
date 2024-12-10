using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{

    public class Day2 : Day
    {
        public string Task1()
        {
            //var tmp = InputUtils.ReadLines("2024/Files/Day1.txt").Select(line => line.Split(" "));
            List <List<int>> list = InputUtils.ReadLines("2024/Files/Day2.txt").Select(line => line.Split(" ").Select(x => int.Parse(x)).ToList()).ToList();

            int safeLevels = 0;
            foreach (var level in list)
            {
                if (IsSave(level))
                {
                    safeLevels++;
                }
            }

            return safeLevels.ToString();
        }

        public string Task2()
        {
            List<List<int>> list = InputUtils.ReadLines("2024/Files/Day2.txt").Select(line => line.Split(" ").Select(x => int.Parse(x)).ToList()).ToList();

            int safeLevels = 0;
            foreach (var level in list)
            {
                if (DampenerSave(level))
                {
                    safeLevels++;
                }
                else
                {
                    Console.WriteLine($"Unsave: {String.Join(" ", level)}");
                }
            }
            return safeLevels.ToString();
        }

        public bool IsSave(List<int> list)
        {
            bool increasing = true;
            for (int i = 0; i < list.Count-1; i++)
            {
                int x = list[i];
                int y = list[i+1];

                //distance 
                int dist = Math.Abs(x - y);
                if( dist < 1 || dist > 3)
                {
                    return false;
                }

                //
                if(i == 0)
                {
                    increasing = x < y;
                }
                else
                {
                    if (increasing && x > y) return false;
                    else if (!increasing && x < y) return false;
                }

            }
            return true;
        }

        public bool DampenerSave(List<int> list)
        {
            bool save = false;
            for (int i = 0; i < list.Count; i++)
            {
                var dampnerList = list.GetRange(0, i);
                dampnerList.AddRange(list.GetRange(i+1, list.Count - i - 1));

                if (IsSave(dampnerList))
                    save = true;
            }
            return save;
        }
    }
}
