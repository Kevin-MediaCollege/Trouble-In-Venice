using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	private Vector2 GetSwipeTarget()
	{
		Vector2 current = Camera.main.WorldToScreenPoint(MathHelper.XYtoXZ(nodeTracker.CurrentNode.Position));
		float swipeRotation = MathHelper.PointToRotation (swipeHandle.StartPosition, swipeHandle.LastPosition);

		int x, y, i;
		float closest = 80f;
		Vector2 closestNode = Vector2.zero;

		for(i = 0; i < 4; i++)
		{
			x = i == 1 ? 1 : i == 3 ? -1 : 0;
			y = i == 0 ? 1 : i == 2 ? -1 : 0;

			Vector2 neighbour = Camera.main.WorldToScreenPoint(MathHelper.XYtoXZ(nodeTracker.CurrentNode.Position + new Vector2(x, y)));
			float diff = GetRotationDifference (swipeRotation, MathHelper.PointToRotation (current, neighbour));
			
			if(Mathf.Abs(diff) < Mathf.Abs(closest) && GridUtils.GetNodeAt(nodeTracker.CurrentNode.GridPosition + new Vector2(x, y)) != null)
			{
				closest = diff;
				closestNode = new Vector2 (x, y);
			}
		}

		return closestNode;
	}

	private float GetRotationDifference(float _a, float _b)
	{
		_b += _b > _a + 180f ? -360f : _b < _a - 180f ? 360f : 0f;
		return _a - _b;
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
			Vector2 node = GetSwipeTarget();
			//movement.Move(nodeTracker.CurrentNode.GridPosition + GetSwipeTarget().GridPosition);
			swipeHandle = null;
		}
	}
}