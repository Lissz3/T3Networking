using System.Net.Sockets;
using System.Net;
using System;

namespace Ex2Client
{
	public partial class Form1 : Form
	{
		Socket server;
		bool conexion;
		IPAddress IP_SERVER;
		int PORT;
		string USER;

		public Form1()
		{
			InitializeComponent();
		}

		public static string? ReadData()
		{
			try
			{
				using (StreamReader sr = new StreamReader(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\data.txt"))
				{
					return sr.ReadToEnd();
				}

			}
			catch (IOException)
			{
				return null;
			}
		}

		public static bool WriteData(string IP, string port, string user)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\data.txt"))
				{
					sw.Write(IP + "-");
					sw.Write(port + "-");
					sw.Write(user);
				}
				return true;
			}
			catch (IOException)
			{
				return false;
			}
		}

		private bool Connect()
		{
			IPEndPoint ie = new IPEndPoint(IP_SERVER, PORT);

			server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				server.Connect(ie);
				return true;
			}
			catch (SocketException se)
			{
				txbAnswer.Text = string.Format("Error connection: {0}\nError code: {1}({2})", se.Message, (SocketError)se.ErrorCode, se.ErrorCode);
				return false;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (!IsAnyData())
			{
				btnAdd.Enabled = false;
				btnList.Enabled = false;
			}
		}

		private bool IsAnyData()
		{
			string data;
			if ((data = ReadData()) != null)
			{
				if (SetIpAndPort(data, true))
				{
					return true;
				}
			}
			return false;
		}

		private void BtnApply_Click(object sender, EventArgs e)
		{
			string data = txbIp + " " + txbPort + " " + txbUser;
			if (SetIpAndPort(data, true) && Connect())
			{
				btnAdd.Enabled = true;
				btnList.Enabled = true;
			}
		}

		private bool SetIpAndPort(string data, bool file)
		{
			string[] splitData;
			if (file)
			{
				splitData = data.Split('-');
			}
			else
			{
				splitData = data.Split(" ");
			}

			if (splitData.Length > 2)
			{
				if (IPAddress.TryParse(splitData[0], out IP_SERVER) && int.TryParse(splitData[1], out PORT))
				{
					USER = splitData[2];
					return true;
				}
			}

			txbAnswer.Text = "IP or Port not valid.";
			return false;
		}
	}
}