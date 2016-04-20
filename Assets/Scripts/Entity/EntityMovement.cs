using UnityEngine;
using System.Collections;

public enum Direction
{
	Up,
	Left,
	Down,
	Right
}

public class EntityMovement : MonoBehaviour
{
	[SerializeField] private EntityNodeTracker nodeTracker;
	[SerializeField] private Direction direction;

	protected void Reset()
	{
		nodeTracker = GetComponent<EntityNodeTracker>();
	}

	protected void OnDrawGizmos()
	{
		Vector3 gizmoDirection = Vector3.zero;

		switch(direction)
		{
		case Direction.Up:
			gizmoDirection = Vector3.forward;
			break;
		case Direction.Left:
			gizmoDirection = Vector3.left;
			break;
		case Direction.Down:
			gizmoDirection = Vector3.back;
			break;
		case Direction.Right:
			gizmoDirection = Vector3.right;
			break;
		}

		Gizmos.color = Color.green;
		GizmosUtils.DrawArrowXZ(transform.position + Vector3.up, gizmoDirection / 2, 0.3f, 0.5f);
	}

	public void Move(Direction direction)
	{
		LookAt(direction);

		GridNode targetNode = GetTargetNode(direction);

		if(targetNode != null)
		{
			Vector3 targetPosition = new Vector3(targetNode.transform.position.x, transform.position.y, targetNode.transform.position.z);

			transform.position = targetPosition;
			nodeTracker.CurrentNode = targetNode;
		}
	}

	public void LookAt(Direction direction)
	{
		this.direction = direction;
	}

	public Direction? GetDirectionTo(GridNode node)
	{
		if(!nodeTracker.CurrentNode.IsNeighbour(node))
		{
			return null;
		}

		Vector3 direction = (node.transform.position - nodeTracker.CurrentNode.transform.position);
		Vector3 directionN = direction.normalized;
		float distance2 = direction.sqrMagnitude;

		if(distance2 != 9f)
		{
			return null;
		}

		if(directionN == Vector3.forward)
		{
			return Direction.Up;
		}
		else if(directionN == Vector3.left)
		{
			return Direction.Left;
		}
		else if(directionN == Vector3.back)
		{
			return Direction.Down;
		}
		else if(directionN == Vector3.right)
		{
			return Direction.Right;
		}

		Debug.LogError("Invalid direction: " + directionN);
		return null;
	}

	private GridNode GetTargetNode(Direction direction)
	{
		switch(direction)
		{
		case Direction.Up:
			return nodeTracker.CurrentNode.NeighbourUp;
		case Direction.Left:
			return nodeTracker.CurrentNode.NeighbourLeft;
		case Direction.Down:
			return nodeTracker.CurrentNode.NeighbourDown;
		case Direction.Right:
			return nodeTracker.CurrentNode.NeighbourRight;
		default:
			return null;
		}
	}
}