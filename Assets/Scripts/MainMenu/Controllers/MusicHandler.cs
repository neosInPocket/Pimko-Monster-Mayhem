using System.Linq;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Start()
	{
		audioSource.volume = PlayerPreferences.PlayerData.volume;

		var controller = GameObject.FindObjectsOfType<MusicHandler>();
		var otherController = controller.FirstOrDefault(x => x != this);

		if (otherController != null)
		{
			Destroy(otherController.gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	public void ChangeMusicVolume(float volume)
	{
		audioSource.volume = volume;
		PlayerPreferences.PlayerData.volume = volume;
		PlayerPreferences.SaveData();
	}

	public void ChangeSFXVolume(float volume)
	{
		PlayerPreferences.PlayerData.sfx = volume;
		PlayerPreferences.SaveData();
	}
}
