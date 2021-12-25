using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day25 : AdventOfCode
    {
        private char[][] _input = File.ReadAllLines("../../../Inputs/Input25.txt")
            .Select(x => x.Select(y => y).ToArray()).ToArray();
        public override void PartOne()
        {
            // First move >
            // Second move v
            bool changed = true;
            for (int steps = 0; true; steps++)
            {
                if (!changed)
                {
                    Console.WriteLine($"Part 1: {steps}");
                    break;
                }
                changed = false;
                for (int y = 0; y < _input.Length; y++)
                {
                    int checkTo = 0;

                    for (int x = _input[0].Length-1; x >= checkTo; x--)
                    {
                        if (_input[y][x] == '>')
                        {
                            if (_input[y][(x + 1) % _input[y].Length] == '.')
                            {
                                _input[y][(x + 1) % _input[y].Length] = '>';
                                _input[y][x] = '.';
                                if ((x + 1) % _input[y].Length == 0)
                                    checkTo++;
                                x--;
                                changed = true;
                            }

                            continue;
                        }
                    }
                }

                for (int x = 0; x < _input[0].Length; x++)
                {
                    int checkTo = 0;
                    for (int y = _input.Length - 1; y >= checkTo; y--)
                    {
                        if (_input[y][x] == 'v')
                        {
                            if (_input[(y + 1) % _input.Length][x] == '.')
                            {
                                _input[(y + 1) % _input.Length][x] = 'v';
                                _input[y][x] = '.';
                                if ((y + 1) % _input.Length == 0)
                                    checkTo++;
                                y--;
                                changed = true;
                            }

                            continue;
                        }
                    }
                }
            }
        }

        public override void PartTwo()
        {
        }
    }
}
