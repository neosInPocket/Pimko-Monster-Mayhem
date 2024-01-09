using System.Collections;
using UnityEngine;

public class UIDissolveScreen : MonoBehaviour
{
	[SerializeField] private GameObject startScreen;
	[SerializeField] private Vector2 dissolveValueBounds;
	[SerializeField] private string dissolvePropertyName;
	[SerializeField] private string directionPropertyName;
	[SerializeField] private Material dissolveMaterial;
	[SerializeField] private float thresholdDistance;
	[SerializeField] private float dissolveSpeed;
	private GameObject currentScreen;

	private void Start()
	{
		currentScreen = startScreen;
		dissolveMaterial.SetFloat(dissolvePropertyName, dissolveValueBounds.y);
	}

	public void ChangeScreen(GameObject gameObject)
	{
		StopAllCoroutines();
		StartCoroutine(Dissolve(-1, gameObject));
	}

	private IEnumerator Dissolve(int direction, GameObject go)
	{
		var target = direction < 0 ? dissolveValueBounds.x : dissolveValueBounds.y;
		var currentDissolveValue = direction < 0 ? dissolveValueBounds.y : dissolveValueBounds.x;
		var distance = Mathf.Abs(target - currentDissolveValue);
		dissolveMaterial.SetFloat(directionPropertyName, direction);
		dissolveMaterial.SetFloat(dissolvePropertyName, currentDissolveValue);

		while ((direction > 0 && currentDissolveValue < target) || (direction < 0 && currentDissolveValue > target))
		{
			distance = Mathf.Abs(target - currentDissolveValue);
			currentDissolveValue += direction * dissolveSpeed * (distance + thresholdDistance) * Time.deltaTime;
			dissolveMaterial.SetFloat(dissolvePropertyName, currentDissolveValue);

			yield return null;
		}

		dissolveMaterial.SetFloat(dissolvePropertyName, target);

		var negateDirection = direction > 0 ? -1 : 1;

		currentScreen.SetActive(false);
		go.SetActive(true);
		currentScreen = go;
		StartCoroutine(SimpleDissolve(negateDirection));
	}

	private IEnumerator SimpleDissolve(int direction)
	{
		var target = direction < 0 ? dissolveValueBounds.x : dissolveValueBounds.y;
		var currentDissolveValue = direction < 0 ? dissolveValueBounds.y : dissolveValueBounds.x;
		var distance = Mathf.Abs(target - currentDissolveValue);
		dissolveMaterial.SetFloat(directionPropertyName, direction);
		dissolveMaterial.SetFloat(dissolvePropertyName, currentDissolveValue);

		while ((direction > 0 && currentDissolveValue < target) || (direction < 0 && currentDissolveValue > target))
		{
			distance = Mathf.Abs(target - currentDissolveValue);
			currentDissolveValue += direction * dissolveSpeed * (distance + thresholdDistance) * Time.deltaTime;
			dissolveMaterial.SetFloat(dissolvePropertyName, currentDissolveValue);

			yield return null;
		}

		dissolveMaterial.SetFloat(dissolvePropertyName, target);
	}
}
