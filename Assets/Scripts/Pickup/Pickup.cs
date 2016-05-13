using System;
using UnityEngine;
using Utils;

namespace Proeve
{
	/// <summary>
	/// The base class for pickups, handles activation, updating an deactivation of implementations.
	/// </summary>
	public abstract class Pickup : NodeObject
	{
		protected bool used;
		
		private bool active;

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
		
		protected override void OnEntityEntered(Entity _entity)
		{
			if(_entity.HasTag("Player"))
			{
				active = true;
				used = false;
				
				OnActivate();
			}
		}
		
		protected override void OnEntityLeft(Entity _entity)
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
