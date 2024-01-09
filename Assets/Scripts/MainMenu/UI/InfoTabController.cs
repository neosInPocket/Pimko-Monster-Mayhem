using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoTabController : EventObserver
{
	[SerializeField] private TMP_Text forceText;
	[SerializeField] private TMP_Text expText;
	[SerializeField] private TMP_Text levelText;
	[SerializeField] private TMP_Text energyText;
	[SerializeField] private TMP_Text ticketText;
	[SerializeField] private Image expFill;

	public void Refresh()
	{
		forceText.text = PlayerPreferences.PlayerData.force.ToString();
		expText.text = $"<color=\"green\">{100 - PlayerPreferences.PlayerData.currentExp}</color> TO NEXT LEVEL";
		levelText.text = PlayerPreferences.PlayerData.level.ToString();
		energyText.text = PlayerPreferences.PlayerData.energy.ToString();
		ticketText.text = PlayerPreferences.PlayerData.tickets.ToString();

		expFill.fillAmount = (float)PlayerPreferences.PlayerData.currentExp / 100f;
	}

	public override void OnNext(bool value)
	{
		Refresh();
	}
}
