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

namespace AppSimulator
{
	public partial class MainScreen : Form
	{
        VartotojoPaskyra pask = new VartotojoPaskyra();
		Common.Message msg = new Common.Message();
        IScsClient client = ScsClientFactory.CreateClient(new ScsTcpEndPoint("127.0.0.1", 10085));
		//IScsClient client = ScsClientFactory.CreateClient(new ScsTcpEndPoint("10.3.8.40", 10085));
		public MainScreen()
		{
			CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
			client.ConnectTimeout = 2000;
		}

		private void Updator(object sender, MessageEventArgs e)
		{
			var message = e.Message as ScsRawDataMessage;
			if (message == null)
			{
				return;
			}

			var msg = Serial.ByteArrayToObject(message.MessageData);
			
			variklioTemp.Text = msg.degalai.ToString();
			laukoTemp.Text = msg.laukoTemp.ToString();
			vidausTemp.Text = msg.vidausTemp.ToString();
            momentinisGreitis.Text = msg.momentinisGreitis.ToString();
            degalai.Text = msg.degalai.ToString();
            krituliai.Text = msg.krituliuKiekis;
            rida.Text = msg.rida.ToString();
            vidutinisGreitis.Text = msg.vidGreitis.ToString();
            degaluSanaudos.Text = msg.degaluSanaudos.ToString();




			if (msg.EngineState == true) engineOn.Checked = true;
			else if (msg.EngineState == false) engineOff.Checked = true;

			if (msg.LightState == true) lightsOn.Checked = true;
			else if (msg.LightState == false) lightsOff.Checked = true;

			if (msg.OpenWindows == true) windowsOpened.Checked = true;
			else if (msg.OpenWindows == false) windowsClosed.Checked = true;

			if (msg.Locked == true) locked.Checked = true;
			else if (msg.Locked == false) unlocked.Checked = true;

			if (msg.AlarmState == true)
			{
				DialogResult result = MessageBox.Show("Suveike signalizacija");
				if (result == DialogResult.OK) AlarmOff();
			}
		}

		private void logIn_Click(object sender, EventArgs e)
		{
			logIn.Enabled = false;
			msg.Isclient = true;
			msg.IsLogin = true;
			msg.Username = userName.Text;
			msg.Password = password.Text;

			Task.Factory.StartNew(() =>
			{
				try
				{
					client.Connect();
					var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
					client.SendMessage(message);
				}
				catch
				{
					logIn.Enabled = true;
					MessageBox.Show("Serveris nepasiekiamas!");
				}
			});	
		}

		private void LogInConfirmMessage(object sender, MessageEventArgs e)
		{
			var msg = e.Message as ScsTextMessage;
			if (msg == null) return;
			MessageBox.Show(msg.Text);
			if (msg.Text == "Sekmingai prisijungete")
			{
				MainMenu.Visible = true;
			}	
		}

		private void MainScreen_Load(object sender, EventArgs e)
		{
			client.MessageReceived += Updator;
			client.MessageReceived += LogInConfirmMessage;
			client.Connected += client_Connected;
			client.Disconnected += client_Disconnected;
			groupBox1.Visible = false;
			MainMenu.Visible = false;
			toMenu.Visible = false;
			panel1.Visible = true;
			SOSCallOptions.BackColor = Color.Red;
		}

		private void locked_CheckedChanged(object sender, EventArgs e)
		{
            msg.SOSSkambutis = null;
			msg.AlarmState = null;
			msg.Locked = (locked.Checked) ? true : false;
			msg.vidausTemp = null;
			msg.CarNmb = null;
			msg.ComputerMsg = null;
			msg.EngineState = null;
			msg.degalai = null;
			msg.vidausTemp = null;
			msg.Isclient = true;
			msg.IsLogin = null;
			msg.LightState = null;
			msg.OpenWindows = null;
			msg.laukoTemp = null;
			msg.Password = null;
			msg.Username = userName.Text;

			var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
			client.SendMessage(message);
		}

		private void windowsClosed_CheckedChanged(object sender, EventArgs e)
		{
            msg.SOSSkambutis = null;
			msg.AlarmState = null;
			msg.Locked = null;
			msg.vidausTemp = null;
			msg.CarNmb = null;
			msg.ComputerMsg = null;
			msg.EngineState = null;
			msg.degalai = null;
			msg.vidausTemp = null;
			msg.Isclient = true;
			msg.IsLogin = null;
			msg.LightState = null;
			msg.OpenWindows = (windowsClosed.Checked) ? false : true;
			msg.laukoTemp = null;
			msg.Password = null;
			msg.Username = userName.Text;

			var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
			client.SendMessage(message);
		}

