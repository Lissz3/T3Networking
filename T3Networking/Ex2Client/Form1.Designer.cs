namespace Ex2Client
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
			this.lblIP = new System.Windows.Forms.Label();
			this.txbIp = new System.Windows.Forms.TextBox();
			this.txbPort = new System.Windows.Forms.TextBox();
			this.lblPort = new System.Windows.Forms.Label();
			this.txbUser = new System.Windows.Forms.TextBox();
			this.lblUser = new System.Windows.Forms.Label();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnList = new System.Windows.Forms.Button();
			this.txbAnswer = new System.Windows.Forms.RichTextBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblIP
			// 
			this.lblIP.AutoSize = true;
			this.lblIP.Location = new System.Drawing.Point(47, 173);
			this.lblIP.Name = "lblIP";
			this.lblIP.Size = new System.Drawing.Size(20, 15);
			this.lblIP.TabIndex = 0;
			this.lblIP.Text = "IP:";
			// 
			// txbIp
			// 
			this.txbIp.Location = new System.Drawing.Point(73, 170);
			this.txbIp.Name = "txbIp";
			this.txbIp.Size = new System.Drawing.Size(158, 23);
			this.txbIp.TabIndex = 1;
			// 
			// txbPort
			// 
			this.txbPort.Location = new System.Drawing.Point(274, 169);
			this.txbPort.Name = "txbPort";
			this.txbPort.Size = new System.Drawing.Size(119, 23);
			this.txbPort.TabIndex = 3;
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(239, 173);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(32, 15);
			this.lblPort.TabIndex = 2;
			this.lblPort.Text = "Port:";
			// 
			// txbUser
			// 
			this.txbUser.Location = new System.Drawing.Point(73, 199);
			this.txbUser.Name = "txbUser";
			this.txbUser.Size = new System.Drawing.Size(158, 23);
			this.txbUser.TabIndex = 5;
			// 
			// lblUser
			// 
			this.lblUser.AutoSize = true;
			this.lblUser.Location = new System.Drawing.Point(34, 202);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(33, 15);
			this.lblUser.TabIndex = 4;
			this.lblUser.Text = "User:";
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(34, 12);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(176, 23);
			this.btnAdd.TabIndex = 6;
			this.btnAdd.Text = "ADD";
			this.btnAdd.UseVisualStyleBackColor = true;
			// 
			// btnList
			// 
			this.btnList.Location = new System.Drawing.Point(226, 12);
			this.btnList.Name = "btnList";
			this.btnList.Size = new System.Drawing.Size(167, 23);
			this.btnList.TabIndex = 7;
			this.btnList.Text = "LIST";
			this.btnList.UseVisualStyleBackColor = true;
			// 
			// txbAnswer
			// 
			this.txbAnswer.Location = new System.Drawing.Point(34, 41);
			this.txbAnswer.Name = "txbAnswer";
			this.txbAnswer.Size = new System.Drawing.Size(360, 122);
			this.txbAnswer.TabIndex = 8;
			this.txbAnswer.Text = "";
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(239, 199);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(154, 23);
			this.btnApply.TabIndex = 9;
			this.btnApply.Text = "APPLY";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(430, 228);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.txbAnswer);
			this.Controls.Add(this.btnList);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.txbUser);
			this.Controls.Add(this.lblUser);
			this.Controls.Add(this.txbPort);
			this.Controls.Add(this.lblPort);
			this.Controls.Add(this.txbIp);
			this.Controls.Add(this.lblIP);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblIP;
		private TextBox txbIp;
		private TextBox txbPort;
		private Label lblPort;
		private TextBox txbUser;
		private Label lblUser;
		private Button btnAdd;
		private Button btnList;
		private RichTextBox txbAnswer;
		private Button btnApply;
	}
}