using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting.Problems.Injection
{
    public interface IShortestPathService
    {
        int CalculateShortestPath(int[][] graph, List<int> candidates, Dictionary<int, List<int>> dicGraph, Dictionary<int, int> parentChild);
    }

    public class ShortestPathService : IShortestPathService
    {
        int min = int.MaxValue;
        public int CalculateShortestPath(int[][] graph, List<int> candidates, Dictionary<int, List<int>> dicGraph, Dictionary<int, int> parentChild)
        {
            min = int.MaxValue;
            for (var i = 0; i < graph.Length; i++)
            {
                dicGraph.Add(i, new List<int>());
                if (graph[i].Length == 1)
                {
                    candidates.Add(i);
                }

                dicGraph[i] = graph[i].ToList();
            }

            if (candidates.Count == 0)
            {
                return graph.Length - 1;
            }

            foreach (var i in candidates)
            {
                parentChild = new Dictionary<int, int>();
                Dfs(-1, i, graph.Length, new HashSet<int>(), 0, false, dicGraph, parentChild);
            }

            return min;
        }


        public bool Dfs(int parent, int current, int N, HashSet<int> seen, int cost, bool rec, Dictionary<int, List<int>> dicGraph, Dictionary<int, int> parentChild)
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
            foreach (var nghr in dicGraph[current])
            {
                if (!seen.Contains(nghr) && nghr != parent)
                {
                    res |= Dfs(current, nghr, N, seen, cost + 1, false, dicGraph, parentChild);
                }
            }
            if (!res && parentChild.ContainsKey(parent))
            {
                res = Dfs(parentChild[parent], parent, N, seen, cost + 1, true, dicGraph, parentChild);
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
