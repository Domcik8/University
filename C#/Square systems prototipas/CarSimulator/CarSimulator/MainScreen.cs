using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSimulator
{
	public partial class MainScreen : Form
	{
        Automobilis auto = new Automobilis();
		IScsClient client = ScsClientFactory.CreateClient(new ScsTcpEndPoint("127.0.0.1", 10085));
		//IScsClient client = ScsClientFactory.CreateClient(new ScsTcpEndPoint("10.3.8.40", 10085));
		Common.Message msg = new Common.Message();
		System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.alarm);
		bool? alarm = null;

		public MainScreen()
		{
			CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
			client.ConnectTimeout = 2000;
		}

		private void MainScreen_Load(object sender, EventArgs e)
		{
			label5.Visible = false;
			client.MessageReceived += Client_MessageReceived;
			client.MessageReceived += LogInConfirmMessage;
			client.Connected += client_Connected;
			client.Disconnected += client_Disconnected;
			panel5.Visible = true;
		}

		private void Client_MessageReceived(object sender, MessageEventArgs e)
		{
			var message = e.Message as ScsRawDataMessage;
			if (message == null)
			{
				return;
			}

			var msg = Serial.ByteArrayToObject(message.MessageData);

            if (msg.SOSSkambutis == true)
            {
                SOSON.Checked = true;
                auto.AutomatinisSOS();
            }
            else if (msg.SOSSkambutis == false)
            {
                SOSOFF.Checked = true;
                auto.AutomatinisSOS();
            }

			if (msg.Locked == true) locked.Checked = true;
			else if (msg.Locked == false) unlocked.Checked = true;

			if (msg.OpenWindows == true) windowsOpened.Checked = true;
			else if (msg.OpenWindows == false) windowsClosed.Checked = true;

			if (msg.LightState == true) lightsOn.Checked = true;
			else if (msg.LightState == false) lightsOff.Checked = true;

			if (msg.EngineState == true) engineOn.Checked = true;
			else if (msg.EngineState == false) engineOff.Checked = true;

			if (msg.AlarmState == false)
			{
				label5.Visible = false;
				player.Stop();
				alarm = null;
			}
		}

		private void LogInConfirmMessage(object sender, MessageEventArgs e)
		{
			var msg = e.Message as ScsTextMessage;
			if (msg == null) return;
			MessageBox.Show(msg.Text);
		}

		private void Updator()
		{
			float temp;
			msg.IsLogin = false;
			msg.degalai = float.TryParse(degalaiAmount.Text, out temp) ? temp : (float?)null;
			msg.laukoTemp = float.TryParse(laukoTemp.Text, out temp) ? temp : (float?)null;
			msg.vidausTemp = float.TryParse(vidausTemp.Text, out temp) ? temp : (float?)null;
            msg.variklioTemp = float.TryParse(variklioTemp.Text, out temp) ? temp : (float?)null;
            msg.krituliuKiekis = krituliuKiekis.Text;
            msg.rida = float.TryParse(rida.Text, out temp) ? temp : (float?)null;
            msg.vidGreitis = float.TryParse(vidutinisGreitis.Text, out temp) ? temp : (float?)null;
            msg.degaluSanaudos = float.TryParse(degaluSanaudos.Text, out temp) ? temp : (float?)null;
            msg.momentinisGreitis = float.TryParse(momentinisGreitis.Text, out temp) ? temp : (float?)null;

			msg.AlarmState = alarm;
			msg.Locked = (locked.Checked) ? true : false;
			msg.OpenWindows = (windowsOpened.Checked) ? true : false;
			msg.LightState = (lightsOn.Checked) ? true : false;
			msg.EngineState = (engineOn.Checked) ? true : false;

			var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
			client.SendMessage(message);
		}

		private void logIn_Click(object sender, EventArgs e)
		{
			logIn.Enabled = false;
			msg.Isclient = false;
			msg.IsLogin = true;
			msg.CarNmb = carNmb.Text;

			float temp;
			msg.degalai = float.TryParse(degalaiAmount.Text, out temp) ? temp : (float?)null;
			msg.laukoTemp = float.TryParse(laukoTemp.Text, out temp) ? temp : (float?)null;
			msg.vidausTemp = float.TryParse(vidausTemp.Text, out temp) ? temp : (float?)null;
			msg.AlarmState = alarm;
			msg.Locked = (locked.Checked) ? true : false;
			msg.OpenWindows = (windowsOpened.Checked) ? true : false;
			msg.LightState = (lightsOn.Checked) ? true : false;
			msg.EngineState = (engineOn.Checked) ? true : false;

			Task.Factory.StartNew(() =>
				{
				try
					{
					client.Connect();
					var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
					client.SendMessage(message);
					while (true)
					{
						Updator();
						Thread.Sleep(5000);
					}
				}
				catch
				{
					logIn.Enabled = true;
					panel5.Visible = true;
					MessageBox.Show("Serveris nepasiekiamas!");
				}
			});	
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			try
			{
				alarm = true;
				Updator();
				label5.Visible = true;
				player.PlayLooping();

                if (auto.sos.ArIjungta())
                {
                    if (auto.sos.Laikmatis())
                    {
                        auto.sos.SOS();
                        auto.sos.NustatytiLaikmati(30, 0);
                    }
                }

				alarm = null;
			}
			catch
			{
				logIn.Enabled = true;
				panel5.Visible = true;
				MessageBox.Show("Serveris nepasiekiamas!");
			}
		}

		void client_Disconnected(object sender, EventArgs e)
		{
			logIn.Enabled = true;
			label1.Text = "Neprisijungta";
			label1.ForeColor = Color.Red;
			panel5.Visible = true;
		}

		void client_Connected(object sender, EventArgs e)
		{
			logIn.Enabled = false;
			label1.Text = "Prisijungta";
			label1.ForeColor = Color.LimeGreen;
			panel5.Visible = false;
		}

        private void label8_Click(object sender, EventArgs e)
        {

        }
	}
}
