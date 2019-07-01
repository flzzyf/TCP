using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCP;
using System.Net.NetworkInformation;

public class ServerManager : Singleton<ServerManager>
{
	Server server;
	Client client;

	const int port = 12345;

	void Start()
    {
		server = GetComponent<Server>();
		client = GetComponent<Client>();
	}

    public void StartServer()
	{
		server.StartServer(port);
	}

	public void JoinServer(string ip)
	{
		client.ConnectToServer(ip, port);
	}

	public void SendMsg()
	{
		//string msg = inputField_Msg.text;

		//client.SendMsg(msg);
	}

}
