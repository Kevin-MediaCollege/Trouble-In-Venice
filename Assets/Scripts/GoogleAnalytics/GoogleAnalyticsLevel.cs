using UnityEngine;
using Utils;

namespace Proeve
{
	public class GoogleAnalyticsLevel : GoogleAnalyticsScreen
	{
		private LevelUnlocker levelUnlocker;

		private int numMoves;
		private int levelIndex;

		protected override void Awake()
		{
			base.Awake();

			string[] parts = screenName.Split('_');
			levelIndex = int.Parse(parts[1]);
			levelUnlocker = Dependency.Get<LevelUnlocker>();
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			GlobalEvents.AddListener<PlayerMovedEvent>(OnPlayerMovedEvent);

			// Log started
			EventHitBuilder ehb = new EventHitBuilder();
			ehb.SetEventCategory(screenName);
			ehb.SetEventAction(levelUnlocker.IsUnlocked(levelIndex + 1) ? "Started After Completion" : "Started");
			googleAnalytics.LogEvent(ehb);
		}

		protected void OnDisable()
		{
			GlobalEvents.RemoveListener<PlayerMovedEvent>(OnPlayerMovedEvent);

			// Log completion status
			EventHitBuilder ehb = new EventHitBuilder();
			ehb.SetEventCategory(screenName);
			ehb.SetEventAction(levelUnlocker.IsUnlocked(levelIndex + 1) ? "Complete" : "Not Complete");
			googleAnalytics.LogEvent(ehb);

			// Log num moves
			ehb = new EventHitBuilder();
			ehb.SetEventCategory(screenName);
			ehb.SetEventAction("Turn Count");
			ehb.SetEventValue(numMoves);
			googleAnalytics.LogEvent(ehb);
		}

		protected void Reset()
		{
			screenName = transform.root.gameObject.name;
		}

		private void OnPlayerMovedEvent(PlayerMovedEvent evt)
		{
			numMoves++;
		}
	}
}
