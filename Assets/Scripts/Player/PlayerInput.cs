using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private EntityNodeTracker nodeTracker;
	[SerializeField] private EntityMovement movement;
	
	// DEBUG
	private Renderer nodeRenderer;
	// DEBUG

	private SwipeHandle swipeHandle;
	private bool canMove;	

	protected void OnEnable()
	{
		GlobalEvents.AddListener<PickupStartEvent>(OnPickupStartEvent);
		GlobalEvents.AddListener<PickupStopEvent>(OnPickupStopEvent);

		GlobalEvents.AddListener<SwipeBeganEvent>(OnSwipeBeganEvent);
		GlobalEvents.AddListener<SwipeUpdateEvent>(OnSwipeUpdateEvent);
		GlobalEvents.AddListener<SwipeEndedEvent>(OnSwipeEndedEvent);

		canMove = true;
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<PickupStartEvent>(OnPickupStartEvent);
		GlobalEvents.RemoveListener<PickupStopEvent>(OnPickupStopEvent);

		GlobalEvents.RemoveListener<SwipeBeganEvent>(OnSwipeBeganEvent);
		GlobalEvents.RemoveListener<SwipeUpdateEvent>(OnSwipeUpdateEvent);
		GlobalEvents.RemoveListener<SwipeEndedEvent>(OnSwipeEndedEvent);
	}

	private void OnSwipeBeganEvent(SwipeBeganEvent evt)
	{
		if(canMove && GridUtils.GetNodeFromScreenPosition(evt.Handle.StartPosition) == nodeTracker.CurrentNode)
		{
			swipeHandle = evt.Handle;
			swipeHandle.IsConsumed = true;

			// DEBUG
			nodeTracker.CurrentNode.GetComponentInChildren<Renderer>().material.color = Color.green;
			// DEBUG
		}
	}

	private void OnSwipeUpdateEvent(SwipeUpdateEvent evt)
	{
		if(evt.Handle == swipeHandle)
		{
			// DEBUG						
			if(nodeRenderer != null)
			{
				nodeRenderer.material.color = Color.white;
			}

			GridNode node = nodeTracker.GetNodeInDirection(GetSwipeDirection(swipeHandle.StartPosition - swipeHandle.LastPosition));
			if(node != null)
			{
				nodeRenderer = node.GetComponentInChildren<Renderer>();
				nodeRenderer.material.color = Color.red;
			}
			// DEBUG
		}
	}

	private void OnSwipeEndedEvent(SwipeEndedEvent evt)
	{
		if(evt.Handle == swipeHandle)
		{
			// DEBUG
			nodeTracker.CurrentNode.GetComponentInChildren<Renderer>().material.color = Color.white;

			if(nodeRenderer != null)
			{
				nodeRenderer.material.color = Color.white;
				nodeRenderer = null;
			}
			// DEBUG

			HandleSwipe(swipeHandle.StartPosition - swipeHandle.LastPosition);
			swipeHandle = null;
		}
	}

	protected void Update()
	{
		if(!canMove)
		{
			return;
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