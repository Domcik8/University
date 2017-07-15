namespace email
{
    partial class MainMenu
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
            this.composeEmailButton = new System.Windows.Forms.Button();
            this.readEmailButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.updateCredentialsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // composeEmailButton
            // 
            this.composeEmailButton.Location = new System.Drawing.Point(12, 12);
            this.composeEmailButton.Name = "composeEmailButton";
            this.composeEmailButton.Size = new System.Drawing.Size(151, 45);
            this.composeEmailButton.TabIndex = 0;
            this.composeEmailButton.Text = "Compose email";
            this.composeEmailButton.UseVisualStyleBackColor = true;
            this.composeEmailButton.Click += new System.EventHandler(this.composeEmailButton_Click);
            // 
            // readEmailButton
            // 
            this.readEmailButton.Location = new System.Drawing.Point(12, 63);
            this.readEmailButton.Name = "readEmailButton";
            this.readEmailButton.Size = new System.Drawing.Size(151, 51);
            this.readEmailButton.TabIndex = 1;
            this.readEmailButton.Text = "Read email";
            this.readEmailButton.UseVisualStyleBackColor = true;
            this.readEmailButton.Click += new System.EventHandler(this.readEmailButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(12, 179);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(151, 46);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // updateCredentialsButton
            // 
            this.updateCredentialsButton.Location = new System.Drawing.Point(12, 120);
            this.updateCredentialsButton.Name = "updateCredentialsButton";
            this.updateCredentialsButton.Size = new System.Drawing.Size(151, 53);
            this.updateCredentialsButton.TabIndex = 3;
            this.updateCredentialsButton.Text = "Update credentials";
            this.updateCredentialsButton.UseVisualStyleBackColor = true;
            this.updateCredentialsButton.Click += new System.EventHandler(this.updateCredentialsButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 236);
            this.Controls.Add(this.updateCredentialsButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.readEmailButton);
            this.Controls.Add(this.composeEmailButton);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button composeEmailButton;
        private System.Windows.Forms.Button readEmailButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button updateCredentialsButton;
    }
}