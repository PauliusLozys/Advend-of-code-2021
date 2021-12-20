using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day20 : AdventOfCode
    {
        private string[] _input = File.ReadAllLines("../../../Inputs/Input20.txt");
        public override void PartOne()
        {
            string header = _input[0];
            HashSet<(int y, int x)> gridCopy = new();
            FillInitialGrid(gridCopy);
            int count = ModifyGrid(header, gridCopy, 2);
            Console.WriteLine($"Part 1:{count}");
        }
        public override void PartTwo()
        {
            string header = _input[0];
            HashSet<(int y, int x)> gridCopy = new();
            FillInitialGrid(gridCopy);
            int count = ModifyGrid(header, gridCopy, 50);
            Console.WriteLine($"Part 2:{count}");
        }
        private int ModifyGrid(string header, HashSet<(int y, int x)> grid, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                int Xmax = grid.Max(x => x.x);
                int Ymax = grid.Max(x => x.y);
                int Xmin = grid.Min(x => x.x);
                int Ymin = grid.Min(x => x.y);

                int smaller = i % 2 == 0 ? 10 : 1;

                HashSet<(int y, int x)> newGrid = new();

                for (int y = Ymin - 10 / smaller + 3; y < Ymax + 10 / smaller - 3; y++)
                {
                    for (int x = Xmin - 10 / smaller + 2; x < Xmax + 10 / smaller - 2; x++)
                    {
                        string binary = "";
                        //Scan 3x3
                        for (int yy = y - 1; yy < y - 1 + 3; yy++)
                        {
                            for (int xx = x - 1; xx < x - 1 + 3; xx++)
                            {
                                if (grid.Contains((yy, xx)))
                                    binary += "1";
                                else
                                    binary += "0";
                            }
                        }

                        var index = Convert.ToInt32(binary, 2);
                        if (header[index] == '#')
                            newGrid.Add((y, x));
                    }
                }
                grid = newGrid;
            }
            return grid.Count;
        }
        private void FillInitialGrid(HashSet<(int y, int x)> grid)
        {
            for (int y = 2; y < _input.Length; y++)
            {
                for (int x = 0; x < _input[y].Length; x++)
                {
                    if (_input[y][x] == '#')
                        grid.Add((y - 2, x));
                }
            }
        }
    }
}
