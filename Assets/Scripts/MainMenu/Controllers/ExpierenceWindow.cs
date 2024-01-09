using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpierenceWindow : EventObserver
{
	[SerializeField] private TMP_Text leftLevelCaption;
	[SerializeField] private TMP_Text rightLevelCaption;
	[SerializeField] private Image fillImage;
	[SerializeField] private float fillSpeed;

	private void Play()
	{
		gameObject.SetActive(true);
		fillImage.fillAmount = 0;
		leftLevelCaption.text = (PlayerPreferences.PlayerData.level - 1).ToString();
		rightLevelCaption.text = PlayerPreferences.PlayerData.level.ToString();
	}

	public void OnWindowAppeared()
	{
		StartCoroutine(PlaySliderAnimation());
	}

	private IEnumerator PlaySliderAnimation()
	{
		float currentFill = 0;

		while (currentFill < 1f)
		{
			currentFill += fillSpeed * Time.deltaTime;
			fillImage.fillAmount = currentFill;
			yield return null;
		}

		currentFill = 0;
		leftLevelCaption.text = PlayerPreferences.PlayerData.level.ToString();
		rightLevelCaption.text = (PlayerPreferences.PlayerData.level + 1).ToString();

		float target = (float)PlayerPreferences.PlayerData.currentExp / 100f;
		while (currentFill < target)
		{
			currentFill += fillSpeed * Time.deltaTime;
			fillImage.fillAmount = currentFill;
			yield return null;
		}

		fillImage.fillAmount = target;
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public override void OnNext()
	{
		Play();
	}
}
