using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Proeve
{
	public class GoogleAnalyticsLevel : MonoBehaviour
	{
		private List<GATracker> trackers;

		protected void Awake()
		{
			GoogleAnalytics googleAnalytics = Dependency.Get<GoogleAnalytics>();

			string[] parts = SceneManager.GetActiveScene().name.Split('_');
			int levelIndex = int.Parse(parts[parts.Length - 1]);

			googleAnalytics.LogScreen(new AppViewHitBuilder().SetScreenName(SceneManager.GetActiveScene().name));

			trackers = new List<GATracker>();
			trackers.Add(new GACompletionTracker(googleAnalytics, levelIndex));
			trackers.Add(new GALevelTracker(googleAnalytics, levelIndex));
			trackers.Add(new GAPickupTracker(googleAnalytics, levelIndex));
			trackers.Add(new GATurnCountTracker(googleAnalytics, levelIndex));
		}

		protected void OnEnable()
		{
			foreach(GATracker tracker in trackers)
			{
				tracker.OnEnable();
			}
		}

		protected void OnDisable()
		{
			foreach(GATracker tracker in trackers)
			{
				tracker.OnDisable();
			}
		}
	}
}
