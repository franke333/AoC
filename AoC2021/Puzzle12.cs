using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2021
{
    class Puzzle12
    {
        class Cave
        {
            public static Dictionary<string, Cave> dict = new Dictionary<string, Cave>();
            public static int paths = 0;

            string name;
            bool visited, end;
            bool isSmall { get => name[0] >= 'a'; }
            public List<Cave> neighbor = new List<Cave>();

            public Cave(string name)
            {
                this.name = name;
                if (name == "end")
                    end = true;
            }

            public void Visit(bool smallSecondVisited)
            {
                bool thisIstheSecondVisit = false;
                if(visited && !smallSecondVisited)
                {
                    smallSecondVisited = true;
                    thisIstheSecondVisit = true;
                }
                else if (visited) return;
                if (end)
                {
                    paths++;
                    return;
                }
                visited = isSmall;
                foreach (var n in neighbor)
                    n.Visit(smallSecondVisited);
                visited = thisIstheSecondVisit;
            }
        }
        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle12Input.txt");
            while (!sr.EndOfStream)
            {
                string[] caves = sr.ReadLine().Split('-');
                foreach (int i in new int[] { 0, 1 })
                    if (!Cave.dict.ContainsKey(caves[i])) 
                        Cave.dict.Add(caves[i], new Cave(caves[i]));
                Cave.dict[caves[0]].neighbor.Add(Cave.dict[caves[1]]);
                Cave.dict[caves[1]].neighbor.Add(Cave.dict[caves[0]]);
            }
            Cave start = Cave.dict["start"];
            foreach (var n in start.neighbor)
                n.neighbor.Remove(start);
            start.Visit(true);
            Console.WriteLine($"part1: {Cave.paths}");
            Cave.paths = 0;
            start.Visit(false);
            Console.WriteLine($"part2: {Cave.paths}");
        }
    }
}
