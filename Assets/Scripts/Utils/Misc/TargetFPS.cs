using UnityEngine;

namespace Utils
{
	/// <summary>
	/// Set the target framerate of the application.
	/// </summary>
	public class TargetFPS : MonoBehaviour
	{
		[SerializeField] private int targetFPS;

		protected void Awake()
		{
			Application.targetFrameRate = targetFPS;
		}
	}
}
