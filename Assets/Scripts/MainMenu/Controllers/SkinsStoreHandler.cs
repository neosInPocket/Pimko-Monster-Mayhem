using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinsStoreHandler : MonoBehaviour
{
	[SerializeField] private StoreHandler storeHandler;
	[SerializeField] private Button leftArrow;
	[SerializeField] private Button rightArrow;
	[SerializeField] private TMP_Text buttonCaption;
	[SerializeField] private MeshRenderer ringRenderer;
	[SerializeField] private RingMaterials ringMaterials;
	[SerializeField] private Button skinButton;
	[SerializeField] private TMP_Text skinName;
	[SerializeField] private TMP_Text pointsLeft;
	private int currentSkinIndex;
	private SkinStatus currentSkinStatus;

	private void Start()
	{
		Refresh();
		currentSkinIndex = PlayerPreferences.PlayerData.currentSkinIndex;
		LoadCurrentSkin();
	}

	private void Refresh()
	{
		pointsLeft.text = $"POINTS LEFT: <color=\"yellow\">{PlayerPreferences.PlayerData.levelPoints}</color>";
	}

	public void GetNextSkin(int value)
	{
		currentSkinIndex += value;
		LoadCurrentSkin();
	}

	private void LoadCurrentSkin()
	{
		rightArrow.interactable = true;
		leftArrow.interactable = true;

		if (currentSkinIndex == 5)
		{
			rightArrow.interactable = false;
		}

		if (currentSkinIndex == 0)
		{
			leftArrow.interactable = false;
		}

		ringRenderer.material = ringMaterials.Materials[currentSkinIndex];
		skinName.text = ringMaterials.MaterialNames[currentSkinIndex];

		if (PlayerPreferences.PlayerData.skinsBought[currentSkinIndex])
		{
			if (PlayerPreferences.PlayerData.currentSkinIndex == currentSkinIndex)
			{
				skinButton.interactable = false;
				buttonCaption.text = "CHOOSED";
				currentSkinStatus = SkinStatus.Purchased;
				return;
			}
			else
			{
				skinButton.interactable = true;
				buttonCaption.text = "CHOOSE";
				currentSkinStatus = SkinStatus.Purchased;
				return;
			}
		}
		else
		{
			if (PlayerPreferences.PlayerData.levelPoints > 0)
			{
				skinButton.interactable = true;
				buttonCaption.text = "PURCHASE";
				currentSkinStatus = SkinStatus.AvaliableToBuy;
			}
			else
			{
				skinButton.interactable = false;
				buttonCaption.text = "NOT ENOUGH POINTS";
				currentSkinStatus = SkinStatus.NotEnoughMoney;
			}
		}
	}

	public void BuySkin()
	{
		if (currentSkinStatus == SkinStatus.AvaliableToBuy)
		{
			PlayerPreferences.PlayerData.skinsBought[currentSkinIndex] = true;
			PlayerPreferences.PlayerData.currentSkinIndex = currentSkinIndex;
			PlayerPreferences.PlayerData.levelPoints--;
			PlayerPreferences.SaveData();
			LoadCurrentSkin();
			Refresh();
			return;
		}

		if (currentSkinStatus == SkinStatus.Purchased)
		{
			PlayerPreferences.PlayerData.currentSkinIndex = currentSkinIndex;
			PlayerPreferences.SaveData();
			LoadCurrentSkin();
			Refresh();
			return;
		}
	}
}

public enum SkinStatus
{
	Purchased,
	NotEnoughMoney,
	AvaliableToBuy
}

