using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2021
{
    class Puzzle11
    {
        class Octopus
        {
            public static Octopus[,] board;
            public static long flashes = 0;
            public int energy;
            bool flashedThisRound;
            bool fake; //border
            int x, y;

            public Octopus(int energy,int x,int y)
            {
                this.energy = energy;
                this.x = x;
                this.y = y;
                fake = false;
                flashedThisRound = false;
            }
            public Octopus()
            {
                fake = true;
            }
            private void Flash()
            {
                flashedThisRound=true;
                flashes++;
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        board[x + i, y + j].IncreaseEnergy();
            }
            public void IncreaseEnergy()
            {
                if (fake || flashedThisRound)
                    return;
                energy++;
                if (energy > 9)
                    Flash();
            }

            public void EndRound()
            {
                if (flashedThisRound)
                {
                    Console.Write('0');
                    flashedThisRound = false;
                    energy = 0;
                }
                else
                    Console.Write('-');
            }
        }
        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle11Input.txt");
            Octopus.board = new Octopus[12,12];

            for (int i = 0; i < 12; i++)
                if (i == 0 || i == 11)
                    for (int j = 0; j < 12; j++)
                        Octopus.board[i, j] = new Octopus(); //fake
                else
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < 12; j++)
                        if (j == 0 || j == 11)
                            Octopus.board[i, j] = new Octopus(); //fake
                        else
                            Octopus.board[i, j] = new Octopus((int)(line[j - 1]-'0'), i, j);
                }
            for (int steps = 0; steps < 100; steps++)
            {
                Console.WriteLine($"Step {steps}");
                for (int i = 1; i < 11; i++)
                    for (int j = 1; j < 11; j++)
                        Octopus.board[i, j].IncreaseEnergy();
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                        Octopus.board[i, j].EndRound();
                    Console.WriteLine();
                }
            }

            Console.WriteLine(Octopus.flashes);
        }
        public static void SolvePuzzle2()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle11Input.txt");
            Octopus.board = new Octopus[12, 12];

            for (int i = 0; i < 12; i++)
                if (i == 0 || i == 11)
                    for (int j = 0; j < 12; j++)
                        Octopus.board[i, j] = new Octopus(); //fake
                else
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < 12; j++)
                        if (j == 0 || j == 11)
                            Octopus.board[i, j] = new Octopus(); //fake
                        else
                            Octopus.board[i, j] = new Octopus((int)(line[j - 1] - '0'), i, j);
                }
            int steps = 0;
            while(Octopus.flashes!=100)
            {
                Octopus.flashes = 0;
                Console.WriteLine($"Step {++steps}");
                for (int i = 1; i < 11; i++)
                    for (int j = 1; j < 11; j++)
                        Octopus.board[i, j].IncreaseEnergy();
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                        Octopus.board[i, j].EndRound();
                    Console.WriteLine();
                }
            }

            Console.WriteLine(steps);
        }
    }
}
