using System.Collections.Generic;
using System.IO;
using System;

namespace TSP
{
    public class Graph
    {
        public string Name { get; set; }
        private AdjTriangle Weights;
        public int VertexCount { get; set; }

        public int this[int i, int j]
        {
            get { return Weights[i, j]; }
        }

        public Graph(int[] adjTriangle, int vertexCount, string name)
        {
            Weights = new AdjTriangle(adjTriangle, vertexCount);
            VertexCount = vertexCount;
            Name = name;
        }

        public Graph(string fileName)
        {
            StreamReader reader = File.OpenText(fileName);
            string line = reader.ReadLine();
            if (line == null)
            {
                throw new ArgumentException("Graph file is empty");
            }
            if (!int.TryParse(line, out int n))
            {
                throw new ArgumentException("The file does not start with integral graph size");
            }

            int[] weights = new int[n * (n - 1) / 2];
            for (int i = 0; i < n - 1; i++)
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    throw new ArgumentException("Too few lines in the graph file");
                }

                string[] items = line.Split(',');
                if (items.Length != n - i - 1)
                {
                    throw new ArgumentException(string.Format("Too few elements in line {0}", i + 1));
                }
                for (int j = i + 1; j < n; j++)
                {
                    int ind = j - i - 1;
                    if (!int.TryParse(items[ind], out int w))
                    {
                        throw new ArgumentException(string.Format("Incorrect entry {0} on line {1}", i + 1, ind));
                    }
                    weights[(i * (2 * n - 1 - i)) / 2 + ind] = w;
                }
            }
            VertexCount = n;
            Weights = new AdjTriangle(weights, VertexCount);
            Name = Path.GetFileName(fileName);
        }
    }

    public class AdjTriangle
    {
        private int[] Weights;
        private int VertexCount;

        public int this[int i, int j]
        {
            get
            {
                if (i < j)
                {
                    return Weights[(i * (2 * VertexCount - 1 - i)) / 2 + j - i - 1];
                }
                if (i > j)
                {
                    return Weights[(j * (2 * VertexCount - 1 - j)) / 2 + i - j - 1];
                }
                throw new ArgumentException("Cannot get distance to itself", "i");
            }
        }

        public AdjTriangle(int[] weights, int vertexCount)
        {
            Weights = weights;
            VertexCount = vertexCount;
        }
    }

    public class PathDesc
    {
        public int Cost { get; set; }
        public List<int> Path { get; set; }
        public PathDesc(int cost, List<int> path)
        {
            Path = path;
            Cost = cost;
        }
    }

}
