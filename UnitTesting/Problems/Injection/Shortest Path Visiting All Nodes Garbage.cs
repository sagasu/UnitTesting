using System.Collections.Generic;

namespace UnitTesting.Problems.Injection
{
    public interface IShortestPathVisitingAllNodesGarbage
    {
        int ShortestPathLength(int[][] graph);
    }

    // https://leetcode.com/problems/shortest-path-visiting-all-nodes/
    // Not my solution but true garbage
    internal class ShortestPathVisitingAllNodesGarbage : IShortestPathVisitingAllNodesGarbage
    {
        public IShortestPathService _shortestPathService;

        public ShortestPathVisitingAllNodesGarbage(IShortestPathService shortestPathService)
        {
            _shortestPathService = shortestPathService;
        }
        
        public int ShortestPathLength(int[][] graph)
        {
            var dic = new Dictionary<int, List<int>>();
            var parentChild = new Dictionary<int, int>();
            var candidates = new List<int>();
            
            return _shortestPathService.CalculateShortestPath(graph, candidates, dic, parentChild);
        }

        
    }
}
