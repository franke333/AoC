using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2021
{
    class Puzzle7
    {
       
        public static void SolvePuzzle()
        {
            string inputLine = new StreamReader("inputs/Puzzle7Input.txt").ReadLine();
            List<int> listInts = new List<int>();
            foreach (var s in inputLine.Split(',')) listInts.Add(int.Parse(s));
            int[] crabs = new int[listInts.Max()+1];
            foreach (var i in listInts) crabs[i]++;
            int prevdiff = int.MaxValue;
            for (int i = 0; i < crabs.Length; i++)
            {
                int diff = 0;
                for (int j = 0; j < crabs.Length; j++)
                    diff += (Math.Abs(i - j) * crabs[j]);
                if (prevdiff < diff)
                    break;
                prevdiff = diff;
            }
            Console.WriteLine(prevdiff);
        }

        public static void SolvePuzzle2()
        {
            string inputLine = new StreamReader("inputs/Puzzle7Input.txt").ReadLine();
            List<int> listInts = new List<int>();
            foreach (var s in inputLine.Split(',')) listInts.Add(int.Parse(s));
            int[] crabs = new int[listInts.Max() + 1];
            foreach (var i in listInts) crabs[i]++;
            long prevdiff = long.MaxValue;
            for (int i = 0; i < crabs.Length; i++)
            {
                long diff = 0;
                for (int j = 0; j < crabs.Length; j++)
                {
                    int n = Math.Abs(i - j);
                    diff += ((n+1)*n/2)*crabs[j];
                    //diff += (Math.Abs(i - j) * crabs[j]); for part one only
                }
                if (prevdiff < diff)
                    break;
                prevdiff = diff;
            }
            Console.WriteLine(prevdiff);
        }
    }
}
