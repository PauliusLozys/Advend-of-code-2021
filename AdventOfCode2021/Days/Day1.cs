using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    class Day1 : AdventOfCode
    {
        private readonly int[] _input = File.ReadAllLines("../../../Inputs/Input1.txt").Select(x => int.Parse(x)).ToArray();

        public override void PartOne()
        {
            int increaseCount = 0;
            int currentNumber = _input[0];

            foreach (var line in _input[1..])
            {
                if (line > currentNumber)
                    increaseCount++;

                currentNumber = line;
            }

            Console.WriteLine($"Part 1: depth increased {increaseCount} times");
        }

        public override void PartTwo()
        {
            int increaseCount = 0;
            int currentNumber = _input[0..3].Sum();

            for (int i = 1; i < _input.Length; i++)
            {
                int sum = i + 3 < _input.Length ? _input[i..(i + 3)].Sum() : _input[i..].Sum();

                if (sum > currentNumber)
                    increaseCount++;
                currentNumber = sum;
            }

            Console.WriteLine($"Part 2: depth increased {increaseCount} times");
        }
    }
}
