using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    internal class Day8 : Day
    {
        public string Task1()
        {
            char[][] charMap = InputUtils.CreateCharMap(InputUtils.ReadLines("2024/Files/Day8.txt"));

            Dictionary<char, List<int[]>> antennaPositions = new Dictionary<char, List<int[]>>();

            for(int y = 0; y < charMap.Length; y++)
            {
                for(int x = 0; x < charMap[y].Length; x++)
                {
                    if(charMap[y][x] != '.')
                    {
                        if (!antennaPositions.ContainsKey(charMap[y][x]))
                            antennaPositions[charMap[y][x]] = new List<int[]>();
                        antennaPositions[charMap[y][x]].Add([x, y]);
                    }
                }
            }

            List<string> antinodePositions = new List<string>();

            foreach(char antenna in antennaPositions.Keys)
            {
                List<int[]> positions = antennaPositions[antenna];

                for (int i = 0; i < positions.Count; i++)
                {
                    for (int j = 0; j < positions.Count; j++)
                    {
                        if(i == j ) continue;

                        //calculate distance
                        int[] dist = [positions[i][0] - positions[j][0], positions[i][1] - positions[j][1]];

                        //antinodes
                        int[] antinode1 = [positions[i][0] + dist[0], positions[i][1] + dist[1]];
                        //int[] antinode2 = [positions[i][0] - dist[0], positions[i][1] - dist[1]];

                        if (antinode1[0] < charMap.Length && antinode1[0] >= 0 && antinode1[1] < charMap[0].Length && antinode1[1] >= 0)
                        {
                            if (charMap[antinode1[1]][antinode1[0]] != '#')
                            {
                                string posKey = $"{antinode1[0]}:{antinode1[1]}";
                                if(!antinodePositions.Contains(posKey))
                                    antinodePositions.Add(posKey);
                            }
                            if (charMap[antinode1[1]][antinode1[0]] == '.')
                            {
                                charMap[antinode1[1]][antinode1[0]] = '#';
                            }
                        }
                        /*
                        if (antinode2[0] < charMap.Length && antinode2[0] >= 0 && antinode2[1] < charMap[0].Length && antinode2[1] >= 0)
                        {
                            if (charMap[antinode2[1]][antinode2[0]] == '.')
                            {
                                charMap[antinode2[1]][antinode2[0]] = '#';
                            }
                                antinodes++;
                        }
                        */

                    }
                }
            }

            LogMap(charMap);    

            return antinodePositions.Count.ToString();
        }

        private void LogMap(char[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    Console.Write(map[y][x]);
                }
                Console.WriteLine();
            }
        }

        public string Task2()
        {
            char[][] charMap = InputUtils.CreateCharMap(InputUtils.ReadLines("2024/Files/Day8.txt"));

            Dictionary<char, List<int[]>> antennaPositions = new Dictionary<char, List<int[]>>();

            for (int y = 0; y < charMap.Length; y++)
            {
                for (int x = 0; x < charMap[y].Length; x++)
                {
                    if (charMap[y][x] != '.')
                    {
                        if (!antennaPositions.ContainsKey(charMap[y][x]))
                            antennaPositions[charMap[y][x]] = new List<int[]>();
                        antennaPositions[charMap[y][x]].Add([x, y]);
                    }
                }
            }

            List<string> antinodePositions = new List<string>();

            foreach (char antenna in antennaPositions.Keys)
            {
                List<int[]> positions = antennaPositions[antenna];

                for (int i = 0; i < positions.Count; i++)
                {
                    for (int j = 0; j < positions.Count; j++)
                    {
                        if (i == j) continue;

                        //calculate distance
                        int[] dist = [positions[i][0] - positions[j][0], positions[i][1] - positions[j][1]];

                        //antinodes
                        int[] antinode1 = [positions[i][0] - dist[0], positions[i][1] - dist[1]];

                        while (antinode1[0] < charMap.Length && antinode1[0] >= 0 && antinode1[1] < charMap[0].Length && antinode1[1] >= 0)
                        {
                            if (charMap[antinode1[1]][antinode1[0]] != '#')
                            {
                                string posKey = $"{antinode1[0]}:{antinode1[1]}";
                                if (!antinodePositions.Contains(posKey))
                                    antinodePositions.Add(posKey);
                            }
                            if (charMap[antinode1[1]][antinode1[0]] == '.')
                            {
                                charMap[antinode1[1]][antinode1[0]] = '#';
                            }

                            antinode1 = [antinode1[0] - dist[0], antinode1[1] - dist[1]];
                        }

                    }
                }
            }

            LogMap(charMap);

            return antinodePositions.Count.ToString();
        }
    }
}
