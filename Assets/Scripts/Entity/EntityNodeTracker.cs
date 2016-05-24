using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The Entity Node Tracker, automatically tracks which <see cref="GridNode"/> the entity is on.
	/// </summary>
	[RequireComponent(typeof(Entity))]
	public class EntityNodeTracker : MonoBehaviour
	{
		/// <summary>
		/// The current node.
		/// </summary>
		public GridNode CurrentNode
		{
			set
			{
				currentNode = value;
			}
			get
			{
				if(currentNode == null)
				{
					currentNode = GridUtils.GetNodeAt(new Vector2(Mathf.Round((transform.position.x - 1.5f) / Grid.SIZE), Mathf.Round((transform.position.z - 1.5f) / Grid.SIZE)));
					currentNode.AddEntity(GetComponent<Entity>());
				}

				return currentNode;
			}
		}

		[SerializeField, HideInInspector] private GridNode currentNode;
	}
}
