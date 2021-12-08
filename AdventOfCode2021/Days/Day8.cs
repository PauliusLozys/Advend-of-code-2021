using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day8 : AdventOfCode
    {
        private readonly string[][] _input = File.ReadAllLines("../../../Inputs/Input8.txt").Select(x => x.Split(" | ")).ToArray();

        public override void PartOne()
        {
            int count = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                var displays = _input[i][1];
                var display = displays.Split();
                foreach (var item in display)
                {
                    var len = item.Length;
                    if (len == 2 || len == 4 || len == 3 || len == 7)
                        count++;

                }
            }
            Console.WriteLine($"Part 1: {count}");
        }

        public override void PartTwo()
        {
            int count = 0;
            string[] digits = new string[10];

            for (int i = 0; i < _input.Length; i++)
            {
                // Part 1
                var displays = _input[i][0];
                var display = displays.Split();
                foreach (var item in display)
                {
                    var len = item.Length;
                    switch (len)
                    {
                        case 2:
                            digits[1] = item;
                            break;
                        case 4:
                            digits[4] = item;
                            break;
                        case 3:
                            digits[7] = item;
                            break;
                        case 7:
                            digits[8] = item;
                            break;
                    }
                }

                foreach (var item in display)
                {
                    switch (item.Length)
                    {
                        case 5: // set 5, 2, 3

                            // match 3 with 7
                            bool contains3 = true;

                            foreach (var c in digits[7])
                            {
                                if (!item.Contains(c))
                                    contains3 = false;
                            }

                            if (contains3)
                            {
                                digits[3] = item;
                                break;
                            }

                            // match 5 with 1 and 4

                            bool contains5 = true;

                            foreach (var c in digits[4].Except(digits[1]))
                            {
                                if (!item.Contains(c))
                                    contains5 = false;
                            }

                            if (contains5)
                            {
                                digits[5] = item;
                                break;
                            }

                            // else only left is 2

                            digits[2] = item;

                            break;
                        case 6: // set 9, 6, 0
                            // match 9 with 4
                            bool contains9 = true;

                            foreach (var c in digits[4])
                            {
                                if (!item.Contains(c))
                                    contains9 = false;
                            }

                            if (contains9)
                            {
                                digits[9] = item;
                                break;
                            }

                            // match 0 with 7

                            bool contains0 = true;

                            foreach (var c in digits[7])
                            {
                                if (!item.Contains(c))
                                    contains0 = false;
                            }

                            if (contains0)
                            {
                                digits[0] = item;
                                break;
                            }

                            // else only left is 6

                            digits[6] = item;

                            break;
                    }
                }

                // Part 2
                var display2 = _input[i][1].Split();
                string number = "";
                foreach (var segment in display2)
                {
                    var t = segment.ToCharArray();
                    // match number
                    for (int i1 = 0; i1 < digits.Length; i1++)
                    {
                        var num = digits[i1].ToCharArray();
                        if (t.Length != num.Length)
                            continue;

                        var tt = num.Except(segment).ToArray();
                        if (tt.Length == 0) // whole was matched
                        {
                            number += i1;
                            break;
                        }
                    }
                }
                count += int.Parse(number);
            }
            Console.WriteLine($"Part 2: {count}");
        }

    }
}
