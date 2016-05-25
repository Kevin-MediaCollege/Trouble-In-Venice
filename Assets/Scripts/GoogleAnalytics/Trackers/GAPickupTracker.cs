using Utils;

namespace Proeve
{
	public class GAPickupTracker : GATracker
	{
		public GAPickupTracker(GoogleAnalytics _googleAnalytics, int _levelIndex) : base(_googleAnalytics, _levelIndex)
		{
		}

		public override void OnEnable()
		{
			GlobalEvents.AddListener<PickupActivatedEvent>(OnPickupActivatedEvent);
		}

		public override void OnDisable()
		{
			GlobalEvents.RemoveListener<PickupActivatedEvent>(OnPickupActivatedEvent);
		}

		private void OnPickupActivatedEvent(PickupActivatedEvent _evt)
		{
			EventHitBuilder eventHitBuilder = new EventHitBuilder();
			eventHitBuilder.SetEventAction("Pickup Activated");
			SendEvent(eventHitBuilder);
		}
	}
}
