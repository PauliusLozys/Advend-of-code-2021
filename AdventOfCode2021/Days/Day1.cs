using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day1
    {
        public static void PartOne()
        {
            var lines = File.ReadAllLines("../../../Days/Input1.txt").Select(x => int.Parse(x)).ToArray();

            int increaseCount = 0;

            int currentNumber = lines[0];
            foreach (var line in lines[1..])
            {
                var current = line;

                if (current > currentNumber)
                    increaseCount++;

                currentNumber = current;
            }

            Console.WriteLine($"Part 1: depth increased {increaseCount} times");
        }

        public static void PartTwo()
        {
            var lines = File.ReadAllLines("../../../Days/Input1.txt").Select(x => int.Parse(x)).ToArray();

            int increaseCount = 0;
            int currentNumber = 0;

            for (int j = 0; j < 3; j++)
            {
                var value = lines[j];
                currentNumber += value;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                int sum = 0;
                for (int j = i; j < (i + 3) && j < lines.Length; j++)
                {
                    var value = lines[j];
                    sum += value;
                }

                if (sum > currentNumber)
                    increaseCount++;

                currentNumber = sum;
            }

            Console.WriteLine($"Part 2: depth increased {increaseCount} times");
        }
    }
}
