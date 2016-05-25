using Utils;

namespace Proeve
{
	public class GALevelTracker : GATracker
	{
		private LevelUnlocker levelUnlocker;

		public GALevelTracker(GoogleAnalytics _googleAnalytics, int _levelIndex) : base(_googleAnalytics, _levelIndex)
		{
			levelUnlocker = Dependency.Get<LevelUnlocker>();
		}

		public override void OnEnable()
		{
			EventHitBuilder eventHitBuilder = new EventHitBuilder();
			eventHitBuilder.SetEventAction(levelUnlocker.IsUnlocked(LevelIndex + 1) ? "Started After Completion" : "Started");
			SendEvent(eventHitBuilder);
		}

		public override void OnDisable()
		{
			EventHitBuilder eventHitBuilder = new EventHitBuilder();
			eventHitBuilder.SetEventAction("Stopped");
			SendEvent(eventHitBuilder);
		}
	}
}
