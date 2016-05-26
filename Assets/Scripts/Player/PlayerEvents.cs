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

	/// <summary>
	/// Event for when the player has died.
	/// </summary>
	public class PlayerDiedEvent : IEvent
	{
		/// <summary>
		/// The node the player died on.
		/// </summary>
		public GridNode Node { private set; get; }

		/// <summary>
		/// The entity which has killed the player.
		/// </summary>
		public Entity Cause { private set; get; }

		/// <summary>
		/// Create a new instance of this event.
		/// </summary>
		/// <param name="_node">The node the player died on.</param>
		/// <param name="_cause">The entity which has killed the player.</param>
		public PlayerDiedEvent(GridNode _node, Entity _cause)
		{
			Node = _node;
			Cause = _cause;
		}
	}
}
