using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Puzzle1
{
    class Puzzle3
    {
        public static void SolvePuzzle()
        {
            string input = Console.ReadLine();
            int linesize = input.Length;
            int[] buckets = new int[linesize];
            int lines = 0;
            while (input != "")
            {
                lines++;
                for (int i = 0; i < linesize; i++)
                {
                    if (input[i] == '1')
                        buckets[i]++;
                }
                input = Console.ReadLine();
            }
            int gamma=0, epsilon=0;
            for (int i = 0; i < linesize; i++)
            {
                gamma *= 2;
                epsilon *= 2;
                if (buckets[i] > lines / 2)
                    gamma++;
                else
                    epsilon++;

            }
            Console.WriteLine("===");
            Console.WriteLine(gamma*epsilon);

        }



        public static void SolvePuzzle2()
        {
            string puzzePath = "../../../Puzzle3Input.txt";
            StreamReader sr = new StreamReader(puzzePath);
            List<string> oxList = new List<string>(),
                coList = new List<string>();
            string input;
            while((input = sr.ReadLine()) != "")
            {
                oxList.Add(input);
                coList.Add(input);
            }
            int linesize = 12;
            string oxygen="";
            string co2="";
            for (int i = 0; i < linesize; i++)
            {
                int oxHits = 0;
                int coHits = 0;

                foreach (string entry in oxList)
                {
                    if (entry[i] == '1')
                        oxHits++;
                }

                foreach (string entry in coList)
                {
                    if (entry[i] == '1')
                        coHits++;
                }

                /*
                Console.WriteLine("====");
                foreach(string s in oxList)
                    Console.WriteLine(s);
                Console.WriteLine($"oxgen 1's is {oxHits}");
                Console.WriteLine("=");
                foreach (string s in coList)
                    Console.WriteLine(s);
                Console.WriteLine($"co2 1's is {coHits}");
                Console.WriteLine("====");
                */

                if (oxHits*2 >= oxList.Count)
                {
                    for (int j = 0; j < oxList.Count; j++)
                        if (oxList[j][i] != '1')
                            oxList.RemoveAt(j--);
                }
                else
                    for (int j = 0; j < oxList.Count; j++)
                        if (oxList[j][i] != '0')
                            oxList.RemoveAt(j--);

                if (coHits*2 < coList.Count)
                {
                    for (int j = 0; j < coList.Count; j++)
                        if (coList[j][i] != '1')
                            coList.RemoveAt(j--);
                }
                else
                    for (int j = 0; j < coList.Count; j++)
                        if (coList[j][i] != '0')
                            coList.RemoveAt(j--);

                if (oxList.Count == 1 && oxygen == "")
                    oxygen = oxList[0];
                if (coList.Count == 1 && co2 == "")
                    co2 = coList[0];
            }

            int oxVal = 0, coVal = 0;
            for (int i = 0; i < linesize; i++)
            {
                oxVal *= 2;
                coVal *= 2;
                if (oxygen[i] == '1')
                    oxVal++;
                if(co2[i] == '1')
                    coVal++;

            }
            Console.WriteLine($"ox  {oxygen}");
            Console.WriteLine($"co2 {co2}");
            Console.WriteLine($"{coVal*oxVal}");
        }
    }
}
