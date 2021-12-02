using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    class Day2 : AdventOfCode
    {
        private readonly (string, int)[] _input = File.ReadAllLines("../../../Inputs/Input2.txt").Select(x =>
        {
            var line = x.Split();
            return (line.First(), int.Parse(line.Last()));
        }).ToArray();

        public override void PartOne()
        {
            int horizontal = 0;
            int depth = 0;

            foreach (var command in _input)
            {
                if (command.Item1.Equals("forward"))
                    horizontal += command.Item2;
                else if (command.Item1.Equals("up"))
                    depth -= command.Item2;
                else if (command.Item1.Equals("down"))
                    depth += command.Item2;
            }

            Console.WriteLine($"Part 1: {horizontal * depth}");
        }

        public override void PartTwo()
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach (var command in _input)
            {
                if (command.Item1.Equals("forward"))
                {
                    horizontal += command.Item2;
                    depth += aim * command.Item2;
                }
                else if (command.Item1.Equals("up"))
                    aim -= command.Item2;
                else if (command.Item1.Equals("down"))
                    aim += command.Item2;
            }

            Console.WriteLine($"Part 2: {horizontal * depth}");
        }
    }
}
