using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterManager
{
    class Topology
    {
        public int Quantity { get; set; }
        public List<Node> nodes = new List<Node>();

        public Topology(List<String> paths) : base()
        {
            char separator = ',';
            Quantity = 0;
            foreach(string path in paths)
            {
                string[] branches = path.Split(separator);
                foreach(string branch in branches)
                {
                    AddStartingBranch(branch);
                }
            }
        }

        public void AddStartingBranch(string branch)
        {
            string[] arguments = new string[3];
            char space = ' ';
            Node node;
            int cost;
            arguments = branch.Split(space);
            CreateNodes(arguments);
            node = FindNode(arguments[0]);
            Int32.TryParse(arguments[2], out cost);
            node.AddStartingBranch(arguments[1], cost);
            node = FindNode(arguments[1]);
            node.AddStartingBranch(arguments[0], cost);
        }

        private void CreateNodes(string[] arguments)
        {
            Node node;
            node = FindNode(arguments[0]);
            if (node == null)
            {
                node = CreateNode(arguments[0]);
            }
            node = FindNode(arguments[1]);
            if (node == null)
            {
                node = CreateNode(arguments[1]);
            }
        }

        private Node CreateNode(string name)
        {
            nodes.Add(new Node(name));
            Quantity++;
            return FindNode(name);
        }
        public Node FindNode(string name)
        {
            foreach (Node node in nodes)
            {
                if (node.name == name)
                {
                    return node;
                }
            }
            return null;
        }
    }
}
