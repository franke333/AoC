using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace AoC2021
{
    public static class MyStringExtension
    {
        public static long BitsToLong(this string s)
        {
            long sum = 0;
            for (int i = 0; i < s.Length; i++)
            {
                sum *= 2;
                if (s[i] == '1')
                    sum++;
            }
            return sum;
        }
    }
    class Puzzle16
    {
        class Packet
        {
            public int version, type;

            public Packet(int version, int type)
            {
                this.version = version;
                this.type = type;
            }

            public virtual int GetVersionSum() => version;
            public virtual long GetExpressionResult()
            {
                throw new Exception("big bruh");
            }

            public static (Packet,int) Process(string s)
            {
                int version = (int)s.Substring(0, 3).BitsToLong();
                int type = (int)s.Substring(3, 3).BitsToLong();
                if (type == 4)
                {
                    Literal l = new Literal(version,type);
                    int index = 6;
                    StringBuilder sb = new StringBuilder();
                    while (s[index] != '0')
                    {
                        sb.Append(s.Substring(index + 1, 4));
                        index += 5;
                    }
                    sb.Append(s.Substring(index + 1, 4));
                    l.value = sb.ToString().BitsToLong();
                    return (l, index + 5);
                }
                else
                {
                    Operator o = new Operator(version,type);
                    char I = s[6];
                    
                    if (I == '1')
                    {
                        int count = (int)s.Substring(7, 11).BitsToLong();
                        int index = 18;
                        for (int i = 0; i < count; i++)
                        {
                            var res = Process(s.Substring(index));
                            o.subpackets.Add(res.Item1);
                            index += res.Item2;
                        }
                        return (o, index);
                    }
                    else
                    {
                        int index = 22;
                        int maxLen = (int)s.Substring(7, 15).BitsToLong() + index;
                        while (index < maxLen)
                        {
                            var res = Process(s.Substring(index));
                            o.subpackets.Add(res.Item1);
                            index += res.Item2;
                        }
                        return (o, index);
                    }
                }
            }
        }

        class Operator : Packet
        {
            public Operator(int version, int type) : base(version, type) { }
            public List<Packet> subpackets = new List<Packet>();
            public override int GetVersionSum()
             => subpackets.Sum(x => x.GetVersionSum()) + base.GetVersionSum();

            public override long GetExpressionResult()
            {
                switch (type)   
                {
                    case 0:
                        return subpackets.Sum(x => x.GetExpressionResult());
                    case 1:
                        long product = 1;
                        foreach (var p in subpackets)
                            product *= p.GetExpressionResult();
                        return product;
                    case 2:
                        return subpackets.Min(x => x.GetExpressionResult());
                    case 3:
                        return subpackets.Max(x => x.GetExpressionResult());
                    case 5:
                        return subpackets[0].GetExpressionResult() > subpackets[1].GetExpressionResult() ? 1 : 0;
                    case 6:
                        return subpackets[0].GetExpressionResult() < subpackets[1].GetExpressionResult() ? 1 : 0;
                    case 7:
                        return subpackets[0].GetExpressionResult() == subpackets[1].GetExpressionResult() ? 1 : 0;
                    default:
                        throw new Exception("???");
                }
            }
        }

        class Literal : Packet
        {
            public long value;

            public override long GetExpressionResult()
            => value;

            public Literal(int version, int type) : base(version, type) { }
        }
      
        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle16Input.txt");
            StringBuilder sb = new StringBuilder();
            foreach (char c in sr.ReadLine())
                sb.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));
            var p = Packet.Process(sb.ToString()).Item1;
            Console.WriteLine(p.GetVersionSum());
            Console.WriteLine(p.GetExpressionResult());
        }
    }
}
