using System;
using UnityEngine;

public class ExpierencePointsController : MonoBehaviour
{
	[SerializeField] private EventObserver[] observers;
	public static int MaxLevelExp => (int)(100 * PlayerPreferences.PlayerData.level);

	private void Start()
	{
		CheckCurrentXP();
	}

	private void CheckCurrentXP()
	{
		if (PlayerPreferences.PlayerData.currentExp >= MaxLevelExp)
		{
			var leftExp = PlayerPreferences.PlayerData.currentExp - MaxLevelExp;

			PlayerPreferences.PlayerData.currentExp = leftExp;
			PlayerPreferences.PlayerData.level++;
			PlayerPreferences.PlayerData.levelPoints++;
			PlayerPreferences.SaveData();
			InvokeObservers();
		}
	}

	private void InvokeObservers()
	{
		foreach (var observer in observers)
		{
			observer.OnNext();
		}
	}
}
