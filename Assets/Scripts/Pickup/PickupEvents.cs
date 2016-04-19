public class PickupStartEvent : IEvent
{
	public PickupBehaviour Behaviour { private set; get; }

	public PickupStartEvent(PickupBehaviour behaviour)
	{
		Behaviour = behaviour;
	}
}

public class PickupStopEvent : IEvent
{
}