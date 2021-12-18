using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day18 : AdventOfCode
    {
        private readonly string[] _input = File.ReadAllLines("../../../Inputs/Input18.txt");
        public override void PartOne()
        {
            string finalStr = _input[0];

            for (int i = 1; i < _input.Length; i++)
            {
                finalStr = $"[{finalStr},{_input[i]}]";

                finalStr = Reduce(SplitString(finalStr));
            }
            //Console.WriteLine(finalStr);
            Console.WriteLine($"Part 1: {CalculateMagnitude(SplitString(finalStr))}");

        }
        public override void PartTwo()
        {
            int max = int.MinValue;
            // Check every combination
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = i+1; j < _input.Length-1; j++)
                {
                    var v1 = $"[{_input[i]},{_input[j+1]}]";
                    var v2 = $"[{_input[j+1]},{_input[i]}]";

                    max = Math.Max(max, CalculateMagnitude(SplitString(Reduce(SplitString(v1)))));
                    max = Math.Max(max, CalculateMagnitude(SplitString(Reduce(SplitString(v2)))));
                }
            }
            Console.WriteLine($"Part 2: {max}");
        }
        private int CalculateMagnitude(string[] finalStr)
        {
            for (int i = 0; i < finalStr.Length && finalStr.Length != 1; i++)
            {
                if(finalStr[i] == "[" && finalStr[i + 4] == "]")
                {
                    var n1 = int.Parse(finalStr[i + 1]);
                    var n2 = int.Parse(finalStr[i + 3]);
                    var sum = 3*n1 + 2*n2;

                    finalStr[i] = sum.ToString();
                    finalStr[i + 1] = "";
                    finalStr[i + 2] = "";
                    finalStr[i + 3] = "";
                    finalStr[i + 4] = "";
                    var newStr = string.Join("", finalStr);
                    if (!newStr.Contains("["))
                        return int.Parse(finalStr[0]);

                    finalStr = SplitString(string.Join("", finalStr));
                    i = -1;
                    continue;
                }

            }
            return -1;
        }
        private string Reduce(string[] finalStr)
        {
            int depth = 0;

            // Do all possible explosions
            for (int i = 0; i < finalStr.Length; i++)
            {
                if (finalStr[i] == "[")
                    depth++;
                if (finalStr[i] == "]")
                    depth--;

                if (depth > 4 && finalStr[i] != "[") // explode
                {
                    var leftNum = int.Parse(finalStr[i].ToString());
                    for (int j = i-1; j >= 0; j--)
                    {
                        if (char.IsDigit(finalStr[j][0]))
                        {
                            var num = int.Parse(finalStr[j].ToString());
                            var sum = num + leftNum;
                            finalStr[j] = sum.ToString();
                            break;
                        }
                    }

                    var rightNum = int.Parse(finalStr[i + 2].ToString());
                    for (int j = i + 3; j < finalStr.Length; j++)
                    {
                        if (char.IsDigit(finalStr[j][0]))
                        {
                            var num = int.Parse(finalStr[j].ToString());
                            var sum = num + rightNum;
                            finalStr[j] = sum.ToString();
                            break;
                        }
                    }
                    
                    for (int s = 0; s < 5; s++)
                    {
                        if(s == 0)
                            finalStr[i-1+s] = "0";
                        else
                            finalStr[i-1+s] = "";

                    }
                    finalStr = SplitString(string.Join("", finalStr));
                    i = -1;
                    depth = 0;
                    continue;
                }
            }

            // Then do all possible splits + explosions
            for (int i = 0; i < finalStr.Length; i++)
            {
                if (finalStr[i].Length >= 2) // 10 or more
                {
                    var num = int.Parse(finalStr[i]);

                    finalStr[i] = NumberFormatToReturn(num);
                    finalStr = SplitString(string.Join("", finalStr));
                    finalStr = SplitString(Reduce(finalStr));
                    break;

                }
            }

            return string.Join("", finalStr);
        }
        private string[] SplitString(string str)
        {
            List<string> l = new();

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsDigit(str[i]))
                    l.Add(str[i].ToString());
                else
                {
                    if (!char.IsDigit(str[i + 1]))
                        l.Add(str[i].ToString());
                    else
                    {
                        string tmp = "";

                        for (int ii = i; ii < str.Length; ii++)
                        {
                            if (char.IsDigit(str[ii]))
                            {
                                tmp += str[ii].ToString();
                            }
                            else
                            {
                                i = ii-1;
                                break;
                            }
                        }

                        l.Add(tmp);
                    }
                }
            }

            return l.ToArray();
        }
        private string NumberFormatToReturn(int sum)
        {
            int n1, n2;
            if (sum % 2 != 0)
            {
                n1 = sum / 2;
                n2 = (sum + 1) / 2;
            }
            else
            {
                n1 = n2 = sum / 2;
            }
            return $"[{n1},{n2}]";
        }
    }
}
