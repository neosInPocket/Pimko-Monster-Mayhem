using System.IO;
using System.Text;
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
			using (FileStream fstream = File.OpenRead(saveFilePath))
			{
				byte[] buffer = new byte[fstream.Length];
				fstream.Read(buffer, 0, buffer.Length);

				string textFromFile = Encoding.Default.GetString(buffer);
				PlayerData = JsonUtility.FromJson<PlayerDataContainer>(textFromFile);
			}
		}
	}

	private static void CreateNewSaveFile()
	{
		using (FileStream fs = File.Create(saveFilePath))
		{
			PlayerData = new PlayerDataContainer();
			var textToWrite = JsonUtility.ToJson(PlayerData);
			byte[] buffer = Encoding.Default.GetBytes(textToWrite);

			fs.Write(buffer);
		}
	}

	private static void WriteDataFile()
	{
		using (FileStream fs = File.OpenWrite(saveFilePath))
		{
			var textToWrite = JsonUtility.ToJson(PlayerData, prettyPrint: true);
			byte[] buffer = Encoding.Default.GetBytes(textToWrite);

			fs.Write(buffer);
		}
	}
}
