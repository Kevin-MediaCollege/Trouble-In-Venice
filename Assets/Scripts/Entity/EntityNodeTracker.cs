using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The Entity Node Tracker, automatically tracks which <see cref="GridNode"/> the entity is on.
	/// </summary>
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
				return currentNode;
			}
		}

		[SerializeField, HideInInspector] private GridNode currentNode;
		[SerializeField] private bool manualY;

		protected void Start()
		{
			if(CurrentNode == null)
			{
				throw new InvalidOperationException("Entity must be attached to a GridNode");
			}

			CurrentNode.AddEntity(GetComponent<Entity>());
		}
	}
}
