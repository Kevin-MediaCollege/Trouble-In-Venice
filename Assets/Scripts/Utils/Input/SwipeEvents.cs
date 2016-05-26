using Utils;

namespace Proeve
{
	/// <summary>
	/// Event for when a swipe has began.
	/// </summary>
	public class SwipeBeganEvent : IEvent
	{
		/// <summary>
		/// The <see cref="SwipeHandle"/> of the swipe.
		/// </summary>
		public SwipeHandle Handle { private set; get; }

		/// <summary>
		/// Create a new <see cref="SwipeBeganEvent"/>.
		/// </summary>
		/// <param name="_handle">The handle of the swipe.</param>
		public SwipeBeganEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}

	/// <summary>
	/// Event for when a swipe has been updated.
	/// </summary>
	public class SwipeUpdateEvent : IEvent
	{
		/// <summary>
		/// The <see cref="SwipeHandle"/> of the swipe.
		/// </summary>
		public SwipeHandle Handle { private set; get; }

		/// <summary>
		/// Create a new <see cref="SwipeUpdateEvent"/>.
		/// </summary>
		/// <param name="_handle">The handle of the swipe.</param>
		public SwipeUpdateEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}

	/// <summary>
	/// Event for when a swipe has ended.
	/// </summary>
	public class SwipeEndedEvent : IEvent
	{
		/// <summary>
		/// The <see cref="SwipeHandle"/> of the swipe.
		/// </summary>
		public SwipeHandle Handle { private set; get; }

		/// <summary>
		/// Create a new <see cref="SwipeEndedEvent"/>.
		/// </summary>
		/// <param name="_handle">The handle of the swipe.</param>
		public SwipeEndedEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}
}
