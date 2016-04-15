using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridNodeCollider))]
public class GridNode : MonoBehaviour
{
	public Vector3 Position
	{
		get
		{
			return new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z)) / Grid.SIZE;
		}
	}

	public GridNodeType Type
	{
		get
		{
			return type;
		}
	}

	public IEnumerable<GridNode> Neighbours
	{
		get
		{
			return neighbours;
		}
	}

	public GridNode NeighbourUp
	{
		get
		{
			return GetNeighbour(Vector3.forward);
		}
	}

	public GridNode NeighbourLeft
	{
		get
		{
			return GetNeighbour(Vector3.left);
		}
	}

	public GridNode NeighbourDown
	{
		get
		{
			return GetNeighbour(Vector3.back);
		}
	}

	public GridNode NeighbourRight
	{
		get
		{
			return GetNeighbour(Vector3.right);
		}
	}

	public bool IsStart
	{
		get
		{
			return Type == GridNodeType.Start;
		}
	}

	public bool IsEnd
	{
		get
		{
			return Type == GridNodeType.End;
		}
	}

	[SerializeField] private GridNodeType type;
	[SerializeField] private List<GridNode> neighbours;
	
	protected void Start()
	{
		Grid grid = GetComponentInParent<Grid>();
		grid.AddNode(this);

		Instantiate(Resources.Load("test"), transform.position, Quaternion.identity);
	}

	protected void OnDrawGizmos()
	{
		if(!enabled)
		{
			return;
		}

		Gizmos.color = type == GridNodeType.Normal ? Color.gray : type == GridNodeType.Start ? Color.green : Color.red;
		Gizmos.DrawCube(transform.position, new Vector3(1, 0.25f, 1));

		foreach(GridNode neighbour in neighbours)
		{
			if(neighbour != null && neighbour.enabled)
			{
				Vector3 distance = (neighbour.transform.position - transform.position);
				Vector3 direction = distance.normalized;

				bool isValid = distance.sqrMagnitude == 9;

				Gizmos.color = isValid ? Color.blue : Color.red;
				Gizmos.DrawLine(transform.position, neighbour.transform.position);

				if(isValid)
				{
					GizmosUtils.DrawArrowXZ(transform.position + (distance * 0.2f), direction / 2, 0.3f, 0.3f);
				}
			}
		}
	}

	public void AddNeighbour(GridNode node)
	{
		if(!neighbours.Contains(node))
		{
			neighbours.Add(node);
		}
	}

	public void RemoveNeighbour(GridNode node)
	{
		if(neighbours.Contains(node))
		{
			neighbours.Remove(node);
		}
	}

	public bool IsNeighbour(GridNode node)
	{
		foreach(GridNode neighbour in neighbours)
		{
			if(neighbour == node)
			{
				return true;
			}
		}

		return false;
	}

	private GridNode GetNeighbour(Vector3 direction)
	{
		foreach(GridNode neighbour in Neighbours)
		{
			if(neighbour != null)
			{
				if(neighbour.Position == (Position + direction))
				{
					if(neighbour.enabled)
					{
						return neighbour;
					}
				}
			}
		}

		return null;
	}
}