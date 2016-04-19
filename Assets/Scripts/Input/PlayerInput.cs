using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private LayerMask gridNodeLayer;
	[SerializeField] private EntityController controller;

#if UNITY_ANDROID
	private Vector2 swipeDirection;
	private Vector2 startPosition;
#endif

	protected void Update()
	{
#if UNITY_ANDROID
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			
			switch(touch.phase)
			{
			case TouchPhase.Began:
				startPosition = touch.position;
				swipeDirection = Vector2.zero;
				break;
			case TouchPhase.Moved:
				swipeDirection = touch.position - startPosition;
				break;
			case TouchPhase.Ended:
				HandleSwipe(swipeDirection);
				break;
			}
		}
#else
		if(Input.GetMouseButtonDown(0))
		{
			HandleTap(Input.mousePosition);
		}
		
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			controller.Move(Direction.Up);
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			controller.Move(Direction.Left);
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			controller.Move(Direction.Down);
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			controller.Move(Direction.Right);
		}
#endif
	}

	private void HandleTap(Vector2 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;

		Debug.DrawRay(ray.origin, ray.direction * 100, Color.gray, 2);

		if(Physics.Raycast(ray, out hit, 100, gridNodeLayer))
		{
			GridNode node = hit.collider.GetComponent<GridNode>();

			if(node != null)
			{
				Direction? direction = controller.GetDirectionTo(node);

				if(direction != null)
				{
					controller.Move(direction.Value);
				}
			}
		}
	}

	private void HandleSwipe(Vector2 direction)
	{
		// Swipe horizontal
		if(Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
		{
			// Swipe right
			if(direction.x > 0)
			{
				controller.Move(Direction.Right);
			}
			// Swipe left
			else if(direction.x < 0)
			{
				controller.Move(Direction.Left);
			}
		}
		// Swipe vertical
		else if(Mathf.Abs(direction.y) >= Mathf.Abs(direction.x))
		{
			// Swipe up
			if(direction.y > 0)
			{
				controller.Move(Direction.Up);
			}
			// Swipe down
			else if(direction.y < 0)
			{
				controller.Move(Direction.Down);
			}
		}
	}
}