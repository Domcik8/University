using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterManager
{
   class Node
    {
       public string name {get; set;}
       public List<int> history = new List<int>();
       public List<RouterData> table = new List<RouterData>();
       public List<RouterData> neighbors = new List<RouterData>();
       public Node(string name)
       {
           this.name = name;
       }
       public void AddBranch(RouterData newData)
       {
           RouterData data, oldData;
           if (newData.from != name)
           {
               if (newData.to != name)
               {
                   oldData = FindBranch(newData.to);
                   if (oldData != null)
                   {
                       if (oldData.cost > newData.cost)
                       {
                           data = FindBranch(newData.owner);
                           if (oldData.cost > newData.cost + data.cost)
                           {
                               oldData.cost = newData.cost + data.cost;
                               oldData.from = data.from;
                           }
                       }
                       else if(oldData.to == newData.to && oldData.from == newData.owner)
                       {
                           data = FindBranch(newData.owner);
                           oldData.cost = newData.cost + data.cost;
                           foreach(RouterData neighbor in neighbors)
                           {
                               if(oldData.to == neighbor.to)
                               {
                                   if(oldData.cost >= neighbor.cost)
                                   {
                                       oldData.cost = neighbor.cost;
                                       oldData.from = neighbor.from;
                                   }
                                   break;
                               }
                           }
                       }
                   }
                   else
                   {
                       data = FindBranch(newData.owner);
                       table.Add(new RouterData(name, data.from, newData.to, newData.cost + data.cost));
                   }
               }
               else
               {
                   if (newData.to == newData.from)
                   {
                       int difference;
                       oldData = FindBranch(newData.owner);
                       if (oldData != null)
                       {
                           difference = newData.cost - oldData.cost;
                           oldData.cost = newData.cost;
                           UpdateCosts(oldData.from, difference);
                       }
                       else
                       {
                           table.Add(new RouterData(name, newData.owner, newData.owner, newData.cost));
                       }
                   }
               }
           }
       }
       private void UpdateCosts(string name, int difference)
       {
           foreach(RouterData data in table)
           {
               if (data.from == name && data.to != name)
               {
                   data.cost += difference;
                   foreach(RouterData neighbor in neighbors)
                   {
                       if(neighbor.to == data.to)
                       {
                           if(neighbor.cost <= data.cost)
                           {
                               data.from = neighbor.from;
                               data.cost = neighbor.cost;
                           }
                           break;
                       }
                   }
               }
           }
       }
       public void AddStartingBranch(string to, int cost)
       {
           int difference;
           RouterData neighbor = null;
           bool found = false;
           foreach(RouterData data in table)
           {
               if (data.to == to)
               {
                   found = true;
                   difference = cost - data.cost;
                   data.cost = cost;
                   neighbor = FindNeighbor(to);
                   neighbor.cost = cost;
                   UpdateCosts(data.from ,difference);
                   break;
               } 
           }
           if (!found)
           {
               neighbors.Add(new RouterData(name, to, to, cost));
               table.Add(new RouterData(name, to, to, cost));
           }
       }
       public RouterData FindBranch(string name)
       {
           foreach(RouterData data in table)
           {
               if (data.to == name)
                   return data;
           }
           return null;
       }
       public RouterData FindNeighbor(string name)
       {
           foreach (RouterData data in neighbors)
           {
               if (data.to == name)
                   return data;
           }
           return null;
       }
       public void ShowNodeData()
       {
           Console.WriteLine("Node table of {0}:", name);
           if (table != null)
               foreach (RouterData data in table)
               {
                   Console.WriteLine("{0}|{1}|{2}", data.from, data.to, data.cost);
               }
           Console.WriteLine("Neightbors:");
           if (neighbors != null)
               foreach (RouterData data in neighbors)
               {
                   Console.WriteLine("{0}|{1}|{2}", data.from, data.to, data.cost);
               }
           Console.WriteLine();
       }
    }
}
