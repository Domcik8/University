#undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouterManager;
using Manager;

namespace Simulation
{
    class RouterSimulation
    {
        Router router;
        List<Package> updatePackages = new List<Package>();
        List<Package> normalPackages = new List<Package>();
        int ticks = 0;
        public RouterSimulation(Router router)
        {
            this.router = router;
        }
        public void Start()
        {
            Boolean simulate = true;
            while (simulate)
            {
                if(ticks == 0)
                {
                    FullUpdate();
                    SkipBirth();
                }
                //if(ticks % 5 == 0)
                {
                    Menu(ticks);
                }
                Tick();
            }
        }
        private void Menu(int ticks)
        {
            int menu;
            string input;
            List<string> changes = new List<string>();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Simulation is at {0} ticks, what would you like to do:\n", ticks);
                Console.WriteLine("0.Continue simulation");
                Console.WriteLine("1.Add branch");
                Console.WriteLine("2.SendPackage");
                Console.WriteLine("3.Check all node tables");
                Console.WriteLine("4.Check packages");
                Console.Write("\nMenu: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out menu);
                Console.Clear();
                switch (menu)
                {
                    case 1:
                        {
                            BranchChange(changes);
                            break;
                        }
                    case 2:
                        {
                            SendPackage();
                            break;
                        }
                    case 3:
                        {
                            router.ShowAllData();
                            break;
                        }
                    case 4:
                        {
                            CheckNormalPackages();
                            break;
                        }
                    default:
                        {
                            if(changes.Count != 0)
                                FullUpdate();
                            exit = true;
                            break;
                        }
                }
                Console.Clear();
            }
        }
        private void Tick()
        {
            Node node;
            bool got = false;
            List<Package> delete = new List<Package>();
            List<Package> forward = new List<Package>();
            RouterData data;
            foreach(Package package in updatePackages)
            {
                package.cost--;
                if (package.cost == 0)
                {
                    delete.Add(package);
                    node = router.topology.FindNode(package.finalDestination);
                    foreach(int history in node.history)
                    {
                        if (history == package.id)
                        {
                            got = true;
                            break;
                        }
                    }
                    if(got == false)
                    {
                        node.history.Add(package.id);
                        UpdateNode(node, package);
                        package.lifetime--;
                        if (package.lifetime != 0)
                        {
                            forward.Add(package);
                        }
                    }
                }
            }
            foreach(Package package in delete)
            {
                updatePackages.Remove(package);
            }
            delete.Clear();
            foreach(Package package in forward)
            {
                node = router.topology.FindNode(package.finalDestination);
                ForwardPackage(node, package);
            }
            forward.Clear();
            foreach(Package package in normalPackages)
            {
                package.cost--;
                if (package.cost == 0)
                {
                    delete.Add(package);
                    node = router.topology.FindNode(package.currentDestination);
                    node.history.Add(package.id);
                    {
                        if(package.currentDestination == package.finalDestination)
                        {
                            Package.GotMessage(package);
                        }
                        else
                        {
                            data = node.FindBranch(package.finalDestination);
                            if(data.from != data.to)
                                data = node.FindBranch(data.from);
                            package.path.Add(data.to);
                            package.pathCost.Add(data.cost);
                            forward.Add(new Package(package.from, node.name, data.to, package.finalDestination, data.cost, package.info, 
                                package.dataToSend, package.lifetime, package.path, package.pathCost));
                        }
                    }
                }
            }
            foreach (Package package in delete)
            {
                normalPackages.Remove(package);
            }
            delete.Clear();
            foreach (Package package in forward)
            {
                Send(package);
            }
#if DEBUG
            if (changed)
                router.ShowAllData();
#endif
            ticks++;
        }
        private void ChangeGraph()
        {
            List<string> changes = new List<string>();
            int menu;
            string input;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("How would you like to change graph?");
                Console.WriteLine("0.End changing");
                Console.WriteLine("1.Add branch");
                Console.Write("\nMenu: ");
                input = Console.ReadLine();
                Int32.TryParse(input, out menu);
                Console.Clear();
                switch (menu)
                {
                    case 1:
                        {
                            BranchChange(changes);
                            break;
                        }
                    default:
                        {
                            FullUpdate();
                            exit = true;
                            break;
                        }
                }
                Console.Clear();
            }
        }
        private void BranchChange(List<String> changes)
        {
            string branch = null;
            int error = 1;
            string[] args;
            while (error == 1)
            {
                branch = GetBranchString();
                error = CheckBranch(branch);
                if (error == 1)
                    Console.WriteLine("Bad branch format. Pleasy try again.");
                Program.Clear();
            }
            args = branch.Split(' ');
            AddToChange(changes, args[0]);
            AddToChange(changes, args[1]);
            router.topology.AddStartingBranch(branch);
        }
        private int AddToChange(List<String> changes, string name)
        {
            foreach(string change in changes)
            {
                if (change == name)
                    return 0;
            }
            changes.Add(name);
            return 0;
        }
        private string GetBranchString()
        {
            string branch;
            Console.Write("From: ");
            branch = Console.ReadLine();
            Console.Write("To: ");
            branch = branch + " " + Console.ReadLine();
            Console.Write("Cost: ");
            branch = branch + " " + Console.ReadLine();
            return branch;
        }
        private int CheckBranch(string branch)
        {
            string[] args = branch.Split(' ');
            if (args.Length != 3 || args[0] == "" || args[1] == "")
                return 1;
            return 0;
        }
        private void CheckNormalPackages()
        {
            int i = 1;
            foreach(Package package in normalPackages)
            {
                Console.WriteLine("{0}. {1} currently sent from {2}.", 
                    i, package.dataToSend, package.sender);
                Console.WriteLine("Time left to reach current destination = {0} ticks", package.cost);
                Package.ShowPath(package);
                i++;
                Console.WriteLine();
            }
            Program.Clear();
        }
        private void SendPackage()
        {
            RouterData data;
            string message, from = null, to = null;
            Console.Write("Message to send: ");
            message = Console.ReadLine();
            data = GetPath(ref from, ref to);
            Send(new Package(from, from, data.to, to, data.cost, "Normal", message, 1, null, null));
            Console.WriteLine("Message successfully sent");
            Program.Clear();
        }
        private RouterData GetPath(ref string from, ref string to)
        {
            Node node = null;
            bool chosen = false;
            RouterData data = null;
            while (!chosen)
            {
                from = GetNode("Sending node: ");
                to = GetNode("Getting node: ");
                node = router.topology.FindNode(from);
                data = node.FindBranch(to);
                if(from == to)
                {
                    Console.WriteLine("Chosen nodes are identical. Please try again.");
                }
                else if (data == null)
                {
                    Console.WriteLine("Chosen nodes are not connected. Please try again.");
                }
                else chosen = true;
            }
            if(data.from != data.to)
                data = node.FindBranch(data.from);
            return data;
        }
        private string GetNode(string message)
        {
            string name;
            Node node;
            while(true)
            {
                Console.Write(message);
                name = Console.ReadLine();
                node = router.topology.FindNode(name);
                if (node == null)
                {
                    Console.WriteLine("Node not found. Please choose again.");
                }
                else return name;
            }
        }
        private Node FindRecepient(Package package)
        {
            Node node = router.topology.FindNode(package.sender);
            RouterData data = node.FindBranch(package.finalDestination);
            return router.topology.FindNode(data.from);
        }

