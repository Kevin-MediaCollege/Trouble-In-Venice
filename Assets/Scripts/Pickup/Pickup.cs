using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The base class for pickups, handles activation, updating an deactivation of implementations.
	/// </summary>
	public abstract class Pickup : MonoBehaviour
	{
		protected GridNode node;

		protected bool used;
		
		private bool active;

		protected void OnEnable()
		{
			node = GetComponent<EntityNodeTracker>().CurrentNode;

			node.onEntityEnteredEvent += OnEntityEntered;
			node.onEntityLeftEvent += OnEntityLeft;
		}

		private void OnDisable()
		{
			node.onEntityEnteredEvent -= OnEntityEntered;
			node.onEntityLeftEvent -= OnEntityLeft;
		}

		protected void LateUpdate()
		{
			if(active && !used)
			{
				OnUpdate();

				if(used)
				{
					OnDeactivate();
				}
			}
		}
		
		private void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				active = true;
				used = false;
				
				OnActivate();
			}
		}
		
		private void OnEntityLeft(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				active = false;
			}
		}

		/// <summary>
		/// Invoked when the pickup should activate.
		/// </summary>
		protected virtual void OnActivate()
		{
		}

		/// <summary>
		/// Invoked when the pickup should deactivate.
		/// </summary>
		protected virtual void OnDeactivate()
		{
		}

		/// <summary>
		/// Invoked when the pickup should update.
		/// </summary>
		protected virtual void OnUpdate()
		{
		}
	}
}
