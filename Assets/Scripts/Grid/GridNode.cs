using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public enum GridNodeType
	{
		Start,
		End,
		Normal
	}

	/// <summary>
	/// 
	/// </summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(BoxCollider))]
	public class GridNode : MonoBehaviour
	{
		public delegate void OnEntityEntered(Entity _entity);
		public event OnEntityEntered onEntityEnteredEvent = delegate { };

		public delegate void OnEntityLeft(Entity _entity);
		public event OnEntityLeft onEntityLeftEvent = delegate { };

		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<GridNode> Connections
		{
			get
			{
				return connections;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Vector2 GridPosition
		{
			get
			{
				return new Vector2(Mathf.Round((transform.position.x - 1.5f) / Grid.SIZE), Mathf.Round((transform.position.z - 1.5f) / Grid.SIZE));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Vector3 Position
		{
			get
			{
				return transform.position;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Vector2 PositionXZ
		{
			get
			{
				return new Vector2(transform.position.x, transform.position.z);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public GridNodeType Type
		{
			get
			{
				return type;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsStart
		{
			get
			{
				return Type == GridNodeType.Start;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsEnd
		{
			get
			{
				return Type == GridNodeType.End;
			}
		}

		/// <summary>
		/// 
		/// </summary>
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

		/// <summary>
		/// 
		/// </summary>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_entity"></param>
		public void AddEntity(Entity _entity)
		{
			if(entities.Add(_entity))
			{
				onEntityEnteredEvent(_entity);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_entity"></param>
		public void RemoveEntity(Entity _entity)
		{
			if(entities.Remove(_entity))
			{
				onEntityLeftEvent(_entity);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_node"></param>
		/// <returns></returns>
		public bool HasConnection(GridNode _node)
		{
			return connections.Contains(_node);
		}

#if UNITY_EDITOR
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_node"></param>
		public void AddConnection(GridNode _node)
		{
			if(!connections.Contains(_node))
			{
				connections.Add(_node);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void RemoveAllConnections()
		{
			foreach(GridNode connection in connections)
			{
				connection.RemoveConnection(this);
			}

			connections.Clear();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_node"></param>
		public void RemoveConnection(GridNode _node)
		{
			connections.Remove(_node);
		}
#endif
	}
}
