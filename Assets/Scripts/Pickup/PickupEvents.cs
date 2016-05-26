using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Event for when a pickup has been activated.
	/// </summary>
	public class PickupActivatedEvent : IEvent
	{
		/// <summary>
		/// The activated pickup.
		/// </summary>
		public Pickup Pickup { private set; get; }

		/// <summary>
		/// The entity which has activated the pickup.
		/// </summary>
		public Entity Entity { private set; get; }

		/// <summary>
		/// Create a new instance of this event.
		/// </summary>
		/// <param name="_pickup">The activated pickup.</param>
		/// <param name="_entity">The entity which has activated the pickup.</param>
		public PickupActivatedEvent(Pickup _pickup, Entity _entity)
		{
			Pickup = _pickup;
			Entity = _entity;
		}
	}
}
