using System.Collections;
using Utils;

namespace Proeve
{
	public class PickupActivatedEvent : IEvent
	{
		public Pickup Pickup { private set; get; }

		public Entity Entity { private set; get; }

		public PickupActivatedEvent(Pickup _pickup, Entity _entity)
		{
			Pickup = _pickup;
			Entity = _entity;
		}
	}
}
