using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckTutorial : MonoBehaviour
{
	public void PlayGame()
	{
		if (PlayerPreferences.PlayerData.firstTime)
		{
			PlayerPreferences.PlayerData.firstTime = false;
			PlayerPreferences.SaveData();

			SceneManager.LoadScene("TutorialScene");
			return;
		}
		else
		{
			SceneManager.LoadScene("GameScene");
			return;
		}
	}
}
