namespace Ex1Client
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnTimer = new System.Windows.Forms.Button();
			this.btnDate = new System.Windows.Forms.Button();
			this.btnAll = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.lblAnswer = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.btnNewIpPort = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnTimer
			// 
			this.btnTimer.Location = new System.Drawing.Point(12, 12);
			this.btnTimer.Name = "btnTimer";
			this.btnTimer.Size = new System.Drawing.Size(75, 23);
			this.btnTimer.TabIndex = 0;
			this.btnTimer.Tag = "time";
			this.btnTimer.Text = "Time";
			this.btnTimer.UseVisualStyleBackColor = true;
			this.btnTimer.Click += new System.EventHandler(this.BtnClick);
			// 
			// btnDate
			// 
			this.btnDate.Location = new System.Drawing.Point(93, 12);
			this.btnDate.Name = "btnDate";
			this.btnDate.Size = new System.Drawing.Size(75, 23);
			this.btnDate.TabIndex = 1;
			this.btnDate.Tag = "date";
			this.btnDate.Text = "Date";
			this.btnDate.UseVisualStyleBackColor = true;
			this.btnDate.Click += new System.EventHandler(this.BtnClick);
			// 
			// btnAll
			// 
			this.btnAll.Location = new System.Drawing.Point(174, 12);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(75, 23);
			this.btnAll.TabIndex = 2;
			this.btnAll.Tag = "all";
			this.btnAll.Text = "All";
			this.btnAll.UseVisualStyleBackColor = true;
			this.btnAll.Click += new System.EventHandler(this.BtnClick);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(174, 41);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Tag = "close";
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.BtnClick);
			// 
			// txtPass
			// 
			this.txtPass.Location = new System.Drawing.Point(85, 41);
			this.txtPass.Name = "txtPass";
			this.txtPass.Size = new System.Drawing.Size(83, 23);
			this.txtPass.TabIndex = 4;
			// 
			// lblAnswer
			// 
			this.lblAnswer.AutoSize = true;
			this.lblAnswer.Location = new System.Drawing.Point(16, 106);
			this.lblAnswer.Name = "lblAnswer";
			this.lblAnswer.Size = new System.Drawing.Size(46, 15);
			this.lblAnswer.TabIndex = 5;
			this.lblAnswer.Text = "Answer";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(16, 45);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(57, 15);
			this.lblPassword.TabIndex = 6;
			this.lblPassword.Text = "Password";
			// 
			// btnNewIpPort
			// 
			this.btnNewIpPort.Location = new System.Drawing.Point(16, 70);
			this.btnNewIpPort.Name = "btnNewIpPort";
			this.btnNewIpPort.Size = new System.Drawing.Size(233, 23);
			this.btnNewIpPort.TabIndex = 7;
			this.btnNewIpPort.Text = "New IP and Port";
			this.btnNewIpPort.UseVisualStyleBackColor = true;
			this.btnNewIpPort.Click += new System.EventHandler(this.BtnNewIpPort_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(279, 204);
			this.Controls.Add(this.btnNewIpPort);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblAnswer);
			this.Controls.Add(this.txtPass);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnAll);
			this.Controls.Add(this.btnDate);
			this.Controls.Add(this.btnTimer);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button btnTimer;
		private Button btnDate;
		private Button btnAll;
		private Button btnClose;
		private TextBox textBox1;
		private Label lblAnswer;
		private Label lblPassword;
		private TextBox txtPass;
		private Button btnNewIpPort;
	}
}