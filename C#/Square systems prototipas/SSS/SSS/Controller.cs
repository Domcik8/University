using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Common;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace SSS
{
    public class Controller
    {
        readonly ConcurrentDictionary<string, long> _usrList = new ConcurrentDictionary<string, long>();//carnumber, clientId
        readonly ConcurrentDictionary<string, long> _carList = new ConcurrentDictionary<string, long>();//Username, carID
        public readonly IScsServer Server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(10085));

        public Controller()
        {
            Server.ClientConnected += Server_ClientConnected;
            Server.ClientDisconnected += Server_ClientDisconnected;
        }
        private void Server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            Console.WriteLine("A new client is connected. Client Id = " + e.Client.ClientId);
            e.Client.MessageReceived += Client_MessageReceived;
        }

        private  void Server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            Console.WriteLine("A client is disconnected! Client Id = " + e.Client.ClientId);
            var result = from usr in _usrList
                where usr.Value.Equals(e.Client.ClientId)
                select usr.Key;
            foreach (var r in result)
            {
                long temp;
                _usrList.TryRemove(r, out temp);
            }
            var result2 = from car in _carList
                          where car.Value.Equals(e.Client.ClientId)
                          select car.Key;
            foreach (var r in result2)
            {
                long temp;
                _carList.TryRemove(r, out temp);
            }
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            Message msg = null;
            var message = e.Message as ScsRawDataMessage;
            var client = (IScsServerClient)sender;
            if(message != null) msg = ByteToMsg(message.MessageData);
            if (message == null || msg == null)
            {   
                client.Disconnect();
                return;
            }
              
            if(msg.IsLogin == true && msg.Isclient == true)
            {
                if (DbController.IsLegalClient(msg.Username, msg.Password))
                {
                    try
                    {
                        if (_usrList.TryAdd(DbController.GetClientCarNmb(msg.Username), client.ClientId))
                        {
                            client.SendMessage(new ScsTextMessage("Sekmingai prisijungete"));
                            Console.WriteLine("Prisijunge vartotojas");
                        }
                        else
                        {
                            client.Disconnect();
                            return;
                        }
                    }
                    catch (Exception exc)
                    {
                        client.SendMessage(new ScsTextMessage("Ivyko klaida"));
                    }
                } 
                else
                {   
                    client.SendMessage(new ScsTextMessage("Neteisingas vardas arba slaptazodis"));
                    client.Disconnect();
                    return;
                }
            }
            else if (msg.IsLogin == true && msg.Isclient == false)
            {
                if (DbController.IsLegalCar(msg.CarNmb))
                {
                    try
                    {
                        _carList.TryAdd(DbController.GetUsername(msg.CarNmb), client.ClientId);
                        client.SendMessage(new ScsTextMessage("Sekmingai prisijungete"));
                        Console.WriteLine("Prisijunge automobilis");
                    }
                    catch
                    {
                        client.SendMessage(new ScsTextMessage("Ivyko klaida"));
                        return;
                    }
                }
                else
                {
                    client.SendMessage(new ScsTextMessage("Neteisingas automobilio nr"));
                     client.Disconnect();
                    return;
                }
            }
            if(msg.Isclient == false)
            {
                Console.WriteLine("Car " + msg.CarNmb + " sent a data");               
                Debug.WriteLine(msg.ComputerMsg + " " + msg.degalai + "  Locked:" + msg.Locked+ "alarm state: " +msg.AlarmState);
                long clientId;
                if (_usrList.TryGetValue(msg.CarNmb, out clientId))
                {   
                    Server.Clients[clientId].SendMessage(new ScsRawDataMessage(MsgToByte(msg)));
                }
            }
            else if(msg.Isclient == true)
            {
                Console.WriteLine("User " + msg.Username + " sent a request");
                long clientId;
                if (_carList.TryGetValue(msg.Username, out clientId))
                {
                    Server.Clients[clientId].SendMessage(new ScsRawDataMessage(MsgToByte(msg)));
                }
                else client.SendMessage(new ScsTextMessage("Automobilis nepasiekiamas"));

            }
        }

        private static Message ByteToMsg(byte[] byteArr)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream(byteArr))
            {
                return (Message) bf.Deserialize(ms);

            }
        }

        private static byte[] MsgToByte(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }

        }
    } 
    
}