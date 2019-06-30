using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

namespace TCP
{
	//服务器上的用户
	public class ServerClient
	{
		public TcpClient tcp;
		public string name;

		public ServerClient(TcpClient tcp)
		{
			name = "Guest";
			this.tcp = tcp;
		}
	}

}
