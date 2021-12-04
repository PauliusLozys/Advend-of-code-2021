using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Board
    {
        public int[][] TheBoard { get; set; }
        public bool BoardIsCompleted { get; set; }
        public int GetUnmarkedNumberSum()
        {
            int sum = 0;
            for (int i = 0; i < TheBoard.Length; i++)
            {
                for (int j = 0; j < TheBoard[i].Length; j++)
                {
                    if(TheBoard[i][j] != int.MinValue)
                        sum += TheBoard[i][j];
                }
            }
            return sum;
        }
        public void MarkNumber(int number)
        {
            for (int i = 0; i < TheBoard.Length; i++)
            {
                for (int j = 0; j < TheBoard[i].Length; j++)
                {
                    if (TheBoard[i][j] == number)
                        TheBoard[i][j] = int.MinValue;
                }
            }
        }
        public bool CheckIfWon()
        {
            for (int i = 0; i < TheBoard.Length; i++)
            {
                // Check rows
                if (TheBoard[i].All(x => x == int.MinValue))
                    return true;
            }


            for (int i = 0; i < TheBoard.Length; i++)
            {
                // Check collumns
                bool won = true;
                for (int j = 0; j < TheBoard.Length; j++)
                {
                    if(TheBoard[j][i] != int.MinValue)
                    {
                        won = false;
                        break;
                    }
                }
                if (won)
                    return true;
            }

            return false;
        }
    }

    class Day4 : AdventOfCode
    {
        private readonly int[] _bingoNumbers;
        private readonly Board[] _boards;

        public Day4()
        {
            var file = File.ReadAllLines("../../../Inputs/Input4.txt");
            _bingoNumbers = file[0].Split(',').Select(x => int.Parse(x)).ToArray();

             List<Board> boards = new();
             List<int[]> currentBoard = new();

            foreach (var line in file[2..]) // Start at 2 to skip ""
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    Board b = new();
                    b.TheBoard = currentBoard.ToArray(); ;
                    boards.Add(b);
                    currentBoard = new();
                    continue;
                }

                var row = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                currentBoard.Add(row);
            }
            // Add last board
            Board last = new();
            last.TheBoard = currentBoard.ToArray();
            boards.Add(last);

            _boards = boards.ToArray();
        }

        public override void PartOne()
        {
            foreach (var number in _bingoNumbers)
            {
                foreach (var board in _boards)
                {
                    board.MarkNumber(number);
                    if (board.CheckIfWon())
                    {
                        board.BoardIsCompleted = true;

                        Console.WriteLine($"Part 1: {board.GetUnmarkedNumberSum() * number}");
                        return;
                    }
                }
            }
        }

        public override void PartTwo()
        {
            var lastScore = 0;
            foreach (var number in _bingoNumbers)
            {
                foreach (var board in _boards)
                {
                    if (board.BoardIsCompleted)
                        continue;

                    board.MarkNumber(number);
                    if (board.CheckIfWon())
                    {
                        board.BoardIsCompleted = true;
                        lastScore = board.GetUnmarkedNumberSum() * number;
                    }
                }
            }
            Console.WriteLine($"Part 2: {lastScore}");
        }
    }
}
