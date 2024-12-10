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

        public static char[][] CreateCharMap(List<string> lines)
        {
            char[][] charMap = new char[lines.Count][];
            for (int i = 0; i < lines.Count; i++) {
                charMap[i] = lines[i].ToCharArray();
            }
            return charMap;
        }

        internal static int[][] CreateIntMap(List<string> lines)
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
    }
}
