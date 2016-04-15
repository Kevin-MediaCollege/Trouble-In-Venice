using UnityEngine;
using System.Collections;

public class EntityController : MonoBehaviour
{
	public GridNode CurrentNode { private set; get; }

	[SerializeField] private Direction direction;
	[SerializeField] private LayerMask gridNodeLayer;

	protected void Start()
	{
		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 5, gridNodeLayer))
		{
			GridNode gridNode = hit.collider.GetComponent<GridNode>();

			if(gridNode != null)
			{
				CurrentNode = gridNode;
				Debug.Log(CurrentNode);
			}
		}
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
		Vector3 targetPosition = new Vector3(targetNode.transform.position.x, transform.position.y, targetNode.transform.position.z);

		transform.position = targetPosition;
		CurrentNode = targetNode;
	}

	public void LookAt(Direction direction)
	{
		this.direction = direction;
	}

	public Direction? GetDirectionTo(GridNode node)
	{
		if(!CurrentNode.IsNeighbour(node))
		{
			return null;
		}

		Vector3 direction = (node.transform.position - CurrentNode.transform.position);
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
			return CurrentNode.NeighbourUp;
		case Direction.Left:
			return CurrentNode.NeighbourLeft;
		case Direction.Down:
			return CurrentNode.NeighbourDown;
		case Direction.Right:
			return CurrentNode.NeighbourRight;
		default:
			return null;
		}
	}
}