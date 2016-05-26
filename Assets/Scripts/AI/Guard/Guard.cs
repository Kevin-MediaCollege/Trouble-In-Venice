using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// AI guard, has multiple modes.
	/// </summary>
	public class Guard : AIBase
	{
		/// <summary>
		/// All available patrol modes for the guard.
		/// </summary>
		public enum GuardPatrolMode
		{
			Static,
			RotatingClockwise,
			RotatingCounterClockwise,
			Patrolling
		}

		/// <summary>
		/// Set or get The direction the guard is facing.
		/// </summary>
		public GridDirection Direction
		{
			set
			{
				direction = value;
			}
			get
			{
				return direction;
			}
		}

		/// <summary>
		/// Get the patrol mode of the guard.
		/// </summary>
		public GuardPatrolMode PatrolMode
		{
			get
			{
				return patrolMode;
			}
		}

		[SerializeField] private GridDirection direction;
		[SerializeField] private GuardPatrolMode patrolMode;

		protected override void Awake()
		{
			base.Awake();

			FindTargetNode();
		}

		protected override void OnEnable()
		{
			GlobalEvents.AddListener<PlayerMovedEvent>(OnPlayerMovedEvent);
		}

		protected override void OnDisable()
		{
			GlobalEvents.RemoveListener<PlayerMovedEvent>(OnPlayerMovedEvent);
		}

		protected void OnDrawGizmosSelected()
		{
			GridNode n = GetComponent<EntityNodeTracker>().CurrentNode;
			n = GridUtils.GetConnectionInDirection(n, direction);

			if(n != null)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawSphere(n.Position, 0.1f);
			}
		}

		protected void OnValidate()
		{
			Vector2 dir = GridUtils.GetDirectionVector(direction);
			Quaternion rotation = transform.rotation;

			if(dir == Vector2.up)
			{
				rotation.eulerAngles = new Vector3(0, 0, 0);
			}
			else if(dir == Vector2.left)
			{
				rotation.eulerAngles = new Vector3(0, 270, 0);
			}
			else if(dir == Vector2.down)
			{
				rotation.eulerAngles = new Vector3(0, 180, 0);
			}
			else if(dir == Vector2.right)
			{
				rotation.eulerAngles = new Vector3(0, 90, 0);
			}

			transform.rotation = rotation;
		}

		protected override void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player") && Movement.CurrentNode.HasConnection(node))
			{
				if(patrolMode != GuardPatrolMode.Patrolling)
				{
					GlobalEvents.Invoke(new SetInputEvent(false));
					
					Movement.onMoveEvent += OnMoveToPlayer;
					ExecuteCommand<GuardCommandMove>();
				}
				else
				{
					ExecuteCommand<GuardCommandAttackPlayer>();
				}
			}
		}

		/// <summary>
		/// Find the target node of the guard.
		/// </summary>
		public void FindTargetNode()
		{
			base.OnDisable();

			switch(patrolMode)
			{
			case GuardPatrolMode.Static:
			case GuardPatrolMode.RotatingClockwise:
			case GuardPatrolMode.RotatingCounterClockwise:
				node = GridUtils.GetConnectionInDirection(node, direction);
				break;
			case GuardPatrolMode.Patrolling:
				base.Awake();
				break;
			}

			base.OnEnable();
		}

		/// <summary>
		/// Update the state of the guard, happens right after the player moves.
		/// </summary>
		private void UpdateState()
		{
			switch(patrolMode)
			{
			case GuardPatrolMode.RotatingClockwise:
			case GuardPatrolMode.RotatingCounterClockwise:
				ExecuteCommand<GuardCommandRotate>();
				break;
			case GuardPatrolMode.Patrolling:
				ExecuteCommand<GuardCommandMove>();
				break;
			}
		}

		/// <summary>
		/// Event called by <see cref="PlayerMovedEvent"/>.
		/// </summary>
		/// <param name="_evt">The event.</param>
		private void OnPlayerMovedEvent(PlayerMovedEvent _evt)
		{
			UpdateState();
		}

		private void OnMoveToPlayer(GridNode _old, GridNode _new)
		{
			Movement.onMoveEvent -= OnMoveToPlayer;

			ExecuteCommand<GuardCommandAttackPlayer>();
		}
	}
}
