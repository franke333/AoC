using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2021
{
    public static class Puzzle8Extensions
    {
        public static string Without(this string a, string b)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in a)
                if (!b.Contains(c))
                    sb.Append(c);
            return sb.ToString();
        }

        public static bool ContainsAll(this string a, string b)
        {
            foreach (char c in b)
                if (!a.Contains(c))
                    return false;
            return true;
        }

        public static bool IsSameAs(this string a, string b)
        {
            if (a.Length != b.Length)
                return false;
            foreach (char c in b)
            {
                if (!a.Contains(c))
                    return false;
            }
            return true;
        }
    }
    class Puzzle8
    {
        public static void SolvePuzzle()
        {

            StreamReader sr = new StreamReader("inputs/Puzzle8Input.txt");
            int sum = 0;
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(" | ");
                string[] outputs = line[1].Split(" ");
                foreach(var output in outputs)
                {
                    int l = output.Length;
                    if (l != 5 && l != 6)
                        sum++;
                }
            }
            Console.WriteLine(sum);
        }
        public static void SolvePuzzle2()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle8Input.txt");
            long sum = 0;
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(" | ");
                string[] inputs = line[0].Split(' ');
                string[] outputs = line[1].Split(" ");
                string[] nums = new string[10];
                List<string> fives = new List<string>();
                List<string> sixes = new List<string>();
                foreach(var inp in inputs)
                {
                    switch (inp.Length)
                    {
                        case 2:
                            nums[1] = inp;
                            break;
                        case 3:
                            nums[7] = inp;
                            break;
                        case 4:
                            nums[4] = inp;
                            break;
                        case 7:
                            nums[8] = inp;
                            break;
                        case 5:
                            fives.Add(inp);
                            break;
                        case 6:
                            sixes.Add(inp);
                            break;
                        default:
                            Console.WriteLine("ERROR: WRONG segLength");
                            break;
                    }
                }
                foreach (string six in sixes)
                    if (!six.ContainsAll(nums[1]))
                        nums[6] = six;
                sixes.Remove(nums[6]);
                foreach (string nine in sixes)
                    if (nine.ContainsAll(nums[4]))
                        nums[9] = nine;
                sixes.Remove(nums[9]);
                nums[0] = sixes[0];
                foreach (string three in fives)
                    if (three.ContainsAll(nums[1]))
                        nums[3] = three;
                fives.Remove(nums[3]);
                if (nums[6].Without(fives[0]).Length == 1) {
                    nums[5] = fives[0];
                    nums[2] = fives[1];
                } // ==2 -> fives[0] contains 2
                else
                {
                    nums[5] = fives[1];
                    nums[2] = fives[0];
                }

                int GetDigit(string s)
                {
                    for (int i = 0; i < 10; i++)
                        if (nums[i].IsSameAs(s))
                            return i;
                    throw new Exception("bruh");
                }

                int num = 0;
                for (int i = 0; i < 4; i++)
                {
                    num *= 10;
                    num += GetDigit(outputs[i]);
                }
                sum += num;
            }
            Console.WriteLine(sum);

        }
    }
}
