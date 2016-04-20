using UnityEngine;

public class EntityNodeTracker : MonoBehaviour
{
	public GridNode CurrentNode
	{
		set
		{
			currentNode = value;
		}
		get
		{
			return currentNode;
		}
	}

	[SerializeField, HideInInspector] private GridNode currentNode;

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

	public GridNode GetNodeInDirection(Direction direction)
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