using System.Collections;
using Utils;

namespace Proeve
{
	/// <summary>
	/// Event fired upon level completion.
	/// </summary>
	public class LevelCompletedEvent : IEvent
	{
		/// <summary>
		/// The index of the completed level.
		/// </summary>
		public int LevelIndex { private set; get; }

		/// <summary>
		/// Create a new instance of this event.
		/// </summary>
		/// <param name="_levelIndex">The index of the completed level.</param>
		public LevelCompletedEvent(int _levelIndex)
		{
			LevelIndex = _levelIndex;
		}
	}
}
