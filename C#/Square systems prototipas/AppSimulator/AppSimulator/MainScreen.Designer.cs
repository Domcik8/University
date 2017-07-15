namespace AppSimulator
{
	partial class MainScreen
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.panel1 = new System.Windows.Forms.Panel();
            this.logIn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.variklioTemp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.laukoTemp = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.engineOn = new System.Windows.Forms.RadioButton();
            this.engineOff = new System.Windows.Forms.RadioButton();
            this.vidausTemp = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lightsOn = new System.Windows.Forms.RadioButton();
            this.lightsOff = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.windowsOpened = new System.Windows.Forms.RadioButton();
            this.windowsClosed = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.unlocked = new System.Windows.Forms.RadioButton();
            this.locked = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rida = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.vidutinisGreitis = new System.Windows.Forms.TextBox();
            this.degaluSanaudos = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblConnection = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.SOSCallOptions = new System.Windows.Forms.Button();
            this.AutoCondition = new System.Windows.Forms.Button();
            this.toMenu = new System.Windows.Forms.Button();
            this.laikmatis = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.momentinisGreitis = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.degalai = new System.Windows.Forms.TextBox();
            this.krituliai = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.logIn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.userName);
            this.panel1.Location = new System.Drawing.Point(70, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 184);
            this.panel1.TabIndex = 0;
            // 
            // logIn
            // 
            this.logIn.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logIn.Location = new System.Drawing.Point(13, 125);
            this.logIn.Name = "logIn";
            this.logIn.Size = new System.Drawing.Size(114, 23);
            this.logIn.TabIndex = 3;
            this.logIn.Text = "Prisijungti";
            this.logIn.UseVisualStyleBackColor = true;
            this.logIn.Click += new System.EventHandler(this.logIn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Slaptažodis";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prisijungimo vardas";
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.password.Location = new System.Drawing.Point(13, 97);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(114, 22);
            this.password.TabIndex = 1;
            // 
            // userName
            // 
            this.userName.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.Location = new System.Drawing.Point(13, 47);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(114, 22);
            this.userName.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.momentinisGreitis);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.degalai);
            this.groupBox1.Controls.Add(this.krituliai);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.variklioTemp);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.laukoTemp);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.vidausTemp);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 315);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automobilio būsena";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Variklio temperatūra (°C)";
            // 
            // variklioTemp
            // 
            this.variklioTemp.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.variklioTemp.Location = new System.Drawing.Point(194, 32);
            this.variklioTemp.Name = "variklioTemp";
            this.variklioTemp.Size = new System.Drawing.Size(67, 26);
            this.variklioTemp.TabIndex = 1;
            this.variklioTemp.Text = "--";
            this.variklioTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Lauko temperatūra (°C)";
            // 
            // laukoTemp
            // 
            this.laukoTemp.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laukoTemp.Location = new System.Drawing.Point(194, 55);
            this.laukoTemp.Name = "laukoTemp";
            this.laukoTemp.Size = new System.Drawing.Size(67, 26);
            this.laukoTemp.TabIndex = 3;
            this.laukoTemp.Text = "--";
            this.laukoTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.engineOn);
            this.panel4.Controls.Add(this.engineOff);
            this.panel4.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(10, 283);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(244, 25);
            this.panel4.TabIndex = 22;
            // 
            // engineOn
            // 
            this.engineOn.AutoSize = true;
            this.engineOn.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineOn.Location = new System.Drawing.Point(0, 3);
            this.engineOn.Name = "engineOn";
            this.engineOn.Size = new System.Drawing.Size(118, 20);
            this.engineOn.TabIndex = 17;
            this.engineOn.Text = "Variklis įjungtas";
            this.engineOn.UseVisualStyleBackColor = true;
            this.engineOn.CheckedChanged += new System.EventHandler(this.engineOn_CheckedChanged);
            // 
            // engineOff
            // 
            this.engineOff.AutoSize = true;
            this.engineOff.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineOff.Location = new System.Drawing.Point(125, 3);
            this.engineOff.Name = "engineOff";
            this.engineOff.Size = new System.Drawing.Size(123, 20);
            this.engineOff.TabIndex = 18;
            this.engineOff.Text = "Variklis išjungtas";
            this.engineOff.UseVisualStyleBackColor = true;
            // 
            // vidausTemp
            // 
            this.vidausTemp.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vidausTemp.Location = new System.Drawing.Point(194, 78);
            this.vidausTemp.Name = "vidausTemp";
            this.vidausTemp.Size = new System.Drawing.Size(67, 26);
            this.vidausTemp.TabIndex = 4;
            this.vidausTemp.Text = "--";
            this.vidausTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lightsOn);
            this.panel3.Controls.Add(this.lightsOff);
            this.panel3.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(10, 252);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 25);
            this.panel3.TabIndex = 21;
            // 
            // lightsOn
            // 
            this.lightsOn.AutoSize = true;
            this.lightsOn.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lightsOn.Location = new System.Drawing.Point(0, 3);
            this.lightsOn.Name = "lightsOn";
            this.lightsOn.Size = new System.Drawing.Size(114, 20);
            this.lightsOn.TabIndex = 15;
            this.lightsOn.Text = "Šviesos įjungtos";
            this.lightsOn.UseVisualStyleBackColor = true;
            this.lightsOn.CheckedChanged += new System.EventHandler(this.lightsOn_CheckedChanged);
            // 
            // lightsOff
            // 
            this.lightsOff.AutoSize = true;
            this.lightsOff.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lightsOff.Location = new System.Drawing.Point(125, 3);
            this.lightsOff.Name = "lightsOff";
            this.lightsOff.Size = new System.Drawing.Size(119, 20);
            this.lightsOff.TabIndex = 16;
            this.lightsOff.Text = "Šviesos išjungtos";
            this.lightsOff.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Salono temperatūra (°C)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.windowsOpened);
            this.panel2.Controls.Add(this.windowsClosed);
            this.panel2.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(10, 219);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 27);
            this.panel2.TabIndex = 20;
            // 
            // windowsOpened
            // 
            this.windowsOpened.AutoSize = true;
            this.windowsOpened.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowsOpened.Location = new System.Drawing.Point(0, 3);
            this.windowsOpened.Name = "windowsOpened";
            this.windowsOpened.Size = new System.Drawing.Size(117, 20);
            this.windowsOpened.TabIndex = 13;
            this.windowsOpened.Text = "Langai atidaryti";
            this.windowsOpened.UseVisualStyleBackColor = true;
            // 
            // windowsClosed
            // 
            this.windowsClosed.AutoSize = true;
            this.windowsClosed.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowsClosed.Location = new System.Drawing.Point(125, 3);
            this.windowsClosed.Name = "windowsClosed";
            this.windowsClosed.Size = new System.Drawing.Size(114, 20);
            this.windowsClosed.TabIndex = 14;
            this.windowsClosed.Text = "Langai uždaryti";
            this.windowsClosed.UseVisualStyleBackColor = true;
            this.windowsClosed.CheckedChanged += new System.EventHandler(this.windowsClosed_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.unlocked);
            this.panel5.Controls.Add(this.locked);
            this.panel5.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(10, 188);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(212, 28);
            this.panel5.TabIndex = 19;
            // 
            // unlocked
            // 
            this.unlocked.AutoSize = true;
            this.unlocked.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unlocked.Location = new System.Drawing.Point(0, 3);
            this.unlocked.Name = "unlocked";
            this.unlocked.Size = new System.Drawing.Size(80, 20);
            this.unlocked.TabIndex = 11;
            this.unlocked.Text = "Atrakinta";
            this.unlocked.UseVisualStyleBackColor = true;
            // 
            // locked
            // 
            this.locked.AutoSize = true;
            this.locked.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locked.Location = new System.Drawing.Point(125, 3);
            this.locked.Name = "locked";
            this.locked.Size = new System.Drawing.Size(81, 20);
            this.locked.TabIndex = 12;
            this.locked.Text = "Užrakinta";
            this.locked.UseVisualStyleBackColor = true;
            this.locked.CheckedChanged += new System.EventHandler(this.locked_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.rida);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.vidutinisGreitis);
            this.groupBox2.Controls.Add(this.degaluSanaudos);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 268);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Automobilio Statistika";
            this.groupBox2.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "Rida (km)";
            // 
            // rida
            // 
            this.rida.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rida.Location = new System.Drawing.Point(203, 32);
            this.rida.Name = "rida";
            this.rida.Size = new System.Drawing.Size(54, 26);
            this.rida.TabIndex = 1;
            this.rida.Text = "--";
            this.rida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 19);
            this.label7.TabIndex = 7;
            this.label7.Text = "Vidutinis greitis (km/h)";
            // 
            // vidutinisGreitis
            // 
            this.vidutinisGreitis.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vidutinisGreitis.Location = new System.Drawing.Point(203, 55);
            this.vidutinisGreitis.Name = "vidutinisGreitis";
            this.vidutinisGreitis.Size = new System.Drawing.Size(54, 26);
            this.vidutinisGreitis.TabIndex = 3;
            this.vidutinisGreitis.Text = "--";
            this.vidutinisGreitis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // degaluSanaudos
            // 
            this.degaluSanaudos.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.degaluSanaudos.Location = new System.Drawing.Point(203, 78);
            this.degaluSanaudos.Name = "degaluSanaudos";
            this.degaluSanaudos.Size = new System.Drawing.Size(54, 26);
            this.degaluSanaudos.TabIndex = 4;
            this.degaluSanaudos.Text = "--";
            this.degaluSanaudos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 19);
            this.label8.TabIndex = 8;
            this.label8.Text = "Degalų sąnaudos (l/100km)";
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.ForeColor = System.Drawing.Color.Red;
            this.lblConnection.Location = new System.Drawing.Point(210, 9);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(68, 13);
            this.lblConnection.TabIndex = 29;
            this.lblConnection.Text = "Neprisijungta";
            // 
            // MainMenu
            // 
            this.MainMenu.Controls.Add(this.button1);
            this.MainMenu.Controls.Add(this.SOSCallOptions);
            this.MainMenu.Controls.Add(this.AutoCondition);
            this.MainMenu.Location = new System.Drawing.Point(27, 42);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(209, 253);
            this.MainMenu.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(4, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 25);
            this.button1.TabIndex = 32;
            this.button1.Text = "Automobilio statistika";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SOSCallOptions
            // 
            this.SOSCallOptions.Font = new System.Drawing.Font("Times New Roman", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SOSCallOptions.Location = new System.Drawing.Point(4, 69);
            this.SOSCallOptions.Name = "SOSCallOptions";
            this.SOSCallOptions.Size = new System.Drawing.Size(203, 25);
            this.SOSCallOptions.TabIndex = 31;
            this.SOSCallOptions.Text = "Nustatyti SOS skambuti";
            this.SOSCallOptions.UseVisualStyleBackColor = true;
            this.SOSCallOptions.Click += new System.EventHandler(this.SOSCallOptions_Click);
            // 
            // AutoCondition
            // 
            this.AutoCondition.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AutoCondition.Location = new System.Drawing.Point(4, 38);
            this.AutoCondition.Name = "AutoCondition";
            this.AutoCondition.Size = new System.Drawing.Size(203, 25);
            this.AutoCondition.TabIndex = 31;
            this.AutoCondition.Text = "Automobilio busena";
            this.AutoCondition.UseVisualStyleBackColor = true;
            this.AutoCondition.Click += new System.EventHandler(this.AutoCondition_Click);
            // 
            // toMenu
            // 
            this.toMenu.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toMenu.Location = new System.Drawing.Point(15, 4);
            this.toMenu.Name = "toMenu";
            this.toMenu.Size = new System.Drawing.Size(75, 23);
            this.toMenu.TabIndex = 31;
            this.toMenu.Text = "Atgal";
            this.toMenu.UseVisualStyleBackColor = true;
            this.toMenu.Click += new System.EventHandler(this.toMenu_Click);
            // 
            // laikmatis
            // 
            this.laikmatis.Interval = 1800000;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(185, 19);
            this.label9.TabIndex = 33;
            this.label9.Text = "Momentinis Greitis (km/h)";
            // 
            // momentinisGreitis
            // 
            this.momentinisGreitis.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.momentinisGreitis.Location = new System.Drawing.Point(194, 103);
            this.momentinisGreitis.Name = "momentinisGreitis";
            this.momentinisGreitis.Size = new System.Drawing.Size(67, 26);
            this.momentinisGreitis.TabIndex = 30;
            this.momentinisGreitis.Text = "--";
            this.momentinisGreitis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 19);
            this.label10.TabIndex = 34;
            this.label10.Text = "Degalų kiekis (l)";
            // 
            // degalai
            // 
            this.degalai.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.degalai.Location = new System.Drawing.Point(194, 126);
            this.degalai.Name = "degalai";
            this.degalai.Size = new System.Drawing.Size(67, 26);
            this.degalai.TabIndex = 31;
            this.degalai.Text = "--";
            this.degalai.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // krituliai
            // 
            this.krituliai.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.krituliai.Location = new System.Drawing.Point(194, 149);
            this.krituliai.Name = "krituliai";
            this.krituliai.Size = new System.Drawing.Size(67, 26);
            this.krituliai.TabIndex = 32;
            this.krituliai.Text = "--";
            this.krituliai.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 19);
            this.label11.TabIndex = 35;
            this.label11.Text = "Kritulių kiekis";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(285, 356);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toMenu);
            this.Controls.Add(this.lblConnection);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "App";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button logIn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.TextBox userName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox variklioTemp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox laukoTemp;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.RadioButton engineOn;
		private System.Windows.Forms.RadioButton engineOff;
		private System.Windows.Forms.TextBox vidausTemp;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton lightsOn;
		private System.Windows.Forms.RadioButton lightsOff;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton windowsOpened;
		private System.Windows.Forms.RadioButton windowsClosed;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.RadioButton unlocked;
		private System.Windows.Forms.RadioButton locked;
		private System.Windows.Forms.Label lblConnection;
		private System.Windows.Forms.Panel MainMenu;
		private System.Windows.Forms.Button SOSCallOptions;
		private System.Windows.Forms.Button AutoCondition;
        private System.Windows.Forms.Button toMenu;
		private System.Windows.Forms.Timer laikmatis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox rida;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox vidutinisGreitis;
        private System.Windows.Forms.TextBox degaluSanaudos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox momentinisGreitis;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox degalai;
        private System.Windows.Forms.TextBox krituliai;
        private System.Windows.Forms.Label label11;
	}
}

