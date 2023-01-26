using System.Net;
using System.Net.Sockets;

namespace Ex1Client
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnClick(object sender, EventArgs e)
		{
			const string IP_SERVER = "127.0.0.1";
			string msg;
			string userMsg;
			IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), 31416);

			Socket server = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);
			try
			{
				server.Connect(ie);
			}
			catch (SocketException se)
			{
				lblAnswer.Text = string.Format("Error connection: {0}\nError code: {1}({2})", se.Message, (SocketError)se.ErrorCode, se.ErrorCode);
			}

			using (NetworkStream ns = new NetworkStream(server))
			using (StreamReader sr = new StreamReader(ns))
			using (StreamWriter sw = new StreamWriter(ns))
			{
				Button b = (Button)sender;
				switch (b)
				{
					case Button timer when b == btnTimer:
						sw.WriteLine("time");
						lblAnswer.Text = sr.ReadLine();
						break;
					case Button date when b == btnDate:
						sw.WriteLine("date");
						break;
					case Button all when b == btnAll:
						break;
					case Button close when b == btnClose:
						break;
					default:
						break;


				}
			}
			lblAnswer.Text += "\tEnding connection";
			server.Close();
		}
	}
}
