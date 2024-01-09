using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GameResultScreen : MonoBehaviour
{
	[SerializeField] private TMP_Text result;
	[SerializeField] private TMP_Text nextLevelButton;
	[SerializeField] private TMP_Text energy;
	[SerializeField] private TMP_Text tickets;
	[SerializeField] private TMP_Text exp;

	public void Show(bool result, int energy = 0, int tickets = 0, int exp = 0)
	{
		if (result)
		{
			this.result.text = "WIN!";
			nextLevelButton.text = "NEXT LEVEL";
		}
		else
		{
			this.result.text = "LOSE..";
			nextLevelButton.text = "TRY AGAIN";
		}

		this.energy.text = energy.ToString();
		this.tickets.text = tickets.ToString();
		this.exp.text = exp.ToString();
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
