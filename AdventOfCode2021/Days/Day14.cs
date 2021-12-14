using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day14 : AdventOfCode
    {
        Dictionary<string, string> pairs = new();
        Dictionary<string, ulong> pairCount = new();
        string startingFormula;
        public Day14()
        {
            string[] _input = File.ReadAllLines("../../../Inputs/Input14.txt");
            startingFormula = _input[0];

            for (int i = 2; i < _input.Length; i++)
            {
                var tokens = _input[i].Split(" -> ");
                pairs.Add(tokens[0], tokens[1]);
            }
        }

        public override void PartOne()
        {
            // Brute force approach
            var copy = startingFormula.ToString();
            for (int steps = 0; steps < 10; steps++)
            {
                StringBuilder newFormula = new();
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    newFormula.Append(copy[i]);
                    var pair = $"{copy[i]}{copy[i + 1]}";
                    if (pairs.ContainsKey(pair))
                    {
                        newFormula.Append(pairs[pair]);
                    }
                }
                newFormula.Append(copy[^1]);
                copy = newFormula.ToString();
            }

            // Count
            Dictionary<char, int> count = new();
            foreach (var ch in copy)
            {
                if (!count.ContainsKey(ch))
                {
                    count.Add(ch, 1);
                }
                else
                {
                    count[ch]++;
                }
            }
            int min = count.Min(x => x.Value);
            int max = count.Max(x => x.Value);
            Console.WriteLine($"Part 1: {max - min}");
        }
        public override void PartTwo()
        {
            // Counting pairs approach
            for (int i = 0; i < startingFormula.Length-1; i++)
            {
                var pair = $"{startingFormula[i]}{startingFormula[i + 1]}";
                if (!pairCount.ContainsKey(pair))
                    pairCount.Add(pair, 1);
                else
                    pairCount[pair]++;
            }

            for (int steps = 0; steps < 40; steps++)
            {
                Dictionary<string, ulong> newPairs = new();

                foreach (var pp in pairCount)
                {
                    // Split pair into two new pairs
                    var exp = pairs[pp.Key];
                    var pair1 = $"{pp.Key[0]}{exp}";
                    var pair2 = $"{exp}{pp.Key[1]}";

                    if (!newPairs.ContainsKey(pair1))
                        newPairs.Add(pair1, pp.Value);
                    else
                        newPairs[pair1] += pp.Value;

                    if (!newPairs.ContainsKey(pair2))
                        newPairs.Add(pair2, pp.Value);
                    else
                        newPairs[pair2] += pp.Value;
                }
                pairCount = newPairs;
            }

            Dictionary<char, ulong> count = new();
            // Count all single chars
            foreach (var ch in pairCount)
            {
                var c1 = ch.Key[0];
                var c2 = ch.Key[1];

                if (!count.ContainsKey(c1))
                    count.Add(c1, ch.Value);
                else
                    count[c1] += ch.Value;

                if (!count.ContainsKey(c2))
                    count.Add(c2, ch.Value);
                else
                    count[c2] += ch.Value;
            }
            //Recount correctly
            foreach (var ch in count)
            {
                if (ch.Value % 2 != 0)
                    count[ch.Key] = (ch.Value + 1) / 2;
                else
                    count[ch.Key] = ch.Value / 2;
            }

            var min = count.Min(x => x.Value);
            var max = count.Max(x => x.Value);
            Console.WriteLine($"Part 2: {max - min}");
        }
    }
}
