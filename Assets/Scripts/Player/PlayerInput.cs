using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private EntityNodeTracker nodeTracker;
	[SerializeField] private EntityMovement movement;

	private SwipeHandle swipeHandle;

	protected void OnEnable()
	{
		GlobalEvents.AddListener<SwipeBeganEvent>(OnSwipeBeganEvent);
		GlobalEvents.AddListener<SwipeUpdateEvent>(OnSwipeUpdateEvent);
		GlobalEvents.AddListener<SwipeEndedEvent>(OnSwipeEndedEvent);
	}

	protected void OnDisable()
	{
		GlobalEvents.RemoveListener<SwipeBeganEvent>(OnSwipeBeganEvent);
		GlobalEvents.RemoveListener<SwipeUpdateEvent>(OnSwipeUpdateEvent);
		GlobalEvents.RemoveListener<SwipeEndedEvent>(OnSwipeEndedEvent);
	}

	protected void Reset()
	{
		movement = GetComponent<EntityMovement>();
	}
	private void HandleTap(Vector2 _position)
	{
		
	}
	
	private void HandleSwipe(Vector2 _direction)
	{

	}

	private void OnSwipeBeganEvent(SwipeBeganEvent _evt)
	{
		if(GridUtils.GetNodeAtGUI(_evt.Handle.StartPosition) == nodeTracker.CurrentNode)
		{
			swipeHandle = _evt.Handle;
			swipeHandle.IsConsumed = true;
		}
	}

	private void OnSwipeUpdateEvent(SwipeUpdateEvent _evt)
	{
		if(_evt.Handle == swipeHandle)
		{
			
		}
	}

	private void OnSwipeEndedEvent(SwipeEndedEvent _evt)
	{
		if(_evt.Handle == swipeHandle)
		{
			HandleSwipe(swipeHandle.StartPosition - swipeHandle.LastPosition);
			swipeHandle = null;
		}
	}
}