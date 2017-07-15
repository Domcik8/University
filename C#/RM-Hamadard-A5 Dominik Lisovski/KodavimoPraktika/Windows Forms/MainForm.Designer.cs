namespace Messenger
{
    partial class MainForm
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
            this.Pranešimas = new System.Windows.Forms.TabPage();
            this.DecodedMessageBox = new System.Windows.Forms.RichTextBox();
            this.ReceivedEncodedMessageBox = new System.Windows.Forms.RichTextBox();
            this.ReceivedMessageBox = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mistakeChanceBox = new System.Windows.Forms.TextBox();
            this.mBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EncodedMessageBox = new System.Windows.Forms.TextBox();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DekoduotiButton = new System.Windows.Forms.Button();
            this.SiustiButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Pranešimas.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pranešimas
            // 
            this.Pranešimas.Controls.Add(this.DecodedMessageBox);
            this.Pranešimas.Controls.Add(this.ReceivedEncodedMessageBox);
            this.Pranešimas.Controls.Add(this.ReceivedMessageBox);
            this.Pranešimas.Controls.Add(this.label5);
            this.Pranešimas.Controls.Add(this.panel1);
            this.Pranešimas.Controls.Add(this.label6);
            this.Pranešimas.Controls.Add(this.EncodedMessageBox);
            this.Pranešimas.Controls.Add(this.MessageBox);
            this.Pranešimas.Controls.Add(this.label4);
            this.Pranešimas.Controls.Add(this.DekoduotiButton);
            this.Pranešimas.Controls.Add(this.SiustiButton);
            this.Pranešimas.Controls.Add(this.label3);
            this.Pranešimas.Controls.Add(this.label2);
            this.Pranešimas.Controls.Add(this.label1);
            this.Pranešimas.Location = new System.Drawing.Point(4, 25);
            this.Pranešimas.Name = "Pranešimas";
            this.Pranešimas.Padding = new System.Windows.Forms.Padding(3);
            this.Pranešimas.Size = new System.Drawing.Size(822, 266);
            this.Pranešimas.TabIndex = 0;
            this.Pranešimas.Text = "Kanalas";
            this.Pranešimas.UseVisualStyleBackColor = true;
            // 
            // DecodedMessageBox
            // 
            this.DecodedMessageBox.Location = new System.Drawing.Point(408, 160);
            this.DecodedMessageBox.Name = "DecodedMessageBox";
            this.DecodedMessageBox.ReadOnly = true;
            this.DecodedMessageBox.Size = new System.Drawing.Size(390, 29);
            this.DecodedMessageBox.TabIndex = 17;
            this.DecodedMessageBox.Text = "";
            // 
            // ReceivedEncodedMessageBox
            // 
            this.ReceivedEncodedMessageBox.Location = new System.Drawing.Point(408, 93);
            this.ReceivedEncodedMessageBox.Name = "ReceivedEncodedMessageBox";
            this.ReceivedEncodedMessageBox.Size = new System.Drawing.Size(390, 27);
            this.ReceivedEncodedMessageBox.TabIndex = 16;
            this.ReceivedEncodedMessageBox.Text = "";
            this.ReceivedEncodedMessageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReceivedEncodedMessageBox_KeyPress);
            // 
            // ReceivedMessageBox
            // 
            this.ReceivedMessageBox.Location = new System.Drawing.Point(7, 93);
            this.ReceivedMessageBox.Name = "ReceivedMessageBox";
            this.ReceivedMessageBox.ReadOnly = true;
            this.ReceivedMessageBox.Size = new System.Drawing.Size(380, 27);
            this.ReceivedMessageBox.TabIndex = 15;
            this.ReceivedMessageBox.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Parametrai";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.mistakeChanceBox);
            this.panel1.Controls.Add(this.mBox);
            this.panel1.Location = new System.Drawing.Point(6, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 73);
            this.panel1.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(157, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Klaidos tikimybė";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Parametras m";
            // 
            // mistakeChanceBox
            // 
            this.mistakeChanceBox.Location = new System.Drawing.Point(160, 33);
            this.mistakeChanceBox.Name = "mistakeChanceBox";
            this.mistakeChanceBox.Size = new System.Drawing.Size(120, 22);
            this.mistakeChanceBox.TabIndex = 1;
            // 
            // mBox
            // 
            this.mBox.Location = new System.Drawing.Point(13, 33);
            this.mBox.Name = "mBox";
            this.mBox.Size = new System.Drawing.Size(120, 22);
            this.mBox.TabIndex = 0;
            this.mBox.TextChanged += new System.EventHandler(this.mBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(405, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Atėjęs užkoduotas pranešimas";
            // 
            // EncodedMessageBox
            // 
            this.EncodedMessageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.EncodedMessageBox.Location = new System.Drawing.Point(408, 33);
            this.EncodedMessageBox.Multiline = true;
            this.EncodedMessageBox.Name = "EncodedMessageBox";
            this.EncodedMessageBox.ReadOnly = true;
            this.EncodedMessageBox.Size = new System.Drawing.Size(390, 27);
            this.EncodedMessageBox.TabIndex = 5;
            // 
            // MessageBox
            // 
            this.MessageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.MessageBox.Location = new System.Drawing.Point(6, 33);
            this.MessageBox.Multiline = true;
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(381, 27);
            this.MessageBox.TabIndex = 3;
            this.MessageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Message_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Dekoduotas pranešimas";
            // 
            // DekoduotiButton
            // 
            this.DekoduotiButton.AutoSize = true;
            this.DekoduotiButton.Enabled = false;
            this.DekoduotiButton.Location = new System.Drawing.Point(612, 205);
            this.DekoduotiButton.Name = "DekoduotiButton";
            this.DekoduotiButton.Size = new System.Drawing.Size(186, 27);
            this.DekoduotiButton.TabIndex = 10;
            this.DekoduotiButton.Text = "Dekoduoti";
            this.DekoduotiButton.UseVisualStyleBackColor = true;
            this.DekoduotiButton.Click += new System.EventHandler(this.DekoduotiButton_Click);
            // 
            // SiustiButton
            // 
            this.SiustiButton.AutoSize = true;
            this.SiustiButton.Location = new System.Drawing.Point(408, 205);
            this.SiustiButton.Name = "SiustiButton";
            this.SiustiButton.Size = new System.Drawing.Size(198, 27);
            this.SiustiButton.TabIndex = 9;
            this.SiustiButton.Text = "Siųsti";
            this.SiustiButton.UseVisualStyleBackColor = true;
            this.SiustiButton.Click += new System.EventHandler(this.Siusti_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Atėjęs pranešimas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Užkoduotas pranešimas";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pranešimas";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Pranešimas);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(830, 295);
            this.tabControl1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(855, 324);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Kodavimo teorija A5, Dominik Gabriel Lisovski PS6";
            this.Pranešimas.ResumeLayout(false);
            this.Pranešimas.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage Pranešimas;
        private System.Windows.Forms.TextBox EncodedMessageBox;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SiustiButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox mistakeChanceBox;
        private System.Windows.Forms.TextBox mBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button DekoduotiButton;
        private System.Windows.Forms.RichTextBox ReceivedMessageBox;
        private System.Windows.Forms.RichTextBox ReceivedEncodedMessageBox;
        private System.Windows.Forms.RichTextBox DecodedMessageBox;
    }
}

