using AOC._2024;

namespace AOC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = DateTime.Now.Day;

            Dictionary<int, Day> days = new Dictionary<int, Day>()
            {
                {1, new Day1() },
                {2, new Day2() },
                {3, new Day3() },
                {4, new Day4() },
                {5, new Day5() },
                {6, new Day6() },
                {7, new Day7() },
                {8, new Day8() },
                {9, new Day9() },
                {10, new Day10() },
            };

            //2024
            DateTime time = DateTime.Now;
            string out1 = days[day].Task1();
            TimeSpan ts = DateTime.Now - time;
            Console.WriteLine($"Aufgabe 1: {out1}");
            Console.WriteLine($"Benötigte Zeit: {ts.ToString()}");

            Console.WriteLine();

            time = DateTime.Now;
            string out2 = days[day].Task2();
            TimeSpan ts2 = DateTime.Now - time;
            Console.WriteLine($"Aufgabe 2: {out2}");
            Console.WriteLine($"Benötigte Zeit: {ts2.ToString()}");
        }
    }
}
