using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC._2024
{
    class Day13 : Day
    {
        public string Task1()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day13.txt");

            Point A = new Point(1,1), B = new Point(1,1), Prize = new Point(1,1);
            double x = 0, y = 0;
            long totalTokens = 0;
            foreach (string line in lines)
            {
                if (line.StartsWith("Button A"))
                {
                    MatchCollection match = Regex.Matches(line, @"X\+(\d+), Y\+(\d+)");
                    A = new Point(int.Parse(match[0].Groups[1].Value), int.Parse(match[0].Groups[2].Value));
                }
                else if (line.StartsWith("Button B"))
                {
                    MatchCollection match = Regex.Matches(line, @"X\+(\d+), Y\+(\d+)");
                    B = new Point(int.Parse(match[0].Groups[1].Value), int.Parse(match[0].Groups[2].Value));
                }
                else if (line.StartsWith("Prize:"))
                {
                    MatchCollection match = Regex.Matches(line, @"X=(\d+), Y=(\d+)");
                    Prize = new Point(int.Parse(match[0].Groups[1].Value), int.Parse(match[0].Groups[2].Value));
                }
                else
                {
                    CalcPushings(A, B, Prize, out x, out y);
                    if (x % 1 == 0 && y % 1 == 0)
                    {
                        Console.WriteLine($"{x} {y}");
                        Console.WriteLine($"{x * 3} {y} = {x * 3 + y} tokens");
                        totalTokens += ((int)x) * 3 + (int)y;
                    }
                }
            }
            CalcPushings(A, B, Prize, out x, out y);
            if (x % 1 == 0 && y % 1 == 0)
            {
                Console.WriteLine($"{x} {y}");
                Console.WriteLine($"{x * 3} {y} = {x * 3 + y} tokens");
                totalTokens += ((int)x) * 3 + (int)y;
            }

            Console.WriteLine();


            return totalTokens.ToString() ;
        }

        private static void CalcPushings(Point A, Point B, Point Prize, out double x, out double y)
        {
            x = (Prize.X * B.Y - Prize.Y * B.X) / (double)(A.X * B.Y - A.Y * B.X);
            y = (A.X * Prize.Y - A.Y * Prize.X) / (double)(A.X * B.Y - A.Y * B.X);
        }

        private static void CalcPushings(Point A, Point B, long PrizeX, long PrizeY, out double x, out double y)
        {
            x = (PrizeX * B.Y - PrizeY * B.X) / (double)(A.X * B.Y - A.Y * B.X);
            y = (A.X * PrizeY - A.Y * PrizeX) / (double)(A.X * B.Y - A.Y * B.X);
        }

        public string Task2()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day13.txt");

            Point A = new Point(1, 1), B = new Point(1, 1);
            long PrizeX = 0;
            long PrizeY = 0;
            double x = 0, y = 0;
            long totalTokens = 0;
            foreach (string line in lines)
            {
                if (line.StartsWith("Button A"))
                {
                    MatchCollection match = Regex.Matches(line, @"X\+(\d+), Y\+(\d+)");
                    A = new Point(int.Parse(match[0].Groups[1].Value), int.Parse(match[0].Groups[2].Value));
                }
                else if (line.StartsWith("Button B"))
                {
                    MatchCollection match = Regex.Matches(line, @"X\+(\d+), Y\+(\d+)");
                    B = new Point(int.Parse(match[0].Groups[1].Value), int.Parse(match[0].Groups[2].Value));
                }
                else if (line.StartsWith("Prize:"))
                {
                    MatchCollection match = Regex.Matches(line, @"X=(\d+), Y=(\d+)");
                    PrizeX = long.Parse(match[0].Groups[1].Value) + 10000000000000;
                    PrizeY = int.Parse(match[0].Groups[2].Value) + 10000000000000;
                }
                else
                {
                    CalcPushings(A, B, PrizeX, PrizeY, out x, out y);
                    if (x % 1 == 0 && y % 1 == 0)
                    {
                        Console.WriteLine($"{x} {y}");
                        Console.WriteLine($"{x * 3} {y} = {x * 3 + y} tokens");
                        totalTokens += ((long)x) * 3 + (long)y;
                    }
                }
            }
            CalcPushings(A, B, PrizeX, PrizeY, out x, out y);
            if (x % 1 == 0 && y % 1 == 0)
            {
                Console.WriteLine($"{x} {y}");
                Console.WriteLine($"{x * 3} {y} = {x * 3 + y} tokens");
                totalTokens += ((long)x) * 3 + (long)y;
            }

            Console.WriteLine();


            return totalTokens.ToString();
        }
    }
}
