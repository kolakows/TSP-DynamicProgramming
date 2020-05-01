using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP {
class Program {
  static void Main(string[] args) {
    int[] distanceMatrix = new int[6]{3, 2, 4, 2, 4, 5};
    Graph testGraph = new Graph(distanceMatrix, 4);
    PathDesc result = Algorithm.FindBestCycle(testGraph);
    Console.WriteLine(result.Cost);
    Console.WriteLine(string.Join(" ", result.Path));
  }
}
}
