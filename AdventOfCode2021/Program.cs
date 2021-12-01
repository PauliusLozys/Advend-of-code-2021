using AdventOfCode2021.Days;
using System;
using System.Diagnostics;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch t = new();
            t.Start();
            Day1.PartOne();
            Day1.PartTwo();

            t.Stop();
            Console.WriteLine($"Time taken: {t.Elapsed}");
        }
    }
}
