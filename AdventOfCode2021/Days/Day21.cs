using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Player
    {
        public int Position { get; set; }
        public int Score { get; set; }
    }
    class Day21 : AdventOfCode
    {
        private string[] _input = File.ReadAllLines("../../../Inputs/Input21.txt");
        Dictionary<(int position1, int position2, int score1, int score2), (ulong, ulong)> cache = new();
        static int dice = 0;
        static int timeDiceRolled = 0;
        public override void PartOne()
        {
            var l1 = _input[0].Split();
            var l2 = _input[1].Split();

            Player p1 = new() { Score = 0, Position = int.Parse(l1[4]) };
            Player p2 = new() { Score = 0, Position = int.Parse(l2[4]) };

            bool player1Turn = true;
            while(true)
            {
                if (player1Turn)
                {
                    player1Turn = false;

                    int r1 = RollDice();
                    int r2 = RollDice();
                    int r3 = RollDice();

                    int sum = r1 + r2 + r3;

                    p1.Position = (sum + p1.Position) % 10;
                    if (p1.Position == 0)
                        p1.Position = 10;
                    p1.Score += p1.Position;
                }
                else
                {
                    player1Turn = true;

                    int r1 = RollDice();
                    int r2 = RollDice();
                    int r3 = RollDice();

                    int sum = r1 + r2 + r3;

                    p2.Position = (sum + p2.Position) % 10;
                    if (p2.Position == 0)
                        p2.Position = 10;

                    p2.Score += p2.Position;
                }

                if (p2.Score >= 1000 || p1.Score >= 1000)
                    break;
            }

            var min = Math.Min(p1.Score, p2.Score);
            Console.WriteLine($"Part 1: {min * timeDiceRolled}");
        }
        private int RollDice()
        {
            dice++;
            timeDiceRolled++;
            if (dice > 100)
                dice = 1;

            return dice;
        }
        public override void PartTwo()
        {
            var l1 = _input[0].Split();
            var l2 = _input[1].Split();
            var (p1, p2)= QuantumSplit(int.Parse(l1[4]), int.Parse(l2[4]), 0, 0);
            var max = Math.Max(p1, p2);
            Console.WriteLine($"Part 2: {max}");
        }
        private (ulong, ulong) QuantumSplit(int position1, int position2, int score1, int score2)
        {
            if (score1 >= 21)
                return (1, 0);
            if (score2 >= 21)
                return (0, 1);

            if (cache.ContainsKey((position1, position2, score1, score2)))
                return cache[(position1, position2, score1, score2)];

            (ulong, ulong) total = (0,0);
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int z = 1; z <= 3; z++)
                    {
                        int sum = i + j + z;
                        var newPos = (sum + position1) % 10;
                        if (newPos == 0)
                            newPos = 10;

                        var newScore = score1 + newPos;

                        // Swap positions
                        var (p2, p1) = QuantumSplit(position2, newPos, score2, newScore);
                        total = (total.Item1 + p1, total.Item2 + p2);
                    }
                }
            }
            cache.Add((position1, position2, score1, score2), total);
            return total;
        }
    }
}
