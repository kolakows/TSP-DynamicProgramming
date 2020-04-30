using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Structures;

namespace TSP
{
	public static class Algorithm
	{
		//comments:
		//- all intermediate paths end in vertex n-1, easier to implement when indexing from 0
		//
		public static Path FindBestCycle(Graph graph)
		{
			int m = graph.VertexCount - 1;
			FuncDict[] funcDicts = new FuncDict[m];
			for (int i = 0; i < m; i++)
			{
				var dict = new FuncDict();
				for (int j = 0; j < m; j++)
				{
					if(i != j)
					{
						var vertexSet = new HashSet<int>(new List<int>() { j });
						var pathOrder = new List<int>() { i, j, m };	
						dict[vertexSet] = new Path(graph[i, j] + graph[j, m], pathOrder);
					}
				}
				funcDicts[i] = dict;
			}
			for (int k = 2; k < m; k++)
			{
				var nextSizeFuncDicts = new FuncDict[m];
				for (int i = 0; i < m; i++)
				{
					var dict = new FuncDict();
					foreach (var set in CombinationsWithout(m-1,k,i)) //i is the start, m is the end vertex
					{
						dict[set] = FindBestPath(graph, funcDicts, i, set);
					}
					nextSizeFuncDicts[i] = dict;
				}
				funcDicts = nextSizeFuncDicts;
			}
			var vSet = new HashSet<int>(Enumerable.Range(0, m));
			var bestCycle = FindBestPath(graph, funcDicts, m, vSet);
			return bestCycle;
		}

		private static Path FindBestPath(Graph graph, FuncDict[] funcDicts, int i, HashSet<int> set)
		{
			int best = int.MaxValue;
			var pathThrough = -1;
			foreach (var vertex in new HashSet<int>(set))
			{
				set.Remove(vertex);
				var val = graph[i, vertex] + funcDicts[vertex][set].Value;
				if (val < best)
				{
					best = val;
					pathThrough = vertex;
				}
				set.Add(vertex);
			}
			var pathOrder = new List<int>() { i };
			set.Remove(pathThrough);
			pathOrder.AddRange(funcDicts[pathThrough][set].Order);
			set.Add(pathThrough);
			return new Path(best, pathOrder);
		}

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
					if(value + 1 == skipVal)
					{
						value++;
						continue;
					}
					result[index++] = ++value;
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

	public class Path
	{
		public int Value { get; set; }
		public List<int> Order { get; set; }
		public Path(int value, List<int> order)
		{
			Order = order;
			Value = value;
		}
	}

	public class FuncDict
	{
		private readonly Dictionary<HashSet<int>, Path> allPaths = new Dictionary<HashSet<int>, Path>(HashSet<int>.CreateSetComparer());
		public Path this[HashSet<int> visitedVertices]
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
