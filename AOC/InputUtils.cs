using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public static class InputUtils
    {

        public static List<string> ReadLines(string path)
        {
            return File.ReadLines(path).ToList();
        }

        public static string ReadString(string path)
        {
            return File.ReadAllText(path);
        }

        public static char[][] CreateCharMap(List<string> lines)
        {
            char[][] charMap = new char[lines.Count][];
            for (int i = 0; i < lines.Count; i++) {
                charMap[i] = lines[i].ToCharArray();
            }
            return charMap;
        }

        public static char[,] CreateCharMap2(List<string> lines)
        {
            char[,] charMap = new char[lines.Count, lines[0].Length];

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    charMap[i, j] = lines[i][j];
                }
            }
            return charMap;
        }

        public static int[][] CreateIntMap(List<string> lines)
        {
            int[][] intMap = new int[lines.Count][];
            for (int i = 0; i < lines.Count; i++)
            {
                intMap[i] = new int[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    intMap[i][j] = int.Parse("" + lines[i][j]);
                }
            }
            return intMap;
        }

        public static int[,] CreateIntMap2(List<string> lines)
        {
            int[,] intMap = new int[lines.Count, lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    intMap[i, j] = int.Parse(lines[i][j].ToString());
                }
            }
            return intMap;
        }

    }
}
