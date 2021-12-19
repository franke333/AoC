using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace AoC2021
{
    class Puzzle19
    {
        class Beacon
        {
            public int x, y, z;

            public Beacon(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public override bool Equals(object obj)
                => obj is Beacon b ? b.x == x && b.y == y && b.z == z : false;

            public override int GetHashCode()
                => (x << 22) + (y << 11) + z;

            public static Beacon operator +(Beacon a, Beacon b)
                => new Beacon(a.x + b.x, a.y + b.y, a.z + b.z);

            public int Distance(Beacon b)
             => Math.Abs(x - b.x) + Math.Abs(y - b.y) + Math.Abs(z - b.z);
        }

        class Scanner
        {
            public static HashSet<Beacon> scanners = new HashSet<Beacon>();
            public HashSet<Beacon> beacons = new HashSet<Beacon>();

            public void ReadFromInput(StreamReader sr)
            {
                string line;
                sr.ReadLine(); // --- scanner x ---
                while (!sr.EndOfStream)
                {
                    if ((line = sr.ReadLine()) == "") return;
                    string[] coords = line.Split(',');
                    beacons.Add(new Beacon(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2])));
                }
            }

            static IEnumerable<Func<Beacon,Beacon>> GetRotations()
            {

                yield return b => new Beacon(b.x, b.y, b.z);
                yield return b => new Beacon(b.x, b.z, -b.y);
                yield return b => new Beacon(b.x, -b.y, -b.z);
                yield return b => new Beacon(b.x, -b.z, b.y);

                yield return b => new Beacon(-b.x, b.z, b.y);
                yield return b => new Beacon(-b.x, b.y, -b.z);
                yield return b => new Beacon(-b.x, -b.z, -b.y);
                yield return b => new Beacon(-b.x, -b.y, b.z);

                yield return b => new Beacon(b.y, b.z, b.x);
                yield return b => new Beacon(b.y, b.x, -b.z);
                yield return b => new Beacon(b.y, -b.z, -b.x);
                yield return b => new Beacon(b.y, -b.x, b.z);

                yield return b => new Beacon(-b.y, b.x, b.z);
                yield return b => new Beacon(-b.y, b.z, -b.x);
                yield return b => new Beacon(-b.y, -b.x, -b.z);
                yield return b => new Beacon(-b.y, -b.z, b.x);

                yield return b => new Beacon(b.z, b.x, b.y);
                yield return b => new Beacon(b.z, b.y, -b.x);
                yield return b => new Beacon(b.z, -b.x, -b.y);
                yield return b => new Beacon(b.z, -b.y, b.x);

                yield return b => new Beacon(-b.z, b.y, b.x);
                yield return b => new Beacon(-b.z, b.x,-b.y);
                yield return b => new Beacon(-b.z, -b.y, -b.x);
                yield return b => new Beacon(-b.z, -b.x, b.y);
            }

            public bool TryAddScanner(Scanner other)
            {
                foreach(var del in GetRotations())
                    foreach(var ob in other.beacons.Select(b => del(b)))
                        foreach(var b in beacons)
                        {   //try  ob = b
                            Beacon OtherscannerPos = new Beacon(b.x - ob.x, b.y - ob.y, b.z - ob.z);
                            int hits = 0;
                            foreach (var oob in other.beacons.Select(b => del(b) + OtherscannerPos))
                                if (beacons.Contains(oob))
                                    if (++hits >= 12)
                                    {
                                        foreach (var oobs in other.beacons.Select(b => del(b) + OtherscannerPos))
                                            beacons.Add(oobs);
                                        scanners.Add(OtherscannerPos);
                                        return true;
                                    }
                        }
                return false;
            }

        }

        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle19Input.txt");
            Scanner baseS = new Scanner();
            List<Scanner> scans = new List<Scanner>();
            Scanner.scanners.Add(new Beacon(0, 0, 0));
            baseS.ReadFromInput(sr);
            while (!sr.EndOfStream)
            {
                var s = new Scanner();
                s.ReadFromInput(sr);
                scans.Add(s);
            }
            while(scans.Count!=0)
                for (int i = 0; i < scans.Count; i++)
                    if (baseS.TryAddScanner(scans[i]))
                        scans.RemoveAt(i--);
            Console.WriteLine($"part1 : {baseS.beacons.Count}");
            int max = 0;
            foreach (var s1 in Scanner.scanners)
                foreach (var s2 in Scanner.scanners)
                    max = Math.Max(max, s1.Distance(s2));
            Console.WriteLine($"part2 : {max}");
        }
    }
}
