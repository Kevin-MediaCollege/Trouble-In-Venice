using UnityEngine;
using Utils;

namespace Proeve
{
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
			node.onEntityEnteredEvent += OnEntityEntered;
			node.onEntityLeftEvent += OnEntityLeft;
		}

		protected virtual void OnDisable()
		{
			node.onEntityEnteredEvent -= OnEntityEntered;
			node.onEntityLeftEvent -= OnEntityLeft;
		}

		protected virtual void OnEntityEntered(Entity _entity)
		{
		}

		protected virtual void OnEntityLeft(Entity _entity)
		{
		}
	}
}
