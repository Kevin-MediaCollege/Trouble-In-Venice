using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private EntityNodeTracker nodeTracker;
	[SerializeField] private EntityMovement movement;

	private Dictionary<SpriteRenderer, Color> originalNodeColors;
	private SpriteRenderer selected;
	private Color selectedOrigColor;

	private SwipeHandle swipeHandle;

	protected void Awake()
	{
		originalNodeColors = new Dictionary<SpriteRenderer, Color>();
	}

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

	protected void Update()
	{
		//===================== ARROW TEST MOVEMENT ==========================
		float arrowRotion = Input.GetKeyDown (KeyCode.A) ? 180f : Input.GetKeyDown (KeyCode.W) ? 90f : Input.GetKeyDown (KeyCode.D) ? 1f : Input.GetKeyDown (KeyCode.S) ? -90f : 0f;
		if (arrowRotion != 0f) 
		{
			Vector2 current = Camera.main.WorldToScreenPoint(MathHelper.XYtoXZ(nodeTracker.CurrentNode.PositionXZ));
			float swipeRotation = arrowRotion;

			int x, y, i;
			float closest = 80f;
			Vector2 closestNode = Vector2.zero;

			for(i = 0; i < 4; i++)
			{
				x = i == 1 ? 1 : i == 3 ? -1 : 0;
				y = i == 0 ? 1 : i == 2 ? -1 : 0;

				Vector2 neighbour = Camera.main.WorldToScreenPoint(MathHelper.XYtoXZ(nodeTracker.CurrentNode.PositionXZ + new Vector2(x, y)));
				float diff = GetRotationDifference (swipeRotation, MathHelper.PointToRotation (current, neighbour));

				if(Mathf.Abs(diff) < Mathf.Abs(closest) && GridUtils.GetNodeAt(nodeTracker.CurrentNode.GridPosition + new Vector2(x, y)) != null)
				{
					closest = diff;
					closestNode = new Vector2 (x, y);
				}
			}

			if(closestNode != Vector2.zero)
			{
				foreach(KeyValuePair<SpriteRenderer, Color> kvp in originalNodeColors)
				{
					kvp.Key.color = kvp.Value;
				}

				originalNodeColors.Clear();

				movement.Move(closestNode);

				foreach(GridNode node in nodeTracker.CurrentNode.Connections)
				{
					SpriteRenderer sr = node.GetComponent<SpriteRenderer>();
					originalNodeColors.Add(sr, sr.color);

					sr.color = Color.blue;
				}
			}
		}
		//====================================================================
	}

	private Vector2 GetSwipeDirection()
	{
		Vector2 current = Camera.main.WorldToScreenPoint(MathHelper.XYtoXZ(nodeTracker.CurrentNode.PositionXZ));
		float swipeRotation = MathHelper.PointToRotation (swipeHandle.StartPosition, swipeHandle.LastPosition);

		int x, y, i;
		float closest = 80f;
		Vector2 closestNode = Vector2.zero;

		for(i = 0; i < 4; i++)
		{
			x = i == 1 ? 1 : i == 3 ? -1 : 0;
			y = i == 0 ? 1 : i == 2 ? -1 : 0;

			Vector2 neighbour = Camera.main.WorldToScreenPoint(MathHelper.XYtoXZ(nodeTracker.CurrentNode.PositionXZ + new Vector2(x, y)));
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

			foreach(KeyValuePair<SpriteRenderer, Color> kvp in originalNodeColors)
			{
				kvp.Key.color = kvp.Value;
			}

			originalNodeColors.Clear();

			foreach(GridNode node in nodeTracker.CurrentNode.Connections)
			{
				SpriteRenderer sr = node.GetComponent<SpriteRenderer>();
				originalNodeColors.Add(sr, sr.color);

				sr.color = Color.blue;
			}
		}
	}

	private void OnSwipeUpdateEvent(SwipeUpdateEvent _evt)
	{
		if(_evt.Handle == swipeHandle)
		{
			GridNode node = GridUtils.GetNodeAt(nodeTracker.CurrentNode.GridPosition + GetSwipeDirection());
			
			if(node != null && nodeTracker.CurrentNode.HasConnection(node))
			{
				SpriteRenderer sr = node.GetComponent<SpriteRenderer>();
				
				if(selected != sr)
				{
					if(selected != null)
					{
						selected.color = selectedOrigColor;
					}

					selected = sr;
					selectedOrigColor = selected.color;
					selected.color = Color.yellow;
				}
			}
			else
			{
				if(selected != null)
				{
					selected.color = selectedOrigColor;
				}
			}
		}
	}

	private void OnSwipeEndedEvent(SwipeEndedEvent _evt)
	{
		if(_evt.Handle == swipeHandle)
		{
			foreach(KeyValuePair<SpriteRenderer, Color> kvp in originalNodeColors)
			{
				kvp.Key.color = kvp.Value;
			}

			originalNodeColors.Clear();

			movement.Move(GetSwipeDirection());
			swipeHandle = null;
			selected = null;
		}
	}
}