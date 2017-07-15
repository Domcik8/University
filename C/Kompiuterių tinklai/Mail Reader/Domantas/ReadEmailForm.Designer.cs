namespace email
{
    partial class ReadEmailForm
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
            this.backButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.totalEmailsLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.readSelectedButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subjectColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loadNewButton = new System.Windows.Forms.Button();
            this.replyButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(481, 166);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(94, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(12, 319);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageTextBox.Size = new System.Drawing.Size(594, 274);
            this.messageTextBox.TabIndex = 1;
            // 
            // totalEmailsLabel
            // 
            this.totalEmailsLabel.AutoSize = true;
            this.totalEmailsLabel.Location = new System.Drawing.Point(478, 24);
            this.totalEmailsLabel.Name = "totalEmailsLabel";
            this.totalEmailsLabel.Size = new System.Drawing.Size(100, 13);
            this.totalEmailsLabel.TabIndex = 2;
            this.totalEmailsLabel.Text = "Total emails shown:";
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Location = new System.Drawing.Point(593, 24);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(13, 13);
            this.amountLabel.TabIndex = 3;
            this.amountLabel.Text = "0";
            // 
            // readSelectedButton
            // 
            this.readSelectedButton.Location = new System.Drawing.Point(481, 80);
            this.readSelectedButton.Name = "readSelectedButton";
            this.readSelectedButton.Size = new System.Drawing.Size(94, 23);
            this.readSelectedButton.TabIndex = 5;
            this.readSelectedButton.Text = "Read selected";
            this.readSelectedButton.UseVisualStyleBackColor = true;
            this.readSelectedButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateColumn,
            this.fromColumn,
            this.subjectColumn,
            this.textColumn,
            this.uid});
            this.dataGridView.Location = new System.Drawing.Point(12, 24);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(444, 289);
            this.dataGridView.TabIndex = 6;
            // 
            // dateColumn
            // 
            this.dateColumn.HeaderText = "Date";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            // 
            // fromColumn
            // 
            this.fromColumn.HeaderText = "From";
            this.fromColumn.Name = "fromColumn";
            this.fromColumn.ReadOnly = true;
            // 
            // subjectColumn
            // 
            this.subjectColumn.HeaderText = "Subject";
            this.subjectColumn.Name = "subjectColumn";
            this.subjectColumn.ReadOnly = true;
            // 
            // textColumn
            // 
            this.textColumn.HeaderText = "Text";
            this.textColumn.Name = "textColumn";
            this.textColumn.ReadOnly = true;
            // 
            // uid
            // 
            this.uid.HeaderText = "uid";
            this.uid.Name = "uid";
            this.uid.ReadOnly = true;
            this.uid.Visible = false;
            // 
            // loadNewButton
            // 
            this.loadNewButton.Location = new System.Drawing.Point(481, 51);
            this.loadNewButton.Name = "loadNewButton";
            this.loadNewButton.Size = new System.Drawing.Size(94, 23);
            this.loadNewButton.TabIndex = 7;
            this.loadNewButton.Text = "Load new";
            this.loadNewButton.UseVisualStyleBackColor = true;
            this.loadNewButton.Click += new System.EventHandler(this.loadNewButton_Click);
            // 
            // replyButton
            // 
            this.replyButton.Location = new System.Drawing.Point(481, 109);
            this.replyButton.Name = "replyButton";
            this.replyButton.Size = new System.Drawing.Size(94, 23);
            this.replyButton.TabIndex = 9;
            this.replyButton.Text = "Reply";
            this.replyButton.UseVisualStyleBackColor = true;
            this.replyButton.Click += new System.EventHandler(this.replyButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(481, 137);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(94, 23);
            this.LoadButton.TabIndex = 10;
            this.LoadButton.Text = "Load oldest";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click_1);
            // 
            // ReadEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 634);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.replyButton);
            this.Controls.Add(this.loadNewButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.readSelectedButton);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.totalEmailsLabel);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.backButton);
            this.Name = "ReadEmailForm";
            this.Text = "ReadEmailForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label totalEmailsLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.Button readSelectedButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button loadNewButton;
        private System.Windows.Forms.Button replyButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subjectColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uid;
        private System.Windows.Forms.Button LoadButton;
    }
}