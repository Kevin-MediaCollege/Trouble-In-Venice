using Utils;

namespace Proeve
{
	/// <summary>
	/// Google Analytics tracker for turn count.
	/// </summary>
	public class GATurnCountTracker : GATracker
	{
		private TurnCountTracker turnCountTracker;

		public GATurnCountTracker(GoogleAnalytics _googleAnalytics, int _levelIndex) : base(_googleAnalytics, _levelIndex)
		{
			turnCountTracker = StatTracker.GetTracker<TurnCountTracker>();
		}

		public override void OnDisable()
		{
			EventHitBuilder eventHitBuilder = new EventHitBuilder();
			eventHitBuilder.SetEventAction("Turn Count");
			eventHitBuilder.SetEventValue(turnCountTracker.GetValue());
			SendEvent(eventHitBuilder);
		}
	}
}
