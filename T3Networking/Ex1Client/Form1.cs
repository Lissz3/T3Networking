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

		private bool Connect()
		{
			IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), PORT);

			server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				server.Connect(ie);
				return true;
			}
			catch (SocketException se)
			{
				lblAnswer.Text = string.Format("Error connection: {0}\nError code: {1}({2})", se.Message, (SocketError)se.ErrorCode, se.ErrorCode);
				return false;
			}
		}

		private void BtnClick(object sender, EventArgs e)
		{
			if (Connect()) //No salta excepcion, si no se puede conectar no entra
			{
				if (conexion)
				{
					using (NetworkStream ns = new NetworkStream(server))
					using (StreamReader sr = new StreamReader(ns))
					using (StreamWriter sw = new StreamWriter(ns))
					{
						Button b = (Button)sender;
						string command; //Envia comando y contraseña en el mismo mensaje
						if (b.Tag != "close")
						{
							command = b.Tag.ToString();
						}
						else
						{
							command = b.Tag.ToString() + txtPass.Text;
						}

						ButtonAction(sw, sr, command);

						server.Close();
					}
				}
				else
				{
					lblAnswer.Text = "No server connected.";
				}
			}
			else
			{
				lblAnswer.Text = "Unnable to connect.";
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
					if (int.TryParse(f.txbPort.Text, out newPort) && IPAddress.TryParse(f.txbIp.Text, out _)) //Parse IP
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
							conexion = false;
						}
					}
					else
					{
						lblAnswer.Text = "IP and Port not changed. Invalid IP or Port.";
						conexion = false;
					}

					break;
				case DialogResult.Cancel:
					lblAnswer.Text = "IP and Port not changed.";
					conexion = false;
					break;
			}
		}
	}
}

