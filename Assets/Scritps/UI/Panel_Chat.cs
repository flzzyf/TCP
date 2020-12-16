using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;
using System.Net.Sockets;

public class Client {
    public string username;
    public Socket socket;
}

public class Room {
    public List<Client> clientList = new List<Client>();
    public Client hoster;
}

public class Panel_Chat : Singleton<Panel_Chat> {
	public Button button_Send;
	public Button button_Exit;
    public InputField inputField;

    public Text text;

    public Room room;

    string username;
    Color textColor;

    private void Awake() {
        button_Send.onClick.AddListener(Button_Send);
        button_Exit.onClick.AddListener(Button_Exit);
    }

    private void Start() {
        text.text = "";
        username = GameManager.GetLocalIPv4();
        textColor = Random.ColorHSV();

        room = new Room();
    }

    private void OnEnable() {
        text.text = "";
    }

    void Button_Send() {
        string msg = inputField.text;

        if(msg == "") {
            msg = ((UnityEngine.UI.Text)inputField.placeholder).text;
        }

        Echo.instance.Send(msg);

        //if(msg != "")
        //    SendText(msg, username);
    }

    void Button_Exit() {
        GameManager.instance.panel_Lobby.gameObject.SetActive(true);
        gameObject.SetActive(false);

        BroadcastManager.instance.StopBroadcasting();
        EchoServer.instance.StopServer();
    }

    public void SendText(string msg, string user = "") {
        if(text.text != "") {
            text.text += "\n";
        }

        if(user != "")
            text.text += string.Format("<color=#{0}>{1}：</color>{2}", ColorUtility.ToHtmlStringRGBA(textColor), username, msg);
        else
            text.text += msg;
    }
}
