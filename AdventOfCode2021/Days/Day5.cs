using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Line
    {
        public int x1, x2;
        public int y1, y2;

        public bool IsHorizontal() => y1 == y2;
        public bool IsVertical() => x1 == x2;
    }
    class Day5 : AdventOfCode
    {
        readonly List<Line> lines;
        readonly int gridSize = 1000;
        public Day5()
        {
            var file = File.ReadAllLines("../../../Inputs/Input5.txt");
            var splt = file.Select(x => x.Split(" -> ")).ToArray();
            List<Line> l = new();
            foreach (var item in splt)
            {
                var t = item[0].Split(',').Select(x=>int.Parse(x)).ToArray();
                var tt = item[1].Split(',').Select(x=>int.Parse(x)).ToArray();
                Line line = new();
                line.x1 = t[0];
                line.y1 = t[1];
                line.x2 = tt[0];
                line.y2 = tt[1];
                l.Add(line);
            }
            lines = l;
        }

        public override void PartOne()
        {
            int[,] grid = new int[gridSize, gridSize];

            foreach (var line in lines)
            {
                if (line.IsHorizontal())
                {
                    for (int x = Math.Min(line.x1, line.x2); x <= Math.Max(line.x2, line.x1); x++)
                    {
                        grid[line.y1, x]++;
                    }
                }
                else if (line.IsVertical())
                {
                    for (int y = Math.Min(line.y1, line.y2); y <= Math.Max(line.y2, line.y1); y++)
                    {
                        grid[y, line.x1]++;
                    }
                }
            }

            int count = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] > 1)
                        count++;
                }
            }
            Console.WriteLine($"Part 1: {count}");
        }

        public override void PartTwo()
        {
            int[,] grid = new int[gridSize, gridSize];

            foreach (var line in lines)
            {
                if (line.IsHorizontal())
                {
                    for (int x = Math.Min(line.x1, line.x2); x <= Math.Max(line.x2, line.x1); x++)
                    {
                        grid[line.y1, x]++;
                    }
                }
                else if (line.IsVertical())
                {
                    for (int y = Math.Min(line.y1, line.y2); y <= Math.Max(line.y2, line.y1); y++)
                    {
                        grid[y, line.x1]++;
                    }
                }
                else
                {
                    // diagonal lines
                    if(line.x1 > line.x2) // go right to left <-
                    {
                        int i, j;
                        for (i = line.x1, j = line.y1; i >= line.x2; i--)
                        {
                            grid[j, i]++;
                            j = line.y1 < line.y2 ? j+1 : j-1;
                        }
                    }
                    else
                    {
                        int i, j;
                        for (i = line.x1, j = line.y1; i <= line.x2; i++)
                        {
                            grid[j, i]++;
                            j = line.y1 < line.y2 ? j + 1 : j - 1;
                        }
                    }
                }
            }

            int count = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] > 1)
                        count++;
                }
            }
            Console.WriteLine($"Part 2: {count}");
        }
    }
}
