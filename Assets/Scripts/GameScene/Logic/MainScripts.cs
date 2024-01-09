using UnityEditor.Rendering;
using UnityEngine;

public class MainScripts : MonoBehaviour
{
	[SerializeField] private PopupText popupText;
	[SerializeField] private RIng ring;
	[SerializeField] private int scoreToAdd;
	private int rowCount;
	private int currentScore;

	private void Start()
	{
		popupText.CountDown(OnCountDownEnd);
		ring.IsHit += OnRingAction;
		rowCount = 0;
	}

	private void OnCountDownEnd()
	{
		ring.Enabled = true;
	}

	private void OnRingAction(bool isHit)
	{
		if (isHit)
		{
			currentScore += scoreToAdd;
			rowCount++;

			if (currentScore >= MaxLevelScore())
			{
				currentScore = MaxLevelScore();
				ring.Enabled = false;
				popupText.Popup("WIN!");
				return;
			}

			if (rowCount <= 1)
			{
				popupText.Popup("HIT!");
			}
			else
			{
				popupText.Popup($"{rowCount} IN A ROW!");
			}

		}
		else
		{
			popupText.Popup("MISS..");
			rowCount = 0;
		}
	}

	private void OnDestroy()
	{
		ring.IsHit -= OnRingAction;
	}

	public int MaxLevelScore()
	{
		return (int)(10 * Mathf.Log(Mathf.Sqrt(PlayerPreferences.PlayerData.currentLevel) + 1));
	}

	public int Reward()
	{
		return (int)(10 * Mathf.Log(Mathf.Pow(PlayerPreferences.PlayerData.currentLevel, 2) + 1) + 14);
	}
}
