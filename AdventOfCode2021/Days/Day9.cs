using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day9 : AdventOfCode
    {
        private int[][] _input = File.ReadAllLines("../../../Inputs/Input9.txt")
            .Select(x => x.ToArray()
                    .Select(y => int.Parse(y.ToString()))
                    .ToArray())
            .ToArray();

        public override void PartOne()
        {
            int sum = 0;
            for (int y = 0; y < _input.Length; y++)
            {
                for (int x = 0; x < _input[0].Length; x++)
                {
                    var up    = y - 1 >= 0 ?                _input[y - 1][x] : 9;
                    var down  = y + 1 < _input.Length ?     _input[y + 1][x] : 9;
                    var left  = x - 1 >= 0 ?                _input[y][x - 1] : 9;
                    var right = x + 1 < _input[0].Length ?  _input[y][x + 1] : 9;

                    var point = _input[y][x];
                    if( point < up 
                        && point < down
                        && point < left
                        && point < right)
                    {
                        sum += 1 + point;
                    }

                }
            }
            Console.WriteLine($"Part 1: {sum}");
        }

        public override void PartTwo()
        {
            List<int> sizes = new();
            for (int y = 0; y < _input.Length; y++)
            {
                for (int x = 0; x < _input[0].Length; x++)
                {
                    var up = y - 1 >= 0 ? _input[y - 1][x] : 9;
                    var down = y + 1 < _input.Length ? _input[y + 1][x] : 9;
                    var left = x - 1 >= 0 ? _input[y][x - 1] : 9;
                    var right = x + 1 < _input[0].Length ? _input[y][x + 1] : 9;

                    var point = _input[y][x];
                    if (point < up
                        && point < down
                        && point < left
                        && point < right)
                    {
                        int size = GetSizeOfBasin(y, x);
                        sizes.Add(size);
                    }
                }
            }

            int sum = sizes.OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y);

            Console.WriteLine($"Part 2: {sum}");
        }

        private int GetSizeOfBasin(int y, int x)
        {
            if (y < 0 || x < 0 || y >= _input.Length || x >= _input[0].Length)
                return 0;

            if (_input[y][x] == 9) // was visited or wall
                return 0;

            _input[y][x] = 9; // mark as visited

            // go in 4 directions
            int u = GetSizeOfBasin(y - 1, x);
            int d = GetSizeOfBasin(y + 1, x);
            int l = GetSizeOfBasin(y, x - 1);
            int r = GetSizeOfBasin(y, x + 1);

            return u + d + l + r + 1;
        }
    }
}
