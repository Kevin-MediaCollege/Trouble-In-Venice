using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Proeve
{
	public class GoogleAnalyticsLevel : MonoBehaviour
	{
		private GoogleAnalytics googleAnalytics;
		private LevelUnlocker levelUnlocker;

		private int numMoves;
		private int levelIndex;

		protected void Awake()
		{
			googleAnalytics = Dependency.Get<GoogleAnalytics>();

			string[] parts = SceneManager.GetActiveScene().name.Split('_');
			levelIndex = int.Parse(parts[1]);
			levelUnlocker = Dependency.Get<LevelUnlocker>();
		}

		protected void OnEnable()
		{
			GlobalEvents.AddListener<PlayerMovedEvent>(OnPlayerMovedEvent);

			googleAnalytics.LogScreen(new AppViewHitBuilder().SetScreenName("Level_" + levelIndex));

			// Log started
			EventHitBuilder ehb = new EventHitBuilder();
			ehb.SetEventCategory("Level_" + levelIndex);
			ehb.SetEventAction(levelUnlocker.IsUnlocked(levelIndex + 1) ? "Started After Completion" : "Started");
			googleAnalytics.LogEvent(ehb);
		}

		protected void OnDisable()
		{
			GlobalEvents.RemoveListener<PlayerMovedEvent>(OnPlayerMovedEvent);

			// Log completion status
			EventHitBuilder ehb = new EventHitBuilder();
			ehb.SetEventCategory("Level_" + levelIndex);
			ehb.SetEventAction(levelUnlocker.IsUnlocked(levelIndex + 1) ? "Complete" : "Not Complete");
			googleAnalytics.LogEvent(ehb);

			// Log num moves
			ehb = new EventHitBuilder();
			ehb.SetEventCategory("Level_" + levelIndex);
			ehb.SetEventAction("Turn Count");
			ehb.SetEventValue(numMoves);
			googleAnalytics.LogEvent(ehb);
		}

		private void OnPlayerMovedEvent(PlayerMovedEvent evt)
		{
			numMoves++;
		}
	}
}
