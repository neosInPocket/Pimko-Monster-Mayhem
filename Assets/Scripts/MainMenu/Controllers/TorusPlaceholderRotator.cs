using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusPlaceholderRotator : MonoBehaviour
{
	[SerializeField] private float rotationSpeed;

	private void FixedUpdate()
	{
		var angles = transform.eulerAngles;
		angles.y += rotationSpeed;

		transform.rotation = Quaternion.Euler(angles);
	}
}
