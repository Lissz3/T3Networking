namespace Ex1Client
{
	partial class Form2
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
			this.lblIp = new System.Windows.Forms.Label();
			this.lblPort = new System.Windows.Forms.Label();
			this.txbIp = new System.Windows.Forms.TextBox();
			this.txbPort = new System.Windows.Forms.TextBox();
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblIp
			// 
			this.lblIp.AutoSize = true;
			this.lblIp.Location = new System.Drawing.Point(65, 22);
			this.lblIp.Name = "lblIp";
			this.lblIp.Size = new System.Drawing.Size(20, 15);
			this.lblIp.TabIndex = 0;
			this.lblIp.Text = "IP:";
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(65, 55);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(32, 15);
			this.lblPort.TabIndex = 1;
			this.lblPort.Text = "Port:";
			// 
			// txbIp
			// 
			this.txbIp.Location = new System.Drawing.Point(109, 19);
			this.txbIp.Name = "txbIp";
			this.txbIp.Size = new System.Drawing.Size(100, 23);
			this.txbIp.TabIndex = 2;
			// 
			// txbPort
			// 
			this.txbPort.Location = new System.Drawing.Point(109, 48);
			this.txbPort.Name = "txbPort";
			this.txbPort.Size = new System.Drawing.Size(100, 23);
			this.txbPort.TabIndex = 3;
			// 
			// btnAccept
			// 
			this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnAccept.Location = new System.Drawing.Point(72, 82);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 4;
			this.btnAccept.Text = "Accept";
			this.btnAccept.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(153, 82);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 117);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.txbPort);
			this.Controls.Add(this.txbIp);
			this.Controls.Add(this.lblPort);
			this.Controls.Add(this.lblIp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New IP and Port";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblIp;
		private Label lblPort;
		public TextBox txbIp;
		public TextBox txbPort;
		private Button btnAccept;
		private Button btnCancel;
	}
}