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
	public delegate void OnEntityEntered(Entity _entity);
	public event OnEntityEntered onEntityEnteredEvent = delegate { };

	public delegate void OnEntityLeft(Entity _entity);
	public event OnEntityLeft onEntityLeftEvent = delegate { };

	public IEnumerable<GridNode> Connections
	{
		get
		{
			return connections;
		}
	}

	public Vector2 GridPosition
	{
		get
		{
			return new Vector2(Mathf.Round((transform.position.x - 1.5f) / Grid.SIZE), Mathf.Round((transform.position.z - 1.5f) / Grid.SIZE));
		}
	}

	public Vector3 Position
	{
		get
		{
			return transform.position;
		}
	}

	public Vector2 PositionXZ
	{
		get
		{
			return new Vector2(transform.position.x, transform.position.z);
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

	public bool Active { set; get; }

	[SerializeField] private GridNodeType type;
	[SerializeField] private List<GridNode> connections;

	private HashSet<Entity> entities;

	protected void Awake()
	{
		entities = new HashSet<Entity>();

		Active = true;
	}

	protected void Start()
	{
		if(!Application.isPlaying)
		{
			Grid grid = GetComponentInParent<Grid>();
			grid.AddNode(this);
		}
	}

#if UNITY_EDITOR
	protected void OnValidate()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		if(type == GridNodeType.Start || type == GridNodeType.Normal)
		{
			LevelCompleter levelCompleter = GetComponent<LevelCompleter>();
			if(levelCompleter != null)
			{
				UnityEditor.EditorApplication.delayCall += () =>
				{
					DestroyImmediate(levelCompleter);
				};
			}

			switch(type)
			{
			case GridNodeType.Start:
				sr.color = Color.green;
				break;
			case GridNodeType.Normal:
				sr.color = Color.white;
				break;
			}
		}
		else if(type == GridNodeType.End)
		{
			LevelCompleter levelCompleter = GetComponent<LevelCompleter>();
			if(levelCompleter == null)
			{
				gameObject.AddComponent<LevelCompleter>();
			}

			sr.color = Color.red;
		}
	}
#endif

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
		if(Active)
		{
			Gizmos.color = Color.blue;

			foreach(GridNode connection in connections)
			{
				if(connection == null)
				{
					continue;
				}

				Gizmos.DrawLine(transform.position, connection.transform.position);
			}
		}
	}

	public void AddEntity(Entity _entity)
	{
		if(entities.Add(_entity))
		{
			onEntityEnteredEvent(_entity);
		}
	}

	public void RemoveEntity(Entity _entity)
	{
		if(entities.Remove(_entity))
		{
			onEntityLeftEvent(_entity);
		}
	}

	public bool HasConnection(GridNode _node)
	{
		return connections.Contains(_node);
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
#endif
}