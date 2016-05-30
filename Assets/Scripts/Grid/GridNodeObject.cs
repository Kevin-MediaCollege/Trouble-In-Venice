using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// A grid node object, base class for all objects on the grid.
	/// </summary>
	[RequireComponent(typeof(EntityNodeTracker))]
	public class GridNodeObject : MonoBehaviour
	{
		protected GridNode node;
		
		protected virtual void Awake()
		{
			node = GetComponent<EntityNodeTracker>().CurrentNode;
		}

		protected virtual void OnEnable()
		{
			if(node == null)
			{
				Debug.LogError("GridNodeObject does not have a node", this);
				return;
			}

			node.onEntityEnteredEvent += OnEntityEntered;
			node.onEntityLeftEvent += OnEntityLeft;
		}

		protected virtual void OnDisable()
		{
			if(node != null)
			{
				node.onEntityEnteredEvent -= OnEntityEntered;
				node.onEntityLeftEvent -= OnEntityLeft;
			}
		}

		/// <summary>
		/// Called when an entity has entered this node.
		/// </summary>
		/// <param name="_entity">The entity.</param>
		protected virtual void OnEntityEntered(Entity _entity)
		{
		}

		/// <summary>
		/// Called when an entity has left this node.
		/// </summary>
		/// <param name="_entity">The entity.</param>
		protected virtual void OnEntityLeft(Entity _entity)
		{
		}
	}
}
