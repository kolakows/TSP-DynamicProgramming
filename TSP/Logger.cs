using System;
using System.IO;

namespace TSP
{

    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogNewFile(string fileName)
        {
            Console.WriteLine();
            Console.WriteLine(new string('>', 80));
            Console.WriteLine("Starting algorithm for {0} ...", fileName);
        }

        public void LogResult(int cost, long timeMikros)
        {
            Console.WriteLine("Single pass result: {0}, time: {1} mikro s", cost, timeMikros);
        }

        public void LogFinalResults(int bestCost, long bestTime, long avgTime)
        {
            Console.WriteLine("Results:");
            Console.WriteLine("Best solution: {0}", bestCost);
            Console.WriteLine("Best time: {0} mikro s, average: {1} mikro s", bestTime, avgTime);
            Console.WriteLine(new string('<', 80));
        }

        public void LogGraph(Graph graph)
        {
            Console.WriteLine("Graph: {0}", graph.Name);
            Console.WriteLine("Size: {0}", graph.VertexCount);
            for (int i = 0; i < graph.VertexCount; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write("    ");
                }
                Console.Write(" ###");
                for (int j = i + 1; j < graph.VertexCount; j++)
                {
                    Console.Write(" {0,3}", graph[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void LogPath(PathDesc pathDesc)
        {
            Console.WriteLine("Path:");
            Console.WriteLine("Cost: {0}", pathDesc.Cost);
            Console.WriteLine(string.Join(", ", pathDesc.Path));
        }

        public void SaveResults(string graphName, string outFileName, int bestCost, long bestTime, long avgTime, long avgMemory, int vertexCount)
        {
            string outFileDir = Path.GetDirectoryName(outFileName);
            if (!Directory.Exists(outFileDir))
            {
                Directory.CreateDirectory(outFileDir);
            }
            bool headersSet = File.Exists(outFileName);

            using (StreamWriter sw = File.AppendText(outFileName))
            {
                if (!headersSet)
                {
                    sw.WriteLine("Graph,Best cost,Best time,Average time,Average memory,Graph size");
                }
                sw.WriteLine("{0},{1},{2},{3},{4},{5}", graphName, bestCost, bestTime, avgTime, avgMemory, vertexCount);
            }
        }

        public void SavePath(string outPathFileName, PathDesc pathDesc)
        {
            string outFileDir = Path.GetDirectoryName(outPathFileName);
            if (!Directory.Exists(outFileDir))
            {
                Directory.CreateDirectory(outFileDir);
            }
            string[] items = { pathDesc.Cost.ToString(), string.Join(", ", pathDesc.Path) };
            File.WriteAllLines(outPathFileName, items);
        }

        private static Logger getLogger = new Logger();
        public static Logger GetLogger => getLogger;
    }
}
