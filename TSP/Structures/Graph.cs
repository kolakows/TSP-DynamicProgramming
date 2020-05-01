using System;

namespace TSP.Structures
{
	public class Graph
	{
		private int[,] DistanceMatrix { get; set; }
		public int VertexCount { get; set; }

		public int this[int i, int j]
		{
			get
			{
				return DistanceMatrix[Math.Min(i, j), Math.Max(i, j)]; //upper triangle matrix, LU
			}
		}

		public Graph(int[,] distanceMatrix, int vertexCount)
		{
			DistanceMatrix = distanceMatrix;
			VertexCount = vertexCount;
		}
	}
}
