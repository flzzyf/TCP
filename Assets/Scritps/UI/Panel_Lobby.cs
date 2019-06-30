using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Lobby : UI
{
	[Header("开启服务器")]
	public Button button_StartNewServer;

	[Header("加入服务器")]
	public InputField inputField_IP;
	public Button button_JoinServer;

	void Start()
	{
		button_StartNewServer.onClick.AddListener(StartNewServer);
		button_JoinServer.onClick.AddListener(JoinServer);
	}

	void StartNewServer()
	{
		ServerManager.instance.StartServer();
	}

	void JoinServer()
	{
		string ip = inputField_IP.text;

		if (ip == "")
			ip = "127.0.0.1";

		print(ip);

		ServerManager.instance.JoinServer(ip);
	}
}
