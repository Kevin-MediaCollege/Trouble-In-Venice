using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private EntityNodeTracker nodeTracker;
	[SerializeField] private EntityMovement movement;

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
		if(canMove && GridUtils.GetNodeAtGUI(evt.Handle.StartPosition) == nodeTracker.CurrentNode)
		{
			swipeHandle = evt.Handle;
			swipeHandle.IsConsumed = true;
		}
	}

	private void OnSwipeUpdateEvent(SwipeUpdateEvent evt)
	{
		if(evt.Handle == swipeHandle)
		{
			
		}
	}

	private void OnSwipeEndedEvent(SwipeEndedEvent evt)
	{
		if(evt.Handle == swipeHandle)
		{
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
			movement.Move(Vector2.up);
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			movement.Move(Vector2.left);
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			movement.Move(Vector2.down);
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			movement.Move(Vector2.right);
		}
	}

	protected void Reset()
	{
		movement = GetComponent<EntityMovement>();
	}

	private void HandleTap(Vector2 position)
	{
		
	}
	
	private void HandleSwipe(Vector2 direction)
	{
		
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