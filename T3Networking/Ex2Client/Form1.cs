using System.Net.Sockets;
using System.Net;
using System;

namespace Ex2Client
{
	public partial class Form1 : Form
	{
		Socket server;
		IPAddress IP_SERVER;
		int PORT;
		string USER;
		string DATA;

		public Form1()
		{
			InitializeComponent();
			ReadData();
		}

		public void NoData()
		{
			if (IPAddress.TryParse(txbIp.Text, out IP_SERVER) && int.TryParse(txbPort.Text, out PORT))
			{
				USER = txbUser.Text;
			}
		}

		public void ReadData()
		{
			try
			{
				using (StreamReader sr = new StreamReader(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\data.txt"))
				{
					DATA = sr.ReadToEnd();
					string[] splitData = DATA.Split('-');

					if (splitData.Length == 3)
					{
						if (IPAddress.TryParse(splitData[0], out IP_SERVER) && int.TryParse(splitData[1], out PORT))
						{
							USER = splitData[splitData.Length - 1];
							txbIp.Text = IP_SERVER.ToString();
							txbPort.Text = PORT.ToString();
							txbUser.Text = USER;
						}
					}
					else
					{
						DATA = null;
					}
				}
			}
			catch (FileNotFoundException e)
			{
				txbAnswer.Text = $"The file was not found: '{e}'";
			}
			catch (DirectoryNotFoundException e)
			{
				txbAnswer.Text = $"The directory was not found: '{e}'";
			}
			catch (UnauthorizedAccessException e)
			{
				txbAnswer.Text = $"Unauthorized access: '{e}'";
			}
			catch (IOException e)
			{
				txbAnswer.Text = $"The file could not be opened: '{e}'";
			}
		}

		public void WriteData()
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\data.txt"))
				{
					sw.Write(txbIp.Text + "-" + txbPort.Text + "-" + txbUser.Text);
				}
			}
			catch (FileNotFoundException e)
			{
				txbAnswer.Text = $"The file was not found: '{e}'";
			}
			catch (DirectoryNotFoundException e)
			{
				txbAnswer.Text = $"The directory was not found: '{e}'";
			}
			catch (UnauthorizedAccessException e)
			{
				txbAnswer.Text = $"Unauthorized access: '{e}'";
			}
			catch (IOException e)
			{
				txbAnswer.Text = $"The file could not be opened: '{e}'";
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
			btnAdd.Enabled = false;
			btnList.Enabled = false;
		}

		private void BtnApply_Click(object sender, EventArgs e)
		{
			if (DATA == null)
			{
				NoData();
			}

			if (Connect())
			{
				btnAdd.Enabled = true;
				btnList.Enabled = true;
			}
			Using("welcome");
		}

		private void Using(string type)
		{
			using (NetworkStream ns = new NetworkStream(server))
			using (StreamReader sr = new StreamReader(ns))
			using (StreamWriter sw = new StreamWriter(ns))
			{
				switch (type)
				{
					case "welcome":
						Welcome(sw, sr);
						break;
					case "add":
						Communication(type, sw, sr);
						break;
					case "list":
						Communication(type, sw, sr);
						break;
				}
			}
		}

		private void Welcome(StreamWriter sw, StreamReader sr)
		{
			txbAnswer.Text = sr.ReadLine();
			sw.WriteLine(USER);
			sw.Flush();
		}

		private void Communication(string msg, StreamWriter sw, StreamReader sr)
		{
			sw.WriteLine(msg);
			sw.Flush();
			string[] waitRoom = sr.ReadLine().Split('-');
			if (waitRoom.Length > 1)
			{
				txbAnswer.Text = ""; 
				for (int i = 0; i < waitRoom.Length; i++)
				{
					txbAnswer.Text += waitRoom[i] + "\n";
				}
			} else
			{
				txbAnswer.Text = waitRoom[0];
			}
			server.Close();
		}

		private void BtnAdd_Click(object sender, EventArgs e)
		{
			Using("add");
			btnAdd.Enabled = false;
			btnList.Enabled = false;
		}

		private void BtnList_Click(object sender, EventArgs e)
		{
			Using("list");
			btnAdd.Enabled = false;
			btnList.Enabled = false;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			WriteData();
		}
	}
}