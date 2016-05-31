using UnityEngine;

namespace Proeve
{
	/// <summary>
	/// Component to make an object face the camera.
	/// </summary>
	public class FaceTowardsCamera : MonoBehaviour
	{
		[SerializeField] private bool constrainY;
		
		protected void LateUpdate()
		{
			Vector3 cameraPosition = Camera.main.transform.position;
			Vector3 targetPostition = new Vector3(cameraPosition.x, constrainY ? transform.position.y : cameraPosition.y, cameraPosition.z);

			transform.LookAt(targetPostition);
		}
	}
}
