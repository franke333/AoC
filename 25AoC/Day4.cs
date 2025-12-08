using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day4
    {

        public static void Execute()
        {
            var sr = new StreamReader("inputs/Input4.txt");
            var input = sr.ReadToEnd().Split("\n").Select(l => l.Trim()).ToList();
            int h = input.Count(); int w = input[0].Length;
            int[,] paper = new int[h,w];
            int rolls = 0;
            for(int i = 0; i < h; i++)
                for(int j = 0; j < w; j++)
                    if(input[i][j] == '.')
                        paper[i,j] = 99;
                    else
                    {
                        paper[i, j] -= 4;
                        rolls++;
                        for (int k = -1; k <= 1; k++)
                            for(int l = -1; l <= 1; l++)
                                if(i + k >= 0 && i + k < h && j + l >= 0 && j + l < w)
                                    paper[i+k, j+l]++;
                    }
            Console.WriteLine(paper.Cast<int>().Sum(a => a <= 0 ? 1 : 0));
            int removedTot = 0;
            while (rolls > 0)
            {
                int removed = 0;
                for (int i = 0; i < h; i++)
                    for (int j = 0; j < w; j++)
                        if (paper[i,j] <= 0)
                        {
                            paper[i,j] = 99;
                            rolls--;
                            removed++;
                            for (int k = -1; k <= 1; k++)
                                for (int l = -1; l <= 1; l++)
                                    if (i + k >= 0 && i + k < h && j + l >= 0 && j + l < w)
                                        paper[i + k, j + l]--;
                        }
                if (removed == 0)
                    break;
                removedTot += removed;
            }
            Console.WriteLine(removedTot);

        }
    }
}
