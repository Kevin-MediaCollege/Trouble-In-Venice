using UnityEngine.SceneManagement;
using Utils;

namespace Proeve
{
	public abstract class GATracker
	{
		protected int LevelIndex { private set; get; }
		
		private GoogleAnalytics googleAnalytics;

		public GATracker(GoogleAnalytics _googleAnalytics, int _levelIndex)
		{
			googleAnalytics = _googleAnalytics;
			LevelIndex = _levelIndex;
		}

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}

		protected void SendEvent(EventHitBuilder _eventHitBuilder)
		{
			_eventHitBuilder.SetEventCategory("Level_" + LevelIndex);

			googleAnalytics.LogEvent(_eventHitBuilder);
		}
	}
}
