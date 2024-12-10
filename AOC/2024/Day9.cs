using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day9 : Day
    {
        class MemBlock
        {
            public int id;
            public int size;
        }

        public string Task1()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day9.txt");
            List<MemBlock> memory = new List<MemBlock>();

            int memId = 0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                int num = int.Parse(lines[0][i].ToString());
                memory.Add(new MemBlock
                {
                    id = i % 2 == 0? memId++ : -1,
                    size = num,
                });
            }

            while (SpaceBetween(memory))
            {
                int blockIdx = memory.LastIndexOf(memory.Where(m => m.id != -1).Last());
                MemBlock block = memory[blockIdx];
                if (block.id == -1) continue;

                int freeBlockIdx = memory.IndexOf(memory.Where(m => m.id == -1).First());
                MemBlock spaceBlock = memory[freeBlockIdx];

                if(spaceBlock.size > block.size)
                {
                    spaceBlock.id = block.id;
                    block.id = -1;
                    int open = spaceBlock.size - block.size;
                    spaceBlock.size = block.size;

                    memory.Insert(freeBlockIdx+1, new MemBlock
                    {
                        id = -1,
                        size = open
                    });
                }else if(spaceBlock.size < block.size)
                {
                    spaceBlock.id = block.id;
                    block.size = block.size - spaceBlock.size;
                    //einfügen von space memory
                    memory.Insert(blockIdx+1, new MemBlock
                    {
                        id = -1,
                        size = spaceBlock.size
                    });
                }
                else
                {
                    spaceBlock.id = block.id;
                    block.id = -1;
                }

                //LogMemory(memory);
            }

            long checksum = CalculateChecksum(memory);

            return checksum.ToString();
        }

        private long CalculateChecksum(List<MemBlock> memory)
        {
            int mult = 0;
            long sum = 0;
            foreach (MemBlock block in memory)
            {
                for (int i = 0; i < block.size; i++)
                {
                    int m = block.id == -1 ? 0 : mult;
                    sum += m * block.id;
                    mult++;
                }
            }

            return sum;
        }

        private bool SpaceBetween(List<MemBlock> memory)
        {
            bool space = false;
            for(int i = 0; i < memory.Count; i++)
            {
                if (space && memory[i].id != -1)
                {
                    return true;
                }
                if (memory[i].id == -1) space = true;
            }
            return false;
        }

        private void LogMemory(List<MemBlock> memory)
        {
            foreach (MemBlock block in memory)
            {
                for(int i = 0; i < block.size; i++)
                {
                    Console.Write(block.id == -1 ? "." : block.id);
                }
            }
            Console.WriteLine();
        }

        public string Task2()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day9.txt");
            List<MemBlock> memory = new List<MemBlock>();

            int memId = 0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                int num = int.Parse(lines[0][i].ToString());
                memory.Add(new MemBlock
                {
                    id = i % 2 == 0 ? memId++ : -1,
                    size = num,
                });
            }

            long checksum = CalculateChecksum(memory);
            long chesumOld = checksum+1;

            int highestId = memory[memory.LastIndexOf(memory.Where(m => m.id != -1).Last())].id;
            int lastMemoryId = highestId;
            while (lastMemoryId != 0)
            {
                MemBlock block = memory.Where(m => m.id == lastMemoryId).First();
                lastMemoryId = lastMemoryId - 1;
                if (lastMemoryId < 0) lastMemoryId = highestId;
                if (block.id == -1) continue;

                MemBlock spaceBlock = memory.Where(m => m.size >= block.size && m.id == -1).FirstOrDefault();
                if (spaceBlock == null) continue;

                int freeBlockIdx = memory.IndexOf(spaceBlock);
                if(freeBlockIdx < memory.IndexOf(block))
                {
                    if (spaceBlock.size > block.size)
                    {
                        spaceBlock.id = block.id;
                        block.id = -1;
                        int open = spaceBlock.size - block.size;
                        spaceBlock.size = block.size;

                        memory.Insert(freeBlockIdx + 1, new MemBlock
                        {
                            id = -1,
                            size = open
                        });
                    }
                    else
                    {
                        spaceBlock.id = block.id;
                        block.id = -1;
                    }
                }

                //Console.Write($"{lastMemoryId+1}: ");
                //LogMemory(memory);
                chesumOld = checksum;
                checksum = CalculateChecksum(memory);

            }

            
            checksum = CalculateChecksum(memory);
            return checksum.ToString();
        }
    }
}
