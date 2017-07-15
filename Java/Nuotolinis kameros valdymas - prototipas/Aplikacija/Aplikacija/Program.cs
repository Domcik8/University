using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Drawing;

namespace Aplikacija
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				int port = 2055;
				string ip;

				//Console.WriteLine("Type the IP: ");
				//ip = Console.ReadLine();

				Console.WriteLine("Enter IP:");
				ip = Console.ReadLine();

				//TcpClient client = new TcpClient();
				//client.Connect(ip, port);

				using (TcpClient client = new TcpClient(ip, port))
				{
					Console.WriteLine("Connection successful...");
					NetworkStream stream = client.GetStream();

					Image img = Image.FromStream(stream);
					Console.WriteLine("Image received...");
					img.Save(@"photo.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
					Console.WriteLine("Image saved...");
					Console.ReadLine();
				}
			}
			catch
			{
				Console.WriteLine("Prisijungti nepavyko");
				Console.ReadLine();
			}
		}
	}
}
