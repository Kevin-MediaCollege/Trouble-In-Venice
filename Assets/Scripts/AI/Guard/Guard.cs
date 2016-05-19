using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	public class Guard : AIBase
	{
		public enum GuardPatrolMode
		{
			Static,
			RotatingClockwise,
			RotatingCounterClockwise,
			Patrolling
		}

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

		protected override void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				ExecuteCommand<GuardCommandAttackPlayer>();
			}
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
			case GuardPatrolMode.RotatingClockwise:
			case GuardPatrolMode.RotatingCounterClockwise:
				ExecuteCommand<GuardCommandRotate>();
				break;
			case GuardPatrolMode.Patrolling:
				ExecuteCommand<GuardCommandMove>();
				break;
			}
		}

		public void FindTargetNode()
		{
			base.OnDisable();

			switch(patrolMode)
			{
			case GuardPatrolMode.Static:
			case GuardPatrolMode.RotatingClockwise:
			case GuardPatrolMode.RotatingCounterClockwise:
				node = GridUtils.GetConnectionInDirection(node, GridUtils.GetDirectionVector(direction));
				break;
			case GuardPatrolMode.Patrolling:
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
