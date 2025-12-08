using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day6
    {
        public static void Execute()
        {
            List<string> lines = new();
            StreamReader sr = new StreamReader("inputs/Input6.txt");
            string? line;
            for (line = sr.ReadLine(); line != null; line = sr.ReadLine())
                lines.Add(line);

            long sum = 0; long currOpSum = 0; bool mult = false;
            for (int i = 0; i < lines[^1].Length; i++)
            {
                if (lines[^1][i] != ' ')
                {
                    sum += currOpSum;
                    mult = lines[^1][i] == '*';
                    currOpSum = mult ? 1 : 0;
                }
                long n = lines[..^1].Aggregate(0L, (acc, l) => l[i] == ' ' ? acc : acc*10 + (l[i] - '0'));
                currOpSum = mult ? (currOpSum * Math.Max(n,1L)) : currOpSum + n;
            }
            Console.WriteLine(sum + currOpSum);
        }

        public static void ExecuteSimple()
        {
            List<List<int>> numbers = new();
            List<char> op = new();
            StreamReader sr = new StreamReader("inputs/Input6.txt");
            string? line;
            for (line = sr.ReadLine(); !sr.EndOfStream; line = sr.ReadLine())
            {
                numbers.Add(new List<int>());
                foreach (var l in line.Split(' '))
                    if(l!="")
                        numbers[^1].Add(int.Parse(l));
            }
            
            foreach (var l in line.Split(' '))
                if (l != "")
                    op.Add(l[0]);
            long sum = 0;
            for (int i = 0; i < op.Count; i++)
                sum += op[i] switch
                {
                    '+' => numbers.Sum(n => n[i]),
                    '*' => numbers.Aggregate(1L, (acc, n) => acc * n[i]),
                };
            Console.WriteLine(sum);
        }
    }
}