        private void ForwardPackage(Node node, Package package)       
        {
            foreach(RouterData data in node.table)
            {
                if(data.from == data.to && package.from != data.from)
                     Send(new Package(package.from, node.name, data.to, data.to, data.cost, package.info, package.dataToSend, package.lifetime, null ,null));
            }
        }
        private void UpdateNode(Node node, Package package)
        {
            foreach(RouterData data in package.dataToSend)
            {
                node.AddBranch(data);
            }
        }
        private void SkipBirth()
        {
            Boolean answered = false;
            string data;
            while (!answered)
            {
                Console.Write("Do you want to skip birth of topology? (y/n): ");
                data = Console.ReadLine();
                //data = "y";  //Change if want an option
                if (data == "y" || data == "Y")
                {
                    answered = true;
                    while(updatePackages.Count != 0)
                    {
                        Tick();
                    }
                    ticks = 0;
                }
                else
                    if (data == "n" || data == "N")
                        answered = true;
            }
        }
        private void FullUpdate()
        {
            foreach(Node node in router.topology.nodes)
            {
                SendUpdate(node);
            }
        }
        private void PartialUpdate(List<String> changes)
        {
            Node node;
            foreach(string name in changes)
            {
                node = router.topology.FindNode(name);
                SendUpdate(node);
            }
        }
        private void SendUpdate(Node node)
        {
            Package package;
            string from, to, info;
            dynamic dataToSend;
            int lifetime;
            from = node.name;
            info = "Update";
            dataToSend = node.table;
            lifetime = router.topology.Quantity;
            foreach(RouterData neighbor in node.neighbors)
            {
                if (neighbor.from == neighbor.to)
                {
                    package = new Package(node.name, node.name, neighbor.to, neighbor.to, neighbor.cost, info, dataToSend, lifetime, null, null);
                    Send(package);
                }
            }
        }
        private void Send(Package package)
        {
            if (package.info == "Update")
                updatePackages.Add(package);
            else if (package.info == "Normal")
                normalPackages.Add(package);
        }
    }
}
