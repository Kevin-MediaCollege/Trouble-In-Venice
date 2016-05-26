using UnityEngine;
using System.Collections;

public class FaceTowardsCamera : MonoBehaviour
{
	protected void LateUpdate()
	{
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 targetPostition = new Vector3(cameraPosition.x, transform.position.y, cameraPosition.z);

		transform.LookAt(targetPostition);
	}
}