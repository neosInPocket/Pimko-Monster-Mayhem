using System.IO;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
	[SerializeField] private bool clearPreferences;
	private static string saveFilePath => Application.persistentDataPath + "/PlayerPreferences.json";
	public static PlayerDataContainer PlayerData { get; private set; }

	private void Awake()
	{
		if (clearPreferences)
		{
			PlayerData = new PlayerDataContainer();
			SaveData();
		}
		else
		{
			GetData();
		}
	}

	public static void SaveData()
	{
		if (!File.Exists(saveFilePath))
		{
			CreateNewSaveFile();
		}
		else
		{
			WriteDataFile();
		}
	}

	public static void GetData()
	{
		if (!File.Exists(saveFilePath))
		{
			CreateNewSaveFile();
		}
		else
		{
			string text = File.ReadAllText(saveFilePath);
			PlayerData = JsonUtility.FromJson<PlayerDataContainer>(text);
		}
	}

	private static void CreateNewSaveFile()
	{
		PlayerData = new PlayerDataContainer();
		File.WriteAllText(saveFilePath, JsonUtility.ToJson(PlayerData, prettyPrint: true));
	}

	private static void WriteDataFile()
	{
		File.WriteAllText(saveFilePath, JsonUtility.ToJson(PlayerData, prettyPrint: true));
	}
}
