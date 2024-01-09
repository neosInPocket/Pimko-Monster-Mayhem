using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;
using System.Linq;
using System;
using System.Collections;

public class RIng : MonoBehaviour
{
	[SerializeField] private Vector3 spawnPosition;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float forceMagnitude;
	[SerializeField] private float maxMagnitude;
	[SerializeField] private float minMagnitude;
	[SerializeField] private float yForce;
	[SerializeField] private Vector3 tossAngularVelocity;
	[SerializeField] private float yThresholdPosition;
	[SerializeField] private GameObject effect;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private RingMaterials ringMaterials;

	public bool Enabled
	{
		get => isEnabled;
		set
		{
			isEnabled = value;

			if (value)
			{
				Touch.onFingerDown += OnTouch;
				Touch.onFingerUp += FingerUp;
			}
			else
			{
				Touch.onFingerDown -= OnTouch;
				Touch.onFingerUp -= FingerUp;
			}
		}
	}

	private bool isEnabled;
	private bool isCollided;
	private bool isShooted;
	private Vector3 startTouchPosition;
	public Action<bool> IsHit;
	private Vector2 screenSize;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		SetScale();
		ReturnToPosition();
		audioSource.volume = PlayerPreferences.PlayerData.sfx;
		meshRenderer.material = ringMaterials.Materials[PlayerPreferences.PlayerData.currentSkinIndex];
		screenSize = ScreenSizeInfo.ScreenSize;
	}

	public void SetScale()
	{
		switch (PlayerPreferences.PlayerData.sizeUpgrade)
		{
			case 0:
				transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				break;

			case 1:
				transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
				break;

			case 2:
				transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
				break;

			case 3:
				transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
				break;
		}
	}

	private void Update()
	{
		if (transform.position.x > 4 * screenSize.x / 3 || transform.position.x < -4 * screenSize.x / 3)
		{
			IsHit?.Invoke(false);
			ReturnToPosition();
			isCollided = false;
			return;
		}

		if (!isCollided) return;

		if (rb.velocity.magnitude > 0)
		{
			return;
		}
		else
		{
			isCollided = false;
			CheckPoint();
		}
	}

	private void OnTouch(Finger finger)
	{
		if (isShooted) return;
		startTouchPosition = ScreenSizeInfo.ScreenToWorld(finger.screenPosition);
	}

	private void FingerUp(Finger finger)
	{
		var upFingerPosition = ScreenSizeInfo.ScreenToWorld(finger.screenPosition);
		var result = upFingerPosition - startTouchPosition;
		if (result.y < 0) return;

		isShooted = true;
		AddRingForce(result);
	}

	private void AddRingForce(Vector2 vector)
	{
		rb.useGravity = true;
		Vector3 rotated = new Vector3(vector.x, yForce, vector.y);
		rb.angularVelocity = tossAngularVelocity;

		var forceVector = forceMagnitude * rotated;

		rb.AddForce(forceVector, ForceMode.Impulse);
	}

	public void CheckPoint()
	{
		var raycast = Physics.SphereCastAll(transform.position, 20f, Vector3.down);
		var tossable = raycast.FirstOrDefault(x => x.collider.GetComponent<Tossable>() != null);

		if (tossable.collider != null)
		{
			if (tossable.collider.TryGetComponent<Tossable>(out Tossable tossableGO))
			{
				var circleEq =
				Mathf.Pow(transform.position.x - tossableGO.transform.position.x, 2) +
				Mathf.Pow(transform.position.z - tossableGO.transform.position.z, 2);

				if (circleEq < tossableGO.Radius && (transform.position.y < yThresholdPosition && transform.position.y > 0))
				{
					IsHit?.Invoke(true);
					tossableGO.ChangePosition();
					StartCoroutine(PlayEffect());
					ReturnToPosition();

					return;
				}
			}
		}

		IsHit?.Invoke(false);
		ReturnToPosition();
	}

	private IEnumerator PlayEffect()
	{
		effect.SetActive(true);
		effect.transform.position = transform.position;

		yield return new WaitForSeconds(1);
		effect.SetActive(false);
	}

	private void OnCollisionEnter()
	{
		if (isCollided) return;

		isCollided = true;
	}

	private void ReturnToPosition()
	{
		rb.useGravity = false;
		isShooted = false;
		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;
		transform.position = spawnPosition;
		transform.rotation = Quaternion.identity;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnTouch;
		Touch.onFingerUp -= FingerUp;
	}
}
