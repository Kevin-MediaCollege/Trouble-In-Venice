using Utils;

namespace Proeve
{
	public class GATurnCountTracker : GATracker
	{
		public GATurnCountTracker(GoogleAnalytics _googleAnalytics, int _levelIndex) : base(_googleAnalytics, _levelIndex)
		{
		}

		public override void OnDisable()
		{
			TurnCountTracker tracker = StatTracker.GetTracker<TurnCountTracker>();

			EventHitBuilder eventHitBuilder = new EventHitBuilder();
			eventHitBuilder.SetEventAction("Turn Count");
			eventHitBuilder.SetEventValue(tracker.GetValue());
			SendEvent(eventHitBuilder);
		}
	}
}
