using System;

namespace TSP {
public class Graph {
  private AdjTriangle Distances;
  public int VertexCount {
    get;
    set;
  }

  public int this[int i, int j] {
    get { return Distances[i, j]; }
  }

  public Graph(int[] adjTriangle, int vertexCount) {
    Distances = new AdjTriangle(adjTriangle);
    VertexCount = vertexCount;
  }
}

public class AdjTriangle {
  private int[] Weights;
  private int VertexCount;

  public int this[int i, int j] {
    get {
      if (i > j) {
        return Weights[(j * (2 * VertexCount - 1 - j)) / 2 + i - j - 1];
      }
      if (i < j) {
        return Weights[(i * (2 * VertexCount - 1 - i)) / 2 + j - i - 1];
      }
      throw new System.ArgumentException("Cannot get distance to itself", "i");
    }
  }

  public AdjTriangle(int[] weights, int vertexCount) {
    Weights = weights;
    VertexCount = vertexCount;
  }
}

public class PathDesc {
  public int Cost {
    get;
    set;
  }
  public List<int>Path {
    get;
    set;
  }
  public PathDesc(int cost, List<int>path) {
    Path = path;
    Cost = cost;
  }
}

}
