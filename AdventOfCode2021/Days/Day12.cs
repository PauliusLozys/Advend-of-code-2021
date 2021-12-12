using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Node
    {
        public string Name { get; set; }
        public List<string> Neighbors { get; set; } = new();
        public bool WasVisited { get; set; } = false;
        public int TimeVisited { get; set; } = 0;
    }
    class Day12 : AdventOfCode
    {
        private readonly Dictionary<string, Node> _graph = new();
        private int countPartOne = 0;
        private int countPartTwo = 0;
        public Day12()
        {
            var file = File.ReadAllLines("../../../Inputs/Input12.txt")
                .Select(x => x.Split('-')).ToArray();

            foreach (var item in file)
            {
                if (!_graph.ContainsKey(item[0]))
                {
                    _graph.Add(item[0], new Node() { Name = item[0] });
                }

                if (!_graph.ContainsKey(item[1]))
                {
                    _graph.Add(item[1], new Node() { Name = item[1] });
                }

                _graph[item[0]].Neighbors.Add(item[1]);
                _graph[item[1]].Neighbors.Add(item[0]);
            }
        }
        public override void PartOne()
        {
            foreach (var start in _graph["start"].Neighbors)
            {
                VisitPartOne(start);
            }

            Console.WriteLine($"Part 1: {countPartOne}");
        }
        public override void PartTwo()
        {
            foreach (var start in _graph["start"].Neighbors)
            {
                VisitPartTwo(start, false);
            }

            Console.WriteLine($"Part 2: {countPartTwo}");
        }
        private void VisitPartOne(string node)
        {
            var currNode = _graph[node];
            if (currNode.WasVisited || currNode.Name == "start")
                return;

            if(currNode.Name == "end")
            {
                countPartOne++;
                return;
            }

            if (char.IsLower(currNode.Name[0]))
            {
                currNode.WasVisited = true;

                // Recursive call
                foreach (var nextNode in currNode.Neighbors)
                {
                    VisitPartOne(nextNode);
                }
                
                currNode.WasVisited = false;
            }
            else
            {
                // Same recursive call
                foreach (var nextNode in currNode.Neighbors)
                {
                    VisitPartOne(nextNode);
                }
            }
        }
        private void VisitPartTwo(string node, bool locked)
        {
            var currNode = _graph[node];
            if (currNode.WasVisited || currNode.Name == "start")
                return;

            if (currNode.Name == "end")
            {
                countPartTwo++;
                return;
            }

            if (char.IsLower(currNode.Name[0])) // For lowercase letters
            {
                bool lockAllOthers = locked;
                currNode.TimeVisited++;

                if(currNode.TimeVisited >= 2 && !locked) // when small node gets visited 2 times
                {
                    currNode.WasVisited = true;
                    lockAllOthers = true; // Lock all small nodes for the rest of the tree
                }
                else if (currNode.TimeVisited >= 2 && locked) // This node counts as already visited
                {
                    currNode.TimeVisited--;
                    return;
                }

                // Recursive call
                foreach (var nextNode in currNode.Neighbors)
                {
                    VisitPartTwo(nextNode, lockAllOthers);
                }

                currNode.WasVisited = false;
                currNode.TimeVisited--;
            }
            else
            {
                // Same recursive call
                foreach (var nextNode in currNode.Neighbors)
                {
                    VisitPartTwo(nextNode, locked);
                }
            }
        }
    }
}