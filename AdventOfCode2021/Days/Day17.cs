using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day17 : AdventOfCode
    {
        private int[][] _input = File.ReadAllText("../../../Inputs/Input17.txt")[15..]
            .Split(", y=").Select(x => x.Split("..").Select(y => int.Parse(y)).ToArray()).ToArray();

        public override void PartOne()
        {
            int xMin = _input[0][0];
            int xMax = _input[0][1];
            int yMin = _input[1][0];
            int yMax = _input[1][1];

            int maxHight = -1;
            for (int x = 1; x < 500; x++)
            {
                for (int y = 1; y < 500; y++)
                {
                    maxHight = Math.Max(maxHight, CheckForHit(xMin, xMax, yMin, yMax, x, y));
                }
            }
            Console.WriteLine($"Part 1: {maxHight}");
        }
        public override void PartTwo()
        {
            int xMin = _input[0][0];
            int xMax = _input[0][1];
            int yMin = _input[1][0];
            int yMax = _input[1][1];

            int count = 0;
            for (int x = 1; x < xMax+1; x++)
            {
                for (int y = -2000; y < 2000; y++)
                {
                    if (CheckForHit(xMin, xMax, yMin, yMax, x, y) != -1)
                        count++;
                }
            }
            Console.WriteLine($"Part 2: {count}");
        }
        private static int CheckForHit(int xMin, int xMax, int yMin, int yMax, int xVel, int yVel)
        {
            int xStart = 0;
            int yStart = 0;
            int highPoint = 0;

            for (int i = 0; i < 2000; i++)
            {
                xStart += xVel;
                yStart += yVel;
                if (xVel > 0)
                    xVel--;
                yVel--;

                if (xVel == 0 && xStart < xMin && xStart > xMax)
                {
                    Console.WriteLine("Missed target");
                    return -1; // Missed target
                }

                if (yStart > highPoint)
                    highPoint = yStart;

                if (xStart >= xMin && xStart <= xMax && yStart >= yMin && yStart <= yMax)
                {
                    return highPoint;
                }
            }
            return -1;
        }
    }
}
