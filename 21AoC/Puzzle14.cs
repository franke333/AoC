using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AoC2021
{
    class Puzzle14
    {
        class Polymer
        {
            public static Dictionary<string, Polymer> dict = new Dictionary<string, Polymer>();
            public static List<Polymer> list = new List<Polymer>();
            public static long[] count;
            public static long[] stepCount;
            public char fst, snd;
            public int index; //in list
            char creates;
            int newFst, newSnd;

            public Polymer(string line)
            {
                var strs = line.Split(" -> ");
                fst = strs[0][0];
                snd = strs[0][1];
                creates = strs[1][0];
                index = list.Count;
                list.Add(this);
                dict.Add(strs[0], this);
            }

            public void Connect()
            {
                newFst = dict[fst.ToString() + creates].index;
                newSnd = dict[creates.ToString() + snd].index;
            }

            public void Populate()
            {
                stepCount[newFst] += count[index];
                stepCount[newSnd] += count[index];
            }
        }
        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle14Input.txt");
            var input = sr.ReadLine();
            sr.ReadLine();
            while (!sr.EndOfStream)
                new Polymer(sr.ReadLine());
            foreach (var p in Polymer.list)
                p.Connect();
            Polymer.count = new long[Polymer.list.Count];
            for (int i = 0; i < input.Length - 1; i++)
                Polymer.count[Polymer.dict[input.Substring(i, 2)].index]++;
            for (int i = 0; i < 40; i++) //10 for part 1
            {
                Polymer.stepCount = new long[Polymer.list.Count];
                foreach (var p in Polymer.list)
                    p.Populate();
                Polymer.count = Polymer.stepCount;
            }
            long[] chars = new long['Z' - 'A' + 1];
            foreach(var p in Polymer.list)
            {
                chars[p.fst-'A'] += Polymer.count[p.index];
            }
            chars[input[input.Length - 1] - 'A']++; //last char is never fst
            Console.WriteLine(chars.Max()-chars.Min(x => x==0 ? long.MaxValue : x));
        }
    }
}
