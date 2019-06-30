using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

namespace TCP
{
	public class Client : MonoBehaviour
	{
		TcpClient socket;

		//各种读取流
		NetworkStream stream;
		StreamWriter writer;
		StreamReader reader;

		//已连接服务器
		bool isConnected;

		void Update()
		{
			//读取信息
			if(isConnected)
			{
				if(stream.DataAvailable)
				{
					string data = reader.ReadLine();

					OnReceivedMsg(data);
				}

				if(Input.GetKeyDown("d"))
				{
					Output("按下d");

					SendMsg("d");
				}
			}
		}

		//收到信息
		void OnReceivedMsg(string data)
		{
			Output("收到信息:" + data);
		}

		//连接到服务器
		public void ConnectToServer(string ip, int port)
		{

			if (isConnected)
				return;

			try
			{
				socket = new TcpClient(ip, port);
				stream = socket.GetStream();
				writer = new StreamWriter(stream);
				reader = new StreamReader(stream);

				isConnected = true;
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		//发送消息
		public void SendMsg(string msg)
		{
			writer.WriteLine(msg);
			writer.Flush();
		}

		void Output(string s)
		{
			GameManager.instance.Output("客户端：" + s);
		}
	}
}
