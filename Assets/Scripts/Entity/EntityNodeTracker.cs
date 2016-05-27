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
				return currentNode;
			}
		}

		[SerializeField, HideInInspector] private GridNode currentNode;

		protected void Awake()
		{
			currentNode = GridUtils.GetNodeAt(transform.position);
			currentNode.AddEntity(GetComponent<Entity>());
		}
	}
}
