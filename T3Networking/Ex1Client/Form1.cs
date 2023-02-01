using System.Net;
using System.Net.Sockets;

namespace Ex1Client
{
	public partial class Form1 : Form
	{
		Socket server;
		bool conexion;
		string IP_SERVER;
		int PORT;

		public Form1()
		{
			InitializeComponent();
			conexion = true;
			IP_SERVER = "127.0.0.1";
			PORT = 31416;
		}

		private void Connect()
		{
			IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), PORT);

			server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				server.Connect(ie);
			}
			catch (SocketException se)
			{
				lblAnswer.Text = string.Format("Error connection: {0}\nError code: {1}({2})", se.Message, (SocketError)se.ErrorCode, se.ErrorCode);
			}
		}

		private void BtnClick(object sender, EventArgs e)
		{
			if (conexion)
			{
				Connect();

				using (NetworkStream ns = new NetworkStream(server))
				using (StreamReader sr = new StreamReader(ns))
				using (StreamWriter sw = new StreamWriter(ns))
				{
					Button b = (Button)sender;
					switch (b)
					{
						case Button timer when b == btnTimer:
							ButtonAction(sw, sr, "time");
							break;
						case Button date when b == btnDate:
							ButtonAction(sw, sr, "date");
							break;
						case Button all when b == btnAll:
							ButtonAction(sw, sr, "all");
							break;
						case Button close when b == btnClose:
							sw.WriteLine("close");
							sw.Flush();
							sw.WriteLine(txtPass.Text);
							sw.Flush();
							lblAnswer.Text = sr.ReadLine();
							if (sr.ReadLine() == "close")
							{
								server.Close();
								conexion = false;
							}
							break;
					}
				}
			}
			else
			{
				lblAnswer.Text = "No server connected.";
			}
		}

		private void ButtonAction(StreamWriter sw, StreamReader sr, string msgToSend)
		{
			sw.WriteLine(msgToSend);
			sw.Flush();
			lblAnswer.Text = sr.ReadLine();
		}

		private void BtnNewIpPort_Click(object sender, EventArgs e)
		{
			Form2 f = new Form2();
			DialogResult res;
			res = f.ShowDialog();
			switch (res)
			{
				case DialogResult.OK:
					int newPort;
					if (int.TryParse(f.txbPort.Text, out newPort))
					{
						if (newPort != PORT && f.txbIp.Text != IP_SERVER && !conexion)
						{
							IP_SERVER = f.txbIp.Text;
							PORT = newPort;
							lblAnswer.Text = string.Format("New IP: {0}\nNew port: {1}", IP_SERVER, PORT);
							conexion = true;
						}
						else
						{
							lblAnswer.Text = "Unnable to connect. This server has been closed before.";
						}
					}
					else
					{
						lblAnswer.Text = "IP and Port not changed. Invalid port.";
					}

					break;
				case DialogResult.Cancel:
					lblAnswer.Text = "IP and Port not changed";
					break;
			}
		}
	}
}

