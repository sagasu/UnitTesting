using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting.Problems.Property
{
    public interface IShortestPathVisitingAllNodesGarbage
    {
        int ShortestPathLength(int[][] graph);
    }

    internal class ShortestPathVisitingAllNodesGarbageProperty : IShortestPathVisitingAllNodesGarbage
    {
        // https://leetcode.com/problems/shortest-path-visiting-all-nodes/
        // Not my solution but true garbage
        Dictionary<int, List<int>> graphDic;
        Dictionary<int, int> parentChild;
        int min = int.MaxValue;

        public List<int> Candidates { get; set; }

        public int ShortestPathLength(int[][] graph)
        {
            graphDic = new Dictionary<int, List<int>>();
            parentChild = new Dictionary<int, int>();
            Candidates = new List<int>();
            
            return CalculateShortestPath(graph, Candidates);
        }

        public int CalculateShortestPath(int[][] graph, List<int> candidates)
        {
            min = int.MaxValue;
            for (var i = 0; i < graph.Length; i++)
            {
                graphDic.Add(i, new List<int>());
                if (graph[i] == null) return 0;
                if (graph[i].Length == 1)
                {
                    candidates.Add(i);
                }

                graphDic[i] = graph[i].ToList();
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
            foreach (var nghr in graphDic[current])
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
