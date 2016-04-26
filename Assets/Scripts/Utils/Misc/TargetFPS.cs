using UnityEngine;
using System.Collections;

public class TargetFPS : MonoBehaviour
{
	[SerializeField] private int targetFPS;

	protected void Awake()
	{
		Application.targetFrameRate = targetFPS;
	}
}