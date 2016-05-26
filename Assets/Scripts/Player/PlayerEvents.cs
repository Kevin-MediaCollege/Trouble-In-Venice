using Utils;

namespace Proeve
{
	/// <summary>
	/// Event for when the player has moved.
	/// </summary>
	public class PlayerMovedEvent : IEvent
	{
		/// <summary>
		/// The source node.
		/// </summary>
		public GridNode From { private set; get; }

		/// <summary>
		/// The destination node.
		/// </summary>
		public GridNode To { private set; get; }

		/// <summary>
		/// Create a new instance of this event.
		/// </summary>
		/// <param name="_from">The source node.</param>
		/// <param name="_to">The destination node.</param>
		public PlayerMovedEvent(GridNode _from, GridNode _to)
		{
			From = _from;
			To = _to;
		}
	}
}
