using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle1
{
    class Puzzle1
    {
        int ReturnIncreases()
        {
            int prevDepth = int.MaxValue;
            string line;
            int count = 0; 
            while ((line = Console.ReadLine()) != "")
            {
                int depth = int.Parse(line);
                if (depth > prevDepth)
                    count++;
                prevDepth = depth;
            }
            return count;
        }

        int ReturnIncreases2()
        {
            string line;
            int[] array = new int[4];
            int count = 0;
            int i = 0;
            while ((line = Console.ReadLine()) != "")
            {
                int depth = int.Parse(line);
                array[i % 4] = depth;
                if (depth > array[++i % 4] && i >=4)
                    count++;
            }
            return count;
        }
        static public void SolvePuzzle()
        {
            Puzzle1 puzzle = new Puzzle1();
            var res = puzzle.ReturnIncreases();
            Console.WriteLine("=====");
            Console.WriteLine($"Part1: {res}");
            Console.WriteLine("=====");
        }

        static public void SolvePuzzle2()
        {
            Puzzle1 puzzle = new Puzzle1();
            var res = puzzle.ReturnIncreases2();
            Console.WriteLine("=====");
            Console.WriteLine($"Part2: {res}");
            Console.WriteLine("=====");
        }
    }
}
