using System;
using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
	[SerializeField] private Material timerMaterial;
	[SerializeField] private Material progressMaterial;
	[SerializeField] private string dissolveSymbols;
	[SerializeField] private float progressSpeed;

	public Action OnTimerEnd;

	private void Start()
	{
		timerMaterial.SetFloat(dissolveSymbols, 1f);
		progressMaterial.SetFloat(dissolveSymbols, 0f);
	}

	public void RefreshProgress(float value)
	{
		StopCoroutine(Progress(value));
		StartCoroutine(Progress(value));
	}

	public void StartTimer(float time)
	{
		StartCoroutine(TimerRoutine(time));
	}

	public void StopTimer()
	{
		StopAllCoroutines();
	}

	private IEnumerator Progress(float value)
	{
		var currentState = progressMaterial.GetFloat(dissolveSymbols);
		var distance = value - currentState;

		while (currentState < value)
		{
			currentState += progressSpeed * Time.deltaTime * distance;
			progressMaterial.SetFloat(dissolveSymbols, currentState);
			yield return null;
		}

		progressMaterial.SetFloat(dissolveSymbols, value);
	}

	private IEnumerator TimerRoutine(float time)
	{
		float currentTime = time;
		while (currentTime > 0)
		{
			currentTime -= Time.deltaTime;
			timerMaterial.SetFloat(dissolveSymbols, currentTime / time);
			yield return null;
		}

		timerMaterial.SetFloat(dissolveSymbols, 0);
		OnTimerEnd?.Invoke();
	}
}
