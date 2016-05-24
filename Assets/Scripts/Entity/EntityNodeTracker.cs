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

		protected void Start()
		{
			if(CurrentNode == null)
			{
				Grid grid = FindObjectOfType<Grid>();
				GridNode nearest = null;
				float nearestDistance = float.PositiveInfinity;

				foreach(GridNode node in grid.Nodes)
				{
					if(node == null)
					{
						continue;
					}

					float distance = (node.transform.position - transform.position).sqrMagnitude;

					if(distance < nearestDistance)
					{
						nearest = node;
						nearestDistance = distance;
					}
				}

				if(nearest != null)
				{
					transform.position = nearest.transform.position;
					CurrentNode = nearest;
				}
			}

			CurrentNode.AddEntity(GetComponent<Entity>());
		}
	}
}
