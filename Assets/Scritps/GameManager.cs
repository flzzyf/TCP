using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	public Text text;

	private void Start()
	{
		text.text = "";
	}

	public void Output(string msg)
	{
		text.text += msg;
		text.text += "\n";

		//print(msg);
	}
}
