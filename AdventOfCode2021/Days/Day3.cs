using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day3 : AdventOfCode
    {
        private readonly string[] _input = File.ReadAllLines("../../../Inputs/Input3.txt");

        public override void PartOne()
        {
            int[] accumulator = new int[_input[0].Length];
            StringBuilder gamma = new(_input[0].Length);
            StringBuilder epsilon = new(_input[0].Length);

            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    if (_input[i][j] == '0')
                        accumulator[j]++;
                }
            }

            for (int i = 0; i < accumulator.Length; i++)
            {
                var y = _input.Length - accumulator[i];
                if(accumulator[i] > y)
                {
                    gamma.Append(0); ;
                    epsilon.Append(1);
                }
                else
                {
                    gamma.Append(1); ;
                    epsilon.Append(0);
                }
            }

            int g = Convert.ToInt32(gamma.ToString(), 2);
            int e = Convert.ToInt32(epsilon.ToString(), 2);
            Console.WriteLine($"Part 1: {g*e}");

        }

        public override void PartTwo()
        {
            List<string> listForOxygen = _input.ToList();
            List<string> listForCo2 = _input.ToList();

            var oxygen = FilterPart2(listForOxygen, (x, y) => x >= y);
            var co2 = FilterPart2(listForCo2, (x, y) => x < y);

            int o = Convert.ToInt32(oxygen, 2);
            int c = Convert.ToInt32(co2, 2);

            Console.WriteLine($"Part 2 {o*c}");
        }

        private string FilterPart2(List<string> list, Func<int, int, bool> condition)
        {
            var length = list[0].Length;
            for (int currentBit = 0; currentBit < length; currentBit++)
            {
                if (list.Count == 1)
                    break;
                // Find most common bit at this place
                int one = 0,
                    zero = 0;

                foreach (var bit in list)
                {
                    if (bit[currentBit] == '0')
                        zero++;
                    else
                        one++;
                }

                char bitsToFilter = condition(one, zero) ? '0' : '1';

                list.RemoveAll(x => x[currentBit] == bitsToFilter);
            }

            return list[0];
        }
    }
}
