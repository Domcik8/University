using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Common;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;


namespace SSS
{

    internal class Program
    {

        private static void Main()
        {
            var c = new Controller();
            c.Server.Start();
            Console.WriteLine("Server is started successfully");
            Console.ReadLine();
            c.Server.Stop();
        }
    }


}
