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
		[SerializeField] private EntityMovement movement;

		[SerializeField] private GridDirection direction;
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
			n = GridUtils.GetConnectionInDirection(n, GridUtils.GetDirectionVector(direction));

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
				if((direction == GridDirection.Up && patrolMode == PatrolMode.RotatingClockwise) ||
				   (direction == GridDirection.Down && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = GridDirection.Right;
					dir = Vector2.right;
				}
				else if((direction == GridDirection.Right && patrolMode == PatrolMode.RotatingClockwise) ||
					    (direction == GridDirection.Left && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = GridDirection.Down;
					dir = Vector2.down;
				}
				else if((direction == GridDirection.Down && patrolMode == PatrolMode.RotatingClockwise) ||
					    (direction == GridDirection.Up && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = GridDirection.Left;
					dir = Vector2.left;
				}
				else if((direction == GridDirection.Left && patrolMode == PatrolMode.RotatingClockwise) ||
					    (direction == GridDirection.Right && patrolMode == PatrolMode.RotatingCounterClockwise))
				{
					direction = GridDirection.Up;
					dir = Vector2.up;
				}
			}

			FindTargetNode();
		}

		private void Patrol()
		{
			while(!movement.CanMove(GridUtils.GetDirectionVector(direction)))
			{
				switch(direction)
				{
				case GridDirection.Up:
					direction = GridDirection.Down;
					break;
				case GridDirection.Left:
					direction = GridDirection.Right;
					break;
				case GridDirection.Down:
					direction = GridDirection.Up;
					break;
				case GridDirection.Right:
					direction = GridDirection.Left;
					break;
				}
			}

			movement.Move(GridUtils.GetDirectionVector(direction));
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
				node = GridUtils.GetConnectionInDirection(node, GridUtils.GetDirectionVector(direction));
				break;
			case PatrolMode.Patrolling:
				base.Awake();
				break;
			}

			base.OnEnable();
		}

		private void OnPlayerMovedEvent(PlayerMovedEvent _evt)
		{
			UpdateState();
		}
	}
}
