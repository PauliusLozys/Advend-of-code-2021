using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day6 : AdventOfCode
    {
        private readonly List<int> _input = File.ReadAllText("../../../Inputs/Input6.txt").Split(',').Select(x => int.Parse(x)).ToList();

        public override void PartOne()
        {
            var input = _input.ToList();

            for (int i = 0; i < 80; i++)
            {
                var currentFishCount = input.Count;

                for (int j = 0; j < currentFishCount; j++)
                {
                    input[j]--;

                    if(input[j] < 0)
                    {
                        input[j] = 6;
                        input.Add(8);
                    }
                }
                //Console.WriteLine($"Day {i+1}: {string.Join(',', input)}");
            }
            Console.WriteLine($"Part 1: {input.Count}");
        }

        public override void PartTwo()
        {
            var input = _input.ToList();
            long[] days = new long[9];

            for (int i = 0; i < input.Count; i++)
            {
                days[input[i]]++;
            }

            for (int day = 0; day < 256; day++)
            {
                var tmp = new long[9];
                for (int i = 0; i < days.Length - 1; i++)
                {
                    if(i == 0)
                    {
                        tmp[8] = days[i]; // New cells
                        tmp[6] = days[i]; // Reset current cells
                    }

                    days[i] = days[i + 1];
                }
                days[8] = 0; // Clear
                for (int i = 0; i < days.Length; i++)
                    days[i] += tmp[i]; // Add
            }

            Console.WriteLine($"Part 2: {days.Sum()}");
        }
    }
}
