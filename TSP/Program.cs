using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP.Structures;

namespace TSP
{
	class Program
	{
		static void Main(string[] args)
		{
			var distanceMatrix = new int[4,4]{
				{0, 3, 2, 4},
				{0, 0, 2, 4},
				{0, 0, 0, 5},
				{0, 0, 0, 0}
			};
			var testGraph = new Graph(distanceMatrix, 4);
			var cycle = Algorithm.FindBestCycle(testGraph);
			Console.WriteLine(cycle.Value);
			Console.WriteLine(string.Join(" ", cycle.Order));
		}
	}
}
