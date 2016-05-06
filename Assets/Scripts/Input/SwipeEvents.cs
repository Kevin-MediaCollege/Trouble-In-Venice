using Utils;

namespace Proeve
{
	/// <summary>
	/// 
	/// </summary>
	public class SwipeBeganEvent : IEvent
	{
		/// <summary>
		/// 
		/// </summary>
		public SwipeHandle Handle { private set; get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_handle"></param>
		public SwipeBeganEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class SwipeUpdateEvent : IEvent
	{
		/// <summary>
		/// 
		/// </summary>
		public SwipeHandle Handle { private set; get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_handle"></param>
		public SwipeUpdateEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class SwipeEndedEvent : IEvent
	{
		/// <summary>
		/// 
		/// </summary>
		public SwipeHandle Handle { private set; get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_handle"></param>
		public SwipeEndedEvent(SwipeHandle _handle)
		{
			Handle = _handle;
		}
	}
}
