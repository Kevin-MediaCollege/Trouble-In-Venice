using Utils;

namespace Proeve
{
	public class SwipeBeganEvent : IEvent
	{
		public SwipeHandle Handle { private set; get; }

		public SwipeBeganEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}

	public class SwipeUpdateEvent : IEvent
	{
		public SwipeHandle Handle { private set; get; }

		public SwipeUpdateEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}

	public class SwipeEndedEvent : IEvent
	{
		public SwipeHandle Handle { private set; get; }

		public SwipeEndedEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}
}
