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

	private GridNode GetSwipeTarget()
	{
		Vector2 screenPoint1 = Camera.main.WorldToScreenPoint(nodeTracker.CurrentNode.Position);

		Vector2 swipePoint1 = swipeHandle.StartPosition;
		Vector2 swipePoint2 = swipeHandle.LastPosition;
		
		foreach(GridNode connection in nodeTracker.CurrentNode.Connections)
		{
			Vector2 screenPoint2 = Camera.main.WorldToScreenPoint(connection.Position);
			

		}

		return null;
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
			GetSwipeTarget();
			//movement.Move(nodeTracker.CurrentNode.GridPosition + GetSwipeTarget().GridPosition);
			swipeHandle = null;
		}
	}
}