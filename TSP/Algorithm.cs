using System.Collections.Generic;
using System.Linq;

namespace TSP
{
    public static class Algorithm
    {
        // Main loop of DP algorithm
        // Note: Start and end vertex is n - 1, not 0
        public static PathDesc FindBestCycle(Graph graph)
        {
            int n = graph.VertexCount;
            int cycleStart = graph.VertexCount - 1;
            Solution[] currentSolutions = new Solution[n - 1];

            // Initialization for all paths i-j-cycleStart
            for (int i = 0; i < cycleStart; i++)
            {
                Solution sol = new Solution();
                for (int j = 0; j < i; j++)
                {
                    HashSet<int> vertexSet = new HashSet<int>(new List<int>() { j });
                    List<int> path = new List<int>() { i, j, cycleStart };
                    sol[vertexSet] = new PathDesc(graph[i, j] + graph[j, cycleStart], path);
                }
                for (int j = i + 1; j < cycleStart; j++)
                {
                    HashSet<int> vertexSet = new HashSet<int>(new List<int>() { j });
                    List<int> path = new List<int>() { i, j, cycleStart };
                    sol[vertexSet] = new PathDesc(graph[i, j] + graph[j, cycleStart], path);
                }
                currentSolutions[i] = sol;
            }

            // k == number of edges on each path from the previous iteration
            for (int k = 2; k < n - 1; k++)
            {
                Solution[] nextSolutions = new Solution[n - 1];
                for (int i = 0; i < cycleStart; i++)
                {
                    Solution sol = new Solution();
                    // Iterate over all k-sized subsets of [n - 1]\{i, cycleStart}
                    // (all previously found paths from jm to cycleStart of length k).
                    // i is the start of the longer (k + 1 edges) path.
                    // Find best shorter path jm...cycleStart to join i to.
                    foreach (
                        HashSet<int> vertexSet in CombinationsWithout(cycleStart - 1, k, i))
                    {
                        sol[vertexSet] = FindBestPath(graph, currentSolutions, i, vertexSet);
                    }
                    nextSolutions[i] = sol;
                }
                currentSolutions = nextSolutions;
            }

            // Get the final solution - join cycleStart to best (n - 1)-sized
            // jm...cycleStart path.
            HashSet<int> fullVertexSet = new HashSet<int>(Enumerable.Range(0, n - 1));
            PathDesc bestCycle = FindBestPath(graph, currentSolutions, cycleStart, fullVertexSet);
            return bestCycle;
        }

        // Choose best path in current solution to join vertex i
        private static PathDesc FindBestPath(Graph graph, Solution[] currentSolutions, int i, HashSet<int> set)
        {
            int bestCost = int.MaxValue;
            int m = -1;
            HashSet<int> copy = new HashSet<int>(set);
            foreach (int vertex in copy)
            {
                set.Remove(vertex);
                int cost = graph[i, vertex] + currentSolutions[vertex][set].Cost;
                if (cost < bestCost)
                {
                    bestCost = cost;
                    m = vertex;
                }
                set.Add(vertex);
            }
            List<int> path = new List<int>() { i };
            set.Remove(m);
            path.AddRange(currentSolutions[m][set].Path);
            set.Add(m);
            return new PathDesc(bestCost, path);
        }

        // Generate all combinations of k elements out of [n]\{skipVal}
        public static IEnumerable<HashSet<int>> CombinationsWithout(int n, int k, int skipVal)
        {
            int[] result = new int[k];
            Stack<int> stack = new Stack<int>();
            stack.Push(-1);
            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();

                while (value < n)
                {
                    value++;

                    if (value == skipVal)
                    {
                        continue;
                    }
                    result[index++] = value;
                    stack.Push(value);

                    if (index == k)
                    {
                        yield return new HashSet<int>(result);
                        break;
                    }
                }
            }
        }
    }

    public class Solution
    {
        private readonly Dictionary<HashSet<int>, PathDesc> allPaths = new Dictionary<HashSet<int>, PathDesc>(HashSet<int>.CreateSetComparer());
        public PathDesc this[HashSet<int> visitedVertices]
        {
            get
            {
                return allPaths[visitedVertices];
            }
            set
            {
                allPaths[visitedVertices] = value;
            }
        }
    }
}
