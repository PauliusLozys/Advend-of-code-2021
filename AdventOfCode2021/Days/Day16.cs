using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Packet
    {
        public int Version { get; set; }
        public int ID { get; set; }
        public long Number { get; set; }
        public long Value { get; set; } = 0;
        public int LengthType { get; set; }
        public int Length { get; set; }
        public List<Packet> SubPackets { get; set; } = new();
        public void Evaluate()
        {
            foreach (var sub in SubPackets)
                sub.Evaluate();

            Value = ID switch
            {
                0 => SubPackets.Sum(c => c.Value),
                1 => SubPackets.Select(x => x.Value).Aggregate((x, y) => x * y),
                2 => SubPackets.Min(x => x.Value),
                3 => SubPackets.Max(x => x.Value),
                4 => Number,
                5 => SubPackets[0].Value > SubPackets[1].Value ? 1 : 0,
                6 => SubPackets[0].Value < SubPackets[1].Value ? 1 : 0,
                7 => SubPackets[0].Value == SubPackets[1].Value ? 1 : 0,
                _ => long.MinValue,
            };
        }

    }
    class Day16 : AdventOfCode
    {
        private readonly string _input = File.ReadAllText("../../../Inputs/Input16.txt");
        public override void PartOne()
        {
            var packet = HexadecimalToBinary(_input);
            int start = 0;
            var package = ParsePacket(packet, ref start);

            int versionSum = GetVersionSum(package);
            Console.WriteLine($"Part 1: {versionSum}");

        }
        private int GetVersionSum(Packet packet)
        {
            if (packet.SubPackets.Count == 0)
                return packet.Version;
            int versionSum = 0;

            foreach (var sub in packet.SubPackets)
            {
                versionSum += GetVersionSum(sub);
            }

            return versionSum + packet.Version;
        }
        private Packet ParsePacket(string packet, ref int index)
        {
            Packet package = new();

            //Read Version number
            package.Version = Convert.ToInt32(packet[index..(index + 3)], 2);
            index += 3; // Move by 3
            //Read packet type
            package.ID = Convert.ToInt32(packet[index..(index + 3)], 2);
            index += 3;
            // if type != 4 read 1 bit
            if(package.ID != 4)
            {
                package.LengthType = Convert.ToInt32(packet[index..(index + 1)], 2);
                index++;
                if (package.LengthType == 0) // Read 15bits
                {
                    package.Length = Convert.ToInt32(packet[index..(index + 15)], 2);
                    index += 15;

                    var startedParsingAt = index;
                    while(index - startedParsingAt != package.Length)
                    {
                        package.SubPackets.Add(ParsePacket(packet, ref index));
                    }
                }
                else
                {
                    // Number of packets to parse
                    package.Length = Convert.ToInt32(packet[index..(index + 11)], 2);
                    index += 11;

                    for (int i = 0; i < package.Length; i++)
                    {
                        package.SubPackets.Add(ParsePacket(packet, ref index));
                    }
                }
            }
            else // Parse literal
            {
                bool keepParsing = true;
                string strNumber = "";
                while (keepParsing)
                {
                    var firstBit = Convert.ToInt32(packet[index..(index + 1)], 2);
                    index++;
                    if (firstBit == 0)
                        keepParsing = false;

                    var number = packet[index..(index + 4)];
                    index += 4;
                    strNumber += number;
                }
                package.Number = Convert.ToInt64(strNumber, 2);
            }
            return package;
        }

        public override void PartTwo()
        {
            var packet = HexadecimalToBinary(_input);
            int start = 0;
            var package = ParsePacket(packet, ref start);
            package.Evaluate();
            Console.WriteLine($"Part 2: {package.Value}");

        }
        private string HexadecimalToBinary(string hexadecimal)
        {
            string binarystring = string.Join(string.Empty,
              hexadecimal.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );

            return binarystring;
        }
    }
}
