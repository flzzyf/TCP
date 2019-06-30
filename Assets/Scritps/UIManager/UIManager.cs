using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	public UIWithName[] uis;

	Stack<UI> uiStack;

	public UI GetUI(string name)
	{
		for (int i = 0; i < uis.Length; i++)
		{
			if (uis[i].name == name)
				return uis[i].ui;
		}

		Debug.LogError("莫得UI");
		return null;
	}


}

[System.Serializable]
public class UIWithName
{
	public string name;
	public UI ui;
}
