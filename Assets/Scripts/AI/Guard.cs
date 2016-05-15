using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	public class Guard : GridNodeObject
	{
		private enum PatrolMode
		{
			Static,
			RotatingClockwise,
			RotatingCounterClockwise,
			Patrolling
		}

		private enum Direction
		{
			Up,
			Left,
			Down,
			Right
		}

		[SerializeField] private EntityMovement movement;

		[SerializeField] private Direction direction;
		[SerializeField] private PatrolMode patrolMode;

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

		protected override void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				Debug.Log("Kill player");
			}
		}

		protected void Reset()
		{
			movement = GetComponent<EntityMovement>();
		}

		protected void OnDrawGizmosSelected()
		{
			GridNode n = GetComponent<EntityNodeTracker>().CurrentNode;
			n = GridUtils.GetConnectionInDirection(n, GetDirectionVector());

			if(n != null)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawSphere(n.Position, 0.1f);
			}
		}

		protected void OnValidate()
		{
			Vector2 dir = GetDirectionVector();
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

		private void UpdateState()
		{
			switch(patrolMode)
			{
			case PatrolMode.RotatingClockwise:
			case PatrolMode.RotatingCounterClockwise:
				Rotate();
				break;
			case PatrolMode.Patrolling:
				Patrol();
				break;
			}
		}

		private void Rotate()
		{
			Vector2 dir = Vector2.zero;

			while(!movement.CanMove(dir))
			{
				if((direction == Direction.Up && patrolMode == PatrolMode.RotatingClockwise) ||
				   (direction == Direction.Down && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = Direction.Right;
					dir = Vector2.right;
				}
				else if((direction == Direction.Right && patrolMode == PatrolMode.RotatingClockwise) ||
					    (direction == Direction.Left && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = Direction.Down;
					dir = Vector2.down;
				}
				else if((direction == Direction.Down && patrolMode == PatrolMode.RotatingClockwise) ||
					    (direction == Direction.Up && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = Direction.Left;
					dir = Vector2.left;
				}
				else if((direction == Direction.Left && patrolMode == PatrolMode.RotatingClockwise) ||
					    (direction == Direction.Right && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = Direction.Up;
					dir = Vector2.up;
				}
			}

			FindTargetNode();
		}

		private void Patrol()
		{
			while(!movement.CanMove(GetDirectionVector()))
			{
				switch(direction)
				{
				case Direction.Up:
					direction = Direction.Down;
					break;
				case Direction.Left:
					direction = Direction.Right;
					break;
				case Direction.Down:
					direction = Direction.Up;
					break;
				case Direction.Right:
					direction = Direction.Left;
					break;
				}
			}

			movement.Move(GetDirectionVector());
			FindTargetNode();
		}

		private void FindTargetNode()
		{
			base.OnDisable();

			switch(patrolMode)
			{
			case PatrolMode.Static:
			case PatrolMode.RotatingClockwise:
			case PatrolMode.RotatingCounterClockwise:
				node = GridUtils.GetConnectionInDirection(node, GetDirectionVector());
				break;
			case PatrolMode.Patrolling:
				base.Awake();
				break;
			}

			base.OnEnable();
		}

		private Vector2 GetDirectionVector()
		{
			switch(direction)
			{
			case Direction.Up:
			default:
				return Vector2.up;
			case Direction.Left:
				return Vector2.left;
			case Direction.Down:
				return Vector2.down;
			case Direction.Right:
				return Vector2.right;
			}
		}

		private void OnPlayerMovedEvent(PlayerMovedEvent _evt)
		{
			UpdateState();
		}
	}
}
