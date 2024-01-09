using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
	[SerializeField] private TMP_Text text;
	[SerializeField] private PopupTextHandler popupTextHandler;

	public void Popup(string value)
	{
		text.gameObject.SetActive(false);
		text.text = value;
		text.gameObject.SetActive(true);
	}

	public void PopupWithAction(Action action, string value)
	{
		popupTextHandler.EndAction = action;
		Popup(value);
	}

	public void CountDown(Action action)
	{
		text.gameObject.SetActive(true);
		StartCoroutine(CountDownAction(action));
	}

	private IEnumerator CountDownAction(Action action)
	{
		text.text = "3";
		yield return new WaitForSeconds(2f / 3f);
		text.gameObject.SetActive(false);
		text.gameObject.SetActive(true);

		text.text = "2";
		yield return new WaitForSeconds(2f / 3f);
		text.gameObject.SetActive(false);
		text.gameObject.SetActive(true);

		text.text = "1";
		yield return new WaitForSeconds(2f / 3f);
		text.gameObject.SetActive(false);
		text.gameObject.SetActive(true);

		text.text = "PLAY!";
		yield return new WaitForSeconds(2f / 3f);

		action();
		text.gameObject.SetActive(false);
	}
}
