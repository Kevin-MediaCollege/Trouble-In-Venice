using Utils;

namespace Proeve
{
	public class PlayerMovedEvent : IEvent
	{
		public GridNode From { private set; get; }

		public GridNode To { private set; get; }

		public PlayerMovedEvent(GridNode _from, GridNode _to)
		{
			From = _from;
			To = _to;
		}
	}
}
