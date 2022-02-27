using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting.Problems
{

    internal class Shortest_Path_Visiting_All_Nodes_Garbage
    {
        // https://leetcode.com/problems/shortest-path-visiting-all-nodes/
        // Not my solution but true garbage
        Dictionary<int, List<int>> _graph;
        Dictionary<int, int> parentChild;
        int min = int.MaxValue;

        public int ShortestPathLength(int[][] graph)
        {
            _graph = new Dictionary<int, List<int>>();
            parentChild = new Dictionary<int, int>();
            var candidates = new List<int>();
            min = int.MaxValue;
            for (var i = 0; i < graph.Length; i++)
            {
                _graph.Add(i, new List<int>());
                if (graph[i].Length == 1)
                {
                    candidates.Add(i);
                }
                _graph[i] = graph[i].ToList();
            }
            if (candidates.Count == 0)
            {
                return graph.Length - 1;
            }
            foreach (var i in candidates)
            {
                parentChild = new Dictionary<int, int>();
                Dfs(-1, i, graph.Length, new HashSet<int>(), 0, false);

            }
            return min;
        }


        public bool Dfs(int parent, int current, int N, HashSet<int> seen, int cost, bool rec)
        {
            seen.Add(current);
            if (!parentChild.ContainsKey(current))
            {
                parentChild.Add(current, parent);
            }
            if (seen.Count == N)
            {
                min = Math.Min(min, cost);
                seen.Remove(current);
                parentChild.Remove(current);
                return true;
            }
            var res = false;
            foreach (var nghr in _graph[current])
            {
                if (!seen.Contains(nghr) && nghr != parent)
                {
                    res |= Dfs(current, nghr, N, seen, cost + 1, false);
                }
            }
            if (!res && parentChild.ContainsKey(parent))
            {
                res = Dfs(parentChild[parent], parent, N, seen, cost + 1, true);
                if (!rec)
                {
                    seen.Remove(current);
                }
                parentChild.Remove(current);
                return res;
            }
            if (!rec)
            {
                seen.Remove(current);
                parentChild.Remove(current);
            }
            return res;
        }
    }
}
