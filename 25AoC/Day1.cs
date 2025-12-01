namespace _25AoC
{
    internal class Day1
    {
        static int MathMod(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }
        public static void Execute()
        {
            StreamReader sr = new StreamReader("inputs/Input1.txt");
            int dial = 50;
            int zerosEnd = 0;
            int zerosClick = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                int change = int.Parse(line.Substring(1));
                bool direction = line[0] == 'R';
                zerosClick += change / 100;
                change %= 100;
                dial += direction ? change : -change;
                zerosClick += Math.Abs(dial)/100 + (dial < 1 && dial + change != 0 ? 1 : 0);
                dial = MathMod(dial, 100);
                Console.WriteLine(dial + " " + zerosClick);
                if (dial == 0)
                    zerosEnd++;
            }
            sr.Close();
            Console.WriteLine(zerosEnd);
            Console.WriteLine(zerosClick);
        }
    }
}
