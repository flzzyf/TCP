using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class GameManager : Singleton<GameManager> {
    public Panel_Lobby panel_Lobby;
    public Panel_Chat panel_Chat;

	public static string GetLocalIPv4() {
		string hostName = Dns.GetHostName();
		IPHostEntry iPEntry = Dns.GetHostEntry(hostName);
		for (int i = 0; i < iPEntry.AddressList.Length; i++) {
			//从IP地址列表中筛选出IPv4类型的IP地址
			if (iPEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
				return iPEntry.AddressList[i].ToString();
		}
		return null;
	}
}
