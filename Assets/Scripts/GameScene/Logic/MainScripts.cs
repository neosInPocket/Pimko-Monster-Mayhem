using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScripts : MonoBehaviour
{
	[SerializeField] private PopupText popupText;
	[SerializeField] private RIng ring;
	[SerializeField] private int scoreToAdd;
	[SerializeField] private GameResultScreen gameResultScreen;
	[SerializeField] private GameTimer gameTimer;
	private int rowCount;
	private int currentScore;

	private void Start()
	{
		popupText.CountDown(OnCountDownEnd);
		ring.IsHit += OnRingAction;
		gameTimer.OnTimerEnd += OnTimerEnd;
		rowCount = 0;
	}

	private void OnCountDownEnd()
	{
		ring.Enabled = true;
		gameTimer.StartTimer(LevelTime());
	}

	private void OnRingAction(bool isHit)
	{
		if (isHit)
		{
			currentScore += scoreToAdd;
			rowCount++;

			if (currentScore >= MaxLevelScore())
			{
				ring.IsHit -= OnRingAction;
				gameTimer.OnTimerEnd -= OnTimerEnd;

				currentScore = MaxLevelScore();
				ring.Enabled = false;
				gameResultScreen.Show(true, Reward(), 1, Reward() + 103);
				PlayerPreferences.PlayerData.energy += Reward();
				PlayerPreferences.PlayerData.tickets++;
				PlayerPreferences.PlayerData.currentExp += Reward() + 103;
				PlayerPreferences.PlayerData.currentLevel++;
				PlayerPreferences.SaveData();

				gameTimer.RefreshProgress(1f);
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

			gameTimer.RefreshProgress(currentScore / (float)MaxLevelScore());
		}
		else
		{
			popupText.Popup("MISS..");
			rowCount = 0;
		}
	}

	public void OnTimerEnd()
	{
		ring.IsHit -= OnRingAction;
		gameTimer.OnTimerEnd -= OnTimerEnd;
		gameTimer.StopTimer();
		ring.Enabled = false;
		gameResultScreen.Show(false);
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

	public int LevelTime()
	{
		return (int)(10 * Mathf.Log(Mathf.Pow(PlayerPreferences.PlayerData.currentLevel, 2) + 1) + 30 + PlayerPreferences.PlayerData.timeUpgrade * 5);
	}

	public void NextLevel()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("MainScene");
	}
}
