using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
	public class ServiceClass
	{
		public string Password()
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

		public void Server()
		{
			string password;
			bool closed = false;
			bool portUsed = true;
			string userPass = "";
			int port = 135;
			IPEndPoint ie = new IPEndPoint(IPAddress.Any, port);
			while (!closed)
			{
				//int port = 31416;

				using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
				{
					do  //Comprobación de puerto en uso y suma +1 al puerto para buscar el siguiente 
					{
						try
						{
							ie = new IPEndPoint(IPAddress.Any, port);
							s.Bind(ie);
							portUsed = false;
						}
						catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
						{
							Console.WriteLine("Port {0} already in use, searching for a new one...", port);
							port++;
						}

					} while (portUsed);

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
						DateTime today = DateTime.Now;
						string msg = "";
						try
						{
							msg = sr.ReadLine();
							if (msg.Contains("close"))
							{
								string passMsg = msg;
								string[] passSplit = passMsg.Split("close", StringSplitOptions.None);
								userPass = passSplit[passSplit.Length - 1].Trim();
								msg = "close";
							}
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
		public void Init()
		{
			Server();
		}
	}

}
