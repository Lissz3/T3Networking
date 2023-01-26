using System.Net.Sockets;
using System.Net;

namespace Ex1
{
	internal class Program
	{

		public static void Server()
		{
			IPEndPoint ie = new IPEndPoint(IPAddress.Any, 31416);
			using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
			ProtocolType.Tcp))
			{
				s.Bind(ie);
				s.Listen(10);
				Console.WriteLine($"Server listening at port:{ie.Port}");
				Socket sClient = s.Accept();
				IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;
				Console.WriteLine("Client connected:{0} at port {1}", ieClient.Address,
				ieClient.Port);
				using (NetworkStream ns = new NetworkStream(sClient))
				using (StreamReader sr = new StreamReader(ns))
				using (StreamWriter sw = new StreamWriter(ns))
				{
					//sr.BaseStream.ReadTimeout = 3000;
					string msg = "";
					while (msg != null)
					{
						try
						{
							msg = sr.ReadLine();
							if (msg != null)
							{
								if (msg == "time")
								{
									msg += " /t";
								}
								Console.WriteLine(msg);
								sw.Flush();
							}
						}
						catch (IOException e)
						{
							msg = null;
						}
					}
					Console.WriteLine("Client disconnected.\nConnection closed");
				}
				sClient.Close(); // Este no se abre con using, pues lo devuelve el accept.
			}
		}

		static void Main(string[] args)
		{
			Server();
		}
	}
}