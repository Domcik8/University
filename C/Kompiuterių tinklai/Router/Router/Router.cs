#undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;

namespace RouterManager
{
    class Router
    {
        public Topology topology = null;
        public void FillData(string path)
        {
            List<String> paths = new List<String>();
            ExtractData(path, paths);
#if DEBUG
            ShowExtractedData(paths);
#endif
            topology = new Topology(paths);
        }
        private static void ExtractData(string path, List<String> paths)
        {
            int n;
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                paths.Add(lines[i]);
            }
        }
        public void ShowAllData()
        {
            System.Console.WriteLine("Whole Data:\n");
            for(int i = 0; i < topology.Quantity; i++)
            {
                topology.nodes[i].ShowNodeData();
            }
            Program.Clear();
        }
        private void ShowExtractedData(List<String> paths)
        {
            Console.WriteLine("*****Start of Debug*****");
            Console.WriteLine("INPUT: ");
            foreach (string path in paths)
            {
                Console.WriteLine(path);
            }
            Console.WriteLine("*****End of Debug*****");
        }
    }
}
