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
		if (PlayerPreferences.PlayerData.currentExp >= 100)
		{
			var leftExp = PlayerPreferences.PlayerData.currentExp - MaxLevelExp;

			PlayerPreferences.PlayerData.currentExp = leftExp;
			PlayerPreferences.PlayerData.level++;
			PlayerPreferences.PlayerData.levelPoints++;
			PlayerPreferences.PlayerData.force = PlayerPreferences.PlayerData.level * 100 + PlayerPreferences.PlayerData.currentExp;
			PlayerPreferences.SaveData();
			InvokeObservers(true);
			return;
		}

		InvokeObservers(false);
	}

	private void InvokeObservers(bool value)
	{
		foreach (var observer in observers)
		{
			observer.OnNext(value);
		}
	}
}
