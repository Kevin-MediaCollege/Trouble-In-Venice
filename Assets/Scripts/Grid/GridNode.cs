using System.Collections.Generic;
using UnityEngine;

public enum GridNodeType
{
	Start,
	End,
	Normal
}

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class GridNode : MonoBehaviour
{
	public Vector2 GridPosition
	{
		get
		{
			return new Vector2(Mathf.Round((transform.position.x - 1.5f) / Grid.SIZE), Mathf.Round((transform.position.z - 1.5f) / Grid.SIZE));
		}
	}

	public GridNodeType Type
	{
		get
		{
			return type;
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
	[SerializeField] private List<GridNode> connections;

	protected void Start()
	{
		if(!Application.isPlaying)
		{
			Grid grid = GetComponentInParent<Grid>();
			grid.AddNode(this);
		}
	}

	protected void OnValidate()
	{
		switch(type)
		{
		case GridNodeType.Start:
			GetComponent<SpriteRenderer>().color = Color.green;
			break;
		case GridNodeType.Normal:
			GetComponent<SpriteRenderer>().color = Color.white;
			break;
		case GridNodeType.End:
			GetComponent<SpriteRenderer>().color = Color.red;
			break;
		}
	}

	protected void OnDestroy()
	{
		if(!Application.isPlaying)
		{
			Grid grid = GetComponentInParent<Grid>();

			if(grid != null)
			{
				grid.RemoveNode(this);
			}
		}
	}

	protected void OnDrawGizmosSelected()
	{
		GetComponentInParent<Grid>().DrawGizmos();
	}

	public void DrawGizmos()
	{
		Gizmos.color = Color.blue;

		foreach(GridNode connection in connections)
		{
			Gizmos.DrawLine(transform.position, connection.transform.position);
		}
	}

#if UNITY_EDITOR
	public void AddConnection(GridNode _node)
	{
		if(!connections.Contains(_node))
		{
			connections.Add(_node);
		}
	}

	public void RemoveAllConnections()
	{
		foreach(GridNode connection in connections)
		{
			connection.RemoveConnection(this);
		}

		connections.Clear();
	}

	public void RemoveConnection(GridNode _node)
	{
		connections.Remove(_node);
	}

	public bool HasConnection(GridNode _node)
	{
		return connections.Contains(_node);
	}
#endif
}