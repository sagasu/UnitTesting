using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting.Problems
{

    internal class Shortest_Path_Visiting_All_Nodes
    {
        // https://leetcode.com/problems/shortest-path-visiting-all-nodes/
        // Not my solution
        public int ShortestPathLength(int[][] graph)
        {
            int res = 0, fullMask = (1 << graph.Length) - 1;

            HashSet<(int, int)> used = new();
            Queue<(int mask, int v)> queue = new(Enumerable.Range(0, graph.Length).Select(i => (1 << i, i)));

            while (queue.Any())
            {
                for (int count = queue.Count; count > 0; --count)
                {
                    var pos = queue.Dequeue();

                    if (used.Contains(pos)) continue;
                    if (pos.mask == fullMask) return res;

                    used.Add(pos);
                    foreach (int u in graph[pos.v]) queue.Enqueue((pos.mask | (1 << u), u));
                }
                ++res;
            }

            throw new ArgumentException("Graph isn't connected");
        }
    }
}
