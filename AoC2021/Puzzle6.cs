using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2021
{
    class Puzzle6
    {
        private static void CalculateFishAfter(int days)
        {
            long[] fish = new long[7];
            long[] freshlyBornFish = new long[7];
            StreamReader sr = new StreamReader("inputs/Puzzle6Input.txt");
            foreach (var s in sr.ReadToEnd().Split(','))
                fish[int.Parse(s)]++;
            for (int i = 0; i <= days; i++)
            {
                freshlyBornFish[(i + 2) % 7] = fish[i % 7];
                fish[i % 7] += freshlyBornFish[i % 7];
            }
            long sum = freshlyBornFish[(days+1)%7];
            foreach (long f in fish) sum += f;

            Console.WriteLine(sum);
        }
        public static void SolvePuzzle()
        {
            CalculateFishAfter(80);
        }

        public static void SolvePuzzle2()
        {
            CalculateFishAfter(256);
        }
    }
}
