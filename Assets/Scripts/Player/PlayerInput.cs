using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private EntityMovement movement;

#if UNITY_ANDROID && !UNITY_EDITOR
	private Vector2 swipeDirection;
	private Vector2 startPosition;
#endif

	private bool canMove;

	protected void OnEnable()
	{
		GlobalEvents.AddListener<PickupStartEvent>(OnPickupStartEvent);
		GlobalEvents.AddListener<PickupStopEvent>(OnPickupStopEvent);

		canMove = true;
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<PickupStartEvent>(OnPickupStartEvent);
		GlobalEvents.RemoveListener<PickupStopEvent>(OnPickupStopEvent);
	}

	protected void Update()
	{
		if(!canMove)
		{
			return;
		}

#if UNITY_ANDROID && !UNITY_EDITOR
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
			movement.Move(Direction.Up);
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			movement.Move(Direction.Left);
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			movement.Move(Direction.Down);
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			movement.Move(Direction.Right);
		}
#endif
	}

	protected void Reset()
	{
		movement = GetComponent<EntityMovement>();
	}

	private void HandleTap(Vector2 position)
	{
		GridNode node = GridUtils.GetNodeFromScreenPosition(position);

		if(node != null)
		{
			Direction? direction = movement.GetDirectionTo(node);

			if(direction != null)
			{
				movement.Move(direction.Value);
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
				movement.Move(Direction.Right);
			}
			// Swipe left
			else if(direction.x < 0)
			{
				movement.Move(Direction.Left);
			}
		}
		// Swipe vertical
		else if(Mathf.Abs(direction.y) >= Mathf.Abs(direction.x))
		{
			// Swipe up
			if(direction.y > 0)
			{
				movement.Move(Direction.Up);
			}
			// Swipe down
			else if(direction.y < 0)
			{
				movement.Move(Direction.Down);
			}
		}
	}

	private void OnPickupStartEvent(PickupStartEvent evt)
	{
		canMove = false;
	}

	private void OnPickupStopEvent(PickupStopEvent evt)
	{
		canMove = true;
	}
}