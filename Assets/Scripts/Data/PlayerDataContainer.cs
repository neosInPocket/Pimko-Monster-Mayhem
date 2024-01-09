using System;

[Serializable]
public class PlayerDataContainer
{
	public int force = 100;
	public int energy = 10;
	public int tickets = 1;
	public bool firstTime = true;

	public int level = 1;
	public int levelPoints = 0;
	public int currentExp = 290;
	public int currentLevel = 1;
	public int sizeUpgrade = 0;
	public int timeUpgrade = 0;
	public float volume = 1f;
	public float sfx = 1f;
	public bool[] skinsBought = { true, false, false, false, false, false };
	public int currentSkinIndex = 0;
}
