using Utils;

namespace Proeve
{
	/// <summary>
	/// Base class for Google Analytics trackers.
	/// </summary>
	public abstract class GATracker
	{
		/// <summary>
		/// Get the level index.
		/// </summary>
		protected int LevelIndex { private set; get; }
		
		private GoogleAnalytics googleAnalytics;

		/// <summary>
		/// Create a new Google Analytics tracker.
		/// </summary>
		/// <param name="_googleAnalytics">The Google Analytics dependency.</param>
		/// <param name="_levelIndex">The index of the current level.</param>
		public GATracker(GoogleAnalytics _googleAnalytics, int _levelIndex)
		{
			googleAnalytics = _googleAnalytics;
			LevelIndex = _levelIndex;
		}

		/// <summary>
		/// Called by <see cref="GoogleAnalyticsLevel.OnEnable"/>.
		/// </summary>
		public virtual void OnEnable()
		{
		}

		/// <summary>
		/// Called by <see cref="GoogleAnalyticsLevel.OnDisable"/>.
		/// </summary>
		public virtual void OnDisable()
		{
		}

		/// <summary>
		/// Send a Google Analytics event.
		/// </summary>
		/// <param name="_eventHitBuilder">The event to send.</param>
		protected void SendEvent(EventHitBuilder _eventHitBuilder)
		{
			_eventHitBuilder.SetEventCategory("Level_" + LevelIndex);

			googleAnalytics.LogEvent(_eventHitBuilder);
		}
	}
}
