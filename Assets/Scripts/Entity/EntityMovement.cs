using UnityEngine;
using System.Collections;

public enum Direction
{
	Up,
	Left,
	Down,
	Right
}

[RequireComponent(typeof(EntityNodeTracker))]
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

		GridNode targetNode = nodeTracker.GetNodeInDirection(direction);

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
}