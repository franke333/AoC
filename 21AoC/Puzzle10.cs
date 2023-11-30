using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AoC2021
{
    class Puzzle10
    {
        static Tuple<char,Stack<int>> GetErrors(string line)
        {
            List<char> openBrackets = new List<char>() { '(','[','{','<' };
            List<char> closeBrackets = new List<char>() { ')', ']', '}', '>' };
            Stack<int> indexStack = new Stack<int>();
            foreach(char c in line)
            {
                int index;
                if ((index = openBrackets.FindIndex(x => x == c)) != -1)
                    indexStack.Push(index);
                else if (closeBrackets.FindIndex(x => x == c) != indexStack.Pop())
                    return new Tuple<char,Stack<int>>(c,indexStack);
            }
            return new Tuple<char, Stack<int>>('_',indexStack);
        }

        static int GetScore(char c)
        {
            switch (c)
            {
                case ')': return 3;
                case ']': return 57;
                case '}': return 1197;
                case '>': return 25137;
                default : return 0;
            }
        }

        static long GetScore(Stack<int> q)
        {
            long sum = 0;
            foreach (int i in q)
                sum = sum * 5 + i + 1;
            return sum;
        }

        public static void SolveBoth()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle10Input.txt");
            List<long> scores = new List<long>();
            int errorSum = 0;
            while (!sr.EndOfStream)
            {
                var result = GetErrors(sr.ReadLine());
                errorSum += GetScore(result.Item1);
                if (result.Item1 == '_')
                    scores.Add(GetScore(result.Item2));
            }
            Console.WriteLine($"Part one: {errorSum}");
            Console.WriteLine("Part two: {0}",scores.OrderBy(x => x).ElementAt(scores.Count / 2));
        }
    }
}
