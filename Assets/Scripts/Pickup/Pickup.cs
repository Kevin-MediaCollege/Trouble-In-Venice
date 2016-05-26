using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The base class for pickups, handles activation, updating an deactivation of implementations.
	/// </summary>
	public abstract class Pickup : GridNodeObject
	{
		private bool active;

		protected void LateUpdate()
		{
			if(active)
			{
				OnUpdate();
			}
		}

		protected override void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				active = true;

				GlobalEvents.Invoke(new PickupActivatedEvent(this, _entity));
				OnActivate();
			}
		}
		
		protected override void OnEntityLeft(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				OnDeactivate();

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
