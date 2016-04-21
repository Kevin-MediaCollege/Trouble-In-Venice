using UnityEngine;
using System.Collections;

public class CameraInput 
{
	public float deltaZoom;
	public float moveX;
	public float moveY;

	private float prevX;
	private float prevY;
	private float dist;

	private bool isMoving;

	public CameraInput()
	{
		deltaZoom = 0f;
		moveX = 0f;
		moveY = 0f;

		prevX = 0f;
		prevY = 0f;
		dist = 0f;

		isMoving = false;
	}

	public void UpdateInput()
	{
		deltaZoom = 0f;
		moveX = 0f;
		moveY = 0f;

		#if UNITY_ANDROID


			
		#endif

		Touch[] touches = Input.touches;
		if (touches.Length > 1)
		{
			if (touches [0].phase == TouchPhase.Began || touches [1].phase == TouchPhase.Began) 
			{
				if(!isTouchingPlayer(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
				{
					dist = MathHelper.dis2 (touches [0].position.x, touches [0].position.y, touches [1].position.x, touches [1].position.y);
					isMoving = true;
				}
				else
				{
					isMoving = false;
				}
			}
			else if (touches [0].phase != TouchPhase.Began && touches [1].phase != TouchPhase.Began && isMoving) 
			{
				float currentDist = MathHelper.dis2 (touches [0].position.x, touches [0].position.y, touches [1].position.x, touches [1].position.y);
				deltaZoom = (currentDist - dist);
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
				prevX = currentX;
				prevY = currentY;
			}
		}

		deltaZoom = Input.mouseScrollDelta.y * 20f;
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(!isTouchingPlayer(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
			{
				prevX = (Input.mousePosition.x / Screen.width) * 1920f;
				prevY = (Input.mousePosition.y / Screen.height) * 1080f;
				isMoving = true;
			}
			else
			{
				isMoving = false;
			}
		}
		else if(Input.GetKey(KeyCode.Mouse0) && isMoving)
		{
			float currentX = (Input.mousePosition.x / Screen.width) * 1920f;
			float currentY = (Input.mousePosition.y / Screen.height) * 1080f;
			moveX = currentX - prevX;
			moveY = currentY - prevY;
			prevX = currentX;
			prevY = currentY;
		}
	}

	public bool isTouchingPlayer(Vector2 _pos)
	{
		Entity e = EntityUtils.GetEntityWithTag ("Player");
		if(e != null && GridUtils.GetNodeFromScreenPosition(_pos) == e.GetComponent<EntityNodeTracker>().CurrentNode)
		{
			return true;
		}
		return false;
	}
}