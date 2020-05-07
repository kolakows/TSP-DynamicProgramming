using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			GenerateGraphs(10, 50, 1, 100, 123, "rnd", "./randomInstances");
		}

		static void GenerateGraphs(int minVertex, int maxVertex, int minWeight, int maxWeight, int seed, string baseFileName, string targetDirectoryPath)
		{
			if (!Directory.Exists(targetDirectoryPath))
				Directory.CreateDirectory(targetDirectoryPath);
			var generator = new Random(seed);
			for (int n = minVertex; n < maxVertex + 1; n++)
			{
				var instance = new StringBuilder();
				instance.AppendLine(n.ToString());
				for (int lineLength = n-1; lineLength > 0; lineLength--)
				{
					var edgeWeights = new List<int>();
					for (int i = 0; i < lineLength; i++)
						edgeWeights.Add(generator.Next(minWeight, maxWeight));
					instance.AppendLine(string.Join(",", edgeWeights.Select(x => x.ToString()).ToArray()));
				}
				var savePath = Path.Combine(targetDirectoryPath, string.Join("",baseFileName,"-n",n.ToString(),".txt"));
				File.WriteAllText(savePath, instance.ToString());
			}
		}
	}
}
