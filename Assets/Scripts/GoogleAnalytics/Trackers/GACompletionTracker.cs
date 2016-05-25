using Utils;

namespace Proeve
{
	/// <summary>
	/// Google Analytics tracker for level completion.
	/// </summary>
	public class GACompletionTracker : GATracker
	{
		public GACompletionTracker(GoogleAnalytics _googleAnalytics, int _levelIndex) : base(_googleAnalytics, _levelIndex)
		{
		}

		public override void OnEnable()
		{
			GlobalEvents.AddListener<LevelCompletedEvent>(OnLevelCompleteEvent);
		}

		public override void OnDisable()
		{
			GlobalEvents.RemoveListener<LevelCompletedEvent>(OnLevelCompleteEvent);
		}

		private void OnLevelCompleteEvent(LevelCompletedEvent _evt)
		{
			EventHitBuilder eventHitBuilder = new EventHitBuilder();
			eventHitBuilder.SetEventAction("Completed");
			SendEvent(eventHitBuilder);
		}
	}
}
