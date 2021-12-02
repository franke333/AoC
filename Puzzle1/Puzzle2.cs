using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle1
{
    class Puzzle2
    {
        
        static public void SolvePuzzle()
        {
            int depth = 0;
            int horizont = 0;
            string line;
            while ((line = Console.ReadLine()) != "")
            {
                string[] args = line.Split(' ');
                int value = int.Parse(args[1]);
                switch (args[0][0])
                {
                    case 'f': //forward
                        horizont += value;
                        break;
                    case 'u': //up
                        depth -= value;
                        break;
                    case 'd': //down
                        depth += value;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("=========");
            Console.WriteLine(depth*horizont);

        }

        static public void SolvePuzzle2()
        {
            int depth = 0;
            int horizont = 0;
            int aim = 0;
            string line;
            while ((line = Console.ReadLine()) != "")
            {
                string[] args = line.Split(' ');
                int value = int.Parse(args[1]);
                switch (args[0][0])
                {
                    case 'f': //forward
                        horizont += value;
                        depth += aim * value;
                        break;
                    case 'u': //up
                        aim -= value;
                        break;
                    case 'd': //down
                        aim += value;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("=========");
            Console.WriteLine(depth * horizont);
        }
    }
}
