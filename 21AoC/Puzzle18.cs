using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace AoC2021
{
    class Puzzle18
    {
        static Regular FindLeftRegular(Element e)
        {
            while(e.Parent.first == e)
            {
                e = e.Parent;
                if (e.Parent is null)
                    return null;
            }
            e = e.Parent.first;
            while (e is Pair p)
                e = p.second;
            return e as Regular;
        }

        static Regular FindRightRegular(Element e)
        {
            while (e.Parent.second == e)
            {
                e = e.Parent;
                if (e.Parent is null)
                    return null;
            }
            e = e.Parent.second;
            while (e is Pair p)
                e = p.first;
            return e as Regular;
        }


        class Element
        {
            public int depth;
            Pair parent;
            public Pair Parent { 
                get => parent;
                set
                {
                    parent = value;
                    depth = parent.depth + 1;
                    if(this is Pair p)
                    {
                        p.first.Parent = p;
                        p.second.Parent = p;
                    }
                    
                }
            }
            public Element(Pair parent)
            {
                if (parent is null)
                    depth = 0;
                else
                {
                    this.parent = parent;
                    depth = parent.depth + 1;
                }
            }
            public virtual int GetMagnitude() => 0;

            public virtual bool React() => false;

        }

        class Pair : Element
        {
            public Element first, second;
            public override int GetMagnitude()
            => 3 * first.GetMagnitude() + 2 * second.GetMagnitude();

            public Pair(Pair parent) : base(parent) { }

            public override string ToString()
            {
                return '[' + first.ToString() + ',' + second.ToString() + ']';
            }

            public override bool React()
            { 
                if(depth == 4)
                {
                    Regular left = FindLeftRegular(this);
                    Regular right = FindRightRegular(this);
                    if (left != null) 
                        left.value += (first as Regular).value;
                    if (right != null)
                        right.value += (second as Regular).value;
                    if (this == Parent.second)
                        Parent.second = new Regular(Parent);
                    else
                        Parent.first = new Regular(Parent);
                    Pair root = this;
                    while (root.Parent != null)
                        root = root.Parent;
                    return true;
                }
                return false;
            }

        }

        class Regular : Element
        {
            public int value;
            public override int GetMagnitude() => value;

            public Regular(Pair parent, int value = 0) : base(parent) { this.value = value; }

            public override string ToString() => value.ToString();

            public override bool React()
            { //split
                if (value >= 10)
                {
                    Pair p = new Pair(Parent);
                    var valLeft = value / 2;
                    p.first = new Regular(p, valLeft);
                    p.second = new Regular(p, value - valLeft);
                    if (this == Parent.second)
                        Parent.second = p;
                    else
                        Parent.first = p;
                    Pair root = Parent;
                    while (root.Parent != null)
                        root = root.Parent;
                    return true;
                }
                return false;
            }
        }
        
        static Pair Add(Pair a,Pair b)
        {
            Pair p = new Pair(null);
            p.first = a;
            p.second = b;
            a.Parent = p; //these will react
            b.Parent = p;
            return p;
        }

        static Element ParseElement(string s,Pair parent)
        {
            if (s[0] == '[')
            {
                Pair root = new Pair(parent);
                int nesting = 0;
                int index = 1;
                while(nesting!=0 || s[index] != ',')
                {
                    if (s[index] == '[') nesting++;
                    if (s[index] == ']') nesting--;
                    index++;
                }
                root.first = ParseElement(s.Substring(1, index-1),root);
                root.second = ParseElement(s.Substring(index + 1,s.Length-index-2), root);
                return root;
            }
            return new Regular(parent, int.Parse(s));
        }

        static bool CheckIntegrityExplode(Element root)
        {
            if(root is Pair p)
                if (p.React() || CheckIntegrityExplode(p.first) || CheckIntegrityExplode(p.second))
                    return true;
            return false;
        }
        static bool CheckIntegritySplit(Element root)
        {
            if(root is Regular r)
                return r.React();
            if (root is Pair p)
                if (CheckIntegritySplit(p.first) || CheckIntegritySplit(p.second)) return true;
            return false;
        }

        static void ReactWhole(Element root)
        {
            while (CheckIntegrityExplode(root) || CheckIntegritySplit(root)) ;
        }

        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle18Input.txt");
            List<string> snails = new List<string>();
            while (!sr.EndOfStream)
                snails.Add(sr.ReadLine());
            Pair root = (Pair)ParseElement(snails[0],null);
            for (int i = 1; i < snails.Count; i++)
            {
                root = Add(root, (Pair)ParseElement(snails[i],null));
                ReactWhole(root);
            }
            Console.WriteLine($"part1: {root.GetMagnitude()}");
            int max = 0;
            for (int i = 0; i < snails.Count; i++)
            {
                for (int j = 0; j < snails.Count; j++)
                {
                    var r = Add((Pair)ParseElement(snails[i], null),
                        (Pair)ParseElement(snails[j], null));
                    ReactWhole(r);
                    max = Math.Max(max,r.GetMagnitude());
                }
            }
            Console.WriteLine($"part2: {max}");
            return;
        }
    }
}
