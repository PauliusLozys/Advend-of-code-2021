using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Cell
    {
        public int Energy { get; set; }
        public bool HasFlashed { get; set; }

        public Cell(int energy)
        {
            Energy = energy;
        }
        public void IncreaseEnergy()
        {
            if (!HasFlashed)
                Energy++;
        }

        public void Flash()
        {
            HasFlashed = true;
            Energy = 0;
        }
        public void Reset()
        {
            HasFlashed = false;
        }
    }
    class Day11 : AdventOfCode
    {
        private readonly int[][] _input = File.ReadAllLines("../../../Inputs/Input11.txt")
            .Select(x => x.ToCharArray()
                .Select(y => int.Parse(y.ToString()))
                .ToArray())
            .ToArray();

        public override void PartOne()
        {
            Cell[][] grid = _input.Select(x => x.Select(y => new Cell(y)).ToArray()).ToArray();
            int flashCount = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int y = 0; y < grid.Length; y++)
                    for (int x = 0; x < grid[0].Length; x++)
                        grid[y][x].Reset();

                bool NeedtoCheckAgain = false;
                for (int y = 0; y < grid.Length; y++)
                {
                    for (int x = 0; x < grid[0].Length; x++)
                    {
                        grid[y][x].IncreaseEnergy();

                        if (grid[y][x].Energy > 9) // Flash
                        {
                            flashCount++;
                            NeedtoCheckAgain = true;
                            grid[y][x].Flash();
                            FlashNeighbors(y, x, grid);
                        }
                    }
                }

                while (NeedtoCheckAgain)
                {
                    NeedtoCheckAgain = false;
                    for (int y = 0; y < grid.Length; y++)
                    {
                        for (int x = 0; x < grid[0].Length; x++)
                        {
                            if (grid[y][x].Energy > 9) // Flash
                            {
                                flashCount++;
                                NeedtoCheckAgain = true;
                                grid[y][x].Flash();
                                FlashNeighbors(y, x, grid);
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Part 1: {flashCount}");
        }
        public override void PartTwo()
        {
            Cell[][] grid = _input.Select(x => x.Select(y => new Cell(y)).ToArray()).ToArray();
            for (int i = 0; i < 10_000; i++)
            {
                int flashCount = 0;

                for (int y = 0; y < grid.Length; y++)
                    for (int x = 0; x < grid[0].Length; x++)
                        grid[y][x].Reset();

                bool NeedToCheckAgain = false;
                for (int y = 0; y < grid.Length; y++)
                {
                    for (int x = 0; x < grid[0].Length; x++)
                    {
                        grid[y][x].IncreaseEnergy();

                        if (grid[y][x].Energy > 9) // Flash
                        {
                            flashCount++;
                            NeedToCheckAgain = true;
                            grid[y][x].Flash();
                            FlashNeighbors(y, x, grid);
                        }
                    }
                }

                while (NeedToCheckAgain)
                {
                    NeedToCheckAgain = false;
                    for (int y = 0; y < grid.Length; y++)
                    {
                        for (int x = 0; x < grid[0].Length; x++)
                        {
                            if (grid[y][x].Energy > 9) // Flash
                            {
                                flashCount++;
                                NeedToCheckAgain = true;
                                grid[y][x].Flash();
                                FlashNeighbors(y, x, grid);
                            }
                        }
                    }
                }

                if(flashCount == 100)
                {
                    Console.WriteLine($"Part 2: {i+1}");
                    break;
                }
            }
        }
        private void FlashNeighbors(int y, int x, Cell[][] grid)
        {
            if (!IsOutsideBounds(y - 1, x - 1))
                grid[y - 1][x - 1].IncreaseEnergy();

            if (!IsOutsideBounds(y - 1, x))
                grid[y - 1][x].IncreaseEnergy();

            if (!IsOutsideBounds(y - 1, x + 1))
                grid[y - 1][x + 1].IncreaseEnergy();

            if (!IsOutsideBounds(y, x - 1))
                grid[y][x - 1].IncreaseEnergy();

            if (!IsOutsideBounds(y, x + 1))
                grid[y][x + 1].IncreaseEnergy();

            if (!IsOutsideBounds(y + 1, x - 1))
                grid[y + 1][x - 1].IncreaseEnergy();

            if (!IsOutsideBounds(y + 1, x))
                grid[y + 1][x].IncreaseEnergy();

            if (!IsOutsideBounds(y + 1, x + 1))
                grid[y + 1][x + 1].IncreaseEnergy();
        }
        private bool IsOutsideBounds(int y, int x)
        {
            return x < 0 || y < 0 || x >= _input.Length || y >= _input[0].Length;
        }
    }
}
