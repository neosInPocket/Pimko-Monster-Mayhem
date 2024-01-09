using UnityEngine;

public class Tossable : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private Vector2 zPositions;
	[SerializeField] private float yPosition;
	public float Radius => radius;
	private Vector2 screenSize;

	private void Start()
	{
		screenSize = ScreenSizeInfo.ScreenSize;
		ChangePosition();
	}

	public void ChangePosition()
	{
		var randomX = Random.Range(-screenSize.x * 3 / 4, screenSize.x * 3 / 4);
		var randomZ = Random.Range(zPositions.x, zPositions.y);

		transform.position = new Vector3(randomX, yPosition, randomZ);
	}
}
