using System;
using UnityEngine;

public class PopupTextHandler : MonoBehaviour
{
	public Action EndAction { get; set; }

	public void OnEndAction()
	{
		if (EndAction != null)
		{
			EndAction();
			EndAction = null;
		}

		gameObject.SetActive(false);
	}
}
