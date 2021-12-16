using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace AoC2021
{
    class Puzzle15
    {
        class NodeComparer : IComparer<Node>
        {
            public int Compare([AllowNull] Node x, [AllowNull] Node y)
            {
                return x.shortesCost.CompareTo(y.shortesCost);
            }
        }
        class Node
        {
            public static HashSet<Node> unlocked = new HashSet<Node>();
            public static Node[,] map;
            public int x, y, value;
            public bool locked;
            public int shortesCost;
            List<Node> list;
            List<Node> GetNodes
            {
                get
                {
                    if (list != null)
                        return list;
                    var l = new List<Node>();
                    if (x != 0)
                        l.Add(map[x - 1, y]);
                    if (y != 0)
                        l.Add(map[x, y - 1]);
                    if (x != map.GetLength(0)-1)
                        l.Add(map[x + 1, y]);
                    if (y != map.GetLength(1)-1)
                        l.Add(map[x, y + 1]);
                    list = l;
                    return list;
                }
            }

            public void Visit(int pathCost)
            {
                if (locked)
                    return;
                if (pathCost + value < shortesCost)
                {
                    shortesCost = pathCost + value;
                    unlocked.Add(this);
                }
            }

            public static bool LockAndLook()
            {
                var min = unlocked.Min(x => x.shortesCost);
                var n = unlocked.First(x => x.shortesCost == min);
                n.locked = true;
                if (n.x == map.GetLength(0) - 1 && n.y == map.GetLength(1) - 1)
                    return true;
                unlocked.Remove(n);
                //Console.WriteLine($"locking {n.x},{n.y} with path {n.shortesCost}");
                foreach (var nn in n.GetNodes)
                    nn.Visit(n.shortesCost);
                return false;
            }

            public override bool Equals(object obj)
            {
                var other = (Node)obj;
                return x==other.x && y==other.y;
            }

            public override int GetHashCode()
            {
                return x * 10000 + y;
            }

            public Node(int x,int y,int cost)
            {
                this.x = x;
                this.y = y;
                this.value = cost;
                shortesCost = int.MaxValue;
            }
        }
        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle15Input.txt");
            string line = sr.ReadLine();
            int size = line.Length;
            Node.map = new Node[size*5, size*5];
            for (int i = 0; i < size; i++)
            {
                if (i != 0) line = sr.ReadLine();
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        for (int l = 0; l < 5; l++)
                        {
                            int riskLevel = (int)(line[j] - '0') + k + l;
                            riskLevel = riskLevel % 10 + riskLevel / 10;
                            Node.map[l*size+j,k*size + i] = new Node(l*size + j,k*size + i, riskLevel);
                        }
                    }
                   
                }
            }
            Node.unlocked.Add(Node.map[0, 0]);
            Node.map[0, 0].shortesCost = 0;
            Node.map[0, 0].locked = true;
            while (!Node.LockAndLook());
            Console.WriteLine(Node.map[size*5-1,size*5-1].shortesCost);
        }
    }
}
