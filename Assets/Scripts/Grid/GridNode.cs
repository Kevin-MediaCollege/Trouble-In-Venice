using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The type of the <see cref="GridNode"/>.
	/// </summary>
	public enum GridNodeType
	{
		/// <summary>
		/// The player starts here.
		/// </summary>
		Start,

		/// <summary>
		/// When the player reaches this node the level is completed.
		/// </summary>
		End,

		/// <summary>
		/// A normal node.
		/// </summary>
		Normal
	}

	/// <summary>
	/// A component representing a node in a <see cref="Grid"/>.
	/// </summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(BoxCollider))]
	public class GridNode : MonoBehaviour
	{
		/// <summary>
		/// An <see cref="Entity"/> entered this node.
		/// </summary>
		/// <param name="_entity">The entity.</param>
		public delegate void OnEntityEntered(Entity _entity);

		/// <summary>
		/// Called when an entity has entered this node.
		/// </summary>
		public event OnEntityEntered onEntityEnteredEvent = delegate { };

		/// <summary>
		/// An <see cref="Entity"/> left this node.
		/// </summary>
		/// <param name="_entity">The entity.</param>
		public delegate void OnEntityLeft(Entity _entity);

		/// <summary>
		/// Called when an entity has left this node.
		/// </summary>
		public event OnEntityLeft onEntityLeftEvent = delegate { };

		public delegate void OnBlockadeAdded(GridNode to);
		public event OnBlockadeAdded onBlockadeAddedEvent = delegate { };

		public delegate void OnBlockadeRemoved(GridNode to);
		public event OnBlockadeRemoved onBlockadeRemovedEvent = delegate { };

		/// <summary>
		/// All connections of the node.
		/// </summary>
		/// <remarks>
		/// Connections do not mean all neighbours,
		/// only neighbouring nodes which have been added as a connection.
		/// </remarks>
		public IEnumerable<GridNode> Connections
		{
			get
			{
				return connections;
			}
		}

		/// <summary>
		/// The position of the node in the <see cref="Grid"/>.
		/// </summary>
		public Vector2 GridPosition
		{
			get
			{
				return new Vector2(Mathf.Round((transform.position.x - 1.5f) / Grid.SIZE), Mathf.Round((transform.position.z - 1.5f) / Grid.SIZE));
			}
		}

		/// <summary>
		/// A shortcut to transform.position.
		/// </summary>
		public Vector3 Position
		{
			get
			{
				return transform.position;
			}
		}

		/// <summary>
		/// A shortcut to the X and Z of transform.position.
		/// </summary>
		public Vector2 PositionXZ
		{
			get
			{
				return new Vector2(transform.position.x, transform.position.z);
			}
		}

		/// <summary>
		/// The type of the node.
		/// </summary>
		public GridNodeType Type
		{
			get
			{
				return type;
			}
		}

		/// <summary>
		/// Whether or not this node is the starting node, equal to
		/// <code>Type == GridNodeType.Start</code>
		/// </summary>
		public bool IsStart
		{
			get
			{
				return Type == GridNodeType.Start;
			}
		}

		/// <summary>
		/// Whether or not this node is the end node, equal to
		/// <code>Type == GridNodeType.End</code>
		/// </summary>
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
		private HashSet<GridNode> blockedConnections;

		protected void Awake()
		{
			entities = new HashSet<Entity>();
			blockedConnections = new HashSet<GridNode>();

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
#if UNITY_EDITOR
					UnityEditor.EditorApplication.delayCall += () =>
					{
						DestroyImmediate(levelCompleter);
					};
#endif
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
		/// Draw gizmos of this node, called by <see cref="Grid.OnDrawGizmosSelected"/>.
		/// </summary>
		public void DrawGizmos()
		{
			if(!Active)
			{
				return;
			}

			Gizmos.color = Color.blue;

			foreach(GridNode connection in connections)
			{
				if(connection == null || (blockedConnections != null && blockedConnections.Contains(connection)))
				{
					continue;
				}

				Gizmos.DrawLine(transform.position, connection.transform.position);
			}
		}

		/// <summary>
		/// Add an <see cref="Entity"/> to the node.
		/// </summary>
		/// <param name="_entity">The entity to add.</param>
		public void AddEntity(Entity _entity)
		{
			if(entities.Add(_entity))
			{
				onEntityEnteredEvent(_entity);
			}
		}

		/// <summary>
		/// Remove an <see cref="Entity"/> from the node.
		/// </summary>
		/// <param name="_entity">The entity to remove.</param>
		public void RemoveEntity(Entity _entity)
		{
			if(entities.Remove(_entity))
			{
				onEntityLeftEvent(_entity);
			}
		}
		
		public void AddBlockade(GridNode _node)
		{
			blockedConnections.Add(_node);
			_node.blockedConnections.Add(this);

			onBlockadeAddedEvent(_node);
			_node.onBlockadeAddedEvent(this);
		}

		public void RemoveBlockade(GridNode _node)
		{
			blockedConnections.Remove(_node);
			_node.blockedConnections.Remove(this);

			onBlockadeRemovedEvent(_node);
			_node.onBlockadeRemovedEvent(this);
		}

		/// <summary>
		/// Add a connection to this node.
		/// </summary>
		/// <param name="_node">The node to add a connection to.</param>
		public void AddConnection(GridNode _node)
		{
			if(!connections.Contains(_node))
			{
				connections.Add(_node);
			}
		}

		/// <summary>
		/// Remove all connections of this node.
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
		/// Remove the connection to <paramref name="_node"/>.
		/// </summary>
		/// <param name="_node">The node to remove.</param>
		public void RemoveConnection(GridNode _node)
		{
			connections.Remove(_node);
		}

		/// <summary>
		/// Check whether or not this node has a connection to <paramref name="_node"/>.
		/// </summary>
		/// <param name="_node">The other node.</param>
		/// <returns>Whether or not <paramref name="_node"/> is connected to this node.</returns>
		public bool HasConnection(GridNode _node)
		{
			return connections.Contains(_node) && !blockedConnections.Contains(_node);
		}
	}
}
