using UnityEngine;
using System.Collections;
using Utils;

namespace Proeve
{
	public class Guard : GridNodeObject
	{
		[SerializeField] private Vector2 direction;

		protected override void Awake()
		{
			GridNode own = GetComponent<EntityNodeTracker>().CurrentNode;
			node = GridUtils.GetNodeAt(own.GridPosition + direction);
		}

		protected void OnDrawGizmosSelected()
		{
			Vector3 position = Vector3.zero;

			if(node != null)
			{
				position = node.Position;
			}
			else
			{
				GridNode own = GetComponent<EntityNodeTracker>().CurrentNode;
				position = GridUtils.GetNodeAt(own.GridPosition + direction).Position;
			}

			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(position, 0.1f);
		}

		protected override void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				Debug.Log("Kill player");
			}
		}
	}
}
