using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;
using System.Linq;

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
	private Vector3 startTouchPosition;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Enabled = true;
		ReturnToPosition();
	}

	private void Update()
	{
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
		startTouchPosition = ScreenSizeInfo.ScreenToWorld(finger.screenPosition);
	}

	private void FingerUp(Finger finger)
	{
		var upFingerPosition = ScreenSizeInfo.ScreenToWorld(finger.screenPosition);
		var result = upFingerPosition - startTouchPosition;
		if (result.y < 0) return;


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
					Debug.Log("Popped");
					tossableGO.ChangePosition();
				}
			}
		}

		ReturnToPosition();
	}

	private void OnCollisionEnter()
	{
		if (isCollided) return;

		isCollided = true;
	}

	private void ReturnToPosition()
	{
		rb.useGravity = false;
		rb.angularVelocity = Vector3.zero;
		transform.position = spawnPosition;
		transform.rotation = Quaternion.identity;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnTouch;
		Touch.onFingerUp -= FingerUp;
	}
}
