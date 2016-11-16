using UnityEngine;
using System.Collections;
// This script makes the game scale itself depending on the resolution of the screen by moving the camera a certain distance.
public class CameraPositioner : MonoBehaviour
{

	#region Public Fields

	public bool moveIfInvisible = false;

	public int iterations = 32;
	public float baseDistance = 15;
	public float distanceMultiplier = 0.75f;

	#endregion

	#region Private Fields

	public Camera camera;

	#endregion

	#region Slots

	void Start()
	{
		camera = Camera.main;
		PositionCamera();
	}

	void Update()
	{
	}

	#endregion

	#region Private Methods

	private void PositionCamera()
	{
		var points = InvisiblePoints();
		if (points.Length == 0)
		{
			return;
		}

		if (!moveIfInvisible && AreVisible(points))
		{
			return;
		}

		float distance = baseDistance;

		for (int i = 0; i < iterations; ++i)
		{
			if (AreVisible(points))
			{
				MoveForward(distance);
			}
			else
			{
				MoveBackward(distance);
			}

			distance *= distanceMultiplier;
		}
	}

	private Vector3[] InvisiblePoints()
	{
		var objects = GameObject.FindGameObjectsWithTag("CameraInvisible");
		var result = new Vector3[objects.Length];
		for (int i = 0; i < objects.Length; ++i)
		{
			result[i] = objects[i].transform.position;
		}

		return result;
	}

	private bool AreVisible(Vector3[] points)
	{
		for (int i = 0; i < points.Length; ++i)
		{
			var screenPoint = camera.WorldToScreenPoint(points[i]);
			if (screenPoint.x >= 0 && screenPoint.x < Screen.width && screenPoint.y > 0 && screenPoint.y < Screen.height)
			{
				return true;
			}
		}

		return false;
	}

	private void MoveForward(float distance)
	{
		Move(distance);
	}

	private void MoveBackward(float distance)
	{
		Move(-distance);
	}

	private void Move(float distance)
	{
		transform.Translate(Vector3.forward * distance, Space.Self);
	}

	#endregion
}