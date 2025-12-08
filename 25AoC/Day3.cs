using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day3
    {
        public static void Execute(bool advanced = true)
        {
            var sr = new StreamReader("inputs/Input3.txt");
            long joltage = 0;
            int turnOn = advanced ? 12 : 2;
            foreach(var line in sr.ReadToEnd().Split("\n").Select(l => l.Trim()))
            {
                long newJoltage = 0;
                int lastIndex = 0;
                for (int i = 0; i < turnOn; i++)
                {
                    var subline = line[lastIndex..^(turnOn-i-1)];
                    int largest = subline.IndexOf(subline.Max());
                    newJoltage = newJoltage * 10 + (subline[largest] - '0');
                    lastIndex += largest + 1;
                }
                joltage += newJoltage;
            }
            Console.WriteLine(joltage);
        }
    }
}
