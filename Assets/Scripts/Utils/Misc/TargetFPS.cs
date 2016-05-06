using UnityEngine;
using System.Collections;

namespace Utils
{
	/// <summary>
	/// 
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
