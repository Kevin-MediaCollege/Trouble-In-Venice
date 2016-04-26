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

		if(Application.isMobilePlatform)
		{
			Touch[] touches = Input.touches;
			if (touches.Length > 1)
			{
				if (touches [0].phase == TouchPhase.Began || touches [1].phase == TouchPhase.Began) 
				{
					dist = MathHelper.Dist2 (touches [0].position.x, touches [0].position.y, touches [1].position.x, touches [1].position.y);
					isMoving = true;
				}
				else if (touches [0].phase != TouchPhase.Began && touches [1].phase != TouchPhase.Began && isMoving) 
				{
					float currentDist = MathHelper.Dist2 (touches [0].position.x, touches [0].position.y, touches [1].position.x, touches [1].position.y);
					deltaZoom = (currentDist - dist);
					dist = currentDist;
				}
			} 
			else if (touches.Length == 1) 
			{
				if (touches [0].phase == TouchPhase.Began)
				{
					if (!IsTouchingPlayer (new Vector2 (touches [0].position.x, touches [0].position.y)))
					{
						prevX = (touches [0].position.x / Screen.width) * 1920f;
						prevY = (touches [0].position.y / Screen.height) * 1080f;
						isMoving = true;
					} 
					else
					{
						isMoving = false;
					}
				}
				else if(touches [0].phase == TouchPhase.Moved && isMoving)
				{
					float currentX = (touches[0].position.x / Screen.width) * 1920f;
					float currentY = (touches[0].position.y / Screen.height) * 1080f;
					moveX = currentX - prevX;
					moveY = currentY - prevY;
					prevX = currentX;
					prevY = currentY;
				}
				else if(touches [0].phase == TouchPhase.Ended || touches [0].phase == TouchPhase.Canceled)
				{
					isMoving = false;
				}
			}
		}
		else
		{
			deltaZoom = Input.mouseScrollDelta.y * 20f;
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(!IsTouchingPlayer(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
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
			else if(Input.GetKeyUp(KeyCode.Mouse0))
			{
				isMoving = false;
			}
		}
	}

	public bool IsTouchingPlayer(Vector2 _pos)
	{
		Entity e = EntityUtils.GetEntityWithTag("Player");
		if(e != null && GridUtils.GetNodeAtGUI(_pos) == e.GetComponent<EntityNodeTracker>().CurrentNode)
		{
			return true;
		}

		return false;
	}
}