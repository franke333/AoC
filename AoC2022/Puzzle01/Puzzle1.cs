using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2022
{
    class Puzzle1
    {
        static public void SolvePuzzle()
        {
            string puzzePath = "inputs/input1.txt";
            StreamReader sr = new StreamReader(puzzePath);
            LinkedList<uint> max_elfs = new LinkedList<uint>();
            for (int i = 0; i < 3; i++)
                max_elfs.AddLast(0);
            string input;
            while (!sr.EndOfStream)
            {
                uint sum = 0;
                while ((input = sr.ReadLine()) != "" && input != null)
                    sum += uint.Parse(input);
                LinkedListNode<uint> current = max_elfs.First;
                while(current != null)
                {
                    if (sum > current.Value)
                    {
                        max_elfs.AddBefore(current, sum);
                        max_elfs.RemoveLast();
                        break;
                    }
                    current = current.Next;
                }
            }
            Console.WriteLine($"1st elf   : {max_elfs.First.Value}");
            Console.WriteLine($"top 3 elfs: {max_elfs.Sum(n => n)}");
        }
    }
}
