using System.Collections;
using Utils;

namespace Proeve
{
	public class LevelCompletedEvent : IEvent
	{
		public int LevelIndex { private set; get; }

		public LevelCompletedEvent(int _levelIndex)
		{
			LevelIndex = _levelIndex;
		}
	}
}
