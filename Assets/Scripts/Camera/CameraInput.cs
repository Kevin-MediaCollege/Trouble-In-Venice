using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Controls the camera input
	/// </summary>
	public class CameraInput
	{
		public float deltaZoom;
		public float moveX;
		public float moveY;

		private float prevX;
		private float prevY;
		private float dist;

		private int touchMode;
		private bool isMoving;

		/// <summary>
		/// Creates the camera input class
		/// </summary>
		public CameraInput()
		{
			deltaZoom = 0f;
			moveX = 0f;
			moveY = 0f;

			prevX = 0f;
			prevY = 0f;
			dist = 0f;

			isMoving = false;
			touchMode = 0;
		}

		/// <summary>
		/// Updates the camera input (windows, web and android)
		/// </summary>
		public void UpdateInput()
		{
			deltaZoom = 0f;
			moveX = 0f;
			moveY = 0f;

			if(Application.isMobilePlatform)
			{
				Touch[] touches = Input.touches;
				if(touches.Length > 1)
				{
					if(touches[0].phase == TouchPhase.Began || touches[1].phase == TouchPhase.Began)
					{
						dist = MathHelper.Dist2(touches[0].position.x, touches[0].position.y, touches[1].position.x, touches[1].position.y);
						touchMode = 2;
					}
					else if(touches[0].phase == TouchPhase.Ended || touches[1].phase == TouchPhase.Ended || touches[0].phase == TouchPhase.Canceled || touches[1].phase == TouchPhase.Canceled)
					{
						touchMode = 0;
					}
					else if(touches[0].phase != TouchPhase.Began && touches[1].phase != TouchPhase.Began && touchMode == 2)
					{
						float currentDist = MathHelper.Dist2(touches[0].position.x, touches[0].position.y, touches[1].position.x, touches[1].position.y);
						deltaZoom = (currentDist - dist);
						dist = currentDist;
					}
				}
				else if(touches.Length == 1)
				{
					if(touches[0].phase == TouchPhase.Began)
					{
						if(!IsTouchingPlayer(new Vector2(touches[0].position.x, touches[0].position.y)))
						{
							prevX = (touches[0].position.x / Screen.width) * 1920f;
							prevY = (touches[0].position.y / Screen.height) * 1080f;
							touchMode = 1;
						}
						else
						{
							touchMode = 0;
						}
					}
					else if(touches[0].phase == TouchPhase.Moved && touchMode == 1)
					{
						float currentX = (touches[0].position.x / Screen.width) * 1920f;
						float currentY = (touches[0].position.y / Screen.height) * 1080f;
						moveX = currentX - prevX;
						moveY = currentY - prevY;
						prevX = currentX;
						prevY = currentY;
					}
					else if(touches[0].phase == TouchPhase.Ended || touches[0].phase == TouchPhase.Canceled)
					{
						touchMode = 0;
					}
				}
				else
				{
					touchMode = 0;
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

		/// <summary>
		/// Checks if this screen position hits player
		/// </summary>
		/// <param name="_pos">Position on screen</param>
		/// <returns></returns>
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
}
