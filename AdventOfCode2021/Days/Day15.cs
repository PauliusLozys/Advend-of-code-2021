using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class PriorityQueue
    {
        private List<(int, int, int)> queue = new();
        public int Count { get => queue.Count; }
        public void Push((int, int, int) cell)
        {
            queue.Add(cell);
            queue = queue.OrderBy(x => x.Item3).ToList();
        }
        public (int y, int x, int value) Pop()
        {
            var val = queue[0];
            queue.RemoveAt(0);
            return val;
        }
    }
    class Day15 : AdventOfCode
    {
        private readonly int[][] _grid = File.ReadAllLines("../../../Inputs/Input15.txt")
            .Select(x => x
                .Select(y => int.Parse(y.ToString()))
                .ToArray())
            .ToArray();
        public override void PartOne()
        {
            CostPathFind(_grid, "Part 1");
        }
        public override void PartTwo()
        {
            var newGrid = ExpandGrid(_grid, 5);
            CostPathFind(newGrid, "Part 2");
        }
        private void CostPathFind(int [][] grid, string partName)
        {
            PriorityQueue pq = new();
            HashSet<(int y, int x)> visited = new();

            pq.Push((0, 0, 0));

            while (pq.Count > 0)
            {
                var item = pq.Pop();
                if (visited.Contains((item.y, item.x)) || IsInNotBounds(grid, item.y, item.x))
                    continue;

                if (item.y == grid.Length - 1 && item.x == grid[0].Length - 1)
                {
                    Console.WriteLine($"{partName}: {item.value}");
                    break;
                }

                // Add all 4 sides
                if (!IsInNotBounds(grid, item.y + 1, item.x))
                    pq.Push((item.y + 1, item.x, grid[item.y + 1][item.x] + item.value)); // down

                if (!IsInNotBounds(grid, item.y - 1, item.x))
                    pq.Push((item.y - 1, item.x, grid[item.y - 1][item.x] + item.value)); // up

                if (!IsInNotBounds(grid, item.y, item.x + 1))
                    pq.Push((item.y, item.x + 1, grid[item.y][item.x + 1] + item.value)); // right

                if (!IsInNotBounds(grid, item.y, item.x - 1))
                    pq.Push((item.y, item.x - 1, grid[item.y][item.x - 1] + item.value)); // left

                visited.Add((item.y, item.x));
            }
        }
        private int[][] ExpandGrid(int[][] grid, int times)
        {
            int[][] newGrid = new int[grid.Length * times][];

            for (int y = 0; y < grid.Length; y++) // Fill to the right first
            {
                newGrid[y] = new int[grid[0].Length * times];
                for (int i = 0; i < grid[0].Length; i++) // Copy to new grid
                    newGrid[y][i] = grid[y][i];

                for (int x = grid[0].Length; x < newGrid[0].Length; x++)
                {
                    var newVal = newGrid[y][x - grid[0].Length] + 1;
                    newGrid[y][x] = newVal <= 9 ? newVal : 1;
                }
            }

            for (int y = grid.Length; y < newGrid.Length; y++) // Fill bottom
            {
                newGrid[y] = new int[grid[0].Length * 5];

                for (int x = 0; x < newGrid[0].Length; x++)
                {
                    var newVal = newGrid[y - grid.Length][x] + 1;
                    newGrid[y][x] = newVal <= 9 ? newVal : 1;
                }
            }

            return newGrid;
        }
        private bool IsInNotBounds(int[][] grid, int y, int x)
        {
            return y < 0 || y >= grid.Length || x < 0 || x >= grid[0].Length;
        }
    }
}
