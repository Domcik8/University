namespace email
{
    partial class SendEmailForm
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
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.subjectTextBox = new System.Windows.Forms.TextBox();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.toLabel = new System.Windows.Forms.Label();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.attachTextBox1 = new System.Windows.Forms.TextBox();
            this.attachButton1 = new System.Windows.Forms.Button();
            this.testInfoButton = new System.Windows.Forms.Button();
            this.attachTextBox2 = new System.Windows.Forms.TextBox();
            this.attachButton2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(111, 12);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.Size = new System.Drawing.Size(216, 20);
            this.toTextBox.TabIndex = 0;
            // 
            // subjectTextBox
            // 
            this.subjectTextBox.Location = new System.Drawing.Point(111, 38);
            this.subjectTextBox.Name = "subjectTextBox";
            this.subjectTextBox.Size = new System.Drawing.Size(216, 20);
            this.subjectTextBox.TabIndex = 2;
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(49, 135);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(529, 184);
            this.messageTextBox.TabIndex = 3;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(49, 325);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(267, 64);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(322, 325);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(256, 64);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(69, 15);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(23, 13);
            this.toLabel.TabIndex = 6;
            this.toLabel.Text = "To:";
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Location = new System.Drawing.Point(46, 41);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(46, 13);
            this.subjectLabel.TabIndex = 7;
            this.subjectLabel.Text = "Subject:";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(46, 119);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(53, 13);
            this.messageLabel.TabIndex = 8;
            this.messageLabel.Text = "Message:";
            // 
            // attachTextBox1
            // 
            this.attachTextBox1.Location = new System.Drawing.Point(111, 65);
            this.attachTextBox1.Name = "attachTextBox1";
            this.attachTextBox1.Size = new System.Drawing.Size(216, 20);
            this.attachTextBox1.TabIndex = 10;
            // 
            // attachButton1
            // 
            this.attachButton1.Location = new System.Drawing.Point(54, 65);
            this.attachButton1.Name = "attachButton1";
            this.attachButton1.Size = new System.Drawing.Size(51, 23);
            this.attachButton1.TabIndex = 11;
            this.attachButton1.Text = "Attach";
            this.attachButton1.UseVisualStyleBackColor = true;
            this.attachButton1.Click += new System.EventHandler(this.attachButton_Click);
            // 
            // testInfoButton
            // 
            this.testInfoButton.Location = new System.Drawing.Point(342, 10);
            this.testInfoButton.Name = "testInfoButton";
            this.testInfoButton.Size = new System.Drawing.Size(103, 23);
            this.testInfoButton.TabIndex = 12;
            this.testInfoButton.Text = "Test Info";
            this.testInfoButton.UseVisualStyleBackColor = true;
            this.testInfoButton.Click += new System.EventHandler(this.testInfoButton_Click);
            // 
            // attachTextBox2
            // 
            this.attachTextBox2.Location = new System.Drawing.Point(111, 92);
            this.attachTextBox2.Name = "attachTextBox2";
            this.attachTextBox2.Size = new System.Drawing.Size(216, 20);
            this.attachTextBox2.TabIndex = 13;
            // 
            // attachButton2
            // 
            this.attachButton2.Location = new System.Drawing.Point(54, 93);
            this.attachButton2.Name = "attachButton2";
            this.attachButton2.Size = new System.Drawing.Size(51, 23);
            this.attachButton2.TabIndex = 14;
            this.attachButton2.Text = "Attach";
            this.attachButton2.UseVisualStyleBackColor = true;
            this.attachButton2.Click += new System.EventHandler(this.attachButton2_Click);
            // 
            // SendEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 404);
            this.Controls.Add(this.attachButton2);
            this.Controls.Add(this.attachTextBox2);
            this.Controls.Add(this.testInfoButton);
            this.Controls.Add(this.attachButton1);
            this.Controls.Add(this.attachTextBox1);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.subjectTextBox);
            this.Controls.Add(this.toTextBox);
            this.Name = "SendEmailForm";
            this.Text = "Send email";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.TextBox subjectTextBox;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.Label subjectLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.TextBox attachTextBox1;
        private System.Windows.Forms.Button attachButton1;
        private System.Windows.Forms.Button testInfoButton;
        private System.Windows.Forms.TextBox attachTextBox2;
        private System.Windows.Forms.Button attachButton2;
    }
}