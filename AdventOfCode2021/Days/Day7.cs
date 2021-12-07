using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{

    class Day7 : AdventOfCode
    {
        private readonly int[] _input = File.ReadAllText("../../../Inputs/Input7.txt").Split(',').Select(x => int.Parse(x)).ToArray();
        public override void PartOne()
        {
            int min = _input.Min();
            int max = _input.Max();
            int minFuel = int.MaxValue;
            for (int i = min; i <= max; i++)
            {
                minFuel = Math.Min(minFuel, CalculateFuelCostForPositionPartOne(i));
            }

            Console.WriteLine($"Part 1: {minFuel}");
        }

        public override void PartTwo()
        {
            int min = _input.Min();
            int max = _input.Max();
            int minFuel = int.MaxValue;
            for (int i = min; i <= max; i++)
            {
                Console.WriteLine($"{i} {CalculateFuelCostForPositionPartTwo(i)}");
                minFuel = Math.Min(minFuel, CalculateFuelCostForPositionPartTwo(i));
            }

            Console.WriteLine($"Part 2: {minFuel}");
        }
        private int CalculateFuelCostForPositionPartOne(int i)
        {
            int cost = 0;
            foreach (var position in _input)
            {
                cost += Math.Abs(position - i);

            }
            return cost;
        }
        private int CalculateFuelCostForPositionPartTwo(int i)
        {
            int cost = 0;

            foreach (var position in _input)
            {
                float min = Math.Min(position, i);
                float max = Math.Max(position, i);

                float n = max - min;
                cost += (int)(n/2 * (2 + (n-1)));
            }

            return cost;
        }
    }
}
