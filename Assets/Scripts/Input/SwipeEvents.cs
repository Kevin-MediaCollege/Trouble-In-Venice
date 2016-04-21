public class SwipeBeganEvent : IEvent
{
	public SwipeHandle Handle { private set; get; }

	public SwipeBeganEvent(SwipeHandle handle)
	{
		Handle = handle;
	}
}

public class SwipeUpdateEvent : IEvent
{
	public SwipeHandle Handle { private set; get; }

	public SwipeUpdateEvent(SwipeHandle handle)
	{
		Handle = handle;
	}
}

public class SwipeEndedEvent : IEvent
{
	public SwipeHandle Handle { private set; get; }

	public SwipeEndedEvent(SwipeHandle handle)
	{
		Handle = handle;
	}
}
