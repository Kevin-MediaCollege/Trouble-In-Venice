using UnityEngine;
using System.Collections;

public class CameraInput 
{
	public float deltaZoom;
	public float moveX;
	public float moveY;
	public string debugString;

	private float prevX;
	private float prevY;
	private float dist;

	public CameraInput()
	{
		deltaZoom = 0f;
		moveX = 0f;
		moveY = 0f;

		prevX = 0f;
		prevY = 0f;
		dist = 0f;
	}

	public void UpdateInput()
	{
		deltaZoom = 0f;
		moveX = 0f;
		moveY = 0f;

		Touch[] touches = Input.touches;
		if (touches.Length > 1)
		{
			if (touches [0].phase == TouchPhase.Began || touches [1].phase == TouchPhase.Began) 
			{
				dist = MathHelper.dis2 (touches [0].position.x, touches [0].position.y, touches [1].position.x, touches [1].position.y);
			}
			else if (touches [0].phase != TouchPhase.Began && touches [1].phase != TouchPhase.Began) 
			{
				float currentDist = MathHelper.dis2 (touches [0].position.x, touches [0].position.y, touches [1].position.x, touches [1].position.y);
				deltaZoom = (currentDist - dist);
				debugString = "Phase=Zoom Change=" + Mathf.FloorToInt(deltaZoom) + " Dist=" + Mathf.FloorToInt(currentDist);
				dist = currentDist;
			}
		} 
		else if (touches.Length == 1) 
		{
			if (touches [0].phase == TouchPhase.Began)
			{
				prevX = (touches[0].position.x / Screen.width) * 1920f;
				prevY = (touches[0].position.y / Screen.height) * 1080f;
			}
			else if(touches [0].phase == TouchPhase.Moved)
			{
				float currentX = (touches[0].position.x / Screen.width) * 1920f;
				float currentY = (touches[0].position.y / Screen.height) * 1080f;
				moveX = currentX - prevX;
				moveY = currentY - prevY;
				debugString = "Phase=Move X=" + Mathf.FloorToInt(currentX) + " Y=" + Mathf.FloorToInt(currentY) + " MX=" + Mathf.FloorToInt(moveX) + " MY=" + Mathf.FloorToInt(moveY);
				prevX = currentX;
				prevY = currentY;
			}
		}
		else 
		{
			debugString = "Phase=Null";
		}
	}
}
	
#if UNITY_ANDROID
#else
#endif