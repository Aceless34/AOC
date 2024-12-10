using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day4 : Day
    {
        public string Task1()
        {
            List<string> lines = File.ReadAllLines("2024/Files/Day4.txt").ToList();
            char[][] charMap = InputUtils.CreateCharMap(lines);

            int result = 0;
            for(int i = 0; i<charMap.Length; i++)
            {
                for(int j = 0; j < charMap[i].Length; j++)
                {
                    char c = charMap[i][j];
                    if(charMap[i][j] == 'X')
                    {
                        result += SearchXmas(charMap, i, j);
                    }
                }
            }

            return result.ToString();
        }

        private int SearchXmas(char[][] charMap, int i, int j)
        {
            int[][] dirs =
            {
                [0,-1],
                [1,-1],
                [1,0],
                [1,1],
                [0,1],
                [-1,1],
                [-1,0],
                [-1,-1]
            };
            char[] searchWord = new char[] { 'M', 'A', 'S' };
            int ret = 0;

            foreach (int[] dir in dirs)
            {
                bool found = false;
                for (int x = 0; x < 3; x++) {
                    int nextI = i + dir[0] + (x * dir[0]);
                    int nextJ = j + dir[1] + (x * dir[1]);
                    if (nextI < 0 || nextJ < 0 || nextI >= charMap.Length || nextJ >= charMap[nextI].Length)
                        break;
                    if (charMap[nextI][nextJ] != searchWord[x])
                        break;
                    if (x == 2 && charMap[nextI][nextJ] == searchWord[x])
                        found = true;
                }
                if (found) ret++;
            }
            return ret;
        }

        public string Task2()
        {
            List<string> lines = File.ReadAllLines("2024/Files/Day4.txt").ToList();
            char[][] charMap = InputUtils.CreateCharMap(lines);

            int result = 0;
            for (int i = 0; i < charMap.Length; i++)
            {
                for (int j = 0; j < charMap[i].Length; j++)
                {
                    char c = charMap[i][j];
                    if (charMap[i][j] == 'A')
                    {
                        try
                        {
                            result += SearchMasX(charMap, i, j);
                        }
                        catch (Exception e) { }
                    }
                }
            }

            return result.ToString();
        }

        private int SearchMasX(char[][] charMap, int i, int j)
        {
            char middle = charMap   [i]  [j];
            char rightUp = charMap  [i-1][j + 1];
            char rightDown = charMap[i+1][j + 1];
            char leftUp = charMap   [i-1][j - 1];
            char leftDown = charMap [i+1][j - 1];

            string cross1 = "" + rightUp + middle + leftDown;
            string cross2 = "" + leftUp + middle + rightDown;

            if( (cross1 == "MAS" || Reverse(cross1) == "MAS") && (cross2 == "MAS" || Reverse(cross2) == "MAS"))
            {
                return 1;
            }
            return 0;
        }



        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


    }
}
