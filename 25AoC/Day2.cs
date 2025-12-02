using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day2
    {

        public static long GetInvalidsInRange(long a,long b)
        {
            long sum = 0;
            int lengthMin = (int)Math.Log10(a)+1;
            int lengthMax = (int)Math.Log10(b)+1;
            if(lengthMin % 2 == 1)
                lengthMin++;
            if(lengthMax % 2 == 1)
                lengthMax--;
            for(int length = lengthMin; length <= lengthMax; length+=2)
            {
                long start = long.Parse("0" + a.ToString().Substring(0, a.ToString().Length - length / 2));
                long end = long.Parse(b.ToString().Substring(0, b.ToString().Length - length / 2));
                for(long firstHalf = start; firstHalf <= end; firstHalf++)
                {
                    string candidate = firstHalf.ToString() + firstHalf.ToString();
                    if (candidate.Length != length)
                        continue;
                    long candidateLong = long.Parse(candidate);
                    if (candidateLong >= a && candidateLong <= b)
                        sum += candidateLong;
                }
            }
            Console.WriteLine($"{sum} found in range {a} - {b}");
            return sum;
        }

        private static string RepeatString(string str, int count)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < count; i++)
                sb.Append(str);
            return sb.ToString();
        }

        public static long GetInvalidsInRangeAdvanced(long a, long b)
        {
            HashSet<long> invalids = new HashSet<long>();
            long sum = 0;
            int lengthMin = (int)Math.Log10(a) + 1;
            int lengthMax = (int)Math.Log10(b) + 1;
            for (int length = 1; length <= lengthMax/2; length++)
            {
                long start = long.Parse("0" + a.ToString().Substring(0, length - (lengthMax > lengthMin ? 1 : 0)));
                long end = long.Parse(b.ToString().Substring(0,length + (lengthMax > lengthMin ? 1 : 0)));
                int repeatStart = 2;
                int repeatEnd = lengthMax / length + 1;
                for (long firstPart = start; firstPart <= end; firstPart++)
                {
                    for(int repeat = repeatStart; repeat <= repeatEnd; repeat++)
                    {
                        string candidate = RepeatString(firstPart.ToString(), repeat);
                        long candidateLong = long.Parse(candidate);
                        if (candidateLong >= a && candidateLong <= b && !invalids.Contains(candidateLong))
                        {
                            invalids.Add(candidateLong);
                            sum += candidateLong;
                            Console.WriteLine(candidateLong);
                        }
                    }
                }

            }
            Console.WriteLine($"{sum} found in range {a} - {b}");
            return sum;
        }

        public static void Execute()
        {
            StreamReader sr = new StreamReader("inputs/Input2.txt");
            long totalId = 0;
            foreach (var range in sr.ReadLine().Split(','))
                totalId += GetInvalidsInRangeAdvanced(long.Parse(range.Split('-')[0]), long.Parse(range.Split('-')[1]));
            Console.WriteLine(totalId);
            return;
        }
    }
}
