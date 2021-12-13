using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    struct Dot
    {
        public Dot(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    class FoldInstruction
    {
        public string Axis { get; set; }
        public int Position { get; set; }
    }
    class Day13 : AdventOfCode
    {
        readonly List<FoldInstruction> folds = new();
        List<Dot> dots = new();

        public Day13()
        {
            string[] input = File.ReadAllLines("../../../Inputs/Input13.txt");
            foreach (var line in input)
            {
                if (line == "")
                    continue;
                var token = line.Split(',');
                if (token.Length == 2)
                    dots.Add(new Dot { X = int.Parse(token[0]), Y = int.Parse(token[1]) });
                else
                {
                    token = line.Split()[2].Split('=');
                    folds.Add(new FoldInstruction { Axis = token[0], Position = int.Parse(token[1]) });
                }
            }
        }
        public override void PartOne()
        {
            List<Dot> copy;
            var fold = folds[0];
            copy = Fold(fold);

            Console.WriteLine($"Part 1: {copy.Count}");
        }
        public override void PartTwo()
        {
            foreach (var fold in folds)
            {
                dots = Fold(fold);
            }

            var maxX = dots.Max(x => x.X);
            var maxY = dots.Max(x => x.Y);

            Console.WriteLine("Part 2:");
            for (int y = 0; y <= maxY + 1; y++)
            {
                for (int x = 0; x < maxX + 1; x++)
                {
                    if(dots.Contains(new Dot(x,y)))
                        Console.Write('#');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
        private List<Dot> Fold(FoldInstruction fold)
        {
            List<Dot> copy;
            if (fold.Axis == "x")
            {
                var pointsToFold = dots.Where(x => x.X > fold.Position);
                var newList = dots.Except(pointsToFold);
                var newPoints = pointsToFold.Select(x => new Dot(fold.Position - (x.X - fold.Position), x.Y));
                copy = newList.Union(newPoints).ToList();
            }
            else
            {
                var pointsToFold = dots.Where(x => x.Y > fold.Position);
                var newList = dots.Except(pointsToFold);
                var newPoints = pointsToFold.Select(x => new Dot(x.X, fold.Position - (x.Y - fold.Position)));
                copy = newList.Union(newPoints).ToList();
            }

            return copy;
        }
    }
}
