
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider sfxSlider;
	private MusicHandler musicHandler;

	private void Start()
	{
		musicHandler = GameObject.FindObjectOfType<MusicHandler>();
		if (musicHandler == null)
		{
			Debug.LogError("Music handler is null");
		}

		musicSlider.value = PlayerPreferences.PlayerData.volume;
		sfxSlider.value = PlayerPreferences.PlayerData.sfx;
	}

	public void ChangeMusicVolume(float value)
	{
		musicHandler.ChangeMusicVolume(value);
	}

	public void ChangeSFXVolume(float value)
	{
		musicHandler.ChangeSFXVolume(value);
	}
}
