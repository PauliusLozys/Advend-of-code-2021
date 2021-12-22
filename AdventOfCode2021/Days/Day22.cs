using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day22 : AdventOfCode
    {
        private string[] _input = File.ReadAllLines("../../../Inputs/Input22.txt");
        public override void PartOne()
        {
            HashSet<(int x, int y, int z)> cube = new();

            foreach (var line in _input)
            {
                var res = ParseLine(line);

                if (res.xMax < -50 || res.yMax < -50 || res.zMax < -50 || res.xMin > 50 || res.yMin > 50 || res.zMin > 50)
                    continue;

                for (int x = Math.Max(-50, res.xMin); x <= Math.Min(50, res.xMax); x++)
                {
                    for (int y = Math.Max(-50, res.yMin); y <= Math.Min(50, res.yMax); y++)
                    {
                        for (int z = Math.Max(-50, res.zMin); z <= Math.Min(50, res.zMax); z++)
                        {
                            if (res.add)
                                cube.Add((x, y, z));
                            else
                                cube.Remove((x, y, z));
                        }
                    }
                }
            }

            // check area
            int count = 0;
            for (int x = -50; x <= 50; x++)
            {
                for (int y = -50; y <= 50; y++)
                {
                    for (int z = -50; z <= 50; z++)
                    {
                        if (cube.Contains((x, y, z)))
                            count++;
                    }
                }
            }

            Console.WriteLine($"Part 1: {count}");

        }

        public override void PartTwo()
        {

        }

        private (bool add, int xMin, int xMax, int yMin, int yMax, int zMin, int zMax) ParseLine(string line)
        {
            var tokens = line.Split();
            bool add = true;
            if (tokens[0] == "off")
                add = false;

            var axes = tokens[1].Split(',');
            var xAxis = axes[0].Split("..");
            var yAxis = axes[1].Split("..");
            var zAxis = axes[2].Split("..");

            int xMin = int.Parse(xAxis[0][2..]);
            int xMax = int.Parse(xAxis[1]);

            int yMin = int.Parse(yAxis[0][2..]);
            int yMax = int.Parse(yAxis[1]);

            int zMin = int.Parse(zAxis[0][2..]);
            int zMax = int.Parse(zAxis[1]);

            return (add, xMin, xMax, yMin, yMax, zMin, zMax);

        }
    }
}
