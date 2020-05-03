using System;
using System.Collections.Generic;
using System.IO;

namespace TSP
{

    public class Logger
    {
        public enum LogLevelType { Info, Debug, Insane }
        public LogLevelType LogLevel = LogLevelType.Info;
        private List<string> LogLevelTypeNames = new List<string>() { " INFO ", " DEBUG", "INSANE" };

        public void Log(LogLevelType level, string message)
        {
            if (level <= LogLevel)
            {
                Console.WriteLine(message);
            }
        }

        public void LogNewFile(LogLevelType level, string fileName)
        {
            if (level <= LogLevel)
            {
                Console.WriteLine();
                Console.WriteLine(new string('>', 80));
                Console.Write("[{0}:] ", LogLevelTypeNames[(int)level]);
                Console.WriteLine("Starting algorithm for {0} ...", fileName);
            }
        }

        public void LogResult(LogLevelType level, int cost, long timeMikros)
        {
            if (level <= LogLevel)
            {
                Console.Write("[{0}:] ", LogLevelTypeNames[(int)level]);
                Console.WriteLine("Single pass result: {0}, time: {1} mikro s", cost, timeMikros);
            }
        }

        public void LogFinalResults(LogLevelType level, int bestCost, int avgCost, long bestTime, long avgTime)
        {
            if (level <= LogLevel)
            {
                Console.WriteLine("[{0}:] Results:", LogLevelTypeNames[(int)level]);
                Console.WriteLine("Best solution: {0}, average: {1}", bestCost, avgCost);
                Console.WriteLine("Best time: {0} mikro s, average: {1} mikro s", bestTime, avgTime);
                Console.WriteLine(new string('<', 80));
            }
        }

        public void LogGraph(LogLevelType level, Graph graph)
        {
            if (level <= LogLevel)
            {
                Console.WriteLine("[{0}:] Graph: {1}", LogLevelTypeNames[(int)level], graph.Name);
                Console.WriteLine("Size: {0}", graph.VertexCount);
                for (int i = 0; i < graph.VertexCount; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        Console.Write("   ");
                    }
                    Console.Write(" x ");
                    for (int j = i + 1; j < graph.VertexCount; j++)
                    {
                        Console.Write(graph[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }

        public void LogPath(LogLevelType level, PathDesc pathDesc)
        {
            if (level <= LogLevel)
            {
                Console.WriteLine("[{0}:] Path:", LogLevelTypeNames[(int)level]);
                Console.WriteLine("Cost: {0}", pathDesc.Cost);
                Console.WriteLine(string.Join(", ", pathDesc.Path));
            }
        }

        public void SaveResults(string graphName, string outFileName, int bestCost, int avgCost, long bestTime, long avgTime)
        {
            bool headersSet = File.Exists(outFileName);

            using (StreamWriter sw = File.AppendText(outFileName))
            {
                if (!headersSet)
                {
                    sw.WriteLine("Graph,Best cost,Average cost,Best time,Average time");
                }
                sw.WriteLine("{0},{1},{2},{3},{4}", graphName, bestCost, avgCost, bestTime, avgTime);
            }
        }

        public void SavePath(string outPathFileName, PathDesc pathDesc)
        {
            string[] items = { pathDesc.Cost.ToString(), string.Join(", ", pathDesc.Path) };
            File.WriteAllLines(outPathFileName, items);
        }

        private static Logger getLogger = new Logger();
        public static Logger GetLogger => getLogger;
    }
}
