using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;

namespace Simulation
{
    class Package
    {
        private static int idCounter = 0; 
        public string from, sender, currentDestination, finalDestination, info;
        public dynamic dataToSend;
        public int lifetime, id = 0, cost;
        public List<string> path = new List<string>();
        public List<int> pathCost = new List<int>();
        public Package(string from, string sender, string currentDestination, string finalDestination, int cost, string info, 
            dynamic dataToSend, int lifetime, List<string> path, List<int> pathCost)
        {
            this.from = from;
            this.sender = sender;
            this.currentDestination = currentDestination;
            this.finalDestination = finalDestination;
            this.cost = cost;
            this.info = info;
            this.dataToSend = dataToSend;
            this.lifetime = lifetime;
            id = ++idCounter;
            if(path == null && pathCost == null)
            {
                this.path.Add(from);
                this.path.Add(currentDestination);
                this.pathCost.Add(cost);
            }
            else
            {
                this.path = path;
                this.pathCost = pathCost;
            }
        }
        public static void GotMessage(Package package)
        {
            int i;
            Console.WriteLine(package.finalDestination + " got message from " + package.from + ":" + package.dataToSend);
            i = ShowPath(package);
            Program.Clear();
        }

        public static int ShowPath(Package package)
        {
            string text = null;
            int i;
            Console.WriteLine("Path used:");
            foreach (string name in package.path)
            {
                text = text + name + "-";
            }
            for (i = 0; i < text.Length - 1; i++)
                Console.Write(text[i]);
            text = null;
            Console.WriteLine("\nPath costs:");
            foreach (int cost in package.pathCost)
            {
                text = text + cost + "-";
            }
            for (i = 0; i < text.Length - 1; i++)
                Console.Write(text[i]);
            return i;
        }
    }
}
