using AOC._2025;
using System.Linq;
using System.Reflection;

namespace AOC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Advent of Code 2025 ===");

            var allSolvers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ISolver).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t => (ISolver)Activator.CreateInstance(t))
                .ToList();

            if (!allSolvers.Any())
            {
                Console.WriteLine("Keine Solver gefunden!");
                return;
            }

            // Verfügbare Jahre anzeigen
            var years = allSolvers.Select(s => s.Year).Distinct().OrderBy(y => y).ToList();

            Console.WriteLine("Verfügbare Jahre:");
            foreach (var y in years)
                Console.WriteLine($" - {y}");

            Console.Write("Welches Jahr soll ausgeführt werden? ");
            if (!uint.TryParse(Console.ReadLine(), out uint year) || !years.Contains(year))
            {
                Console.WriteLine("Ungültiges Jahr.");
                return;
            }

            // Solver für Jahr filtern
            var solvers = allSolvers.Where(s => s.Year == year).OrderBy(s => s.Day).ToList();

            Console.Write("Welcher Tag soll ausgeführt werden (1–25)? ");
            if (!uint.TryParse(Console.ReadLine(), out uint day))
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }

            var solver = solvers.FirstOrDefault(s => s.Day == day);
            if (solver == null)
            {
                Console.WriteLine($"Kein Solver für Tag {day} gefunden.");
                return;
            }

            // Input Datei laden
            string inputPath = Path.Combine(year.ToString(), "Inputs", $"Day{day}.txt");
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input-Datei fehlt: {inputPath}");
                return;
            }

            string input = File.ReadAllText(inputPath).TrimEnd();

            Console.WriteLine($"\n--- Day {solver.Day}: {solver.Title} ---");

            // Part 1
            try
            {
                var result1 = solver.SolvePart1(input);
                Console.WriteLine($"Part 1: {result1}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Part 1: Noch nicht implementiert.");
            }

            // Part 2
            try
            {
                var result2 = solver.SolvePart2(input);
                Console.WriteLine($"Part 2: {result2}");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Part 2: Noch nicht implementiert.");
            }

            Console.WriteLine("\nFertig!");
        }
    }
}
