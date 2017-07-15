using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Kamera
{
	class Program
	{
		public static Image Fotografuoti()
		{
			Form f = new Form();
			PictureBox pb = new PictureBox();
			pb.Size = new System.Drawing.Size(320, 240);
			f.Controls.Add(pb);

			const int VIDEODEVICE = 0; // zero based index of video capture device to use
			const int VIDEOWIDTH = 640; // Depends on video device caps
			const int VIDEOHEIGHT = 480; // Depends on video device caps
			const int VIDEOBITSPERPIXEL = 24; // BitsPerPixel values determined by device

			Capture cam = new Capture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, pb);

			//f.ShowDialog();


			IntPtr m_ip = IntPtr.Zero;
			Cursor.Current = Cursors.WaitCursor;

			// Release any previous buffer
			if (m_ip != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(m_ip);
				m_ip = IntPtr.Zero;
			}

			// capture image
			m_ip = cam.Click();
			Bitmap b = new Bitmap(cam.Width, cam.Height, cam.Stride, PixelFormat.Format24bppRgb, m_ip);

			// If the image is upsidedown
			b.RotateFlip(RotateFlipType.RotateNoneFlipY);
			//pb.Image = b;

			Cursor.Current = Cursors.Default;

			Image i = (Image)b;



			cam.Dispose();
			if (m_ip != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(m_ip);
				m_ip = IntPtr.Zero;
			}

			return i;
		}
		public static byte[] ImageToByte(Image img)
		{
			ImageConverter converter = new ImageConverter();
			return (byte[])converter.ConvertTo(img, typeof(byte[]));
		}
		static void Main(string[] args)
		{
			TcpListener server = null;
			try
			{
				int port = 2055;

				server = new TcpListener(IPAddress.Any, port);
				server.Start();
				Console.WriteLine("Server started...");
				Console.WriteLine("Server IPs:");
				//Console.WriteLine("Server IP: {0}", ip.ToString());
				foreach(IPAddress ip in Dns.GetHostEntry("127.0.0.1").AddressList)
				{
					Console.WriteLine(ip.ToString());
				}

				while (true)
				{
					Console.WriteLine("Waiting for a connection... ");

					using (TcpClient client = server.AcceptTcpClient())
					{
						//Socket socket = server.AcceptSocket();
						Console.WriteLine("User connected!");

						NetworkStream stream = client.GetStream();

						//Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
						//Graphics graphics = Graphics.FromImage(printscreen as Image);
						//graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

						Image photo = Fotografuoti();
						Console.WriteLine("Image created...");

						//byte[] img = ImageToByte(printscreen);
						byte[] img = ImageToByte(photo);

						stream.Write(img, 0, img.Length);
						//socket.Send(img);
						Console.WriteLine("Image sent...");           
					}
				}
			}
			catch
			{
				Console.WriteLine("Ivyko klaida");
				Console.ReadLine();
			}
		}
	}
}