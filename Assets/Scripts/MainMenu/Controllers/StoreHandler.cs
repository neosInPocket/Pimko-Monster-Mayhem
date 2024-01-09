using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreHandler : MonoBehaviour
{
	[SerializeField] private TMP_Text energyText;
	[SerializeField] private TMP_Text ticketsText;
	[SerializeField] private Button sizeButton;
	[SerializeField] private Button timeButton;
	[SerializeField] private Image[] sizePoints;
	[SerializeField] private Image[] timePoints;
	[SerializeField] private TMP_Text energyAmount;
	[SerializeField] private TMP_Text ticketsAmount;
	[SerializeField] private Button upgradesButton;
	[SerializeField] private Button skinsButton;
	[SerializeField] private GameObject upgradesContainer;
	[SerializeField] private GameObject skinsContainer;
	[SerializeField] private string upgradesCharacterText;
	[SerializeField] private string skinsCharacterText;
	[SerializeField] private TMP_Text characterText;

	private void Start()
	{
		Refresh();
		SelectSkins(false);
	}

	public void Refresh()
	{
		energyText.text = PlayerPreferences.PlayerData.energy.ToString();
		ticketsText.text = PlayerPreferences.PlayerData.tickets.ToString();

		sizeButton.interactable = PlayerPreferences.PlayerData.sizeUpgrade < 3 && PlayerPreferences.PlayerData.energy >= 20;
		timeButton.interactable = PlayerPreferences.PlayerData.timeUpgrade < 3 && PlayerPreferences.PlayerData.tickets >= 1;

		UnlockPoints(sizePoints, PlayerPreferences.PlayerData.sizeUpgrade);
		UnlockPoints(timePoints, PlayerPreferences.PlayerData.timeUpgrade);

		energyAmount.text = PlayerPreferences.PlayerData.energy.ToString();
		ticketsAmount.text = PlayerPreferences.PlayerData.tickets.ToString();
	}

	private void UnlockPoints(Image[] points, int value)
	{
		foreach (var point in points)
		{
			point.enabled = false;
		}

		for (int i = 0; i < value; i++)
		{
			points[i].enabled = true;
		}
	}

	public void PurchaseEnergyUpgrade()
	{
		PlayerPreferences.PlayerData.sizeUpgrade++;
		PlayerPreferences.PlayerData.energy -= 20;
		PlayerPreferences.SaveData();

		Refresh();
	}

	public void PurchaseTimeUpgrade()
	{
		PlayerPreferences.PlayerData.timeUpgrade++;
		PlayerPreferences.PlayerData.tickets -= 1;
		PlayerPreferences.SaveData();

		Refresh();
	}

	public void SelectSkins(bool value)
	{
		skinsContainer.SetActive(value);
		upgradesContainer.SetActive(!value);

		skinsButton.interactable = !value;
		upgradesButton.interactable = value;

		characterText.text = value ? skinsCharacterText : upgradesCharacterText;
	}
}
