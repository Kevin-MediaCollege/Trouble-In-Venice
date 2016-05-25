using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The Entity Node Tracker, automatically tracks which <see cref="GridNode"/> the entity is on.
	/// </summary>
	/// <remarks>
	/// Requires the <see cref="Entity"/> component to work.
	/// </remarks>
	[RequireComponent(typeof(Entity))]
	public class EntityNodeTracker : MonoBehaviour
	{
		/// <summary>
		/// Set or get the current node.
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
