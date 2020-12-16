using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Lobby : MonoBehaviour {
	public Button button_StartNewServer;

	public Transform roomButtonParent;
	public GameObject roomButtonPrefab;

	public Text text;

	Dictionary<string, Room> serverDic = new Dictionary<string, Room>();

	void Start() {
		button_StartNewServer.onClick.AddListener(Button_StartNewServer);

		text.text = "本机IP: " + GameManager.GetLocalIPv4();
	}

    private void OnEnable() {
		//刷新列表
		ClearRoomButtons();

		serverDic.Clear();

		BroadcastManager.instance.StartReceiving(msg => {
			OnReceiveMsg(msg);
		});
	}

    void Button_StartNewServer() {
		string ip = GameManager.GetLocalIPv4();
		//开始广播IP地址
		BroadcastManager.instance.StartBroadcast(ip);

		//开启服务器
		EchoServer.instance.StartServer();

		//进入房间界面
		EnterRoom();
		Echo.instance.Connect(ip);
	}

	//当收到消息
	void OnReceiveMsg(string msg) {
        if (serverDic.ContainsKey(msg)) {
			return;
        }

		serverDic.Add(msg, new Room());

		UpdateRoomList();
	}

	//更新房间列表
	void UpdateRoomList() {
		ClearRoomButtons();

        foreach (var item in serverDic) {
			AddRoomButton(item.Key);
		}
	}

	//清除房间按钮
	void ClearRoomButtons() {
        for (int i = 0; i < roomButtonParent.childCount; i++) {
			Destroy(roomButtonParent.GetChild(i).gameObject);
        }
    }

	//添加房间按钮
	void AddRoomButton(string ip) {
		GameObject button = Instantiate(roomButtonPrefab, roomButtonParent);
		button.GetComponent<Button>().onClick.AddListener(() => {
			//进入房间界面
			EnterRoom(ip);
		});

		button.GetComponentInChildren<Text>().text = ip;
	}

	//进入房间界面
	void EnterRoom(string ip = "") {
		if(ip != "") {
			Echo.instance.Connect(ip);
		}

		//切换界面
		GameManager.instance.panel_Chat.gameObject.SetActive(true);
		gameObject.SetActive(false);

		//停止接收消息
		BroadcastManager.instance.StopReceiving();
	}
}
