using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace ShiftServer
{
	public class ShiftServer
	{
		private List<string> users = new List<string>();
		private List<string> waitRoom = new List<string>();
		private readonly object l = new object();
		private bool closed = false;
		private int pin = 0;
		private Socket s;

		public void ReadNames(string path)
		{
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					users = sr.ReadToEnd().Split(';').ToList();
				}
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine($"The file was not found: '{e}'");
			}
			catch (DirectoryNotFoundException e)
			{
				Console.WriteLine($"The directory was not found: '{e}'");
			}
			catch (UnauthorizedAccessException e)
			{
				Console.WriteLine($"Unauthorized access: '{e}'");
			}
			catch (IOException e)
			{
				Console.WriteLine($"The file could not be opened: '{e}'");
			}
		}

		public int ReadPin(string path)
		{
			try
			{
				using (BinaryReader br = new BinaryReader(new FileStream(path, FileMode.Open)))
				{
					pin = br.ReadInt32();

					if (pin > 9999 || pin < 1000)
					{
						return -1;
					}
					return pin;
				}
			}
			catch (IOException)
			{
				return -1;
			}
		}

		public void Init()
		{
			bool portUsed = true;
			int port = 31416;
			int newPort = 1023;
			IPEndPoint ie = null;
			while (!closed)
			{
				using (s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
				{
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
							Console.WriteLine($"Port {port} already in use, searching for a new one...");
							port = newPort;
							newPort++;
						}
					} while (portUsed);

					s.Listen(10);

					ReadNames(Environment.GetEnvironmentVariable("userprofile") + "\\names.txt");
					Console.WriteLine($"Server listening at port:{ie.Port}");

					try
					{
						while (!closed)
						{
							Socket newClient = s.Accept();
							Thread newThreadClient = new Thread(ClientThread);
							newThreadClient.Start(newClient);
							newThreadClient.IsBackground = true;
						}
					}
					catch (SocketException)
					{
						Console.WriteLine("Server closed.");
						s.Close();
					}

				}
			}
		}

		public void ClientThread(object socket)
		{
			DateTime d = DateTime.Now;
			string[] userMsg;
			Socket client = (Socket)socket;
			IPEndPoint ieClient = (IPEndPoint)client.RemoteEndPoint;
			Console.WriteLine("Connected with client {0} at port {1}", ieClient.Address, ieClient.Port);
			using (NetworkStream ns = new NetworkStream(client))
			using (StreamReader sr = new StreamReader(ns))
			using (StreamWriter sw = new StreamWriter(ns))
			{
				string serverContest = "Welcome to the ShiftServer\nEnter your name:".Replace("\n", Environment.NewLine);
				sw.WriteLine(serverContest);
				sw.Flush();
				string user = sr.ReadLine();
				try
				{
					if (user != null)
					{
						lock (l)
						{
							if (!users.Contains(user) && user != "admin")
							{
								serverContest = "Usuario desconocido.";
								sw.WriteLine(serverContest);
								sw.Flush();
								client.Close();
							}
						}

						bool isAdmin = false;
						if (user == "admin")
						{
							serverContest = "Introduzca el pin:";
							sw.WriteLine(serverContest);
							sw.Flush();
							string readPinFromUser = sr.ReadLine();
							int adminPin;
							int readPin = ReadPin(Environment.GetEnvironmentVariable("userprofile") + "\\pin.bin");
							if (!int.TryParse(readPinFromUser, out adminPin) || adminPin != readPin)
							{
								client.Close();
							}
							isAdmin = true;
						}

						do
						{
							try
							{
								userMsg = sr.ReadLine().Split(" ");
							}
							catch (NullReferenceException)
							{
								throw new IOException();
							}
							lock (l)
							{
								if (userMsg != null)
								{
									switch (userMsg[0])
									{
										case "list":
											string listUsers = "";
											foreach (string u in waitRoom)
											{
												listUsers += u + "-";
											}
											sw.WriteLine(listUsers);
											sw.Flush();
											break;
										case "add":
											string studentAdd = user + " " + d.ToString("MM/dd/yyyy HH:mm");
											waitRoom.Add(studentAdd);
											serverContest = "OK";
											sw.WriteLine(serverContest);
											sw.Flush();
											break;
										case "del" when isAdmin:
											int posToDel;
											if (int.TryParse(userMsg[userMsg.Length - 1], out posToDel))
											{
												waitRoom.RemoveAt(posToDel);
											}
											else
											{
												serverContest = "Delete error";
												sw.WriteLine(serverContest);
												sw.Flush();
											}
											break;
										case "chpin" when isAdmin:
											int pin;
											try
											{
												if (int.TryParse(userMsg[userMsg.Length - 1], out pin) && pin > 999)
												{
													using (BinaryWriter br = new BinaryWriter(new FileStream(Environment.GetEnvironmentVariable("userprofile") + "\\pin.bin", FileMode.Create)))
													{
														br.Write(pin);
													}
												}
												else
												{
													throw new IOException();
												}
											}
											catch (IOException)
											{
												serverContest = "Save error";
												sw.WriteLine(serverContest);
												sw.Flush();
											}
											break;
										case "exit" when isAdmin:
											isAdmin = false;
											client.Close();
											break;
										case "shutdown" when isAdmin:
											isAdmin = false;
											closed = true;
											client.Close();
											s.Close();
											break;

										default:
											throw new IOException();
									}
								}
								else
								{
									throw new IOException();
								}
							}
						} while (isAdmin);
					}
				}
				catch (IOException)
				{
					userMsg = null;
				}
			}
			client.Close();
		}
	}
}
