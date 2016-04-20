using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private EntityNodeTracker nodeTracker;
	[SerializeField] private EntityMovement movement;

#if UNITY_ANDROID && !UNITY_EDITOR
	private Vector2 swipeDirection;
	private Vector2 startPosition;

	private bool moving;

	// DEBUG
	private Renderer nodeRenderer;
	// DEBUG
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

			if(touch.phase == TouchPhase.Began)
			{
				if(GridUtils.GetNodeFromScreenPosition(touch.position) == nodeTracker.CurrentNode)
				{
					// DEBUG
					nodeTracker.CurrentNode.GetComponentInChildren<Renderer>().material.color = Color.green;
					// DEBUG

					startPosition = touch.position;
					swipeDirection = Vector2.zero;
					moving = true;
				}
			}
			else
			{
				if(moving)
				{
					if(touch.phase == TouchPhase.Moved)
					{
						swipeDirection = touch.position - startPosition;

						// DEBUG						
						if(nodeRenderer != null)
						{
							nodeRenderer.material.color = Color.white;
						}

						GridNode node = nodeTracker.GetNodeInDirection(GetSwipeDirection(swipeDirection));
						if(node != null)
						{
							nodeRenderer = node.GetComponentInChildren<Renderer>();
							nodeRenderer.material.color = Color.red;
						}
						// DEBUG
					}
					else if(touch.phase == TouchPhase.Ended)
					{
						// DEBUG
						nodeTracker.CurrentNode.GetComponentInChildren<Renderer>().material.color = Color.white;

						if(nodeRenderer != null)
						{
							nodeRenderer.material.color = Color.white;
							nodeRenderer = null;
						}						
						// DEBUG

						HandleSwipe(swipeDirection);
						moving = false;
					}
				}
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
			Direction? direction = nodeTracker.GetDirectionTo(node);

			if(direction != null)
			{
				movement.Move(direction.Value);
			}
		}
	}

	private Direction GetSwipeDirection(Vector2 direction)
	{
		// Swipe horizontal
		if(Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
		{
			// Swipe right
			if(direction.x > 0)
			{
				return Direction.Right;
			}
			// Swipe left
			else if(direction.x < 0)
			{
				return Direction.Left;
			}
		}
		// Swipe vertical
		else if(Mathf.Abs(direction.y) >= Mathf.Abs(direction.x))
		{
			// Swipe up
			if(direction.y > 0)
			{
				return Direction.Up;
			}
			// Swipe down
			else if(direction.y < 0)
			{
				return Direction.Down;
			}
		}

		// Should never happen
		Debug.LogError("Invalid swipe direction");
		return Direction.Up;
	}

	private void HandleSwipe(Vector2 direction)
	{
		Direction swipeDirection = GetSwipeDirection(direction);
		movement.Move(swipeDirection);
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