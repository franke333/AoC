using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;


namespace AoC2021
{
    class Puzzle21
    {
        static class Dice
        {
            static int lastThrow = 0;
            public static int throwCount = 0;
            public static int ThrowDice()
            {
                throwCount++;
                lastThrow %= 100;
                return ++lastThrow;
            }
        }

        static int Move(ref int standingAt)
        {
            for (int i = 0; i < 3; i++)
                standingAt = (standingAt + Dice.ThrowDice()) % 10;
            return standingAt + 1; //0-9 -> 1-10
        }

        struct GameState
        {
            public int p1At, p2At, p1Score, p2Score;
            public bool p1Turn;

            public GameState(int p1At, int p2At, int p1Score, int p2Score,bool p1Turn)
            {
                this.p1At = p1At;
                this.p2At = p2At;
                this.p1Score = p1Score;
                this.p2Score = p2Score;
                this.p1Turn = p1Turn;
            }

            public override bool Equals(object obj)
             => obj is GameState g ? g.p1At == p1At && g.p2At == p2At &&
                g.p1Score == p1Score && g.p2Score == p2Score && g.p1Turn == p1Turn : false;

            public override int GetHashCode()
             => (p1At << 1) + (p2At << 4) + (p1Score << 7) + (p2Score << 20) + (p1Turn ? 1 : 0);
        }

        static Dictionary<GameState, (long, long)> metUniversa = new Dictionary<GameState, (long, long)>();
        // (rolled,universes)
        static (int, int)[] possibleRolls = new (int, int)[]
        {
                (3,1),(4,3),(5,6),(6,7),(7,6),(8,3),(9,1)

        };
        static (long, long) ProbeUniverse(GameState gs)
        {

            (long, long) results = (0, 0);
            if (metUniversa.TryGetValue(gs, out results))
                return results;
            if (gs.p1Turn)
                foreach (var (rolled, quantum) in possibleRolls)
                {
                    GameState newGs = gs;
                    newGs.p1At = (gs.p1At + rolled) % 10;
                    newGs.p1Score += newGs.p1At + 1;
                    newGs.p1Turn = false;
                    if (newGs.p1Score >= 21)
                        results.Item1 += quantum;
                    else
                    {
                        var (p1Won, p2Won) = ProbeUniverse(newGs);
                        results = (results.Item1 + p1Won * quantum, results.Item2 + p2Won * quantum);
                    }
                }
            else
            {
                foreach (var (rolled, quantum) in possibleRolls)
                {
                    GameState newGs = gs;
                    newGs.p2At = (gs.p2At + rolled) % 10;
                    newGs.p2Score += newGs.p2At + 1;
                    newGs.p1Turn = true;
                    if (newGs.p2Score >= 21)
                        results.Item2 += quantum;
                    else
                    {
                        var (p1Won, p2Won) = ProbeUniverse(newGs);
                        results = (results.Item1 + p1Won * quantum, results.Item2 + p2Won * quantum);
                    }
                }
            }

            metUniversa.Add(gs, results);
            return results;
        }


        public static void SolvePuzzle()
        {
            int input1 = 1, input2 = 5;

            int p1At = input1 -1, p2At = input2 -1, p1Score = 0, p2Score = 0;
            

            while (true)
            {
                p1Score += Move(ref p1At);
                if (p1Score >= 1000) break;
                p2Score += Move(ref p2At);
                if (p2Score >= 1000) break;
            }
            int lessScore = p1Score >= 1000 ? p2Score : p1Score;
            Console.WriteLine($"part1 {lessScore} * {Dice.throwCount} ={lessScore*Dice.throwCount}");


            
            var finalResult = ProbeUniverse(new GameState(input1-1, input2-1, 0, 0, true));
            Console.WriteLine($"won  {finalResult.Item1}\nlost {finalResult.Item2}");

        }
    }
}
