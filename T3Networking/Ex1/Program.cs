using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Intrinsics.X86;

namespace Ex1
{
	internal class Program
	{
		public static string Password()
		{
			try
			{
				using (StreamReader srpsw = new StreamReader(Environment.GetEnvironmentVariable("PROGRAMDATA") + "\\pass.txt"))
				{
					return srpsw.ReadToEnd();
				}

			}
			catch (IOException e)
			{
				return null;
			}
		}

		public static void Server()
		{
			string password;
			bool closed = false;
			while (!closed)
			{
				IPEndPoint ie = new IPEndPoint(IPAddress.Any, 31416);
				using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
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
						DateTime today = DateTime.Now;
						string msg = "";
						try
						{
							msg = sr.ReadLine();
							if (msg != null)
							{
								switch (msg)
								{
									case "time":
										sw.WriteLine(today.ToString("HH:mm"));
										sw.Flush();
										break;
									case "date":
										sw.WriteLine(today.ToString("dd/MM/yyyy"));
										sw.Flush();
										break;
									case "all":
										sw.WriteLine(today.ToString("dd/MM/yyyy - HH:mm"));
										sw.Flush();
										break;
									case "close":
										password = Password();
										string userPass = sr.ReadLine();
										if (userPass != "" && password != null)
										{
											if (userPass == password)
											{
												sw.WriteLine("Contraseña válida. Cerrando servidor.");
												sw.Flush();
												sw.WriteLine("close");
												sw.Flush();
												closed = true;
											}
											else
											{
												sw.WriteLine("ERROR04");
											}
										}
										else
										{
											sw.WriteLine("ERROR03");
										}
										break;
									default:
										sw.WriteLine("ERR0R02");
										break;
								}
								//Console.WriteLine(msg);
								//sw.Flush();
							}
						}
						catch (IOException e)
						{
							msg = null;
							sw.WriteLine("ERR0R01");
						}
						Console.WriteLine("\nClient disconnected.\nConnection closed");
					}
					sClient.Close(); // Este no se abre con using, pues lo devuelve el accept.
				}
			}
		}

		static void Main(string[] args)
		{
			Server();
		}
	}
}