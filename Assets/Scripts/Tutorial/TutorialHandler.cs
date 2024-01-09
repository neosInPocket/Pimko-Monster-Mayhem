using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{
	[SerializeField] private TMP_Text characterText;
	[SerializeField] private PopupText popupText;
	[SerializeField] private RIng ring;
	[SerializeField] private int scoreToAdd;
	[SerializeField] private GameTimer gameTimer;
	[SerializeField] private string[] phrases;
	[SerializeField] private GameObject tutorialObject;
	private int rowCount;
	private int currentScore;
	private int currentPhrase;
	private bool isCompleted;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += OnFingerDown;
		ring.IsHit += OnRingAction;
		gameTimer.OnTimerEnd += OnTimerEnd;
		rowCount = 0;
	}

	private void OnFingerDown(Finger finger)
	{
		if (currentPhrase >= phrases.Length)
		{
			LoadGameScene();
		}

		if (currentPhrase == 5 && !isCompleted)
		{
			isCompleted = true;
			Touch.onFingerDown -= OnFingerDown;
			ProceedToGame();
		}

		characterText.text = phrases[currentPhrase];
		currentPhrase++;
	}

	private void ProceedToGame()
	{
		tutorialObject.SetActive(false);
		popupText.CountDown(OnCountDownEnd);
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
				ring.IsHit -= OnRingAction;
				gameTimer.OnTimerEnd -= OnTimerEnd;

				currentScore = MaxLevelScore();
				ring.Enabled = false;

				gameTimer.RefreshProgress(1f);

				tutorialObject.SetActive(true);
				Touch.onFingerDown += OnFingerDown;
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
	}

	private void OnDestroy()
	{
		ring.IsHit -= OnRingAction;
		Touch.onFingerDown -= OnFingerDown;
	}

	public int MaxLevelScore()
	{
		return (int)(10 * Mathf.Log(Mathf.Sqrt(PlayerPreferences.PlayerData.currentLevel) + 1));
	}

	public int LevelTime()
	{
		return (int)(10 * Mathf.Log(Mathf.Pow(PlayerPreferences.PlayerData.currentLevel, 2) + 1) + 30 + PlayerPreferences.PlayerData.timeUpgrade * 5);
	}

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}
}
