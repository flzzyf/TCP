using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System;
using System.IO;

namespace TCP
{
	public class Server : MonoBehaviour
	{
		public int port = 1234;

		List<ServerClient> clients;

		TcpListener server;
		bool serverStarted;

		void Start()
		{
			clients = new List<ServerClient>();
		}

		void Update()
		{
			if (!serverStarted)
				return;

			//判断客户是否还连着
			for (int i = clients.Count - 1; i >= 0; i--)
			{
				ServerClient c = clients[i];

				if (!IsConnected(c.tcp))
				{
					Broadcast(string.Format("{0}已断开连接。", c.name), clients);

					c.tcp.Close();
					clients.Remove(c);
				}
				else
				{
					//否则检查其发送的讯息
					NetworkStream stream = c.tcp.GetStream();
					if (stream.DataAvailable)
					{
						StreamReader reader = new StreamReader(stream, true);

						string data = reader.ReadLine();

						if (data != null)
						{
							OnReceivedMsg(c, data);
						}
					}
				}
			}
		}

		void OnReceivedMsg(ServerClient client, string data)
		{
			Broadcast(string.Format("{0}:{1}", client.name, data), clients);
		}

		//判断客户还连接着
		bool IsConnected(TcpClient client)
		{
			try
				{
				if (client != null && client.Client != null && client.Client.Connected)
				{
					if (client.Client.Poll(0, SelectMode.SelectRead))
					{
						return !(client.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
					}

					return true;
				}
				else
					return false;
			}
			catch
			{
				return false;
			}
		}

		//启动服务器
		public void StartServer(int port)
		{
			try
			{
				//print(GetIP());
				//print(GetIP().MapToIPv4());
				print(IPAddress.Any.MapToIPv4());
				server = new TcpListener(IPAddress.Any, port);
				server.Start();

				serverStarted = true;

				Output("启动服务器");
				StartListening();

			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		//开始监听获取新客户
		void StartListening()
		{
			server.BeginAcceptTcpClient(OnAcceptNewClient, server);
		}

		//当接收到客户 
		private void OnAcceptNewClient(IAsyncResult ar)
		{
			try
			{
				TcpListener listener = (TcpListener)ar.AsyncState;

				ServerClient client = new ServerClient(listener.EndAcceptTcpClient(ar));

				clients.Add(client);

				//继续监听
				StartListening();

				//发送全体信息，新玩家加入
				Broadcast(string.Format("玩家{0}加入。", client.name), clients);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		//广播消息
		void Broadcast(string msg, List<ServerClient> clients)
		{
			//Output("广播：" + msg);

			foreach (var c in clients)
			{
				try
				{
					StreamWriter writer = new StreamWriter(c.tcp.GetStream());

					writer.WriteLine(msg);
					writer.Flush();
				}
				catch(Exception e)
				{
					Debug.LogError(e);
				}
			}
		}

		private IPAddress GetIP()
		{ 
			IPHostEntry iep = Dns.GetHostEntry(Dns.GetHostName());

			IPAddress ip = iep.AddressList[0];
			ip = ip.MapToIPv4();

			return ip;
		}

		void Output(string s)
		{
			GameManager.instance.Output("服务器：" + s);
		}
	}

}

