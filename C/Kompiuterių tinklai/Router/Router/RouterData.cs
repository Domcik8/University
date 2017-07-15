using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterManager
{
    class RouterData
    {
        public string owner;
        public string from;
        public string to;
        public int cost;
        public RouterData(string owner, string from, string to, int cost)
        {
            this.owner = owner;
            this.from = from;
            this.to = to;
            this.cost = cost;
        }
    }
}
