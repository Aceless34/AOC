using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    class Day6 : Day
    {
        public string Task1()
        {
            char[][] map = InputUtils.CreateCharMap(InputUtils.ReadLines("2024/Files/Day6.txt"));
            List<string> visited = new List<string>();

            int posX = 70;
            int posY = 59;
            int positions = 1;
            int[] direction = [0, -1];
            visited.Add($"{posX}:{posY}");

            while (true)
            {
                int nextX = posX + direction[0];
                int nextY = posY + direction[1];

                if( nextX < 0 || nextY < 0 || nextX >= map[0].Length || nextY >= map.Length) 
                    break;

                //Console.Write($"{nextX}:{nextY}");

                char next = map[nextY][nextX];
                if(next == '#')
                {
                    //turn right
                    direction = rotate90Right(direction);
                    //Console.Write($"\t{positions}");
                    //Console.WriteLine("\tTurn");
                }
                else
                {
                    if (!visited.Contains($"{nextX}:{nextY}"))
                    {
                        positions++;
                        visited.Add($"{nextX}:{nextY}");
                    }
                    posX = nextX;
                    posY = nextY;
                    //Console.WriteLine("\tStep");
                }


            }

            //LogMap(map, visited);

            return visited.Count.ToString();
        }

        private void LogMap(char[][] map, List<string> visited)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for(int x = 0; x < map[y].Length; x++)
                {
                    if (visited.Contains($"{x}:{y}"))
                    {
                        Console.Write("X");
                    }
                    else
                        Console.Write(map[y][x]);
                }
                Console.WriteLine();
            }
        }

        public static int[] rotate90Right(int[] dir)
        {
            return new int[] { -dir[1], dir[0] };
        }

        static char[][] CopyJaggedCharArray(char[][] array)
        {
            char[][] copy = new char[array.Length][];

            for (int i = 0; i < array.Length; i++)
            {
                // Erstelle ein neues Unterarray mit derselben Länge
                copy[i] = new char[array[i].Length];

                // Kopiere die Elemente
                for (int j = 0; j < array[i].Length; j++)
                {
                    copy[i][j] = array[i][j];
                }
            }

            return copy;
        }

        public string Task2()
        {
            char[][] map = InputUtils.CreateCharMap(InputUtils.ReadLines("2024/Files/Day6.txt"));
            

            int loops = 0;
            int count = 0;
            

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if(x == 70 && y == 59)
                    {
                        continue;
                    }
                    count++;
                    Console.WriteLine($"{count}/{map.Length * map[0].Length}");

                    //place the obstacle
                    char[][] mapCopy = CopyJaggedCharArray(map);
                    mapCopy[y][x] = '#';
                    int posX = 70;
                    int posY = 59;
                    //int posX = 4;
                    //int posY = 5;
                    int positions = 1;
                    int[] direction = [0, -1];
                    int loopingPos = 0;
                    List<string> visited = new List<string>();
                    visited.Add($"{posX}:{posY}");

                    bool loop = true;
                    while (true)
                    {
                        int nextX = posX + direction[0];
                        int nextY = posY + direction[1];

                        if (nextX < 0 || nextY < 0 || nextX >= mapCopy[0].Length || nextY >= mapCopy.Length)
                        {
                            loop = false;
                            break;
                        }

                        //Console.Write($"{nextX}:{nextY}");

                        char next = mapCopy[nextY][nextX];
                        if (next == '#')
                        {
                            //turn right
                            direction = rotate90Right(direction);
                            //Console.Write($"\t{positions}");
                            //Console.WriteLine("\tTurn");
                        }
                        else
                        {
                            if (!visited.Contains($"{nextX}:{nextY}"))
                            {
                                positions++;
                                visited.Add($"{nextX}:{nextY}");
                                loopingPos = 0;
                            }
                            else
                            {
                                loopingPos++;
                            }
                            posX = nextX;
                            posY = nextY;

                            if( loopingPos > 1160)
                            {
                                //loop
                                loop = true;
                                break;
                            }
                            //Console.WriteLine("\tStep");
                        }
                    }

                    if (loop)
                    {
                        //LogMap(mapCopy, visited);
                        
                        loops++;
                    }
                
                }
            }


            return loops.ToString();
        }
    }
}
