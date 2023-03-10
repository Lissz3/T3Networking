using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Ex3Server
{
	internal class Program
	{
		static Dictionary<string, Socket> clients = new Dictionary<string, Socket>();
		static readonly private object l = new object();

		static void ClientThread(object socket)
		{
			string mensaje;
			Socket cliente = (Socket)socket;
			IPEndPoint ieCliente = (IPEndPoint)cliente.RemoteEndPoint;
			Console.WriteLine("Connected with client {0} at port {1}", ieCliente.Address, ieCliente.Port);
			using (NetworkStream ns = new NetworkStream(cliente))
			using (StreamReader sr = new StreamReader(ns))
			using (StreamWriter sw = new StreamWriter(ns))
			{
				bool disconnected = false;
				string welcome = "Welcome to the ChatRoom. Enter your name:".Replace("\n", Environment.NewLine);
				sw.WriteLine(welcome);
				sw.Flush();
				string name = sr.ReadLine();
				if (name != null)
				{
					lock (l)
					{
						clients.Add(name, cliente);
						SendMsg("Connected", name, cliente);
					}

					while (!disconnected)
					{
						try
						{
							mensaje = sr.ReadLine();
							lock (l)
							{
								if (mensaje != null)
								{
									if (mensaje == "#exit") //contains
									{
										throw new IOException();
									}
									else if (mensaje == "#lista") //contains
									{
										sw.WriteLine("Usuarios conectados:");
										sw.Flush();
										foreach (string client in clients.Keys)
										{
											sw.WriteLine("Nombre: " + client);
											sw.Flush();
										}
									}
									else
									{
										SendMsg(mensaje, name, cliente);
									}
								}
								else
								{
									throw new IOException();
								}
							}
						}
						catch (IOException)
						{
							disconnected = true;
							lock (l)
							{
								clients.Remove(name);
								SendMsg("Disconnected", name, cliente);
							}
						}
					}
				}
			}
			cliente.Close();
		}


		public static void SendMsg(string msg, string name, Socket client)
		{
			foreach (Socket c in clients.Values)
			{
				if (c != client)
				{
					IPEndPoint ieCliente = (IPEndPoint)client.RemoteEndPoint;
					using (NetworkStream clientns = new NetworkStream(c))
					using (StreamWriter clientsw = new StreamWriter(clientns))
					{
						clientsw.WriteLine("{0}@{1}:{2}", ieCliente.Address, name, msg);
						clientsw.Flush();
					}

				}
			}
		}

		public static void Server()
		{
			bool portUsed = true;
			int port = 31416;
			using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{

				IPEndPoint ie = new IPEndPoint(IPAddress.Any, port); //comprobacion puerto ocupado
				do
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
				Console.WriteLine("Server waiting at port {0}", ie.Port);
				while (true)
				{
					Socket client = s.Accept();
					Thread t = new Thread(ClientThread);
					t.Start(client);
				}
			}

		}
		static void Main(string[] args)
		{
			Server();
		}
	}
}