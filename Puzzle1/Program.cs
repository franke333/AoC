using System;

namespace Puzzle1
{
    
    class Program
    {
        static PuzzleType activePuzzle = PuzzleType.SubTravel2;
        enum PuzzleType
        {
            ScanDepth,ScanDepth2,
            SubTravel, SubTravel2,


        }
        static void Main(string[] args)
        {


            switch (activePuzzle)
            {
                case PuzzleType.ScanDepth:
                    Puzzle1.SolvePuzzle();
                    break;
                case PuzzleType.ScanDepth2:
                    Puzzle1.SolvePuzzle2();
                    break;
                case PuzzleType.SubTravel:
                    Puzzle2.SolvePuzzle();
                    break;
                case PuzzleType.SubTravel2:
                    Puzzle2.SolvePuzzle2();
                    break;
                default:
                    break;
            }
        }
    }
}
