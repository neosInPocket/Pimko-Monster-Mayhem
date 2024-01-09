using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameResultScreen : MonoBehaviour
{
	private Action onCloseAction;

	public void Show(Action action)
	{
		onCloseAction = action;
	}

	public void Close()
	{
		onCloseAction();
		gameObject.SetActive(false);
	}
}
