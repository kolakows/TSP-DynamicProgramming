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
            while (mode != 'b' && mode != 'i')
            {
                Console.WriteLine("Please choose 'b' or 'i'");
                mode = Console.Read();
                Console.ReadLine();
            }

            try
            {
                if (mode == 'b')
                {
                    RunBatchMode();
                }
                else
                {
                    RunInteractiveMode();
                }
            }
            finally
            { // Keep console open also in case of an uncaught exception
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
        }

        static void RunBatchMode()
        {
            int repetitions = 10;

            foreach (var filePath in Directory.GetFiles("tests/", "*.txt"))
            {
                string fileName = Path.GetFileName(filePath);
                Algorithm.RunAlgorithm(filePath, repetitions, "results/results.csv", string.Format("results/{0}_out.txt", fileName));
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

            string outPathFileName = string.Format("{0}_out.txt", fileName);
            Console.WriteLine("Write results file name (default: {0}", outPathFileName);
            string pathFileStr = Console.ReadLine();
            if (pathFileStr != "")
            {
                outPathFileName = pathFileStr;
            }

            string outFileName = "results.csv";
            Console.WriteLine("Write results statistics file name (default: {0}", outFileName);
            string outFileStr = Console.ReadLine();
            if (outFileStr != "")
            {
                outFileName = outFileStr;
            }

            // TODO: Any better way to select new file?
            //Console.WriteLine("Select the results file");
            //string outFileName = ChooseFile("Select the results file", "All files|*");
            //if (outFileName == "")
            //{
            //    return;
            //}

            Console.WriteLine("Select logging level: Info, Debug, Insane (default: {0})", Logger.GetLogger.LogLevel);
            string level = Console.ReadLine();
            if (level != "")
            {
                Logger.GetLogger.LogLevel = (Logger.LogLevelType)Enum.Parse(typeof(Logger.LogLevelType), level);
            }

            int repetitions = 10;
            Console.WriteLine("Write number of repetitions of the algorithm (default: {0})", repetitions);
            string rep = Console.ReadLine();
            if (rep != "")
            {
                while (!int.TryParse(rep, out repetitions))
                {
                    Console.WriteLine("Please provide an integer number");
                    rep = Console.ReadLine();
                }
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
