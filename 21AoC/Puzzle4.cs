using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Puzzle1
{
    class Puzzle4
    {
        class Bingo
        {
            int[,] values;
            bool[,] marked;
            public Bingo()
            {
                values = new int[5, 5];
                marked = new bool[5, 5];
            }

            public void ReadFromInput(StreamReader sr)
            {
                for (int i = 0; i < 5; i++)
                {
                    string line = sr.ReadLine();
                    List<int> lineVals = new List<int>();
                    foreach (string s in line.Split(' '))
                        if (s != "") lineVals.Add(int.Parse(s));
                    for (int j = 0; j < 5; j++)
                    {
                        values[i, j] = lineVals[j];
                    }
                }
            }
            //return true if Won
            public bool Mark(int val)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (values[i, j] == val)
                        {
                            marked[i, j] = true;
                            return IsBingo();
                        }
                    }
                }
                return false;
            }

            private bool IsBingo()
            {
                //rows
                bool allMarked;
                for (int i = 0; i < 5; i++)
                {
                    allMarked = true;
                    for (int j = 0; j < 5; j++)
                    {
                        allMarked = marked[i, j] && allMarked;
                    }
                    if (allMarked)
                        return true;
                }

                //colls
                for (int i = 0; i < 5; i++)
                {
                    allMarked = true;
                    for (int j = 0; j < 5; j++)
                    {
                        allMarked = marked[j,i] && allMarked;
                    }
                    if (allMarked)
                        return true;
                }
                return false;
            }

            public int GetSumOfUnmarked()
            {
                int sum = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (!marked[i, j])
                            sum += values[i, j];
                    }
                }

                return sum;
            }
        }
        static public void SolvePuzzle()
        {
            Queue<int> valQueue = new Queue<int>();
            List<Bingo> bingos = new List<Bingo>();

            StreamReader sr = new StreamReader("inputs/Puzzle4Input.txt");
            string valLine = sr.ReadLine();
            foreach(string s in valLine.Split(','))
            {
                valQueue.Enqueue(int.Parse(s));
            }
            while (!sr.EndOfStream)
            {
                string emptyLine = sr.ReadLine();
                Bingo bingo = new Bingo();
                bingo.ReadFromInput(sr);
                bingos.Add(bingo);
            }
            int drawn = 0;
            int winSum = -1;
            while (valQueue.Count != 0)
            {
                drawn = valQueue.Dequeue();
                Console.WriteLine($"drawn {drawn}");
                foreach(Bingo bingo in bingos)
                {
                    if (bingo.Mark(drawn))
                        winSum = bingo.GetSumOfUnmarked();
                    if (winSum!=-1)
                        break;
                }
                if (winSum != -1)
                    break;
            }
            Console.WriteLine("=======");
            Console.WriteLine(winSum*drawn);

        }

        static public void SolvePuzzle2()
        {
            Queue<int> valQueue = new Queue<int>();
            List<Bingo> bingos = new List<Bingo>();

            StreamReader sr = new StreamReader("inputs/Puzzle4Input.txt");
            string valLine = sr.ReadLine();
            foreach (string s in valLine.Split(','))
            {
                valQueue.Enqueue(int.Parse(s));
            }
            while (!sr.EndOfStream)
            {
                string emptyLine = sr.ReadLine();
                Bingo bingo = new Bingo();
                bingo.ReadFromInput(sr);
                bingos.Add(bingo);
            }
            int drawn = 0;
            int winSum = -1;
            while (bingos.Count!=1)
            {
                drawn = valQueue.Dequeue();
                Console.WriteLine($"drawn {drawn}");
                for (int i = 0; i < bingos.Count; i++)
                {
                    if (bingos[i].Mark(drawn))
                        bingos.RemoveAt(i--);
                }
            }
            drawn = valQueue.Dequeue();
            while (!bingos[0].Mark(drawn))
                drawn = valQueue.Dequeue();
            Console.WriteLine("=======");
            Console.WriteLine(bingos[0].GetSumOfUnmarked() * drawn);
        }
    }
}
