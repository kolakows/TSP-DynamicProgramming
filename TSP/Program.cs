using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace TSP
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Starting Dynamic TSP...");

            Console.WriteLine("Select mode: b - batch, i - interactive");
            int mode = Console.Read();
            Console.ReadLine();

            if (mode == 'b')
            {
                RunBatchMode();
            }
            else
            {
                RunInteractiveMode();
            }
        }

        static void RunBatchMode()
        {
            int repetitions = 10;
            List<string> files = new List<string>()
            {
                "testGraph.txt"
            };

            foreach (var file in files)
            {
                Algorithm.RunAlgorithm(file, repetitions, "results.csv", string.Format("{0}_out.txt", file));
            }
        }

        static void RunInteractiveMode()
        {
            Console.WriteLine("Choose a graph instance for testing");
            string fileName = ChooseFile("Choose a graph instance for tests", "Graph files (*.txt)|*.txt");
            if (fileName == "")
            {
                return;
            }

            Console.WriteLine("Write results file name (default: <graph_name>_out.txt)");
            string outPathFileName = Console.ReadLine();
            if (outPathFileName == "")
            {
                outPathFileName = string.Format("{0}_out.txt", fileName);
            }

            Console.WriteLine("Write results statistics file name (default: results.csv)");
            string outFileName = Console.ReadLine();
            if (outFileName == "")
            {
                outFileName = "results.csv";
            }

            // TODO: Any better way to select new file?
            //Console.WriteLine("Select the results file");
            //string outFileName = ChooseFile("Select the results file", "All files|*");
            //if (outFileName == "")
            //{
            //    return;
            //}

            Console.WriteLine("Select logging level: Info, Debug, Insane");
            string level = Console.ReadLine();
            Logger.GetLogger.LogLevel = (Logger.LogLevelType)Enum.Parse(typeof(Logger.LogLevelType), level);

            Console.WriteLine("Write number of repetitions of the algorithm");
            string rep = Console.ReadLine();
            int repetitions;
            while (!int.TryParse(rep, out repetitions))
            {
                Console.WriteLine("Please provide an integer number");
            }

            Algorithm.RunAlgorithm(fileName, repetitions, outFileName, outPathFileName);
        }

        static string ChooseFile(string message, string filter)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = message,
                Filter = filter,
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            using (dialog)
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
                else
                {
                    MessageBox.Show("No file was chosen, exiting...");
                    return "";
                }
            }
        }
    }
}
