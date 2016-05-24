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
		protected bool used;

		private GoogleAnalytics googleAnalytics;
		private string eventCategory;

		private bool active;

		protected override void Awake()
		{
			base.Awake();

			googleAnalytics = Dependency.Get<GoogleAnalytics>();
			eventCategory = transform.root.gameObject.name;
		}

		protected void LateUpdate()
		{
			if(active && !used)
			{
				OnUpdate();

				if(used)
				{
					EventHitBuilder ehb = new EventHitBuilder();
					ehb.SetEventCategory(eventCategory);
					ehb.SetEventAction("Pickup Deactivated");
					googleAnalytics.LogEvent(ehb);

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

				GlobalEvents.Invoke(new PickupActivatedEvent(this, _entity));

				EventHitBuilder ehb = new EventHitBuilder();
				ehb.SetEventCategory(eventCategory);
				ehb.SetEventAction("Pickup Activated");
				googleAnalytics.LogEvent(ehb);

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