		private void lightsOn_CheckedChanged(object sender, EventArgs e)
		{
            msg.SOSSkambutis = null;
			msg.AlarmState = null;
			msg.Locked = null;
			msg.vidausTemp = null;
			msg.CarNmb = null;
			msg.ComputerMsg = null;
			msg.EngineState = null;
			msg.degalai = null;
			msg.vidausTemp = null;
			msg.Isclient = true;
			msg.IsLogin = null;
			msg.LightState = (lightsOn.Checked) ? true : false;
			msg.OpenWindows = null;
			msg.laukoTemp = null;
			msg.Password = null;
			msg.Username = userName.Text;

			var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
			client.SendMessage(message);
		}

		private void engineOn_CheckedChanged(object sender, EventArgs e)
		{
            msg.SOSSkambutis = null;
			msg.AlarmState = null;
			msg.Locked = null;
			msg.vidausTemp = null;
			msg.CarNmb = null;
			msg.ComputerMsg = null;
			msg.EngineState = (engineOn.Checked) ? true : false;
			msg.degalai = null;
			msg.vidausTemp = null;
			msg.Isclient = true;
			msg.IsLogin = null;
			msg.LightState = null;
			msg.OpenWindows = null;
			msg.laukoTemp = null;
			msg.Password = null;
			msg.Username = userName.Text;

			var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
			client.SendMessage(message);
		}

		private void AlarmOff()
		{
            msg.SOSSkambutis = null;
			msg.AlarmState = false;
			msg.Locked = null;
			msg.vidausTemp = null;
			msg.CarNmb = null;
			msg.ComputerMsg = null;
			msg.EngineState = (engineOn.Checked) ? true : false;
			msg.degalai = null;
			msg.vidausTemp = null;
			msg.Isclient = true;
			msg.IsLogin = null;
			msg.LightState = null;
			msg.OpenWindows = null;
			msg.laukoTemp = null;
			msg.Password = null;
			msg.Username = userName.Text;

			var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
			client.SendMessage(message);
		}

		void client_Disconnected(object sender, EventArgs e)
		{
			logIn.Enabled = true;
			lblConnection.Text = "Neprisijungta";
			lblConnection.ForeColor = Color.Red;
			panel1.Visible = true;
			groupBox1.Visible = false;
		}

		void client_Connected(object sender, EventArgs e)
		{
			logIn.Enabled = false;
			lblConnection.Text = "Prisijungta";
			lblConnection.ForeColor = Color.LimeGreen;
			panel1.Visible = false;
		}

		private void AutoCondition_Click(object sender, EventArgs e)
		{
			MainMenu.Visible = false;
			groupBox1.Visible = true;
			toMenu.Visible = true;
		}

		private void SOSCallOptions_Click(object sender, EventArgs e)
		{
            if (SOSCallOptions.BackColor == Color.Red)
            {
                SOSCallOptions.BackColor = Color.Green;
            }
            else
            {
                SOSCallOptions.BackColor = Color.Red;
            }
            
            pask.AutomatinisSOS(this);
		}

		private void toMenu_Click(object sender, EventArgs e)
		{
			MainMenu.Visible = true;
			groupBox1.Visible = false;
            groupBox2.Visible = false;
			toMenu.Visible = false;
		}
        public void AutomatinisSOS()
        {
            msg.AlarmState = false;
            msg.Locked = null;
            msg.vidausTemp = null;
            msg.CarNmb = null;
            msg.ComputerMsg = null;
            msg.EngineState = false;
            msg.degalai = null;
            msg.vidausTemp = null;
            msg.Isclient = true;
            msg.IsLogin = null;
            msg.LightState = null;
            msg.OpenWindows = null;
            msg.laukoTemp = null;
            msg.Password = null;
            msg.Username = userName.Text;
            if (SOSCallOptions.BackColor == Color.Green)
                msg.SOSSkambutis = true;
            else msg.SOSSkambutis = false;

            var message = new ScsRawDataMessage(Serial.ObjectToByteArray(msg));
            client.SendMessage(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu.Visible = false;
            groupBox1.Visible = false;
            toMenu.Visible = true;
            groupBox2.Visible = true;
        }
		
	}
}